using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze
{
    [ProtoContract]
    public class GrammarFileAnalyzedEvent
    {
        [ProtoMember(1)]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [ProtoMember(2)]
        [JsonPropertyName("fileId")]
        public Guid FileId { get; set; }
        
        [ProtoMember(3)]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [ProtoMember(4)]
        [JsonPropertyName("createdTime")]
        public DateTime CreatedTime { get; set; }
        
        [ProtoMember(5)]
        [JsonPropertyName("path")]
        public IReadOnlyCollection<string> Path { get; set; }
    }
}