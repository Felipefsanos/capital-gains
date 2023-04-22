using capital_gains.Entities;
using capital_gains.Services;

namespace capital_gains.Handlers
{
    public class OperationHandler
    {
        private readonly PurchaseService _purchaseService;
        private readonly SaleService _saleService;
        private readonly OutputService _outputService;

        public OperationHandler(OutputService outputService)
        {
            var executionState = new ExecutionState();
            _purchaseService = new PurchaseService(executionState);
            _saleService = new SaleService(executionState);
            _outputService = outputService;
        }

        public void Handle(FinancyMarketOperation marketOperation)
        {
            switch (marketOperation.Operation)
            {
                case "buy":
                    _purchaseService.Purchase(marketOperation);
                    _outputService.AddPurhaseTax();
                    break;
                case "sell":
                    var tax = _saleService.Sell(marketOperation);
                    _outputService.AddSellTax(tax);
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation.");
            }
        }
    }
}
