using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Runerne.Xml
{
    public static class XmlComperator
    {
        public enum DeclarationHandling
        {
            Ignore,
            Compare,
        }

        public enum TextNodeHandling
        {
            Concatenated,
            CDataAsText,
            Strict,
        }

        public enum AttributeHandling
        {
            Ignore,
            IgnoreMissingInB,
            IgnoreMissingInA,
            IgnoreOrder,
            Strict,
        }

        public enum CommentHandling
        {
            Ignore,
            Compare,
        }

        public enum ProcessingInstructionHandling
        {
            Ignore,
            Compare,
        }

        public interface IOptions
        {
            DeclarationHandling DeclarationHandling { get; }
            TextNodeHandling TextNodeHandling { get; }
            AttributeHandling AttributeHandling { get; }
            CommentHandling CommentHandling { get; }
            ProcessingInstructionHandling ProcessingInstructionHandling { get; }
        }


        public class ImmutableOptions : IOptions
        {
            public DeclarationHandling DeclarationHandling { get; }
            public TextNodeHandling TextNodeHandling { get; }
            public AttributeHandling AttributeHandling { get; }
            public CommentHandling CommentHandling { get; }
            public ProcessingInstructionHandling ProcessingInstructionHandling { get; }

            public ImmutableOptions(DeclarationHandling declarationHandling, TextNodeHandling textNodeHandling, AttributeHandling attributeHandling, CommentHandling commentHandling, ProcessingInstructionHandling processingInstructionHandling)
            {
                DeclarationHandling = declarationHandling;
                TextNodeHandling = textNodeHandling;
                AttributeHandling = attributeHandling;
                CommentHandling = commentHandling;
                ProcessingInstructionHandling = processingInstructionHandling;
            }
        }

        public class Options : IOptions
        {
            public DeclarationHandling DeclarationHandling { get; set; }
            public TextNodeHandling TextNodeHandling { get; set; }
            public AttributeHandling AttributeHandling { get; set; }
            public CommentHandling CommentHandling { get; set; }
            public ProcessingInstructionHandling ProcessingInstructionHandling { get; set; }

            public Options()
            {
                DeclarationHandling = DeclarationHandling.Compare;
                TextNodeHandling = TextNodeHandling.Strict;
                AttributeHandling = AttributeHandling.Strict;
                CommentHandling = CommentHandling.Compare;
                ProcessingInstructionHandling = ProcessingInstructionHandling.Compare;
            }

            public Options(DeclarationHandling declarationHandling, TextNodeHandling textNodeHandling, AttributeHandling attributeHandling, CommentHandling commentHandling, ProcessingInstructionHandling processingInstructionHandling)
            {
                DeclarationHandling = declarationHandling;
                TextNodeHandling = textNodeHandling;
                AttributeHandling = attributeHandling;
                CommentHandling = commentHandling;
                ProcessingInstructionHandling = processingInstructionHandling;
            }
        }

        public static readonly IOptions Strict = new ImmutableOptions(
            DeclarationHandling.Compare,
            TextNodeHandling.Strict,
            AttributeHandling.Strict,
            CommentHandling.Compare,
            ProcessingInstructionHandling.Compare
        );

        public static readonly IOptions Relaxed = new ImmutableOptions(
            DeclarationHandling.Ignore,
            TextNodeHandling.Concatenated,
            AttributeHandling.IgnoreOrder,
            CommentHandling.Ignore,
            ProcessingInstructionHandling.Ignore
        );

        public class XmlComparatorResult
        {
            public bool AreEqual
            {
                get
                {
                    return Reason == null;
                }
            }

            public UnequalityReason Reason { get; }

            internal XmlComparatorResult(UnequalityReason reason = null)
            {
                Reason = reason;
            }
        }

        public static readonly XmlComparatorResult EqualsComperatorResult = new XmlComparatorResult();

        public class UnequalityReason
        {
            public string Description { get; }
            public IXmlLineInfo XmlLineInfoA { get; }
            public IXmlLineInfo XmlLineInfoB { get; }

            public UnequalityReason(string description, object a = null, object b = null)
            {
                Description = description;
                XmlLineInfoA = a as IXmlLineInfo;
                XmlLineInfoB = b as IXmlLineInfo;
            }
        }

        public class UnequalityException : Exception
        {
            public UnequalityReason Reason { get; }

            public UnequalityException(UnequalityReason reason)
            {
                Reason = reason;
            }
        }


        public static bool AreEqual(XDocument a, XDocument b)
        {
            return Compare(a, b, Strict).AreEqual;
        }

        public static bool AreEqual(XDocument a, XDocument b, IOptions options)
        {
            return Compare(a, b, options).AreEqual;
        }

        public static XmlComparatorResult Compare(XDocument a, XDocument b)
        {
            return Compare(a, b, Strict);
        }

        public static XmlComparatorResult Compare(XDocument a, XDocument b, IOptions options)
        {
            try
            {
                CompareDocuments(a, b, options);
                return EqualsComperatorResult;
            }
            catch (UnequalityException ex)
            {
                return new XmlComparatorResult(ex.Reason);
            }
        }

        private static void CompareDocuments(XDocument a, XDocument b, IOptions options)
        {
            CompareDeclarations(a.Declaration, b.Declaration, options.DeclarationHandling);
            CompareElements(a.Root, b.Root, options);
        }

        private static void CompareDeclarations(XDeclaration a, XDeclaration b, DeclarationHandling declarationHandling)
        {
            switch (declarationHandling)
            {
                case DeclarationHandling.Compare:
                    if (a == null && b == null)
                        return;

                    if (a == null || b == null)
                        throw new UnequalityException(new UnequalityReason("Declaration was defined in only one document", a, b));

                    if (a.Encoding != b.Encoding)
                        throw new UnequalityException(new UnequalityReason("Different declaration encodings", a, b));

                    if (a.Standalone != b.Standalone)
                        throw new UnequalityException(new UnequalityReason("Different declaration standalone values", a, b));

                    if (a.Version != b.Version)
                        throw new UnequalityException(new UnequalityReason("Different declaration versions", a, b));

                    return;

                case DeclarationHandling.Ignore:
                    return;

                default:
                    return;
            }
        }

        private static void CompareElements(XElement a, XElement b, IOptions options)
        {
            if (a.Name != b.Name)
                throw new UnequalityException(new UnequalityReason("Different element names", a, b));

            CompareAttributes(a, b, options.AttributeHandling);
            CompareNodeCollections(a.Nodes(), b.Nodes(), options);
        }

        private static void CompareAttributes(XElement a, XElement b, AttributeHandling attributeHandling)
        {
            var attributesA = a.Attributes();
            var attributesB = b.Attributes();
            switch (attributeHandling)
            {
                case AttributeHandling.Ignore:
                    return;

                case AttributeHandling.IgnoreMissingInA:
                    CompareSubSets(a, b);
                    return;

                case AttributeHandling.IgnoreMissingInB:
                    CompareSubSets(b, a);
                    return;

                case AttributeHandling.IgnoreOrder:
                    CompareSubSets(a, b);
                    CompareSubSets(b, a);
                    return;

                case AttributeHandling.Strict:
                    if (attributesA.Count() != attributesB.Count())
                        throw new UnequalityException(new UnequalityReason("The number of attributes differ.", a, b));

                    var aArray = attributesA.ToArray();
                    var bArray = attributesB.ToArray();
                    for (var i = 0; i < aArray.Length; i++)
                        if (!AreEqual(aArray[i], bArray[i]))
                            throw new UnequalityException(new UnequalityReason($"Attributes at positition {i + 1} differ.", a, b));

                    return;

                default:
                    throw new UnequalityException(new UnequalityReason("Unexpected"));
            }
        }

        private static void CompareNodeCollections(IEnumerable<XNode> a, IEnumerable<XNode> b, IOptions options)
        {
            var itorA = a.GetEnumerator();
            var itorB = b.GetEnumerator();
            for (; ; )
            {
                var aContainsValue = itorA.MoveNext();
                var bContainsValue = itorB.MoveNext();

                if (!aContainsValue && !bContainsValue)
                    return;

                if (!aContainsValue)
                    throw new UnequalityException(new UnequalityReason("Node B does not have a matching A node.", itorA, itorB.Current));

                if (!bContainsValue)
                    throw new UnequalityException(new UnequalityReason("Node A does not have a matching B node.", itorA.Current, itorB));

                CompareNodes(itorA, itorB, options);
            }
        }

        private static void CompareSubSets(XElement a, XElement b)
        {
            var attributesA = a.Attributes();
            var attributesB = b.Attributes();

            var pos = 1;
            foreach (var aAttribute in attributesA)
            {
                var aName = aAttribute.Name;
                if (aName.LocalName.StartsWith("xmlns"))
                {
                    pos++;
                    continue;
                }

                var aValue = aAttribute.Value;
                var bAttribute = attributesB.SingleOrDefault(o => o.Name == aName);

                if (bAttribute == null)
                    throw new UnequalityException(new UnequalityReason("The number of attributes in B is less than in A.", a, b));

                if (aValue != bAttribute.Value)
                    throw new UnequalityException(new UnequalityReason($"The vallues of attribute at position {pos} differ.", a, b));

                pos++;
            }
        }

        private static bool AreEqual(XAttribute a, XAttribute b)
        {
            if (a == null && b == null)
                return true;

            if (a == null || b == null)
                return false;

            if (a.Name != b.Name)
                return false;

            if (a.Value != b.Value)
                return false;

            return true;
        }

        private static void CompareNodes(IEnumerator<XNode> aNodes, IEnumerator<XNode> bNodes, IOptions options)
        {
            var a = aNodes.Current;
            var b = bNodes.Current;

            if (a == null && b == null)
                return;

            if (a == null)
                throw new UnequalityException(new UnequalityReason("Node A was null but node B was defined.", b));

            if (b == null)
                throw new UnequalityException(new UnequalityReason("Node B was null but node A was defined.", a));

            var aType = a.GetType();
            var bType = b.GetType();

            if (!aType.IsAssignableFrom(bType) && !bType.IsAssignableFrom(aType))
                throw new UnequalityException(new UnequalityReason("Node A and node B cannot be assigned to each other.", a, b));

            if (typeof(XElement).IsAssignableFrom(aType) && typeof(XElement).IsAssignableFrom(bType))
            {
                CompareElements((XElement)a, (XElement)b, options);
                return;
            }

            if (typeof(XText).IsAssignableFrom(aType) && typeof(XText).IsAssignableFrom(bType))
            {
                CompareTexts(aNodes, bNodes, options.TextNodeHandling);
                return;
            }

            if (typeof(XComment).IsAssignableFrom(aType) && typeof(XComment).IsAssignableFrom(bType))
            {
                CompareComments((XComment)a, (XComment)b, options.CommentHandling);
                return;
            }

            if (typeof(XDocumentType).IsAssignableFrom(aType) && typeof(XDocumentType).IsAssignableFrom(bType))
            {
                CompareDocumentTypes((XDocumentType)a, (XDocumentType)b);
                return;
            }

            if (typeof(XProcessingInstruction).IsAssignableFrom(aType) && typeof(XProcessingInstruction).IsAssignableFrom(bType))
            {
                CompareProcessingInstructions((XProcessingInstruction)a, (XProcessingInstruction)b, options.ProcessingInstructionHandling);
                return;
            }

            throw new XmlException(a, $"Unsupported node ({aType})");
        }

        private static void CompareTexts(IEnumerator<XNode> aNodes, IEnumerator<XNode> bNodes, TextNodeHandling textNodeHandling)
        {
            var a = aNodes.Current as XText;
            var b = bNodes.Current as XText;

            switch (textNodeHandling)
            {
                case TextNodeHandling.Strict:
                    if (a.GetType() != b.GetType())
                        throw new UnequalityException(new UnequalityReason($"A CData is treated different than a text node. A is a {a.GetType()} and B is a {b.GetType()}.", a, b));

                    if (a.Value != b.Value)
                        throw new UnequalityException(new UnequalityReason($"Text contained texts ('{a.Value}' and '{b.Value}') differs.", a, b));
                    return;

                case TextNodeHandling.CDataAsText:
                    if (a.Value != b.Value)
                        throw new UnequalityException(new UnequalityReason($"Text contained texts ('{a.Value}' and '{b.Value}') differs.", a, b));
                    return;

                case TextNodeHandling.Concatenated:
                    if (a.PreviousNode != null && a.PreviousNode as XText != null)
                        return;

                    if (b.PreviousNode != null && b.PreviousNode as XText != null)
                        return;

                    var aText = GetConcatenatedText(aNodes);
                    var bText = GetConcatenatedText(bNodes);
                    if (aText != bText)
                        throw new UnequalityException(new UnequalityReason($"Text contained texts ('{a.Value}' and '{b.Value}') differs.", a, b));

                    return;

                default:
                    throw new Exception("Reached unexpected point in code.");
            }
        }

        private static string GetConcatenatedText(IEnumerator<XNode> iNode)
        {
            var text = new StringBuilder();

            for (; ; )
            {
                var current = iNode.Current as XText;
                text.Append(current.Value);
                var next = current.NextNode;
                if (next == null)
                    return text.ToString();

                if (next as XText == null)
                    return text.ToString();

                iNode.MoveNext();
            }
        }

        private static void CompareComments(XComment a, XComment b, CommentHandling commentHandling)
        {
            switch (commentHandling)
            {
                case CommentHandling.Ignore:
                    return;

                case CommentHandling.Compare:
                    if (a.Value != b.Value)
                        throw new UnequalityException(new UnequalityReason($"The comments ('{a.Value}' and '{b.Value}') are different", a, b));
                    return;

                default:
                    throw new Exception("Reached unexpected point in code.");
            }
        }

        private static void CompareDocumentTypes(XDocumentType a, XDocumentType b)
        {
            if (a.InternalSubset != b.InternalSubset)
                throw new UnequalityException(new UnequalityReason($"The internal sub-sets ({a.InternalSubset} and {b.InternalSubset}) of the document types differs", a, b));

            if (a.Name != b.Name)
                throw new UnequalityException(new UnequalityReason($"The names ({a.Name} and {b.Name}) of the document types differs", a, b));

            if (a.PublicId != b.PublicId)
                throw new UnequalityException(new UnequalityReason($"The public identifiers ({a.PublicId} and {b.PublicId}) of the document types differs", a, b));

            if (a.SystemId != b.SystemId)
                throw new UnequalityException(new UnequalityReason($"The system identifiers ({a.SystemId} and {b.SystemId}) of the document types differs", a, b));
        }

        private static void CompareProcessingInstructions(XProcessingInstruction a, XProcessingInstruction b, ProcessingInstructionHandling processingInstructionHandling)
        {
            switch (processingInstructionHandling)
            {
                case ProcessingInstructionHandling.Compare:
                    if (a.Data != b.Data)
                        throw new UnequalityException(new UnequalityReason($"The data parts ({a.Data} and {b.Data}) of the processing instructions differs", a, b));

                    if (a.Target != b.Target)
                        throw new UnequalityException(new UnequalityReason($"The target parts ({a.Target} and {b.Target}) of the processing instructions differs", a, b));

                    return;

                case ProcessingInstructionHandling.Ignore:
                    return;

                default:
                    throw new Exception("Reached unexpected point in code.");
            }
        }
    }
}
