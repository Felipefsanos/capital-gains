using capital_gains.Entities;

namespace capital_gains.Services.Abstractions
{
    public interface IPurchaseService
    {
        void Purchase(FinancialMarketOperation operation);
    }
}
