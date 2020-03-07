using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WhatsAppApi.Operation.Helpers;
using WhatsAppApi.Operation.Helpers.Deserialize;
using WhatsAppApi.Operation.Helpers.Serialize;

namespace WhatsAppApi.Operation.Messages
{
    public enum MessageType
    {
        Chat, Image, Ptt, Document, Audio, Call_log
    }
    public enum ChatType
    {
        ChatId, Phone
    }

    //:Done
    /// <summary>
    /// Класс сообщений
    /// </summary>
    public class WhatsAppMessageProperties : ISerialize<WhatsAppMessageProperties>, IDeserialize<WhatsAppMessageProperties>
    {
        [JsonIgnore] public MessageType Type
        {
            get
            {
                switch (_type)
                {
                    case "chat": return MessageType.Chat;
                    case "image": return MessageType.Image;
                    case "ptt": return MessageType.Ptt;
                    case "document": return MessageType.Document;
                    case "audio": return MessageType.Audio;
                    case "call_log": return MessageType.Call_log;
                    default: throw new ArgumentOutOfRangeException(_type);
                }

            }
        }
        [JsonIgnore] public ChatType ChatsCharacteristic => string.IsNullOrEmpty(Phone.ToString()) & !string.IsNullOrEmpty(ChatId) ? ChatType.ChatId : ChatType.Phone;
        [JsonIgnore] public DateTime Time
        {
            get
            {
                if (_time == null) return default;
                var date = DateTime.FromFileTimeUtc((long)(_time * 10000068.987));//10000068.987 - константное значение тк в WhatsApp не используется год

                return date.AddYears(DateTime.Now.Year - date.Year);
            }
        }
        [JsonIgnore] public IWhatsAppApi<WhatsAppMessageProperties> Deserialize { get; set; } = new DeserializeMessage<WhatsAppMessageProperties>();
        [JsonIgnore] public WhatsAppMessageProperties Serialize { get; set; }
        [JsonIgnore] public DateTime DateTime { get; set; }
        [JsonIgnore] public string TypeMessageOperation { get; } = string.Empty;



        #region JSON fields

        /// <summary>
        /// Идентификационный номер сообщения
        /// </summary>
        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        private string _type;

        /// <summary>
        /// Автор сообщения
        /// </summary>
        [JsonProperty("senderName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string SenderName { get; set; }

        /// <summary>
        /// true - исходящее, false - входящее
        /// </summary>
        [JsonProperty("fromMe", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? FromMe { get; set; }

        /// <summary>
        /// Идентификационный номер автора сообщения
        /// </summary>
        [JsonProperty("author", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        /// <summary>
        /// Время отправления. Unix TimeStamp
        /// </summary>

        [JsonProperty("time", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        private long? _time;

        /// <summary>
        /// Порядковый номер сообщения в базе данных
        /// </summary>
        [JsonProperty("messageNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? MessageNumber { get; set; }


        [JsonProperty("chatId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ChatId { get; set; }

        [JsonProperty("phone", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long Phone { get; set; }

        [JsonProperty("body", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }

        [JsonProperty("filename", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        [JsonProperty("caption", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Caption { get; set; }

        [JsonProperty("audio", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Audio { get; set; }

        [JsonProperty("previewBase64", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PreviewBase64 { get; set; }

        [JsonProperty("title", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("contactId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ContactId { get; set; }

        [JsonProperty("lat", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Latitude { get; set; }

        [JsonProperty("lng", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Longitude { get; set; }

        [JsonProperty("address", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty("messageId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MessageId { get; set; }

        [JsonProperty("description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        

        #endregion

        /// <summary>
        /// Преобразование всех данных в классе в строковой тип
        /// </summary>
        public override string ToString()
        {
            return $"Id: {Id} \n" + $"Body: {Body} \n" + $"Type: {Type} \n" + $"SenderName: {SenderName} \n" +
                   $"FromMe: {FromMe} \n" + $"Author: {Author} \n" +
                    "Time: " + (this._time != null ? Time.ToString() : "null") + "\n" +
                    (ChatsCharacteristic == ChatType.ChatId ? $"ChatId: {ChatId} \n" : $"Phone: {Phone} \n") +
                   $"MessageNumber: {MessageNumber} \n\n";
        }



    }
}