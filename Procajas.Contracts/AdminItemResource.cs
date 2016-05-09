using Newtonsoft.Json;

namespace Procajas.Contracts
{
    public class AdminItemResource
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
