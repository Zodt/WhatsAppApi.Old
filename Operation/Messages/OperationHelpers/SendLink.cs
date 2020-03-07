using Newtonsoft.Json;
using WhatsAppApi.Operation.Helpers;

namespace WhatsAppApi.Operation.Messages.OperationHelpers
{
    public class SendLink : ChatOrPhone
    {
        [JsonIgnore] private string _link;


        [JsonProperty("body", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Include)]
        private string _body;
        [JsonProperty("previewBase64", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Include)]
        private string _previewBase64;
        [JsonProperty("title", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Include)]
        public string Title { get; set; }
        [JsonProperty("description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonIgnore] public override string TypeMessageOperation { get; } = "sendLink";
        [JsonIgnore]
        public string Link
        {
            get => _link;
            set
            {
                if (string.IsNullOrEmpty(PreviewBase64)) _body = PreviewBase64 = _link = value;
                _body = _link = value;
            }
        }
        [JsonIgnore]
        public string PreviewBase64
        {
            get => _previewBase64;
            set => _previewBase64 = new Base64Image(value, true).ToString();
        }
        ~SendLink()
        {
            _body = default;
            _previewBase64 = default;
            Title = default;
            Description = default;
            Link = default;
            PreviewBase64 = default;
        }

    }
}