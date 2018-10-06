using System.Linq;

namespace Runerne.Instantiation
{
    /// <summary>
    /// The namespace of a <see cref="RIName"/> instance.
    /// </summary>
    public class RINamespace : INamespace
    {
        /// <summary>
        /// Represents the default namespace.
        /// </summary>
        internal sealed class DefaultNamespace : INamespace
        {
            /// <summary>
            /// Returns the empty string.
            /// </summary>
            /// <returns>An empty string.</returns>
            public override string ToString()
            {
                return string.Empty;
            }

            /// <summary>
            /// Returns a hash code made from the empty string.
            /// </summary>
            /// <returns>A hash code.</returns>
            public override int GetHashCode()
            {
                return string.Empty.GetHashCode();
            }

            /// <summary>
            /// Determines whether the given object is a default namespace.
            /// </summary>
            /// <param name="obj">The object that is tested.</param>
            /// <returns>True of the specified object is the default namespace; Otherwise false.</returns>
            public override bool Equals(object obj)
            {
                var ns = obj as DefaultNamespace;
                return ns != null;
            }
        }

        /// <summary>
        /// The default namespace.
        /// </summary>
        public static readonly INamespace Default = new DefaultNamespace();

        /// <summary>
        /// The name of the namespace.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Creates a namespace by the given name.
        /// </summary>
        /// <param name="name">The name of the created namespace.</param>
        public RINamespace(string name)
        {
            Validate(name);
            Name = name;
        }

        /// <summary>
        /// Creates a namespace from a string.
        /// </summary>
        /// <param name="name">The name of the namespace that is created.</param>
        public static implicit operator RINamespace(string name)
        {
            return new RINamespace(name);
        }

        /// <summary>
        /// Creates a <see cref="RIName"/> instance from a namespace and a local name.
        /// </summary>
        /// <param name="ns">The namespace.</param>
        /// <param name="localName">The local name.</param>
        /// <returns>The created RIName instance.</returns>
        public static RIName operator +(RINamespace ns, string localName)
        {
            return new RIName(ns, localName);
        }

        /// <summary>
        /// Returns a string representing the namespace. The string is the name of the namespace encaptulated by curled braces.
        /// </summary>
        /// <returns>The string representing the namespace.</returns>
        public override string ToString()
        {
            return "{" + Name + "}";
        }

        /// <summary>
        /// A hash code made from the name.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>
        /// Compares the namespace to another namespace.
        /// </summary>
        /// <param name="obj">The namespace towards this namespace is compared.</param>
        /// <returns>True, of this namespace equals the specified namespace; Otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Name == ((RINamespace)obj).Name;
        }

        /// <summary>
        /// Compares two namespaces.
        /// </summary>
        /// <param name="ns1">The namespace left to the operator.</param>
        /// <param name="ns2">The namespace right to the operator.</param>
        /// <returns>True, if the two namespaces equals; Otherwise false.</returns>
        public static bool operator ==(RINamespace ns1, INamespace ns2)
        {
            if (ReferenceEquals(ns1, ns2))
                return true;

            if ((object)ns1 == null)
                return false;

            return ns1.Equals(ns2);
        }

        /// <summary>
        /// Compares two namespaces.
        /// </summary>
        /// <param name="ns1">The namespace left to the operator.</param>
        /// <param name="ns2">The namespace right to the operator.</param>
        /// <returns>False, if the two namespaces equals; Otherwise true.</returns>
        public static bool operator !=(RINamespace ns1, INamespace ns2)
        {
            return !(ns1 == ns2);
        }

        private static void Validate(string name)
        {
            if (name.Any(ch => ch == '{'))
                throw new RINamespaceException("The '{' cannot be contained in a Namespace.");

            if (name.Any(ch => ch == '}'))
                throw new RINamespaceException("The '}' cannot be contained in a Namespace.");
        }
    }
}
