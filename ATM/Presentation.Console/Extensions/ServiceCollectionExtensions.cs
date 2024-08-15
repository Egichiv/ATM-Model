using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.Scenarios.Accounts.Deposit;
using Presentation.Console.Scenarios.Accounts.Login;
using Presentation.Console.Scenarios.Accounts.Logout;
using Presentation.Console.Scenarios.Accounts.ShowBalance;
using Presentation.Console.Scenarios.Accounts.ShowHistory;
using Presentation.Console.Scenarios.Accounts.Transfer;
using Presentation.Console.Scenarios.Accounts.Withdraw;
using Presentation.Console.Scenarios.Admins.Login;
using Presentation.Console.Scenarios.Admins.Logout;
using Presentation.Console.Scenarios.Admins.RegisterAccount;
using Presentation.Console.Scenarios.Admins.RegisterNewAdmin;
using Presentation.Console.Scenarios.Exits.ExitBank;

namespace Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DepositScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawScenarioProvider>();
        collection.AddScoped<IScenarioProvider, TransferScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowHistoryScenarioProvider>();

        collection.AddScoped<IScenarioProvider, LoginAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminLogoutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, RegisterAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, RegisterNewAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ExitScenarioProvider>();

        return collection;
    }
}