using capital_gains.Entities;
using capital_gains.Services.Abstractions;

namespace capital_gains.Services
{
    public class SaleService : ISaleService
    {
        private readonly BatchExecutionState _batchOperationState;
        public SaleService(BatchExecutionState batchOperationState) => _batchOperationState = batchOperationState;
        public decimal Sell(FinancialMarketOperation operation)
        {
            var operationProfit = GetProfitFromOperation(operation);
            var profit = GetProfitToTax(operationProfit);
            _batchOperationState.SetProfit(operationProfit);

            var tax = GetTax(operation, profit);
            _batchOperationState.DeductTax(tax);

            _batchOperationState.Sell(operation.Quantity);
            return tax;
        }

        private decimal GetTax(FinancialMarketOperation operation, decimal profit)
        {
            if (operation.UnitCost <= _batchOperationState.WeightedAverage)
                return 0.00m;

            if (!operation.ShouldPayTax())
                return 0.00m;

            if (_batchOperationState.HasLoss())
                return 0.00m;

            return profit * 0.2M;
        }

        private decimal GetProfitToTax(decimal operationProfit) 
        {
            if (_batchOperationState.HasLoss())
                return _batchOperationState.ActualProfit + operationProfit;

            return operationProfit;
        }

        private decimal GetProfitFromOperation(FinancialMarketOperation operation) => operation.Quantity * (operation.UnitCost - _batchOperationState.WeightedAverage);
    }
}
