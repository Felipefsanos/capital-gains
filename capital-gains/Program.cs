using capital_gains.Entities;
using capital_gains.Services;
using System.Text.Json;

Console.WriteLine("Entre com as operações ou digite 'EXIT' para sair.");
string? input = Console.ReadLine();

while (input?.ToLower() != "exit")
{
    var operacoesFinanceiras = JsonSerializer.Deserialize<IEnumerable<OperacaoMercadoFinanceiro>>(input!);

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

    Console.WriteLine("Entre com as operações ou digite 'EXIT' para sair.");
    input = Console.ReadLine();
}
