using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using WhatsAppApi.Operation.Helpers;

namespace WhatsAppApi.Operation.Messages.OperationHelpers
{
    public class SendFile : ChatOrPhone
    {
        [JsonIgnore] private Image _image;
        [JsonIgnore] private string _link;
        [JsonIgnore] private string _imagePathOrLink;


        [JsonProperty("body", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Include)]
        private string _body;
        [JsonProperty("filename", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Include)]
        public string FileName { get; set; }
        [JsonProperty("caption", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Include)]
        public string Caption { get; set; }


        [JsonIgnore]
        public string Link
        {
            get => _link;
            set => _body = _link = value;
        }
        [JsonIgnore]
        public Image Image
        {
            get => _image;
            set => _body = new Base64Image(_image = value).ToString();
        }
        [JsonIgnore]
        public string ImagePathOrLink
        {
            get => _imagePathOrLink;
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute)) { Link = value; _imagePathOrLink = value; FileName = "1.tiff"; }
                else { Image = Image.FromFile(value); _imagePathOrLink = value; FileName = new FileInfo(value).Name; }
            }
        }
        [JsonIgnore] public override string TypeMessageOperation { get; } = "sendFile";


        ~SendFile()
        {
            _link = null;
            _image = null;
            _imagePathOrLink = null;
            _body = null;
            FileName = null;
            Caption = null;
            ImagePathOrLink = default;
        }
    }
}