using Newtonsoft.Json;
using System;

namespace Procajas.Contracts
{
    public class WarehouseResource
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("material")]
        public string Material { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("dateOfInsertion")]
        public DateTime DateOfInsertion { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("invoiceNumber")]
        public string InvoiceNumber { get; set; }
    }
}
