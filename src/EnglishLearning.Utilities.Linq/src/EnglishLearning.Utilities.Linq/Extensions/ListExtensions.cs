using System;
using System.Collections.Generic;

namespace EnglishLearning.Utilities.Linq.Extensions
{
    public static class ListExtensions
    {
        public static T RemoveAndGetFromList<T>(this IList<T> list, int index)
        {
            if (index >= list.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var element = list[index];
            list.RemoveAt(index);

            return element;
        }
        
        public static IReadOnlyList<KeyValuePair<int, int>> SplintOnRangesIndexes<T>(this List<T> list, int sizeOfRanges)
        {
            var result = new List<KeyValuePair<int, int>>();

            int index = 0;
            while (index < list.Count)
            {
                if (index + sizeOfRanges < list.Count)
                {
                    result.Add(new KeyValuePair<int, int>(index, sizeOfRanges));
                    index += sizeOfRanges;
                }
                else
                {
                    result.Add(new KeyValuePair<int, int>(index, list.Count - index));
                    index += list.Count - index;
                }
            }
            
            return result;
        }
    }
}
