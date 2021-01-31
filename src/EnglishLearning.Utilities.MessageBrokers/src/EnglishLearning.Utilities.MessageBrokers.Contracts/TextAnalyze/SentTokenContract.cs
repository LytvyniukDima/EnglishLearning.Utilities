using System.Text.Json.Serialization;
using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze
{
    [ProtoContract]
    public class SentTokenContract
    {
        [ProtoMember(1)]
        [JsonPropertyName("word")]
        public string Word { get; set; }
        
        [ProtoMember(2)]
        [JsonPropertyName("pos")]
        public string Pos { get; set; }
        
        [ProtoMember(3)]
        [JsonPropertyName("tag")]
        public string Tag { get; set; }
        
        [ProtoMember(4)]
        [JsonPropertyName("dep")]
        public string Dep { get; set; }
    }
}