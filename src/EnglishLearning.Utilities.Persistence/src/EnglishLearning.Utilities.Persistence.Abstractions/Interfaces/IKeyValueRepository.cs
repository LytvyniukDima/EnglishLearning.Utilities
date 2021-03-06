﻿namespace EnglishLearning.Utilities.Persistence.Interfaces
{
    public interface IKeyValueRepository
    {
        void SetStringKeyValue(string key, string value);
        string GetStringValueByKey(string key);
        
        void SetByteValue(string key, byte[] value);
        byte[] GetByteValue(string key);
        
        void SetObjectValue<T>(string key, T value) 
            where T : class;
        T GetObjectValue<T>(string key)
            where T : class;
    }
}
