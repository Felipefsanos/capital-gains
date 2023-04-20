using System.Text.Json.Serialization;

namespace capital_gains.Entities
{
    public class OperacaoMercadoFinanceiro
    {
        [JsonPropertyName("operation")]
        public string Operation { get; set; }
        [JsonPropertyName("unit-cost")]
        public decimal UnitCost { get; set; }
        [JsonPropertyName("quantity")]
        public long Quantity { get; set; }

        public bool PagaImposto() => UnitCost * Quantity > 20000;
    }
}
