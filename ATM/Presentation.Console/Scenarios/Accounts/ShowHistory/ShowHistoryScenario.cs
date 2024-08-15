using Application.Contracts.Accounts;
using Application.Models.Histories;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.ShowHistory;

public class ShowHistoryScenario : IScenario
{
    private readonly IAccountService _accountService;

    public ShowHistoryScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Show Account history";

    public void Run()
    {
        IEnumerable<History> result = _accountService.ShowHistory().ToList();

        if (result.Any())
        {
            foreach (History history in result)
            {
                switch (history.Operation)
                {
                    case Operation.deposit:
                        AnsiConsole.WriteLine($"Added {history.Money} dollars");
                        break;
                    case Operation.withdraw:
                        AnsiConsole.WriteLine($"Withdraw {history.Money} dollars");
                        break;
                    case Operation.transfer:
                        AnsiConsole.WriteLine($"Transfered {history.Money} dollars");
                        break;
                    case Operation.getTransfer:
                        AnsiConsole.WriteLine($"Got {history.Money} dollars via Transfer");
                        break;
                }
            }
        }
        else
        {
            AnsiConsole.WriteLine("No history for current user");
        }

        AnsiConsole.Ask<string>("Ok");
    }
}