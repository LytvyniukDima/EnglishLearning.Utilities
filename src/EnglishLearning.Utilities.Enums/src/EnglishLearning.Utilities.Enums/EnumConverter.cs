using System;
using System.Collections.Generic;

namespace EnglishLearning.Utilities.Enums
{
    public static class EnumConverter
    {
        public static IEnumerable<T> ConverToEnumArray<T>(string[] stringValues) 
            where T : Enum
        {
            foreach (var stringValue in stringValues)
            {
                yield return ConvertToEnum<T>(stringValue);
            }
        }
        
        public static T ConvertToEnum<T>(string stringValue) 
            where T : Enum
        {
            return (T)Enum.Parse(typeof(T), stringValue);
        }
    }
}
