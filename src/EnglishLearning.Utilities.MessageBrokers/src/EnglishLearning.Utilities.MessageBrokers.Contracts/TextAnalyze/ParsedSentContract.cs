using System.Collections.Generic;
using System.Text.Json.Serialization;
using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze
{
    [ProtoContract]
    public class ParsedSentContract
    {
        [ProtoMember(1)]
        public string Sent { get; set; }
        
        [ProtoMember(2)]
        [JsonPropertyName("sent_type")]
        public string SentType { get; set; }
        
        [ProtoMember(3)]
        public IReadOnlyCollection<SentTokenContract> Tokens { get; set; }
    }
}