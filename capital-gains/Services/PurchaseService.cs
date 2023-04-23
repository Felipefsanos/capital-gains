using capital_gains.Entities;
using capital_gains.Services.Abstractions;

namespace capital_gains.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly BatchExecutionState _executionState;
        public PurchaseService(BatchExecutionState executionState) => _executionState = executionState;

        public void Purchase(FinancialMarketOperation operation)
        {
            _executionState.WeightedAverage = GetWeightedAverage(operation.Quantity, operation.UnitCost);

            _executionState.Purchase(operation.Quantity);
        }

        private decimal GetWeightedAverage(long numberPurchased, decimal purchaseValue)
        {
            var resultado = (_executionState.CurrentQuantity * _executionState.WeightedAverage) + (numberPurchased * purchaseValue);
            var totalAcoes = _executionState.CurrentQuantity + numberPurchased;

            return Math.Round(resultado / totalAcoes, 2);
        }
    }
}
