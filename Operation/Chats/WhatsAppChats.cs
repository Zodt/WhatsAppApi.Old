using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WhatsAppApi.Operation.Helpers;
using WhatsAppApi.Operation.Helpers.Deserialize;
using WhatsAppApi.Operation.Helpers.Serialize;
using WhatsAppApi.Operation.Messages.OperationHelpers;

namespace WhatsAppApi.Operation.Chats
{
    
    public class WhatsAppChatsOperation : ISerialize<WhatsAppChatsOperation>, IDeserialize<WhatsAppChatsOperation>
    {
        [JsonProperty("dialogs", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public WhatsAppChat[] Dialogs { get; set; }

        public WhatsAppChatsOperation Serialize { get; set; }
        public IWhatsAppApi<WhatsAppChatsOperation> Deserialize { get; set; }
        public DateTime DateTime { get; set; }
        public string TypeMessageOperation { get; } = "dialogs";
    }

    public class ChatCreation : ISerialize<ChatCreation>, IDeserialize<ChatCreation>
    {
        [JsonProperty("groupName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string GroupName { get; set; }

        [JsonProperty("chatIds", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore, ItemConverterType = typeof(ChatOrPhone))]
        public List<string> ChatIds { get; set; }

        [JsonProperty("phones", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore, ItemConverterType = typeof(ChatOrPhone))]
        public List<string> Phones { get; set; }

        [JsonProperty("messageText", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MessageText { get; set; }




        [JsonProperty("created", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool Created { get; set; }

        [JsonProperty("message", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("chatId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ChatId { get; set; }

        [JsonProperty("groupInviteLink", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string GroupInviteLink { get; set; }

        public ChatCreation Serialize { get; set; }
        public IWhatsAppApi<ChatCreation> Deserialize { get; set; }
        public DateTime DateTime { get; set; }
        public string TypeMessageOperation { get; } = "group";
    }
}