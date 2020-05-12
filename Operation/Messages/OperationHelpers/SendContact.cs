using Newtonsoft.Json;
using System.Collections.Generic;

namespace WhatsAppApi.Operation.Messages.OperationHelpers
{
    public class SendContact : ChatOrPhone
    {
        [JsonProperty("contactId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ContactId { get; set; }
    }
}