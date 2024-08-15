using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.Withdraw;

public class WithdrawScenario : IScenario
{
    private readonly IAccountService _accountService;

    public WithdrawScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Withdraw money from Account";

    public void Run()
    {
        double money = AnsiConsole.Ask<double>("How much money you want to withdraw?");

        WithdrawResult result = _accountService.WithdrawMoney(money);

        string message = result switch
        {
            WithdrawResult.Success => "Successful withdrawing money",
            WithdrawResult.NotEnoughMoney => "Not enough money on account",
            WithdrawResult.NotAuthorized => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}