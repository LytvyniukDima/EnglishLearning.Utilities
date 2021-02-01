using System;
using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze
{
    [ProtoContract]
    public class GrammarFileAnalyzedEvent
    {
        [ProtoMember(1)]
        public Guid Id { get; set; }
        
        [ProtoMember(2)]
        public Guid FileId { get; set; }
        
        [ProtoMember(3)]
        public string Name { get; set; }
        
        [ProtoMember(4)]
        public DateTime CreatedTime { get; set; }
    }
}