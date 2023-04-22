using capital_gains.Entities;

namespace capital_gains.Services
{
    public class SaleService
    {
        private readonly ExecutionState _executionState;
        public SaleService(ExecutionState executionState) => _executionState = executionState;
        public decimal Sell(FinancyMarketOperation operation)
        {
            var tax = GetTax(operation);

            _executionState.SellActions(operation.Quantity);

            return tax;
        }

        private decimal GetTax(FinancyMarketOperation operation)
        {
            if (operation.UnitCost == _executionState.WeightedAverage)
            {
                return 0.00m;
            }

            var profitOperation = GetProfitOperation(operation);

            if (operation.UnitCost < _executionState.WeightedAverage)
            {
                _executionState.SetProfit(profitOperation);
                return 0.00m;
            }

            if (!operation.PayTax())
            {
                return 0.00m;
            }

            return GetOperationProfitTax(profitOperation);
        }

        private decimal GetOperationProfitTax(decimal profitOperation)
        {
            decimal tax = 0.00m;

            if (_executionState.HasLoss())
            {
                _executionState.SetProfit(profitOperation);

                if (_executionState.ActualProfit > 0)
                    tax = ApplyTax(_executionState.ActualProfit);
            }
            else
            {
                tax = ApplyTax(profitOperation);

                _executionState.SetProfit(profitOperation - tax);
            }

            return tax;
        }

        private decimal GetProfitOperation(FinancyMarketOperation operation) => operation.Quantity * (operation.UnitCost - _executionState.WeightedAverage);

        private static decimal ApplyTax(decimal profitOperation) => 0.2M * profitOperation;
    }
}
