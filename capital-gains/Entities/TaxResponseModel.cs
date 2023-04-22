namespace capital_gains.Entities
{
    public class TaxResponseModel
    {
        public decimal Tax { get; set; }

        public TaxResponseModel(decimal tax) => Tax = Math.Round(tax, 2);
    }
}
