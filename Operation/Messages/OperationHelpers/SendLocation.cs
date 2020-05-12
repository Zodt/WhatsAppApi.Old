using Newtonsoft.Json;

namespace WhatsAppApi.Operation.Messages.OperationHelpers
{
    public class SendLocation : ChatOrPhone
    {

        [JsonProperty("lat", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double Latitude { get; set; }

        [JsonProperty("lng", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double Longitude { get; set; }

        [JsonProperty("address", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

    }
}