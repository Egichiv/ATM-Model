using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Accounts;

namespace Presentation.Console.Scenarios.Accounts.Transfer;

public class TransferScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _transactionService;
    private readonly ICurrentAccountService _currentAccount;

    public TransferScenarioProvider(
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
            scenario = new TransferScenario(_transactionService);
            return true;
        }

        scenario = null;
        return false;
    }
}