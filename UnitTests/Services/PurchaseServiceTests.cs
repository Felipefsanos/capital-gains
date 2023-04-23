using capital_gains.Entities;
using capital_gains.Services;

namespace UnitTests.Services
{
    public class PurchaseServiceTests
    {
        private readonly PurchaseService _service;
        private readonly BatchExecutionState _operationState;

        public PurchaseServiceTests()
        {
            _operationState = new BatchExecutionState();
            _service = new PurchaseService(_operationState);
        }

        [Fact]
        public void ShouldAddQuantityPurchased_WhenPurchaseOperation()
        {
            var operation = new FinancialMarketOperation
            {
                Quantity = 5,
                UnitCost = 15
            };

            _service.Purchase(operation);

            Assert.True(_operationState.CurrentQuantity == 5);
        }

        [Fact]
        public void ShouldCalculateWeightedAverage_WhenPurchaseOperation()
        {
            var operation = new FinancialMarketOperation
            {
                Quantity = 5,
                UnitCost = 15
            };

            _service.Purchase(operation);

            Assert.True(_operationState.WeightedAverage != 0);
        }

        [Fact]
        public void ShouldSetWeightedAverageValueEqualsUnitCost_WhenPurchaseOperationAndDontHaveAnyActionActual()
        {
            var operation = new FinancialMarketOperation
            {
                Quantity = 5,
                UnitCost = 45
            };

            _service.Purchase(operation);

            Assert.True(_operationState.WeightedAverage == 45);
        }
    }
}
