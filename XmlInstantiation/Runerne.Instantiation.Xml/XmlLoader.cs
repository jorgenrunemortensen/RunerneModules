using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using Runerne.Utilities;
using Runerne.Xml;
using Runerne.Instantiation;

namespace Runerne.Instantiation.Xml
{
    /// <summary>
    /// This class is responsible for building Instantiation contexts from an XML sources.
    /// </summary>
    public static class XmlLoader
    {
        private static readonly XNamespace Ns = "http://runerne.dk/Instantiation.Xml";

        /// <summary>
        /// Loads a context by parsing the specified file. The optional specified arguments will be properties in the context.
        /// </summary>
        /// <param name="filePath">Path to the XML file configuring the context.</param>
        /// <param name="arguments">Initial properties.</param>
        /// <returns>The loaded context.</returns>
        public static RIContext Load(string filePath, IDictionary<string, object> arguments = null)
        {
            var xDocument = XDocument.Load(filePath, LoadOptions.SetLineInfo);
            return Parse(xDocument, arguments);
        }

        /// <summary>
        /// Loads a context by parsing the specified XML string. The specified arguments will be properties in the context.
        /// </summary>
        /// <param name="xml">The XML string that will be parsed./param>
        /// <param name="arguments">Initial properties.</param>
        /// <returns>The loaded context.</returns>
        public static RIContext Parse(string xml, IDictionary<string, object> arguments = null)
        {
            return Parse(XDocument.Parse(xml), arguments);
        }

        /// <summary>
        /// Loads a context by interpreting the specified document. The specified arguments will be properties in the context.
        /// </summary>
        /// <param name="xDocument">The document that is interpreted.</param>
        /// <param name="arguments">Initial properties.</param>
        /// <returns>The loaded context.</returns>
        public static RIContext Parse(XDocument xDocument, IDictionary<string, object> arguments = null)
        {
            var validator = new XmlValidator("context.xsd", "Runerne.Instantiation.Xml", Assembly.GetExecutingAssembly());
            validator.Validate(xDocument);

            var xRoot = xDocument.Root;
            var context = Parse(xRoot, arguments);

            return context;
        }

        /// <summary>
        /// Loads a context by interpreting the specified root element. The specified arguments will be properties in the context.
        /// </summary>
        /// <param name="xRoot">The root element that will be interpreted along with all the child elements.</param>
        /// <param name="arguments">Initial properties.</param>
        /// <returns>The loaded context.</returns>
        public static RIContext Parse(XElement xRoot, IDictionary<string, object> arguments = null)
        {
            if (arguments == null)
                arguments = new Dictionary<string, object> { };

            var context = new RIContext();

            foreach (var key in arguments.Keys)
                context.AddNamedInstanceProvider(key, new SimpleInstanceProvider(arguments[key]));

            ParseRootElement(xRoot, context);

            return context;
        }

        private static void ParseRootElement(XElement xRoot, RIContext context)
        {
            if (xRoot.Name != Ns + "context")
                throw new UnexpectedXmlElementException(xRoot, $"A '{{{Ns}}}:context' element was expected.");

            foreach (var xIncludes in xRoot.Elements(Ns + "includes"))
                ParseIncludes(xIncludes, context);

            foreach (var xInstances in xRoot.Elements(Ns + "instances"))
                ParseNamedInstances(xInstances, context);

            foreach (var xNameMappings in xRoot.Elements(Ns + "name-mappings"))
                ParseNameMappings(xNameMappings, context);
        }

        private static void ParseIncludes(XContainer xIncludes, RIContext context)
        {
            foreach (var xChild in xIncludes.Elements())
                context.IncludeAssembly(xChild.Value);
        }

        private static void ParseNamedInstances(XContainer xNamedInstances, RIContext context)
        {
            foreach (var xChild in xNamedInstances.Elements())
            {
                var instanceProvider = ParseInstanceElement(xChild, context);
                var name = xChild.Attribute("name").Value;
                context.AddNamedInstanceProvider(name, instanceProvider);
            }
        }

        private static IInstanceProvider ParseInstanceElement(XElement xInstance, RIContext context)
        {
            var tagName = xInstance.Name.LocalName;
            switch (tagName)
            {
                case "simple-instance":
                    return ParseSimpleInstanceElement(xInstance);

                case "complex-instance":
                    return ParseComplexInstanceElement(xInstance, context);

                case "static-instance":
                    return ParseStaticInstanceElement(xInstance, context);

                case "reference":
                    return ParseReferenceInstanceElement(xInstance, context);

                case "list":
                    return ParseListInstanceElement(xInstance, context);

                case "null-instance":
                    return ParseNullInstanceElement(xInstance, context);

                default:
                    throw new UnexpectedXmlElementException(xInstance, "A 'simple-instance' or a 'complex-instance' element was expected.");
            }
        }

        private static SimpleInstanceProvider ParseSimpleInstanceElement(XElement xSimpleInstance)
        {            
            var xValue = xSimpleInstance.Value;
            var typeName = xSimpleInstance.Attribute("type")?.Value;
            if (typeName == null)
            {
                try
                {
                    return new SimpleInstanceProvider(ValueUtilities.Parse(xValue));

                }
                catch (Exception ex)
                {
                    throw new XmlException(xSimpleInstance, ex);
                }
            }

            var value = ValueUtilities.ParseToType(xValue, typeName);
            return new SimpleInstanceProvider(value);
        }

        private static ComplexInstanceProvider ParseComplexInstanceElement(XElement xComplexInstance, RIContext context)
        {
            var typeName = xComplexInstance.Attribute("class").Value;
            try
            {
                var type = context.GetType(typeName);

                var constructorInstanceProviders = new List<IInstanceProvider>();

                foreach (var xConstructorArg in xComplexInstance.Elements(Ns + "constructor-args").Elements())
                    constructorInstanceProviders.Add(ParseInstanceElement(xConstructorArg, context));

                var propertiesInstanceProviders = new Dictionary<string, IInstanceProvider>();
                foreach (var xProperty in xComplexInstance.Elements(Ns + "properties").Elements())
                {
                    var name = xProperty.Attribute("name").Value;
                    if (propertiesInstanceProviders.ContainsKey(name))
                        throw new XmlException(xProperty, $"A value for the property by the name '{name}' has already been defined.");

                    propertiesInstanceProviders[name] = ParseInstanceElement(xProperty, context);
                }

                return new ComplexInstanceProvider(type, constructorInstanceProviders, propertiesInstanceProviders);
            }
            catch (XmlException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new XmlException(xComplexInstance, ex);
            }
        }

        private static IInstanceProvider ParseStaticInstanceElement(XElement xStaticInstance, RIContext context)
        {
            var typeName = xStaticInstance.Attribute("type").Value;
            try
            {
                var type = context.GetType(typeName);
                var memberName = xStaticInstance.Attribute("member").Value;
                return new StaticInstanceProvider(type, memberName);
            }
            catch(XmlException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new XmlException(xStaticInstance, ex);
            }
        }

        private static IInstanceProvider ParseReferenceInstanceElement(XElement xReference, RIContext context)
        {
            var refAttribute = xReference.Attribute("ref");
            var referencedName = refAttribute.Value;
            var instanceProvider = new ReferenceProvider(referencedName, context);
            return instanceProvider;
        }

        private static IInstanceProvider ParseListInstanceElement(XElement xList, RIContext context)
        {
            var collector = new List<IInstanceProvider>();
            foreach (var xChild in xList.Elements())
            {
                var instanceProvider = ParseInstanceElement(xChild, context);
                collector.Add(instanceProvider);
            }

            var collectionType = GetCollectionType(xList);
            var elementTypeAttribute = xList.Attribute("element-type");
            if (elementTypeAttribute == null)
                return new ListInstanceProvider(collector, collectionType);

            var elementType = context.GetType(elementTypeAttribute.Value);
            return new ListInstanceProvider(collector, elementType, collectionType);
        }

        private static ListInstanceProvider.CollectionType GetCollectionType(XElement xElement)
        {
            var name = xElement.Attribute("as")?.Value ?? "list";
            switch(name)
            {
                case "list":
                    return ListInstanceProvider.CollectionType.List;

                case "array":
                    return ListInstanceProvider.CollectionType.Array;

                default:
                    throw new XmlException(xElement, $"Unsupported list type '{name}'.");
            }
        }

        private static IInstanceProvider ParseNullInstanceElement(XElement xNullInstance, RIContext context)
        {
            var typeName = xNullInstance.Attribute("type")?.Value;

            if (typeName == null)
            {
                try
                {
                    return new NullTypeInstanceProvider(typeof(object));

                }
                catch (Exception ex)
                {
                    throw new XmlException(xNullInstance, ex);
                }
            }
            else
            {
                var type = context.GetType(typeName);
                return new NullTypeInstanceProvider(type);
            }
        }

        private static void ParseNameMappings(XElement xNameMappings, RIContext context)
        {
            foreach (var xNameMapping in xNameMappings.Elements())
                ParseNameMapping(xNameMapping, context);
        }

        private static void ParseNameMapping(XElement xNameMapping, RIContext context)
        {
            RIName to = xNameMapping.Attribute("to").Value;
            var instanceProvider = context.GetInstanceProvider(to);

            var fromAttribute = xNameMapping.Attribute("from");
            if (fromAttribute != null)
                context.AddNamedInstanceProvider(fromAttribute.Value, instanceProvider);

            foreach (var xFrom in xNameMapping.Elements(Ns + "from"))
            {
                RIName from = xFrom.Value;
                context.AddNamedInstanceProvider(from, instanceProvider);
            }
        }
    }
}
