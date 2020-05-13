using System.Collections.Generic;

namespace CmsApiSamples.Protocol.Extensions
{
    /// <summary>
    /// Extension methods for collections.
    /// </summary>
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Determines whether the specified collection is null or empty.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns><c>true</c> if the specified collection is null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty(this IReadOnlyCollection<object> collection)
        {
            return (collection == null || collection.Count == 0);
        }
    }
}
