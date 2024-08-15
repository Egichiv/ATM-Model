using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Accounts;

namespace Presentation.Console.Scenarios.Accounts.Deposit;

public class DepositScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _transactionService;
    private readonly ICurrentAccountService _currentAccount;

    public DepositScenarioProvider(
        IAccountService transactionService,
        ICurrentAccountService currentState)
    {
        _transactionService = transactionService;
        _currentAccount = currentState;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccount.Account is not null)
        {
            scenario = new DepositScenario(_transactionService);
            return true;
        }

        scenario = null;
        return false;
    }
}