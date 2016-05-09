using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Procajas.Contracts
{
    public class ProcessResource
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("material")]
        public string Material { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("processCheckinDate")]
        public DateTime ProcessCheckinDate { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("details")]
        public IDictionary<object,object> Details { get; set; }
    }
}
