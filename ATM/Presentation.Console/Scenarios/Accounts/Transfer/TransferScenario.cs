using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.Transfer;

public class TransferScenario : IScenario
{
    private readonly IAccountService _accountService;

    public TransferScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Transfer money to another Account";

    public void Run()
    {
        double money = AnsiConsole.Ask<double>("How much money you want to transfer?");
        string username = AnsiConsole.Ask<string>("Enter the recipient's username");

        TransferResult result = _accountService.TransferMoney(money, username);

        string message = result switch
        {
            TransferResult.Success => "Successful money transfer",
            TransferResult.NotEnoughMoney => "Not enough money on account",
            TransferResult.NotAuthorized => "Current user not found",
            TransferResult.UserNotFound => "User-receiver not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}