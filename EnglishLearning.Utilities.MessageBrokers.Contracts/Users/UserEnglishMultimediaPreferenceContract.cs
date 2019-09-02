using System.Collections.Generic;
using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.Users
{
    [ProtoContract]
    public class UserEnglishMultimediaPreferenceContract
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public List<string> TextTypes { get; set; }
        [ProtoMember(3)]
        public List<string> VideoTypes { get; set; }
    }
}
