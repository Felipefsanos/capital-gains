namespace capital_gains.Services.Abstractions
{
    public interface IOutputService
    {
        void AddPurhaseTax();
        void AddSellTax(decimal taxValue);
        string GetProgramOutput();
    }
}
