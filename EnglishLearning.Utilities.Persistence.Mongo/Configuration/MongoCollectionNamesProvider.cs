using System;
using System.Collections.Generic;

namespace EnglishLearning.Utilities.Persistence.Mongo.Configuration
{
    public  class MongoCollectionNamesProvider
    {
        private readonly Dictionary<Type, string> _collectionNames;

        public MongoCollectionNamesProvider()
        {
            _collectionNames = new Dictionary<Type, string>();
        }

        public MongoCollectionNamesProvider Add<T>(string name)
        {
            _collectionNames.Add(typeof(T), name);
            return this;
        }

        public string GetCollectionName<T>()
        {
            return _collectionNames[typeof(T)];
        }
    }
}
