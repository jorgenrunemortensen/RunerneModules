using Runerne.Utilities;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Runerne.Xml
{
    /// <summary>
    /// Utility class containing useful method for analyzing and manipulating XML.
    /// </summary>
    public static class XmlUtilities
    {
        /// <summary>
        /// This text is returned if line numbers are requested but not available.
        /// </summary>
        public static readonly string NoLineNumberDefinedText = "No line number defined";

        /// <summary>
        /// Gets the position in an XML file on the following textual form:
        ///   “Line = <i>line</i>, Column = <i>column</i>”,
        /// Where <i>line</i> is the line number of the position and <i>column</i> is the column number of the position.If the provided argument does 
        /// not contain any line information, then the text “No line number defined” is returned.
        /// </summary>
        /// <param name="xmlLineInfo">The information by which the position is retrieved.</param>
        /// <returns>A text describing the position.</returns>
        public static string GetLineInfoText(IXmlLineInfo xmlLineInfo)
        {
            return !xmlLineInfo.HasLineInfo() ? NoLineNumberDefinedText : $"Line = {xmlLineInfo.LineNumber}, Column = {xmlLineInfo.LinePosition}";
        }

        /// <summary>
        /// Returns a single element from an XContainer. The single element is identified by the element name. If none or multiple element 
        /// are found by the specified name, then an exception is thrown.
        /// </summary>
        /// <param name="xContainer">The container in which the element is searched.</param>
        /// <param name="elementName">The name of the element that the method searches for.</param>
        /// <returns>The one and single element found by the specified name.</returns>
        public static XElement GetSingleElement(XContainer xContainer, XName elementName)
        {
            var xChilds = xContainer.Elements(elementName);
            switch (xChilds.Count())
            {
                case 0:
                    {
                        var message = $"Missing required child element ({elementName}).";
                        if (xContainer is XElement)
                            throw new XmlException((XElement)xContainer, message);
                        throw new Exception(message);
                    }

                case 1:
                    return xChilds.Single();

                default:
                    {
                        var message = $"Too many child elements ({elementName}). Only one is allowed.";
                        if (xContainer is XElement)
                            throw new XmlException((XElement)xContainer, message);
                        throw new Exception(message);
                    }
            }
        }

        /// <summary>
        /// Returns a single element from an XContainer. If none or multiple element are contained, then an exception is thrown.
        /// </summary>
        /// <param name="xContainer">The container that must contain one single element.</param>
        /// <returns>The single contained element.</returns>
        public static XElement GetSingleElement(XContainer xContainer)
        {
            var xChilds = xContainer.Elements();
            switch (xChilds.Count())
            {
                case 0:
                    {
                        const string message = "Missing required child element.";
                        if (xContainer is XElement)
                            throw new XmlException((XElement)xContainer, message);
                        throw new Exception(message);
                    }

                case 1:
                    return xChilds.Single();

                default:
                    {
                        const string message = "Too many child elements. Only one is allowed.";
                        if (xContainer is XElement)
                            throw new XmlException((XElement)xContainer, message);
                        throw new Exception(message);
                    }
            }
        }

        /// <summary>
        /// Searches a container for a single element of a specific name. If the element is found then the value of the element is returned
        /// ; otherwise an exception is thrown.
        /// </summary>
        /// <param name="xContainer">The container that is searched for a specific element.</param>
        /// <param name="elementName">The name of the element that the method searches for.</param>
        /// <returns>The value contained in the found element.</returns>
        public static string GetRequiredStringValue(XContainer xContainer, XName elementName)
        {
            return GetSingleElement(xContainer, elementName).Value;
        }

        /// <summary>
        /// Searches a container for a single element of a specific name. If the element is found then the value of the element is returned
        /// . If no such element was found, then the specified default value is returned. If multiple elements are found, then an exception 
        /// is thrown.
        /// </summary>
        /// <param name="xContainer">The container that is searched for a specific element.</param>
        /// <param name="elementName"></param>
        /// <param name="defaultValue">The value that is returned if no such element was found.</param>
        /// <param name="elementName">The name of the element that the method searches for.</param>
        /// <returns>The value contained in the found element or the specified default value.</returns>
        public static string GetOptionalStringValue(XContainer xContainer, XName elementName, string defaultValue)
        {
            var elements = xContainer.Elements(elementName);
            switch (elements.Count())
            {
                case 0:
                    return defaultValue;

                case 1:
                    return elements.Single().Value;

                default:
                    var message = $"Multiple child elements named {elementName} was found. Only zero or one was expected.";
                    if (xContainer is XElement)
                        throw new XmlException((XElement)xContainer, message);
                    throw new Exception(message);
            }
        }

        /// <summary>
        /// Finds and returns the value of the specified attribute on the specified element. If no such attribute was found, then an
        /// exception is thrown.
        /// </summary>
        /// <param name="xElement">The element that shall contained the attribute.</param>
        /// <param name="attributeName">The name of the attribute, which value will be returned.</param>
        /// <returns>The value contained in the attribute by the specified name on the specified element.</returns>
        public static string GetRequiredAttributeStringValue(XElement xElement, XName attributeName)
        {
            var attribute = xElement.Attribute(attributeName);
            if (attribute == null)
                throw new XmlException(xElement, $"Missing required attribute '{attributeName}'.");
            return attribute.Value;
        }

        /// <summary>
        /// Returns the value of the specified attribute on the specified element. If no such attribute was found, then a default value is 
        /// returned.
        /// </summary>
        /// <param name="xElement">The element that is searched for an attribute by the specified name.</param>
        /// <param name="attributeName">The name of the attribute that the method seaches for.</param>
        /// <param name="defaultValue">The value returned if no such attribute was found.</param>
        /// <returns>The value of the attribute or the default value.</returns>
        public static string GetOptionalAttributeStringValue(XElement xElement, XName attributeName, string defaultValue)
        {
            var attribute = xElement.Attribute(attributeName);
            return attribute?.Value ?? defaultValue;
        }

        /// <summary>
        /// Searches for a specified element in the specified container. If no such element was found, then an exception is thrown. If the 
        /// element is found the method interprets the element value into an enumeration value of the specified type. If the value cannot 
        /// be interpreted into an enumeration of the type, then an exception is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="xContainer">The container in which the method searches for the element.</param>
        /// <param name="elementName">The name of the element that the method searches for.</param>
        /// <returns>The enumeration value of the value contained in the element.</returns>
        public static T GetRequiredEnumValue<T>(XContainer xContainer, XName elementName)
        {
            var sValue = GetRequiredStringValue(xContainer, elementName);
            return EnumUtilities.ToValue<T>(sValue);
        }

        /// <summary>
        /// Searches for a specified element in the specified container. If no such element was found, then a default value is returned. If
        /// the element is found the method interprets the element value into an enumeration value of the specified type. If the value
        /// cannot be interpreted into an enumeration of the type, then an exception is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="xContainer">The container in which the method searches for the element.</param>
        /// <param name="elementName">The name of the element that the method searches for.</param>
        /// <param name="defaultValue">The enumeration value that will be returned if no element by the specified name is contained.</param>
        /// <returns></returns>
        public static T GetOptionalEnumValue<T>(XContainer xContainer, XName elementName, T defaultValue)
        {
            var sValue = GetOptionalStringValue(xContainer, elementName, defaultValue.ToString());
            return EnumUtilities.ToValue<T>(sValue);
        }

        /// <summary>
        /// Searches for a specific attribute on the specified element. If no such attribute was found, then an exception is thrown. If 
        /// the attribute is found the method interprets the attribute value into an enumeration value of the specified type. If the value cannot be interpreted into an enumeration of the type, then an exception is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="xElement">The element that is searched for an attribute of the specified name.</param>
        /// <param name="attributeName">The name of the attribute that the method searches for in the element.</param>
        /// <returns>The enumeration value of the attribute value.</returns>
        public static T GetRequiredAttributeEnumValue<T>(XElement xElement, XName attributeName)
        {
            var sValue = GetRequiredAttributeStringValue(xElement, attributeName);
            return EnumUtilities.ToValue<T>(sValue);
        }

        /// <summary>
        /// Searches for a specified element in the specified container. If no such element was found, then a default value is returned. If 
        /// the element is found the method searches for an attribute by the specified name. If no such attribute was found, then a default
        /// value is returned as well. If the attribute was found, then the attribute value is interpreted into an enumeration value of the
        /// specified type. If the value cannot be interpreted into an enumeration of the type, then an exception is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="xElement">The element that is searched for an attribute of the specified name.</param>
        /// <param name="attributeName">The name of the attribute that the method searches for in the element.</param>
        /// <param name="defaultValue"></param>
        /// <param name="defaultValue">The enumeration value that will be returned if no element or attribute by the specified name is
        /// contained.</param>
        public static T GetOptionalAttributeEnumValue<T>(XElement xElement, XName attributeName, T defaultValue)
        {
            var sValue = GetOptionalAttributeStringValue(xElement, attributeName, defaultValue.ToString());
            return EnumUtilities.ToValue<T>(sValue);
        }

        /// <summary>
        /// Searches for a specified element in the specified container. If no such element was found, then an exception is thrown. If the 
        /// element is found then the value of the element is converted into the specified type and returned (<see cref="StringUtilities.ConvertString"/>).
        /// </summary>
        /// <typeparam name="T">The type into which the element value is converted.</typeparam>
        /// <param name="xContainer">The container in which the method searches for the element.</param>
        /// <param name="elementName">The name of the element that the method searches for.</param>
        /// <returns>The element value as a specific type.</returns>
        public static T GetRequiredValue<T>(XContainer xContainer, XName elementName)
        {
            var sValue = GetRequiredStringValue(xContainer, elementName);
            return StringUtilities.ConvertString<T>(sValue);
        }

        /// <summary>
        /// Searches for a specified element in the specified container. If no such element was found, then the specified default value is 
        /// returned. If the element is found, then the value of the element is converted into the specified type and returned
        /// (<see cref="StringUtilities.ConvertString"/>).
        /// </summary>
        /// <typeparam name="T">The type into which the element value is converted.</typeparam>
        /// <param name="xContainer">The container in which the method searches for the element.</param>
        /// <param name="elementName">The name of the element that the method searches for.</param>
        /// <param name="defaultValue">The default value that will be returned if no element was found by the specified name.</param>
        /// <returns>The value of the element converted into the specified type.</returns>
        public static T GetOptionalValue<T>(XContainer xContainer, XName elementName, T defaultValue)
        {
            if (defaultValue == null)
                return GetOptionalValue<T>(xContainer, elementName);

            var sValue = GetOptionalStringValue(xContainer, elementName, null);
            return sValue == null ? defaultValue : StringUtilities.ConvertString<T>(sValue);
        }

        /// <summary>
        /// Searches for an attribute by the specified name on the specified element. If no such attribute was found, then an exception is 
        /// thrown as well; otherwise the value of the attribute is converted into the  specified type and returned
        /// (<see cref="StringUtilities.ConvertString"/>).
        /// </summary>
        /// <typeparam name="T">The type into which the attribute value is converted.</typeparam>
        /// <param name="xElement">The element on which the method searches for the attribute.</param>
        /// <param name="attributeName">The name of the attribute that the method searches for.</param>
        /// <returns>The attribute value as a specific type.</returns>
        public static T GetRequiredAttributeValue<T>(XElement xElement, XName attributeName)
        {
            var sValue = GetRequiredAttributeStringValue(xElement, attributeName);
            return StringUtilities.ConvertString<T>(sValue);
        }

        /// <summary>
        /// Searches for an attribute by the specified name on the specified element. If no such attribute was found, then the specified 
        /// default value is returned; otherwise the value of the attribute is converted into the  specified type and returned
        /// (<see cref="StringUtilities.ConvertString"/>).
        /// </summary>
        /// <typeparam name="T">The type into which the attribute value is converted.</typeparam>
        /// <param name="xElement">The element on which the method searches for the attribute.</param>
        /// <param name="attributeName">The name of the attribute that the method searches for.</param>
        /// <param name="defaultValue">The value that is returned if no such element or attribute were found.</param>
        /// <returns>The value of the attribute or the default value.</returns>
        public static T GetOptionalAttributeValue<T>(XElement xElement, XName attributeName, T defaultValue)
        {
            if (defaultValue == null)
                return GetOptionalAttributeValue<T>(xElement, attributeName);

            var sValue = GetOptionalAttributeStringValue(xElement, attributeName, null);
            return sValue == null ? defaultValue : StringUtilities.ConvertString<T>(sValue);
        }

        /// <summary>
        /// Searches for an element in the specified container by the specified name. If no such element was found, then a null value is 
        /// returned. If the element is found, then the method converts the element value into an object of the specified type and returns 
        /// it.
        /// </summary>
        /// <typeparam name="T">The type into which the element value is converted.</typeparam>
        /// <param name="xContainer">The container in which the method searches for the element.</param>
        /// <param name="elementName">The name of the element that the method searches for.</param>
        /// <returns>The value of the found element or null.</returns>
        public static T GetOptionalValue<T>(XContainer xContainer, XName elementName)
        {
            var sValue = GetOptionalStringValue(xContainer, elementName, null);
            return sValue == null ? default(T) : StringUtilities.ConvertString<T>(sValue);
        }

        /// <summary>
        /// Searches for an attribute by the specified name on the specified element. If no such attribute was found, then a null value is 
        /// returned; otherwise the value of the attribute is converted into the specified type and returned 
        /// (<see cref="StringUtilities.ConvertString"/>).
        /// </summary>
        /// <typeparam name="T">The type into which the attribute value is converted.</typeparam>
        /// <param name="xElement">The element on which the method searches for the attribute.</param>
        /// <param name="attributeName">The name of the attribute that the method searches for.</param>
        /// <returns>The valye of the attribute; otherwise null.</returns>
        public static T GetOptionalAttributeValue<T>(XElement xElement, XName attributeName)
        {
            var sValue = GetOptionalAttributeStringValue(xElement, attributeName, null);
            return sValue == null ? default(T) : StringUtilities.ConvertString<T>(sValue);
        }

        /// <summary>
        /// Parses the specified document and removes all namespaces and namespace references.
        /// </summary>
        /// <param name="xDocument">The document in which all namespaces and namespace references are removed.</param>
        /// <returns>A new document without namespaces or namespace references.</returns>
        public static XDocument RemoveAllNamespaces(XDocument xDocument)
        {
            var visitor = new RemoveNamespaceVisitor();
            return visitor.Visit(xDocument);
        }
    }
}
