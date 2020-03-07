using Newtonsoft.Json;
using System;
using WhatsAppApi.Operation.Helpers.Deserialize;
using WhatsAppApi.Operation.Helpers.Serialize;

namespace WhatsAppApi.Operation.Helpers
{
    public class WhatsAppApiAnswers : ISerialize<WhatsAppApiAnswers>, IDeserialize<WhatsAppApiAnswers>
    {
        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("sent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Sent { get; set; }

        [JsonProperty("message", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("queueNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string QueueNumber { get; set; }

        [JsonIgnore] public DateTime DateTime { get; set; }
        [JsonIgnore] public WhatsAppApiAnswers Serialize { get; set; }
        [JsonIgnore] public string TypeMessageOperation { get; set; } = string.Empty;
        [JsonIgnore] public IWhatsAppApi<WhatsAppApiAnswers> Deserialize { get; set; } = new DeserializeMessage<WhatsAppApiAnswers>();
        public override string ToString() => $"{nameof(Id)}: {Id}; {nameof(Sent)}:{Sent}; {nameof(Message)}: {Message}; {nameof(QueueNumber)}: {QueueNumber}; dtSending: {DateTime}";
    }
}