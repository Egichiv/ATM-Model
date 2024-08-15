using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.Deposit;

public class DepositScenario : IScenario
{
    private readonly IAccountService _accountService;

    public DepositScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Deposit money to Account";

    public void Run()
    {
        double money = AnsiConsole.Ask<double>("How much money you want to deposit?");

        DepositResult result = _accountService.AddMoney(money);

        string message = result switch
        {
            DepositResult.Success => "Successful deposit money",
            DepositResult.NotAuthorized => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}