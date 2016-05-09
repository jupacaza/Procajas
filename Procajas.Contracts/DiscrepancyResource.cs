using Newtonsoft.Json;
using System;

namespace Procajas.Contracts
{
    public class DiscrepancyResource
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// The material from which we now have a discrepancy between checkin and checkout.
        /// </summary>
        [JsonProperty("material")]
        public string Material { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("discrepancyDate")]
        public DateTime DiscrepancyDate { get; set; }

        /// <summary>
        /// In which process (e.g. IMP_Fuego) did this material generate a discrepancy.
        /// </summary>
        [JsonProperty("job")]
        public string Job { get; set; }
    }
}
