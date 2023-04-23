using capital_gains.Entities;
using capital_gains.Handlers.Abstractions;
using capital_gains.Services.Abstractions;

namespace capital_gains.Handlers
{
    public class OperationHandler : IOperationHandler
    {
        private readonly IPurchaseService _purchaseService;
        private readonly ISaleService _saleService;
        private readonly IOutputService _outputService;

        public OperationHandler(IOutputService outputService, ISaleService saleService, IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
            _saleService = saleService;
            _outputService = outputService;
        }

        public void Handle(FinancialMarketOperation marketOperation)
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
