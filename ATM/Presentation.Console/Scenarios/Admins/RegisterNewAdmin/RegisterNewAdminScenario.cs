using Application.Contracts.Admins;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Admins.RegisterNewAdmin;

public class RegisterNewAdminScenario : IScenario
{
    private readonly IAdminService _adminService;

    public RegisterNewAdminScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Register new admin account";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter new admin account username");
        string password = AnsiConsole.Ask<string>("Enter new admin account password");

        _adminService.RegisterNewAdmin(username, password);

        string message = $"Registered new admin account {username} : {password}";

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}