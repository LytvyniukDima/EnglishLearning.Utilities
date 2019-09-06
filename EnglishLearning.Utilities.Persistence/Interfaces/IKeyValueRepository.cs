namespace EnglishLearning.Utilities.Persistence.Interfaces
{
    public interface IKeyValueRepository
    {
        void SetStringKeyValue(string key, string value);
        string GetStringValueByKey(string key);
    }
}
