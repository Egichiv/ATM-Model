using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Accounts;
using Application.Contracts.Admins;

namespace Presentation.Console.Scenarios.Admins.Logout;

public class AdminLogoutScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly ICurrentAdminService _currentAdminService;
    private readonly ICurrentAccountService _currentAccountService;

    public AdminLogoutScenarioProvider(
        IAdminService adminService,
        ICurrentAccountService currentAccountService,
        ICurrentAdminService currentAdminService)
    {
        _adminService = adminService;
        _currentAccountService = currentAccountService;
        _currentAdminService = currentAdminService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdminService.Admin is null)
        {
            scenario = null;
            return false;
        }

        scenario = new AdminLogoutScenario(_adminService);
        return true;
    }
}