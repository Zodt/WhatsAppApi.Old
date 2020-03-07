using Newtonsoft.Json;
using System;
using WhatsAppApi.Operation.Helpers;
using WhatsAppApi.Operation.Helpers.Deserialize;
using WhatsAppApi.Operation.Helpers.Serialize;

namespace WhatsAppApi.Operation.Instances
{
    public class WhatsAppInstanceOperation : ISerialize<WhatsAppInstanceOperation>, IDeserialize<WhatsAppInstanceOperation>
    {
        [JsonProperty("accountStatus", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string AccountStatus { get; set; }

        [JsonProperty("qrCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string QrCode { get; set; }

        [JsonProperty("statusData", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public StatusData StatusData { get; set; }

        [JsonIgnore] public DateTime DateTime { get; set; }
        [JsonIgnore] public WhatsAppInstanceOperation Serialize { get; set; }
        [JsonIgnore] public string TypeMessageOperation { get; set; } = string.Empty;
        [JsonIgnore] public IWhatsAppApi<WhatsAppInstanceOperation> Deserialize { get; set; } = new DeserializeMessage<WhatsAppInstanceOperation>();
    }

    public class Actions
    {
        [JsonProperty("expiry", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Expiry Expiry { get; set; }

        [JsonProperty("retry", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Expiry Retry { get; set; }

        [JsonProperty("logout", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Expiry Logout { get; set; }

        [JsonProperty("takeover", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Expiry Takeover { get; set; }

        [JsonProperty("learn_more", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public LearnMore LearnMore { get; set; }
    }

    public class Expiry
    {
        [JsonProperty("act", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Act { get; set; }

        [JsonProperty("label", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
    }

    public class LearnMore
    {
        [JsonProperty("label", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("link", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Link { get; set; }
    }

    public class StatusData
    {
        [JsonProperty("substatus", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Substatus { get; set; }

        [JsonProperty("title", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("msg", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        [JsonProperty("submsg", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Submsg { get; set; }

        [JsonProperty("actions", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Actions Actions { get; set; }

        [JsonProperty("reason", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }
    }
}