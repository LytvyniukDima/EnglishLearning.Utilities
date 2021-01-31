using System.Collections.Generic;
using System.Text.Json.Serialization;
using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze
{
    [ProtoContract]
    public class ParsedSentContract
    {
        [ProtoMember(1)]
        [JsonPropertyName("sent")]
        public string Sent { get; set; }
        
        [ProtoMember(2)]
        [JsonPropertyName("sent_type")]
        public string SentType { get; set; }
        
        [ProtoMember(3)]
        [JsonPropertyName("tokens")]
        public IReadOnlyCollection<SentTokenContract> Tokens { get; set; }
    }
}