using System.Text.Json.Serialization;

namespace capital_gains.Entities
{
    public class FinancialMarketOperation
    {
        [JsonPropertyName("operation")]
        public string? Operation { get; set; }
        [JsonPropertyName("unit-cost")]
        public decimal UnitCost { get; set; }
        [JsonPropertyName("quantity")]
        public long Quantity { get; set; }

        public bool PayTax() => UnitCost * Quantity > 20000;
    }
}
