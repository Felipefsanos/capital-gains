using capital_gains.Entities;
using capital_gains.Services;
using System.Text.Json;

string? input = Console.ReadLine();

while (input != null)
{
    var operacoesFinanceiras = JsonSerializer.Deserialize<IEnumerable<OperacaoMercadoFinanceiro>>(input);

    var taxas = new List<TaxaResponseModel>();
    var calculadorService = new CalculadorService();

    foreach (var operacaoFinanceira in operacoesFinanceiras!)
    {
        switch (operacaoFinanceira.Operation)
        {
            case "buy":
                calculadorService.ProcessarCompraAcoes(operacaoFinanceira);
                taxas.Add(new TaxaResponseModel(0.00m));
                break;
            case "sell":
                calculadorService.ProcessarVendaAcoes(operacaoFinanceira);
                var taxa = calculadorService.CalcularImposto(operacaoFinanceira);
                taxas.Add(new TaxaResponseModel(taxa));
                break;
            default:
                throw new InvalidOperationException("Operação inválido ou não existente");
        }
    }

    Console.WriteLine(JsonSerializer.Serialize(taxas, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));

    input = Console.ReadLine();
}
