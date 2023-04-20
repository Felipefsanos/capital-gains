using capital_gains.Entities;

namespace capital_gains.Services
{
    public class CalculadorService
    {
        private decimal LucroAtual { get; set; }
        private decimal MediaPonderada { get; set; }
        private long QuantidadeTotalAcoesAtual { get; set; }

        public CalculadorService()
        {
            LucroAtual = decimal.Zero;
            MediaPonderada = decimal.Zero;
            QuantidadeTotalAcoesAtual = 0;
        }

        public void ProcessarCompraAcoes(OperacaoMercadoFinanceiro operacaoMercadoFinanceiro)
        {
            if(QuantidadeTotalAcoesAtual == 0)
                LucroAtual = decimal.Zero;

            CalcularMediaPonderada(operacaoMercadoFinanceiro.Quantity, operacaoMercadoFinanceiro.UnitCost);

            QuantidadeTotalAcoesAtual += operacaoMercadoFinanceiro.Quantity;
        }

        public void ProcessarVendaAcoes(OperacaoMercadoFinanceiro operacaoMercadoFinanceiro)
        {
            QuantidadeTotalAcoesAtual -= operacaoMercadoFinanceiro.Quantity;
        }

        public decimal CalcularImposto(OperacaoMercadoFinanceiro operacao)
        {
            var valorSobreOperacao = CalcularValorSobreOperacao(operacao);

            if (operacao.UnitCost <= MediaPonderada)
            {
                if (operacao.UnitCost < MediaPonderada)
                    LucroAtual += valorSobreOperacao;

                return 0.00m;
            }

            if (!operacao.PagaImposto())
                return 0.00m;

            return CalcularImpostoSobreLucroDeOperacao(valorSobreOperacao);
        }

        private void CalcularMediaPonderada(long quantidadeAcoesCompradas, decimal valorCompra)
        {
            var resultado = (QuantidadeTotalAcoesAtual * MediaPonderada) + (quantidadeAcoesCompradas * valorCompra);
            var totalAcoes = QuantidadeTotalAcoesAtual + quantidadeAcoesCompradas;

            MediaPonderada = Math.Round(resultado / totalAcoes, 2);
        }

        private decimal CalcularValorSobreOperacao(OperacaoMercadoFinanceiro operacao) => operacao.Quantity * (operacao.UnitCost - MediaPonderada);

        private decimal CalcularImpostoSobreLucroDeOperacao(decimal lucroOperacao)
        {
            decimal taxa = 0.00m;

            if (PrejuizoParaDeduzir())
            {
                LucroAtual += lucroOperacao;

                if (LucroAtual > 0)
                    taxa = CalcularTaxa(LucroAtual);
            }
            else
            {
                taxa = CalcularTaxa(lucroOperacao);

                LucroAtual += (lucroOperacao - taxa);
            }

            return taxa;
        }

        private static decimal CalcularTaxa(decimal lucroOperacao)
        {
            var result = 0.2M * lucroOperacao;
            return result;
        }

        private bool PrejuizoParaDeduzir() => LucroAtual < 0;
    }
}
