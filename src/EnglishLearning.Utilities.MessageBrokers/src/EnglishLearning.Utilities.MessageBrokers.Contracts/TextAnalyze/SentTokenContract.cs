using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze
{
    [ProtoContract]
    public class SentTokenContract
    {
        [ProtoMember(1)]
        public string Word { get; set; }
        
        [ProtoMember(2)]
        public string Pos { get; set; }
        
        [ProtoMember(3)]
        public string Tag { get; set; }
        
        [ProtoMember(4)]
        public string Dep { get; set; }
    }
}