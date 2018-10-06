using System.Linq;

namespace Runerne.Instantiation
{
    /// <summary>
    /// The name class that is used for registering instance providers in the Runerne Instantiation Framework.
    /// </summary>
    public sealed class RIName
    {
        /// <summary>
        /// The local name by which the instance provider is known inside the given namespace.
        /// </summary>
        public string LocalName { get; }

        /// <summary>
        /// The namespace in which this name exists.
        /// </summary>
        public INamespace Namespace { get; }

        /// <summary>
        /// Creates an instance of this name.
        /// </summary>
        /// <param name="ns">The namespace in which this name exists.</param>
        /// <param name="localName">The local name by which the instance provider is known inside the given namespace.</param>
        public RIName(INamespace ns, string localName)
        {
            Namespace = ns;
            ValidateLocalName(localName);
            LocalName = localName;
        }

        /// <summary>
        /// Create an instance of this name inside the default namespace.
        /// </summary>
        /// <param name="localName">The local name by which the instance provider is known inside the default namespace.</param>
        public RIName(string localName)
        {
            Namespace = RINamespace.Default;
            ValidateLocalName(localName);
            LocalName = localName;
        }

        /// <summary>
        /// Returns a string representing the name at the form: “<localName>” if it exists in the default namespace; otherwise the string
        /// will be at the following form: ”{<namespace>}<localName>”. E.g. “MyInstance” or “{MyNamespace}MyInstance”.
        /// </summary>
        /// <returns>A string representing the name.</returns>
        public override string ToString()
        {
            return Namespace == null ? LocalName : $"{Namespace}{LocalName}";
        }

        /// <summary>
        /// A ash code made from the name.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Compares the name towards another name.
        /// </summary>
        /// <param name="obj">The name towards this name is compared.</param>
        /// <returns>True, of this name equals the specified name; Otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var riName = (RIName)obj;

            if (riName.LocalName != LocalName)
                return false;

            if (!Namespace.Equals(riName.Namespace))
                return false;

            return true;
        }

        /// <summary>
        /// Compares two names.
        /// </summary>
        /// <param name="name1">The name left to the operator.</param>
        /// <param name="name2">The name right to the operator.</param>
        /// <returns>True, if the two names equals; Otherwise false.</returns>
        public static bool operator ==(RIName name1, RIName name2)
        {
            if (ReferenceEquals(name1, name2))
                return true;

            if ((object)name1 == null)
                return false;

            return name1.Equals(name2);
        }

        /// <summary>
        /// Compares two names.
        /// </summary>
        /// <param name="name1">The name left to the operator.</param>
        /// <param name="name2">The name right to the operator.</param>
        /// <returns>False, if the two names equals; Otherwise true.</returns>
        public static bool operator !=(RIName name1, RIName name2)
        {
            return !(name1 == name2);
        }

        private enum ParseState
        {
            Starting,
            InNamespace,
            InName
        }

        /// <summary>
        /// Creates an RIName instance from the specified name.
        /// </summary>
        /// <param name="expandedName">The name from which the instance is created.</param>
        public static implicit operator RIName(string expandedName)
        {
            string sNamespace;
            string sLocalName;
            ParseExpandedName(expandedName, out sNamespace, out sLocalName);
            if (sNamespace == string.Empty)
                return new RIName(RINamespace.Default, sLocalName);

            var ns = new RINamespace(sNamespace);
            return new RIName(ns, sLocalName);
        }

        private static void ParseExpandedName(string expandedName, out string sNamespace, out string sLocalName)
        {
            sNamespace = "";
            sLocalName = "";

            ParseState state = ParseState.Starting;
            foreach (var ch in expandedName.ToCharArray())
            {
                switch (state)
                {
                    case ParseState.Starting:
                        switch (ch)
                        {
                            case '{':
                                state = ParseState.InNamespace;
                                continue;

                            case '}':
                                throw new RINameException($"The '{ch}' character can neither start a namespace or a name.");

                            default:
                                state = ParseState.InName;
                                sLocalName += ch;
                                continue;
                        }

                    case ParseState.InNamespace:
                        switch (ch)
                        {
                            case '{':
                                throw new RINameException($"The '{ch}' character cannot be used inside a namespace.");

                            case '}':
                                state = ParseState.InName;
                                continue;

                            default:
                                sNamespace += ch;
                                continue;
                        }

                    case ParseState.InName:
                        switch (ch)
                        {
                            case '{':
                            case '}':
                                throw new RINameException($"The '{ch}' character cannot be included in a name.");

                            default:
                                sLocalName += ch;
                                continue;
                        }
                }
            }

            switch (state)
            {
                case ParseState.InNamespace:
                    throw new RINameException($"The namespace qualifier must be terminated");
            }
        }

        private static void ValidateLocalName(string localName)
        {
            if (localName.Any(ch => ch == '{'))
                throw new RINameException("The '{' character cannot be included in a name.");

            if (localName.Any(ch => ch == '}'))
                throw new RINameException("The '}' character cannot be included in a name.");
        }
    }
}
