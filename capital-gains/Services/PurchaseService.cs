using capital_gains.Entities;

namespace capital_gains.Services
{
    public class PurchaseService
    {
        private readonly ExecutionState _executionState;
        public PurchaseService(ExecutionState executionState) => _executionState = executionState;

        public void Purchase(FinancialMarketOperation operation)
        {
            _executionState.WeightedAverage = GetWeightedAverage(operation.Quantity, operation.UnitCost);

            _executionState.Purchase(operation.Quantity);
        }

        private decimal GetWeightedAverage(long quantidadeAcoesCompradas, decimal valorCompra)
        {
            var resultado = (_executionState.CurrentQuantity * _executionState.WeightedAverage) + (quantidadeAcoesCompradas * valorCompra);
            var totalAcoes = _executionState.CurrentQuantity + quantidadeAcoesCompradas;

            return Math.Round(resultado / totalAcoes, 2);
        }
    }
}
