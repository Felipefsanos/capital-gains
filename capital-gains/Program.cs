using capital_gains.Entities;
using capital_gains.Handlers;
using capital_gains.Services;
using System.Text.Json;

string? input = Console.ReadLine();

while (input != null)
{
    var financyOperations = JsonSerializer.Deserialize<IEnumerable<FinancialMarketOperation>>(input);

    var batchExecutionState = new BatchExecutionState();
    var outputService = new OutputService();
    var saleService = new SaleService(batchExecutionState);
    var purchaseService = new PurchaseService(batchExecutionState);

    var handler = new OperationHandler(outputService, saleService, purchaseService);

    foreach (var financyOperation in financyOperations!)
    {
        handler.Handle(financyOperation);
    }

    Console.WriteLine(outputService.GetProgramOutput());

    input = Console.ReadLine();
}
