using Newtonsoft.Json;
using System;

namespace WhatsAppApi.Operation.Helpers
{
    public interface IWhatsAppApi<T> where T : class
    {
        T Result { get; set; }
        JsonError Error { get; set; }
        [JsonIgnore] DateTime DateTime { get; set; }
    }
}