using Application.Contracts.Admins;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Admins.Logout;

public class AdminLogoutScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AdminLogoutScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Logout Account";
    public void Run()
    {
        AdminLogoutResult result = _adminService.Logout();

        string message = result switch
        {
            AdminLogoutResult.Success => "Successful logout",
            AdminLogoutResult.Failure => "Failure logout",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}