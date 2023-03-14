namespace Polokus.Core.Helpers
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static bool In<T>(this T item, params T[] items)
        {
            return items.Contains(item);
        }

        public static bool IsSameOrSubclass(this Type type, Type superType)
        {
            return type.IsSubclassOf(superType) || type == superType;
        }

    }

}
