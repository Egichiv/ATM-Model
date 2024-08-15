using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Accounts;
using Application.Contracts.Admins;

namespace Presentation.Console.Scenarios.Accounts.Logout;

public class LogoutScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ICurrentAdminService _currentAdminService;

    public LogoutScenarioProvider(
        IAccountService accountService,
        ICurrentAccountService currentAccountService,
        ICurrentAdminService currentAdminService)
    {
        _accountService = accountService;
        _currentAccountService = currentAccountService;
        _currentAdminService = currentAdminService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountService.Account is null)
        {
            scenario = null;
            return false;
        }

        scenario = new LogoutScenario(_accountService);
        return true;
    }
}