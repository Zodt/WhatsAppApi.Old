using System;
using System.Threading.Tasks;

namespace WhatsAppApi.Operation.Helpers.Deserialize
{
    //:Done
    public class DeserializeMessage<T> : IWhatsAppApi<T> where T : class, IDeserialize<T>
    {
        public DeserializeMessage(T result, JsonError error)
        {
            Result = result;
            Error = error;
        }

        public DeserializeMessage() { }

        ~DeserializeMessage()
        {
            Result = default;
            Error = default;
        }

        public T Result { get; set; }
        public JsonError Error { get; set; }
        public DateTime DateTime { get; set; }
        public override string ToString() => $"({Result}, {Error})";

        public static explicit operator DeserializeMessage<T>(T answers) => answers.Deserialize as DeserializeMessage<T>;
        public static implicit operator T(DeserializeMessage<T> answers) => answers.Result;
        public static explicit operator DeserializeMessage<T>(Task<DeserializeMessage<T>> task) => task.Result;
        public static explicit operator DeserializeMessage<T>(Task<IWhatsAppApi<T>> task) => (DeserializeMessage<T>)task.Result;



    }
}