namespace Runerne.Computation
{
    /// <summary>
    /// This is the base interface for all computable instances. A computable is known by being capable of deriving a value based on its 
    /// implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IComputable<out T>
    {
        /// <summary>
        /// An implementation of a computable shall can return a value of the specified type.
        /// </summary>
        T Value { get; }
    }
}
