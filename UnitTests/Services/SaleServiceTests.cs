using capital_gains.Entities;
using capital_gains.Services;

namespace UnitTests.Services
{
    public class SaleServiceTests
    {
        private readonly SaleService _service;
        private readonly BatchExecutionState _operationState;

        public SaleServiceTests()
        {
            _operationState = new BatchExecutionState();
            _service = new SaleService(_operationState);
        }

        [Fact]
        public void ShouldReturnTaxZero_WhenOperationProfitIsZero()
        {
            _operationState.Purchase(1);
            _operationState.WeightedAverage = 15.00m;

            var operation = new FinancialMarketOperation
            {
                Quantity = 1,
                UnitCost = 15.00m
            };

            var tax = _service.Sell(operation);

            Assert.True(tax == 0);
        }

        [Fact]
        public void ShouldReturnTaxZero_WhenOperationProfitIsLessThan20k()
        {
            _operationState.Purchase(10);
            _operationState.WeightedAverage = 15.00m;

            var operation = new FinancialMarketOperation
            {
                Quantity = 10,
                UnitCost = 20.00m
            };

            var tax = _service.Sell(operation);

            Assert.True(tax == 0);
        }

        [Fact]
        public void ShouldReturnTaxZero_WhenHasLoss()
        {
            _operationState.SetProfit(-2000000);
            _operationState.Purchase(10000);
            _operationState.WeightedAverage = 15.00m;

            var operation = new FinancialMarketOperation
            {
                Quantity = 10000,
                UnitCost = 50.00m
            };

            var tax = _service.Sell(operation);

            Assert.True(tax == 0);
        }

        [Fact]
        public void ShouldReturnTax_WhenHasProfitMoreThan20k()
        {
            _operationState.Purchase(10000);
            _operationState.WeightedAverage = 15.00m;

            var operation = new FinancialMarketOperation
            {
                Quantity = 10000,
                UnitCost = 50.00m
            };

            var tax = _service.Sell(operation);

            Assert.True(tax > 0);
        }
    }
}
