using Newtonsoft.Json;

namespace Procajas.Contracts
{
    public class IdResource
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
