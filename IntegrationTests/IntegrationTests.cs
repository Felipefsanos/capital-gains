using capital_gains.Entities;
using capital_gains.Handlers;
using capital_gains.Handlers.Abstractions;
using capital_gains.Services;
using capital_gains.Services.Abstractions;

namespace IntegrationTests
{
    public class IntegrationTests
    {
        private readonly IOperationHandler _handler;
        private readonly OutputService _outputService;

        public IntegrationTests()
        {
            _outputService = new OutputService();
            var batchExecutionState = new BatchExecutionState();
            _handler = new OperationHandler(_outputService, new SaleService(batchExecutionState), new PurchaseService(batchExecutionState));
        }

        [Fact]
        public void PurchaseIntegrationTest()
        {
            var operation = new FinancialMarketOperation
            {
                Operation = "buy",
                Quantity = 1,
                UnitCost = 10.00m
            };

            _handler.Handle(operation);

            Assert.Contains(_outputService.Taxes, t => t.Tax == 0.00m);
        }

        [Fact]
        public void SaleIntegrationTest_WithoutTax()
        {
            var operation = new FinancialMarketOperation
            {
                Operation = "sell",
                Quantity = 1,
                UnitCost = 10.00m
            };

            _handler.Handle(operation);

            Assert.Contains(_outputService.Taxes, t => t.Tax == 0.00m);
        }

        [Fact]
        public void SaleIntegrationTest_WithTax()
        {
            var operation = new FinancialMarketOperation
            {
                Operation = "sell",
                Quantity = 5000,
                UnitCost = 30.00m
            };

            var tax = (operation.Quantity * operation.UnitCost) * 0.2m;

            _handler.Handle(operation);

            Assert.Contains(_outputService.Taxes, t => t.Tax == tax);
        }
    }
}
