using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Runerne.Xml
{
    /// <summary>
    /// Instances of this class is capable of validating XML documents against a specified schema.
    /// </summary>
    public class XmlValidator
    {
        /// <summary>
        /// Defines various levels of regidity, i.e. what level of error will make the validation fail.
        /// </summary>
        public enum RegidityLevel
        {
            /// <summary>
            /// Fail whenever a warning or an error occur.
            /// </summary>
            FailOnWarningsAndErrors,

            /// <summary>
            /// Fail only when an error occurs.
            /// </summary>
            FailOnErrors,

            /// <summary>
            /// Never fail, neither when an error or a warning occur.
            /// </summary>
            Tolerant
        }

        /// <summary>
        /// Defines the level of regidity. By default the level is FailOnErrors.
        /// </summary>
        public RegidityLevel Regidity = RegidityLevel.FailOnErrors;

        /// <summary>
        /// Observers are called when an error or a warning occur.
        /// </summary>
        public event ValidationEventHandler ValidationEventHandler;

        /// <summary>
        /// Maps the schemas, which are loaded before the validation can take place. Initially the base schema is loaded.
        /// Subsequently all included and imported schemas are loaded recursively. The loading methods utilizes the map to prevent the same
        /// schema from being loaded multiple times.
        /// </summary>
        private readonly Dictionary<string, XmlSchema> _loadedSchemas = new Dictionary<string, XmlSchema>();

        /// <summary>
        /// Creates an instance of the XmlValidator based on the schema file stored as an embedded resource. The resource will loaded by 
        /// using the executing assembly.
        /// </summary>
        /// <param name="baseSchemaEmbededResourceName">The name of the schema file, which is an embedded resource.</param>
        /// <param name="baseResourcePackageName">The name of the package (namespace) in which the embedded resource is located.</param>
        public XmlValidator(string baseSchemaEmbededResourceName, string baseResourcePackageName)
            : this(baseSchemaEmbededResourceName, baseResourcePackageName, Assembly.GetExecutingAssembly())
        {
        }

        /// <summary>
        /// Creates an instance of the XmlValidator based on the schema file stored as an embedded resource.
        /// </summary>
        /// <param name="baseSchemaEmbededResourceName">The name of the schema file, which is an embedded resource.</param>
        /// <param name="baseResourcePackageName">The name of the package (namespace) in which the embedded resource is located.</param>
        /// <param name="assembly">The assembly from which the resources are loaded.</param>
        public XmlValidator(string baseSchemaEmbededResourceName, string baseResourcePackageName, Assembly assembly)
        {
            // Load the base schema and parse all included and imported schema recursive
            LoadSchemaWithDependencies(baseSchemaEmbededResourceName, baseResourcePackageName, assembly);
        }

        /// <summary>
        /// Validates the provided XML document. If the XML document does not comply with the schemas defined by the validator, then an
        /// exception is thrown.
        /// </summary>
        /// <param name="xmlDocument">The XML document that will be validated.</param>
        public void Validate(XmlDocument xmlDocument)
        {
            Validate(xmlDocument, InnerValidationCallback);
        }

        /// <summary>
        /// Validates the provided XML document. If the XML document does not comply with the schemas defined by the validator, then an
        /// exception is thrown.
        /// </summary>
        /// <param name="xmlDocument">The XML document that will be validated.</param>
        /// <param name="validationEventHandler">If an error or a warning occurs during the validation, then the provided validation
        /// handler will be called.</param>
        public void Validate(XmlDocument xmlDocument, ValidationEventHandler validationEventHandler)
        {
            foreach (var loadedSchema in _loadedSchemas.Values)
            {
                xmlDocument.Schemas.Add(loadedSchema);
            }

            xmlDocument.Validate(validationEventHandler);
        }

        /// <summary>
        /// Validates the provided XML document. If the XML document does not comply with the schemas defined by the validator, then an
        /// exception is thrown.
        /// </summary>
        /// <param name="xDocument">The XML document that will be validated.</param>
        public void Validate(XDocument xDocument)
        {
            Validate(ToXmlDocument(xDocument));
        }

        /// <summary>
        /// Validates the provided XML document. If the XML document does not comply with the schemas defined by the validator, then an
        /// exception is thrown.
        /// </summary>
        /// <param name="xDocument">The XML document that will be validated.</param>
        /// <param name="validationEventHandler">If an error or a warning occurs during the validation, then the provided validation
        /// handler will be called.</param>
        public void Validate(XDocument xDocument, ValidationEventHandler validationEventHandler)
        {
            Validate(ToXmlDocument(xDocument), validationEventHandler);
        }

        /// <summary>
        /// Load the schema from the specified stream and registers it by the specified name. Subsequently the referenced schemas are
        /// attempted loaded by calling the LoadSchemaWithDependencies-method.
        /// </summary>
        /// <param name="stream">The stream from which the schema is loaded.</param>
        /// <param name="registerName">The name by which the loaded shema is registered.</param>
        /// <param name="basePackageName">The base of the package name that is used to locate other schemas referenced from the loaded
        /// schema.</param>
        /// <param name="assembly">The assembly in which the referenced schemas are embedded.</param>
        private void LoadAndRegisterSchema(Stream stream, string registerName, string basePackageName, Assembly assembly)
        {
            // The xmlSchema and the foreach block are located outside the using scope in order to close the stream right after the
            // xmlSchema has been loaded.
            XmlSchema xmlSchema;
            using (var streamReader = new StreamReader(stream))
            {
                // Load the schema
                xmlSchema = XmlSchema.Read(streamReader, InnerValidationCallback);

                // Register the schema
                _loadedSchemas.Add(registerName, xmlSchema);
            }

            // Loop through the includes and imports (i.e. XmlSchemaExternal) and load the dependencies
            foreach (var xmlSchemaObject in xmlSchema.Includes)
            {
                if (xmlSchemaObject is XmlSchemaExternal)
                {
                    var xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
                    LoadSchemaWithDependencies(xmlSchemaExternal.SchemaLocation, basePackageName, assembly);
                }
            }
        }

        /// <summary>
        /// Loads the schema from an embedded resource. When the schema has been loaded it is registered by calling
        /// LoadAndRegisterSchema(Stream, string, string, Assembly)
        /// </summary>
        /// <param name="fqResourceName">The fully qualified name of the embeded resource containing the schema.</param>
        /// <param name="basePackageName">The base package name where the resource is located.</param>
        /// <param name="assembly">The assembly from which the schema is loaded.</param>
        private void LoadEmbededSchema(string fqResourceName, string basePackageName, Assembly assembly)
        {
            using (var resourceStream = assembly.GetManifestResourceStream(fqResourceName))
            {
                if (resourceStream == null)
                    throw new FileNotFoundException($"Unable to find or include resource - {fqResourceName}", fqResourceName);

                LoadAndRegisterSchema(resourceStream, fqResourceName, basePackageName, assembly);
            }
        }

        /// <summary>
        /// Examines the specified resource name. If the resource name specifies a protocol, then it is ignored, in fact it assumed, that
        /// the subsequent process handles the load of the resources. If no protocol is specified, then it loads the schema (by calling
        /// LoadEmbededSchema) taking into account whether the schema has been loaded already.
        /// </summary>
        /// <param name="resourceName">The name of the embeded resource containing the schema.</param>
        /// <param name="basePackageName">The base package name where the resource is located.</param>
        /// <param name="assembly">The assembly from which the schema is loaded.</param>
        private void LoadSchemaWithDependencies(string resourceName, string basePackageName, Assembly assembly)
        {
            // The fully qualified name of the resource is determined like this:
            var fqResourceName = GetEmbeddedResourcePath(basePackageName, resourceName);

            var splittedName = Regex.Split(resourceName, "://");

            switch (splittedName.Length)
            {
                case 0:
                    // Empty resource name provided.
                    throw new Exception("Unable to interprete resource name.");

                case 1:
                    // Check if the schema already has been loaded
                    if (_loadedSchemas.ContainsKey(fqResourceName))
                        return; // Already loaded

                    LoadEmbededSchema(fqResourceName, basePackageName, assembly);
                    break;

                // A protocol is specified.
            }
        }

        /// <summary>
        /// Object used to implement mutual exclusion to the Validation Call Back method.
        /// </summary>
        private readonly object _validationCallBackLockObject = new object();

        /// <summary>
        /// This method implements the call back on validation events. This is done by testing if any objects are observing the validation 
        /// events and then to delegate the calls to the observers. This is a workaround of Microsoft “filthy” way of implementing observer
        /// patterns.
        /// </summary>
        /// <param name="sender">The object that produces the event.</param>
        /// <param name="args">The parametres of the event.</param>
        private void OnValidationCallback(object sender, ValidationEventArgs args)
        {
            // The ValidationEventHandler is null if no observeres are subscribing.
            // For that reason we must test ValidationEventHandler for null before we invoke the observers.
            // In situations where an observer is unsubscribing at the same time this method is called,
            // in theory the ValidationEventHandler can not null during the test but null just after.
            // To avoid this race we syncronize the two steps.
            lock (_validationCallBackLockObject)
            {
                if (ValidationEventHandler == null)
                    return;

                ValidationEventHandler(sender, args);
            }
        }

        /// <summary>
        /// Default delegate to process error and warning events when parsing schemas. The delegate handles errors and warnings by throwing
        /// exceptions. The delegate uses the defined Regidity to determine in which cases the exceptions are thrown.
        /// </summary>
        /// <param name="sender">The object that produces the event.</param>
        /// <param name="args">The parametres of the event.</param>
        private void InnerValidationCallback(object sender, ValidationEventArgs args)
        {
            OnValidationCallback(sender, args);
            switch (args.Severity)
            {
                case XmlSeverityType.Error:
                    switch (Regidity)
                    {
                        case RegidityLevel.FailOnWarningsAndErrors:
                        case RegidityLevel.FailOnErrors:
                            throw new Exception($"ERROR: {args.Message}");

                        case RegidityLevel.Tolerant:
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;

                case XmlSeverityType.Warning:
                    switch (Regidity)
                    {
                        case RegidityLevel.FailOnWarningsAndErrors:
                            throw new Exception($"WARNING: {args.Message}");
                    }
                    break;
            }
        }

        /// <summary>
        /// Retrieves the path to the embedded resource, which is specified by the namespace of the resource 
        /// and the relative path in which the resource is located.
        /// E.g. if the namespace is “a.b.c” and the relative path is “..\d\MyFile.txt” then the returned path
        /// will be “a.b.d.MyFile.txt”.
        /// </summary>
        /// <param name="nameSpace">The name space in which the resource exsits.</param>
        /// <param name="relativePath">The path to the resource</param>
        /// <returns>The resource path.</returns>
        private static string GetEmbeddedResourcePath(string nameSpace, string relativePath)
        {
            // List of strings, each representing a part of the path to the embeded resource. The first part is the part closest to the
            // root.

            // Split the name space into parts and add each part to the part list.
            var partList = nameSpace.Split('.').ToList();

            // Loop throug all path items in the relative path and interpret them one by one.
            foreach (string relativePathItem in relativePath.Split('/'))
            {
                switch (relativePathItem)
                {
                    case ".":
                        continue; // Current folder is where we are - No action required.

                    case "..":
                        // Go one step up in the structure.
                        int nPathItems = partList.Count;
                        if (nPathItems == 0) // Are we already at the root?
                            throw new Exception($"Final path cannot be determined from base path ({nameSpace}) and relative path ({relativePath})");

                        // Remove the previous part of the part list and continue on with the next item.
                        partList.RemoveAt(nPathItems - 1);
                        continue;

                    default:
                        // Ordinary path item - just add it.
                        partList.Add(relativePathItem);
                        continue;
                }
            }

            // Build a string with the path as a .-separated list of parts.
            var sb = new StringBuilder();
            foreach (var part in partList)
            {
                if (sb.Length > 0) sb.Append(".");
                sb.Append(part);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts an XDocument to an XmlDocument
        /// </summary>
        /// 
        /// <param name="xDocument">The XML document which will be converted.</param>
        /// <returns>The converted XML document</returns>
        public static XmlDocument ToXmlDocument(XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }
    }
}
