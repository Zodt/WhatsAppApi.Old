using Newtonsoft.Json;
using System.Collections.Generic;

namespace WhatsAppApi.Operation.Messages.OperationHelpers
{
    public class SendContact : ChatOrPhone
    {
        [JsonIgnore] public override string TypeMessageOperation { get; } = "sendContact";

        [JsonProperty("contactId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ContactId { get; set; }

    }
}