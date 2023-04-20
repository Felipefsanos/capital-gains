namespace capital_gains.Entities
{
    public class TaxaResponseModel
    {
        public decimal Tax { get; set; }

        public TaxaResponseModel(decimal tax) => Tax = Math.Round(tax, 2);
    }
}
