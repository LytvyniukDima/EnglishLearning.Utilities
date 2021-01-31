using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze
{
    [ProtoContract]
    public class GrammarTextAnalyzedEvent
    {
        [ProtoMember(1)]
        [JsonPropertyName("analyze_id")]
        public Guid AnalyzeId { get; set; }
        
        [ProtoMember(2)]
        public List<ParsedSentContract> Sents { get; set; }
    }
}