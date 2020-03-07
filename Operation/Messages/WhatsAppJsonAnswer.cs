using System;
using System.IO;
using System.Net;
using System.Text;

namespace WhatsAppApi.Operation.Messages
{
    //:Done
    public class WhatsAppJsonAnswer
    {
        private protected WhatsAppMessageProperties MessageProperty { get; set; }

        ~WhatsAppJsonAnswer() => MessageProperty = default;


        public WhatsAppJsonAnswer(string phoneOrChatId) =>
            MessageProperty = !phoneOrChatId.Contains("g.us") && !phoneOrChatId.Contains("-")
                ? new WhatsAppMessageProperties { Phone = long.Parse(phoneOrChatId) }
                : new WhatsAppMessageProperties { ChatId = phoneOrChatId };

        /// <summary>
        /// Параметры для сериализации в текстовое сообщение
        /// </summary>
        /// <param name="phoneOrChatId">Чат или номер телефона на который будет отправлено сообщение</param>
        /// <param name="body">Текст сообщения</param>
        public WhatsAppJsonAnswer(string phoneOrChatId, string body) : this(phoneOrChatId) => MessageProperty.Body = body;

        #region Сообщения с файлом
        /// <summary>
        /// Параметры для сериализации сообщения с файлом
        /// </summary>
        /// <param name="phoneOrChatId">Чат или номер телефона на который будет отправлено сообщение</param>
        /// <param type="Base64"
        ///        name="body">Ссылка на файл</param>
        /// <param name="filename">Имя файла</param>
        public WhatsAppJsonAnswer(string phoneOrChatId, string body, string filename) : this(phoneOrChatId, ConvertImageUrlToBase64(body)) => MessageProperty.FileName = filename;
        /// <summary>
        /// Параметры для сериализации сообщения с файлом и текстом
        /// </summary>
        /// <param name="phoneOrChatId">Чат или номер телефона на который будет отправлено сообщение</param>
        /// <param type="Base64"
        ///        name="body">Ссылка на файл</param>
        /// <param name="filename">Имя файла</param>
        /// <param name="caption">Текст под файлом</param>
        public WhatsAppJsonAnswer(string phoneOrChatId, string body, string filename, string caption) : this(phoneOrChatId, body, filename) => MessageProperty.Caption = caption;
        #endregion

        //public WhatsAppJsonAnswer(string phoneOrChatId, string body, string audio) : this(phoneOrChatId, body) => MessageProperty.Audio = audio;

        public WhatsAppJsonAnswer(string phoneOrChatId, string body, string previewBase64, string title, string description) : this(phoneOrChatId, body)
        {
            MessageProperty.PreviewBase64 = ConvertImageUrlToBase64(previewBase64);
            MessageProperty.Title = title;
            MessageProperty.Description = description;
        }

        public static explicit operator WhatsAppMessageProperties(WhatsAppJsonAnswer answer) => answer.MessageProperty;

        public static string ConvertImageUrlToBase64(string url)
        {
            StringBuilder _sb = new StringBuilder();

            Byte[] _byte = GetImage(url);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return "data:image/jpeg;base64," + _sb;
        }
        private static byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch
            {
                buf = null;
            }

            return (buf);
        }
    }
}