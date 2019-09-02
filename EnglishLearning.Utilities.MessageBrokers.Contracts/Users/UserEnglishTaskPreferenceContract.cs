using System.Collections.Generic;
using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.Users
{
    [ProtoContract]
    public class UserEnglishTaskPreferenceContract
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public List<string> GrammarParts { get; set; }
    }
}
