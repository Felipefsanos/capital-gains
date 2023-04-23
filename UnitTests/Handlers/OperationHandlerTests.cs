using capital_gains.Entities;
using capital_gains.Handlers;
using capital_gains.Services.Abstractions;
using Moq;

namespace UnitTests.Handlers
{
    public class OperationHandlerTests
    {
        private readonly Mock<IPurchaseService> _purchaseService;
        private readonly Mock<ISaleService> _saleService;
        private readonly Mock<IOutputService> _outputService;
        private readonly OperationHandler _handler;

        public OperationHandlerTests()
        {
            _purchaseService = new Mock<IPurchaseService>();
            _saleService = new Mock<ISaleService>();
            _outputService = new Mock<IOutputService>();

            _handler = new OperationHandler(_outputService.Object, _saleService.Object, _purchaseService.Object);
        }

        [Fact]
        public void ShouldCallPurchaseAndAddPurchaseTax_WhenIsBuyOperation()
        {
            var operation = new FinancialMarketOperation
            {
                Operation = "buy"
            };

            _handler.Handle(operation);

            _purchaseService.Verify(p => p.Purchase(operation), Times.Once);
            _outputService.Verify(o => o.AddPurhaseTax(), Times.Once);
        }

        [Fact]
        public void ShouldCallSellAndAddSellTax_WhenIsSellOperation()
        {
            var operation = new FinancialMarketOperation
            {
                Operation = "sell"
            };

            _handler.Handle(operation);

            _saleService.Verify(s => s.Sell(operation), Times.Once);
            _outputService.Verify(o => o.AddSellTax(It.IsAny<decimal>()), Times.Once);
        }

        [Fact]
        public void ShouldThowException_WhenOperationInvalid()
        {
            var operation = new FinancialMarketOperation
            {
                Operation = "test"
            };

            Assert.Throws<InvalidOperationException>(() => _handler.Handle(operation));
        }
    }
}
