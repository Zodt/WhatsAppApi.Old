using Newtonsoft.Json;

namespace WhatsAppApi.Operation.Messages.OperationHelpers
{
    public class TextMessage : ChatOrPhone
    {
        [JsonProperty("body", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        private string _body;

        [JsonIgnore] public override string TypeMessageOperation { get; } = "sendMessage";

        // ReSharper disable once ConvertToAutoProperty
        public string Message
        {
            get => _body;
            set => _body = value;
        }
    }
}