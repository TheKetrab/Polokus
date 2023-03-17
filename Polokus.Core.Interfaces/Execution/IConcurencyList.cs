namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// Safe thread list collection with mutex.
    /// </summary>
    /// <typeparam name="T">Type of list.</typeparam>
    public interface IConcurencyList<T>
    {
        /// <summary>
        /// Adds to <paramref name="item"/> list.
        /// </summary>
        /// <param name="item">Item to be added.</param>
        public void Add(T item);

        /// <summary>
        /// Remove all items that satisfies given predicate. Returns amount of removed elements.
        /// </summary>
        /// <param name="predicate">All items that satisfy this function will be removed.</param>
        public int Remove(Predicate<T> predicate);

        /// <summary>
        /// Returns all elements.
        /// </summary>
        public List<T> GetAll();

        /// <summary>
        /// Returns true iif list is not empty.
        /// </summary>
        public bool Any();

        /// <summary>
        /// Returns true iif any element satisfy given predicate.
        /// </summary>
        /// <param name="predicate">Function to filter with.</param>
        public bool Any(Predicate<T> predicate);
    }
}
