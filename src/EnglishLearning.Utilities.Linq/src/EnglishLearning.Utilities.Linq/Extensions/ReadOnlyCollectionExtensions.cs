using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishLearning.Utilities.Linq.Extensions
{
    public static class ReadOnlyCollectionExtensions
    {
        public static int FindIndexOf<T>(this IReadOnlyCollection<T> collection, Func<T, bool> predicate)
        {
            var item = collection
                .Select((value, index) => new { value, index })
                .FirstOrDefault(x => predicate(x.value));

            if (item == null)
            {
                return -1;
            }

            return item.index;
        }
    }
}