using Newtonsoft.Json;
using System;

namespace WhatsAppApi.Operation.Helpers.Serialize
{
    public interface ISerialize<T>
    {
        [JsonIgnore] T Serialize { get; set; }
        [JsonIgnore] DateTime DateTime { get; set; }
        [JsonIgnore] string TypeMessageOperation { get; }

    }
}
