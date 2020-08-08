using System.Collections.Generic;
using System.Linq;

namespace EnglishLearning.Utilities.Linq.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, TValue> ConvertToStringKeyDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary)
        {
            return dictionary
                .Select(x => (key: x.Key.ToString(), value: x.Value))
                .ToDictionary(x => x.key, x => x.value);
        }
    }
}
