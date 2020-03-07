using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using WhatsAppApi.Operation.Helpers;
using WhatsAppApi.Operation.Helpers.Deserialize;
using WhatsAppApi.Operation.Helpers.Serialize;

namespace WhatsAppApi.Operation.Messages
{
    //:Done
    /// <summary>
    /// Класс структуры JSON-строки, присылаемой WhatsApp
    /// </summary>
    public class WhatsAppMessage : ISerialize<WhatsAppMessage>, IDeserialize<WhatsAppMessage>
    {
        /// <summary>
        /// Массив сообщений
        /// </summary>
        [JsonProperty("messages", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        private protected List<WhatsAppMessageProperties> MessagesList { get; set; }

        /// <summary>
        /// Номер последнего извлеченного сообщения
        /// </summary>
        [JsonProperty("lastMessageNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public BigInteger? LastMessageNumber { get; set; }

        [JsonIgnore] public ListMessage Messages => (ListMessage)(MessagesList ?? new List<WhatsAppMessageProperties>());
        [JsonIgnore] public IWhatsAppApi<WhatsAppMessage> Deserialize { get; set; } = new DeserializeMessage<WhatsAppMessage>();
        [JsonIgnore] public WhatsAppMessage Serialize { get; set; }
        [JsonIgnore] public DateTime DateTime { get; set; }
        [JsonIgnore] public string TypeMessageOperation { get; } = "messages";


        /// <summary>
        /// Предбразование всех данных в классе в строковой тип
        /// </summary>
        public override string ToString()
        {
            return Messages.Aggregate(string.Empty,
                       (current, val) => (current ?? "null") + val) + $"LastMessageNumber: {LastMessageNumber}; dtSending: {DateTime} \n";
        }
    }
    
}