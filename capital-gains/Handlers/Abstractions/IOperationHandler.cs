using capital_gains.Entities;

namespace capital_gains.Handlers.Abstractions
{
    public interface IOperationHandler
    {
        void Handle(FinancialMarketOperation marketOperation);
    }
}
