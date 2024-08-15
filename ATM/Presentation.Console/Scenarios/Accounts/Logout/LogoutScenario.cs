using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.Logout;

public class LogoutScenario : IScenario
{
    private readonly IAccountService _accountService;

    public LogoutScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Logout Account";
    public void Run()
    {
        LogoutResult result = _accountService.Logout();

        string message = result switch
        {
            LogoutResult.Success => "Successful logout",
            LogoutResult.Failure => "Failure logout",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}