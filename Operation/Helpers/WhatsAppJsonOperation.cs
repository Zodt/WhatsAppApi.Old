using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Threading.Tasks;
using WhatsAppApi.Operation.Helpers.Deserialize;
using WhatsAppApi.Operation.Helpers.Serialize;

namespace WhatsAppApi.Operation.Helpers
{
    //:Done
    /// <summary>
    /// Класс сериализирующий JSON
    /// </summary>
    public static class WhatsAppJsonOperation
    {
        /// <summary>
        /// Описание ошибки, если есть
        /// </summary>
        public static JsonError Error { get; set; }

        /// <summary>
        /// Сериализация обработанных данных из класса WhatsAppJsonProperties
        /// </summary>
        public static SerializeMessage Serialize<T>(this ISerialize<T> self)
        {
            var serialize = JsonConvert.SerializeObject((object)self, (JsonSerializerSettings)Settings);
            if (!(serialize is null)) return new SerializeMessage(serialize, Error);
            Error = new JsonError { CodeOfError = 2, IsError = true, TextError = "WhatsAppJsonOperation is fail" };
            return new SerializeMessage(string.Empty, Error);
        }

        /// <summary>
        /// Метод десериализации и проверок на ошибки
        /// </summary>
        /// <param name="self"></param>
        /// <param name="json">Входная JSON-строка</param>
        /// <returns></returns>
        public static IWhatsAppApi<T> Deserialize<T>(this T self, string json) where T : class, IDeserialize<T>, new()
        {
            if (self is null)
            {
                Error = new JsonError { IsError = true, CodeOfError = 4, TextError = "WhatsAppMessage is null" };
                var result = new DeserializeMessage<T> { Result = new T(), Error = Error };
                return result;
            }

            if (string.IsNullOrEmpty(json))
            {
                self.Deserialize.Error = new JsonError { IsError = true, CodeOfError = 0, TextError = "Json file is empty" };
                return self.Deserialize;
            }

            try
            {
                self.Deserialize.Result = JsonConvert.DeserializeObject<T>(json, Settings);
                self.Deserialize.Error = Error;
            }
            catch (JsonException)
            {
                self.Deserialize.Result = (T)self;
                self.Deserialize.Error = new JsonError { IsError = true, CodeOfError = 1, TextError = "Json file is broken" };
            }
            self.Deserialize.Result.DateTime = DateTime.Now;
            return self.Deserialize;
        }

        public static async Task<IWhatsAppApi<T>> Deserialize<T>(this IDeserialize<T> self, Task<string> json) where T : class, IDeserialize<T>, new()
        {
            if (self is null)
            {
                Error = new JsonError { IsError = true, CodeOfError = 4, TextError = "WhatsAppMessage is null" };
                var result = new DeserializeMessage<T> { Result = new T(), Error = Error };
                return result;
            }

            var jsonMessage = await json;

            if (string.IsNullOrEmpty(jsonMessage))
            {
                self.Deserialize.Error = new JsonError { IsError = true, CodeOfError = 0, TextError = "Json file is empty" };
                return self.Deserialize;
            }

            try
            {
                self.Deserialize.Result = JsonConvert.DeserializeObject<T>(jsonMessage, Settings);
                self.Deserialize.Error = Error;
            }
            catch (JsonException)
            {
                self.Deserialize.Result = (T)self;
                self.Deserialize.Error = new JsonError { IsError = true, CodeOfError = 1, TextError = "Json file is broken" };
            }
            self.Deserialize.Result.DateTime = DateTime.Now;
            return self.Deserialize;
        }



        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = { new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal } },
        };

    }
}