using System;
using System.Collections.Generic;

namespace EnglishLearning.Utilities.Linq.Extensions
{
    public static class ListExtensions
    {
        public static T RemoveAndGetFromList<T>(this IList<T> list, int index)
        {
            if (index >= list.Count || index < 0)
                throw new IndexOutOfRangeException();

            var element = list[index];
            list.RemoveAt(index);

            return element;
        }
    }
}