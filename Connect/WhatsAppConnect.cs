using System;
using WhatsAppApi.Operation.Messages;

namespace WhatsAppApi.Connect
{
    //:Done
    public class WhatsAppConnect
    {
        private static string _appLinkForRead;
        private static string _appLinkForSend;
        private const string CORE = "chat-api.com/";


        public string Protocol { get; set; } = "https://";
        public string Server { get; set; }
        public string Instance { get; set; }
        public string Token { get; set; }
        public WhatsAppConnectOperation ConnectOperation { get; set; }
        public string ChatId { get; set; }
        public string TypeOperationSend { get; set; } = "sendMessage";
        public string TypeOperationRead { get; set; } = "messages";

        public string ApiLinkForSend
        {
            get => _appLinkForSend = $"{Protocol}{Server}.{CORE}instance{Instance}/{TypeOperationSend}?token={Token}";
            set => _appLinkForSend = value;
        }
        public string ApiLinkForRead
        {
            get => _appLinkForRead = $"{Protocol}api.{CORE}instance{Instance}/{TypeOperationRead}?token={Token}";
            set => _appLinkForRead = value;
        }

        public WhatsAppConnect(string server, string instance, string token)
        {
            Server = server;
            Instance = instance;
            Token = token;
        }
        public WhatsAppConnect(string server, int instance, string token) : this(server, instance.ToString(), token) { }

        public string AddTypeOperation(string typeOperation, WhatsAppConnectOperation connectOperation)
        {
            switch (connectOperation)
            {
                case WhatsAppConnectOperation.Send:
                    ConnectOperation = connectOperation;
                    TypeOperationSend = typeOperation;
                    return ApiLinkForSend;

                case WhatsAppConnectOperation.Read:
                    ConnectOperation = connectOperation;
                    TypeOperationRead = typeOperation;
                    return ApiLinkForRead;
                default:
                    throw new ArgumentOutOfRangeException(nameof(connectOperation), connectOperation, null);
            }
        }
        public string AddChatId(string phoneOrChatId)
        {
            var vChatId = (WhatsAppMessageProperties)new WhatsAppJsonAnswer(phoneOrChatId);
            if (vChatId.ChatId is null) return string.Empty;
            ChatId = vChatId.ChatId;
            return $"&chatId={ChatId}";
        }

        public string AddTypeOperationAndChatId(string typeOperation, WhatsAppConnectOperation connectOperation, string phoneOrChatId) =>
            AddTypeOperation(typeOperation, connectOperation) + AddChatId(phoneOrChatId);

    }
}