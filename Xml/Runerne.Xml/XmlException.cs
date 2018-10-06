using System;
using System.Runtime.Serialization;
using System.Xml;

namespace Runerne.Xml
{
    [Serializable]
    public class XmlException : Exception
    {
        public XmlException(IXmlLineInfo element, string message)
            : base(FormMessage(element, message))
        {
        }

        public XmlException(IXmlLineInfo xmlLineInfo, Exception cause)
            : base(FormMessage(xmlLineInfo), cause)
        {
        }

        public XmlException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string FormMessage(IXmlLineInfo xmlLineInfo)
        {
            var lineInfoText = XmlUtilities.GetLineInfoText(xmlLineInfo);
            return $"Xml Error [{lineInfoText}].";
        }

        private static string FormMessage(IXmlLineInfo xmlLineInfo, string message)
        {
            var lineInfoText = XmlUtilities.GetLineInfoText(xmlLineInfo);
            return $"Xml Error [{lineInfoText}]. {message}";
        }
    }
}
