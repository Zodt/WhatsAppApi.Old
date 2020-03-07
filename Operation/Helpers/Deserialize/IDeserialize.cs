using Newtonsoft.Json;
using System;

namespace WhatsAppApi.Operation.Helpers.Deserialize
{
    public interface IDeserialize<T> where T : class
    {
        [JsonIgnore] IWhatsAppApi<T> Deserialize { get; set; }
        [JsonIgnore] DateTime DateTime { get; set; }

    }
}