using Newtonsoft.Json;
using System;

namespace Procajas.Contracts
{
    public class FinishedProductResource
    {
        [JsonProperty("material")]
        public string Material { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("finishDate")]
        public DateTime FinishDate { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("invoiceNumber")]
        public string InvoiceNumber { get; set; }
    }
}
