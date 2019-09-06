using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishLearning.Utilities.Linq.Extensions
{
    public static class EnumerableExtensions
    {
        private static Random random = new Random();
        
        public static T GetRandomElement<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
                throw new ArgumentException();

            List<T> list;
            if ((list = sequence as List<T>) == null)
                list = new List<T>(sequence);
                
            if (!list.Any())
                return default(T);

            return list.ElementAt(random.Next(0, list.Count));
        }

        public static IEnumerable<T> GetRandomCountOfElements<T>(this IEnumerable<T> sequence, int count)
        {
            if (sequence == null)
                throw new ArgumentException();
            
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            
            List<T> list;
            if ((list = sequence as List<T>) == null)
                list = new List<T>(sequence);
            
            if (!list.Any())
                return Enumerable.Empty<T>();
            
            if (list.Count <= count)
                return list;

            var resultList = new List<T>();
            
            for (var i = 0; i < count; i++)
            {
                var index = random.Next(0, list.Count);

                resultList.Add(list.RemoveAndGetFromList(index));
            }

            return resultList;
        }
        
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
                return true;

            if (!sequence.Any())
                return true;

            return false;
        }
    }
}