using System;

namespace WhatsAppApi.Operation.Helpers.Serialize
{
    //:Done
    public class SerializeMessage : IWhatsAppApi<string>
    {
        public SerializeMessage(string serialize, JsonError error)
        {
            Result = serialize;
            Error = error;
        }

        ~SerializeMessage()
        {
            Result = default;
            Error = default;
        }

        public string Result { get; set; }
        public JsonError Error { get; set; }
        public DateTime DateTime { get; set; }
        public override string ToString() => "(" + (Result ?? string.Empty) + $", {Error})";
    }
}