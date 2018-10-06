using System;
using System.Xml.Linq;

namespace Runerne.Xml
{
    /// <summary>
    /// This visitor can visit a node in an XDocument and remove all namespaces.
    /// </summary>
    internal class RemoveNamespaceVisitor
    {
        /// <summary>
        /// Removes the namespaces from the provided XDocument inclusive all child nodes.
        /// </summary>
        /// <param name="input">The XDocument which namespace references shall be remove.</param>
        /// <returns>A new XDocument without namespace references.</returns>
        public XDocument Visit(XDocument input)
        {
            var output = new XDocument();
            foreach (var xNode in input.Nodes())
                output.Add(Visit(xNode));
            return output;
        }

        /// <summary>
        /// Removes the namespaces from the provided XNode inclusive all child nodes.
        /// </summary>
        /// <param name="input">The XNode which namespace references shall be remove.</param>
        /// <returns>A new XNode without namespace references.</returns>
        public XNode Visit(XNode input)
        {
            if (input is XElement)
                return Visit((XElement)input);

            if (input is XCData)
                return Visit((XCData)input);

            if (input is XText)
                return Visit((XText)input);

            if (input is XComment)
                return Visit((XComment)input);

            if (input is XProcessingInstruction)
                return Visit((XProcessingInstruction)input);

            if (input is XDocumentType)
                return Visit((XDocumentType)input);

            throw new Exception($"Unable to visit a node of the type {input.GetType().FullName}.");
        }

        /// <summary>
        /// Removes the namespaces from the provided XElement inclusive all child nodes.
        /// </summary>
        /// <param name="input">The XElement which namespace references shall be remove.</param>
        /// <returns>A new XElement without namespace references.</returns>
        public XElement Visit(XElement input)
        {

            var output = new XElement(input.Name.LocalName);

            // Attributes
            foreach (var xAttribute in input.Attributes())
            {
                var attributeName = xAttribute.Name.LocalName;
                if (attributeName == "xmlns")
                    continue;

                if (xAttribute.Name.NamespaceName == "http://www.w3.org/2000/xmlns/")
                    continue;

                output.SetAttributeValue(xAttribute.Name.LocalName, xAttribute.Value);
            }

            // Children
            foreach (var xNode in input.Nodes())
                output.Add(Visit(xNode));

            return output;
        }

        /// <summary>
        /// Removes the namespaces from the provided XText.
        /// </summary>
        /// <param name="input">The XText which namespace references shall be remove.</param>
        /// <returns>A new XText without namespace references.</returns>
        public XText Visit(XText input)
        {
            return input;
        }

        /// <summary>
        /// Removes the namespaces from the provided XComment.
        /// </summary>
        /// <param name="xComment">The XComment which namespace references shall be remove.</param>
        /// <returns>A new XComment without namespace references.</returns>
        public XComment Visit(XComment xComment)
        {
            return xComment;
        }

        /// <summary>
        /// Removes the namespaces from the provided XDocumentType.
        /// </summary>
        /// <param name="input">The XDocumentType which namespace references shall be remove.</param>
        /// <returns>A new XDocumentType without namespace references.</returns>
        public XDocumentType Visit(XDocumentType input)
        {
            return input;
        }

        /// <summary>
        /// Removes the namespaces from the provided XProcessingInstruction.
        /// </summary>
        /// <param name="input">The XProcessingInstruction which namespace references shall be remove.</param>
        /// <returns>A new XProcessingInstruction without namespace references.</returns>
        public XProcessingInstruction Visit(XProcessingInstruction input)
        {
            return input;
        }

        /// <summary>
        /// Removes the namespaces from the provided XCData.
        /// </summary>
        /// <param name="input">The XCData which namespace references shall be remove.</param>
        /// <returns>A new XCData without namespace references.</returns>
        public XCData Visit(XCData input)
        {
            return input;
        }
    }
}
