using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Runerne.Xml
{
    [Serializable]
    public class UnexpectedXmlElementException : XmlException
    {
        public UnexpectedXmlElementException(XElement xElement)
            : base(xElement, $"Unexpected element '{xElement.Name.LocalName}'.")
        {
        }

        public UnexpectedXmlElementException(XElement xElement, string message)
            : base(xElement, $"Unexpected element '{xElement.Name.LocalName}'. {message}")
        {
        }

        public UnexpectedXmlElementException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
