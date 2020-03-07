using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using WhatsAppApi.Connect;
using WhatsAppApi.Operation.Helpers;
using WhatsAppApi.Operation.Helpers.Deserialize;
using WhatsAppApi.Operation.Helpers.Serialize;
using WhatsAppApi.Operation.Instances;
using WhatsAppApi.Operation.Messages;
using xNet;
using static System.Console;
using static System.Text.Encoding;

namespace WhatsAppApi.Operation
{
    public class WhatsAppOperation
    {
        public InstanceOperation InstanceOperation { set; get; }
        public MessageOperation MessageOperation { get; set; }
        protected GroupsOperation GroupsOperation { get; set; }
        protected QueuesOperation QueuesOperation { get; set; }
        protected BannedOperation BannedOperation { get; set; }

        public WhatsAppOperation(WhatsAppConnect connect)
        {
            InstanceOperation = new InstanceOperation(connect);
            MessageOperation = new MessageOperation(connect);
            GroupsOperation = new GroupsOperation(connect);
            QueuesOperation = new QueuesOperation(connect);
            BannedOperation = new BannedOperation(connect);
        }

    }

    public class InstanceOperation : HelperOperation
    {
        public WhatsAppInstanceOperation WhatsAppInstanceOperation { get; set; }
        public bool IsAuthenticated => GetStatus().AccountStatus.Equals("authenticated");


        ~InstanceOperation() { Connect = null; WhatsAppInstanceOperation = null; }
        public InstanceOperation(WhatsAppConnect connect) => Connect = connect;



        //:ToDo
        public WhatsAppInstanceOperation GetStatus()
        {
            using (var webClient = new WebClient())
            {
                var deserialize = new WhatsAppInstanceOperation().Deserialize(
                    EncodingMessage(
                        webClient.DownloadString(
                            Connect.AddTypeOperation("status", WhatsAppConnectOperation.Read))));

                if (!(deserialize?.Error is null) && (bool)deserialize.Error?.IsError) return new WhatsAppInstanceOperation { Deserialize = { Result = null, Error = { CodeOfError = 1, IsError = true, TextError = "" } } };
                if (deserialize != null) return deserialize.Result;
            }

            return new WhatsAppInstanceOperation { Deserialize = { Result = null, Error = { CodeOfError = 1, IsError = true, TextError = "" } } };
        }
    }

    //:Done
    public class MessageOperation : HelperOperation
    {
        ~MessageOperation() { Connect = null; }
        public MessageOperation(WhatsAppConnect connect) => Connect = connect;



        public WhatsAppMessage ReadMessage(string phoneOrChatId)
        {
            var deserialize =
                Get<WhatsAppMessage>(Connect.AddTypeOperationAndChatId("messages", WhatsAppConnectOperation.Read,
                    phoneOrChatId)).Deserialize.Result;

            return deserialize;
        }

        public DeserializeMessage<WhatsAppApiAnswers> SendMessage<T>(ISerialize<T> completedFields)
        {
            Connect.AddTypeOperation(completedFields.TypeMessageOperation, WhatsAppConnectOperation.Send);
            var serialize = completedFields.Serialize();

            if (!(serialize.Error is null) && (bool)serialize.Error?.IsError)
            {
                WriteLine(serialize.Error.TextError);
                return new DeserializeMessage<WhatsAppApiAnswers>(null, serialize.Error);
            }

            var message =
                new WhatsAppApiAnswers().Deserialize(
                    Post(Connect.ApiLinkForSend, serialize.Result));


            return (DeserializeMessage<WhatsAppApiAnswers>)message;
        }

        public async Task<DeserializeMessage<WhatsAppApiAnswers>> SendMessageAsync<T>(ISerialize<T> completedFields) => 
            await Task.Factory.StartNew(() => SendMessage(completedFields));
    }

    public class GroupsOperation : HelperOperation
    {
        ~GroupsOperation() { Connect = null; }
        public GroupsOperation(WhatsAppConnect connect) => Connect = connect;
    }

    public class BannedOperation : HelperOperation
    {
        ~BannedOperation() { Connect = null; }
        public BannedOperation(WhatsAppConnect connect) => Connect = connect;
    }

    public class QueuesOperation : HelperOperation
    {
        ~QueuesOperation() { Connect = null; }
        public QueuesOperation(WhatsAppConnect connect) => Connect = connect;
    }

    public abstract class HelperOperation
    {
        public static WhatsAppConnect Connect { get; set; }

        private protected static string Post(string connect, string jsonMessage)
        {
            string response;
            using (var request = new HttpRequest())
            {
                request.AddHeader("Accept", "application/json"); request.AddHeader("Content-Type:", "application/json");
                response = request.Post(connect, jsonMessage, "application/json").ToString();
            }

            return response;
        }
        private protected static T Get<T>(string connect) where T : class, IDeserialize<T>, new()
        {
            using (var webClient = new WebClient())
            {
                IWhatsAppApi<T> deserialize = new T().Deserialize(EncodingMessage(webClient.DownloadString(connect)));
                if (deserialize?.Error is null || !(bool)deserialize.Error?.IsError)
                    return deserialize?.Result ?? new T
                    {
                        Deserialize = new DeserializeMessage<T>
                        {
                            Result = null,
                            Error = new JsonError
                            {
                                IsError = true,
                                CodeOfError = 7,
                                TextError = "Ошибка при десериализации"
                            }
                        }
                    };

                return new T()
                {
                    Deserialize = new DeserializeMessage<T>
                    {
                        Result = null,
                        Error = new JsonError
                        {
                            IsError = true,
                            CodeOfError = 6,
                            TextError = "Ошибка при чтении"
                        }
                    }
                };
            }
        }

        private protected static string EncodingMessage(string message) =>
            GetEncoding("Windows-1251")
                .GetString(
                    Convert(
                        GetEncoding("UTF-8"),
                        GetEncoding("Windows-1251"),
                        GetEncoding("Windows-1251").GetBytes(message)));

    }
}