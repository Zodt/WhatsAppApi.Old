using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WhatsAppApi.Operation.Helpers;
using WhatsAppApi.Operation.Helpers.Deserialize;
using WhatsAppApi.Operation.Helpers.Serialize;

namespace WhatsAppApi.Operation.Messages.OperationHelpers
{
    public class ChatOrPhone : ISerialize<ChatOrPhone>, IDeserialize<ChatOrPhone>
    {
        [JsonProperty("phone", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        private long _phone;

        [JsonProperty("chatId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        private string _chatId;


        [JsonIgnore]
        public string PhoneOrChatId
        {
            get => string.IsNullOrEmpty(_chatId) ? _phone.ToString() : _chatId;
            set { if (!value.Contains("g.us") && !value.Contains("-")) _phone = GetPhoneCorrect(value); else _chatId = value; }
        }
        [JsonIgnore] public DateTime DateTime { get; set; }
        [JsonIgnore] public ChatOrPhone Serialize { get; set; }
        [JsonIgnore] public virtual string TypeMessageOperation { get; }
        [JsonIgnore] public IWhatsAppApi<ChatOrPhone> Deserialize { get; set; }


        protected ChatOrPhone() => TypeMessageOperation = GetTypeMessageOperation();

        private string GetTypeMessageOperation()
        {
            var name = this.GetType().Name;
            return string.Concat(
                name[0].ToString().ToLower(),
                name.Substring(1)
            );
        }


        /// <summary>
        /// Приведение номера телефона к стандартному виду типа 71234567890
        /// </summary>
        /// <param name="phone">номер телефона</param>
        /// <returns>номер телефона в формате WhatsAppApi</returns>
        public long GetPhoneCorrect(string phone)
        {
            phone = new Regex(@"(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?").Match(phone).Value;
            phone = new Regex(@"\d+").Matches(phone).Cast<Match>().Aggregate(string.Empty, (current, match) => current + match.Value);

            if (
                  phone.Length.Equals(12) & 
                  (
                    GetRange(phone, 2).Equals("77") ||
                    GetRange(phone, 2).Equals("87") ||
                    GetRange(phone, 2).Equals("78")
                  )
                ) phone = phone.Substring(2, phone.Length - 1);

            if (phone.Length.Equals(10))
                phone = string.Concat("7", phone);
            if (GetRange(phone,1).Equals("8")) phone = string.Concat("7", phone.Substring(1, phone.Length - 1));

            return long.TryParse(phone, out var phoneResult) || !phone.Length.Equals(11) ? phoneResult : default;
        }
        private static string GetRange(string text, int range, int startIndex = 0)
        {
            var dynamicPattern = @"(?>.{" + startIndex + @"})(?<res>\d{" + range + "})(?>.*)";
            return new Regex(@dynamicPattern).Match(text).Groups["res"].Value;
        }


        public static explicit operator ChatOrPhone(string x) => new ChatOrPhone{PhoneOrChatId = x};
    }
}