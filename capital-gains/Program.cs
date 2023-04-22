using capital_gains.Entities;
using capital_gains.Handlers;
using capital_gains.Services;
using System.Text.Json;

string? input = Console.ReadLine();

while (input != null)
{
    var financyOperations = JsonSerializer.Deserialize<IEnumerable<FinancialMarketOperation>>(input);

    var outoputService = new OutputService();

    var handler = new OperationHandler(outoputService);

    foreach (var financyOperation in financyOperations!)
    {
        handler.Handle(financyOperation);
    }

    Console.WriteLine(outoputService.GetProgramOutput());

    input = Console.ReadLine();
}
