using System;
using ProtoBuf;

namespace EnglishLearning.Utilities.MessageBrokers.Contracts.Users
{
    [ProtoContract]
    public class UserCreatedEvent
    {
        [ProtoMember(1)]
        public Guid Id { get; set; }
        [ProtoMember(2)]
        public string EnglishLevel { get; set; }
        [ProtoMember(3)]
        public string Email { get; set; }
        
        [ProtoMember(4)]
        public UserEnglishMultimediaPreferenceContract UserEnglishMultimediaPreferences { get; set; }
        [ProtoMember(5)]
        public UserEnglishTaskPreferenceContract UserEnglishTaskPreferences { get; set; }
    }
}
