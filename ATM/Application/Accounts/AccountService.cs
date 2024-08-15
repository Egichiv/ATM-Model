using Application.Abstractions.Repositories;
using Application.Contracts.Accounts;
using Application.Contracts.Histories;
using Application.Models.Accounts;
using Application.Models.Histories;

namespace Application.Accounts;

public class AccountService : IAccountService
{
    private readonly IHistoryService _historyService;
    private readonly CurrentAccountManager _currentAccountManager;
    private readonly IAccountRepository _accountRepository;
    private readonly IHistoryRepository _historyRepository;

    public AccountService(
        IHistoryService historyService,
        CurrentAccountManager currentAccountManager,
        IAccountRepository accountRepository,
        IHistoryRepository historyRepository)
    {
        _historyService = historyService;
        _currentAccountManager = currentAccountManager;
        _accountRepository = accountRepository;
        _historyRepository = historyRepository;
    }

    public AccountLoginResult Login(string username, string password)
    {
        Account? account = _accountRepository.FindAccountByName(username).Result;

        if (account is null)
        {
            return new AccountLoginResult.AccountNotFound();
        }

        if (account.Password != password)
        {
            return new AccountLoginResult.IncorrectPassword();
        }

        _currentAccountManager.Account = account;
        return new AccountLoginResult.Success();
    }

    public LogoutResult Logout()
    {
        if (_currentAccountManager.Account is null)
        {
            return new LogoutResult.Failure();
        }

        _currentAccountManager.Account = null;
        return new LogoutResult.Success();
    }

    public double ShowMoney()
    {
        return _currentAccountManager.Account?.Money ?? 0;
    }

    public DepositResult AddMoney(double money)
    {
        if (_currentAccountManager.Account is null)
        {
            return new DepositResult.NotAuthorized();
        }

        _accountRepository.UpdateAccount(
            _currentAccountManager.Account.Id,
            _currentAccountManager.Account.Money + money);

        _currentAccountManager.Account = _currentAccountManager.Account with { Money = _currentAccountManager.Account.Money + money };

        _historyService.SaveHistory(_currentAccountManager.Account.Id, Operation.deposit, money);
        return new DepositResult.Success();
    }

    public WithdrawResult WithdrawMoney(double money)
    {
        if (_currentAccountManager.Account is null)
        {
            return new WithdrawResult.NotAuthorized();
        }

        if (_currentAccountManager.Account.Money < money)
        {
            return new WithdrawResult.NotEnoughMoney();
        }

        _accountRepository.UpdateAccount(
            _currentAccountManager.Account.Id,
            _currentAccountManager.Account.Money - money);

        _currentAccountManager.Account = _currentAccountManager.Account with { Money = _currentAccountManager.Account.Money - money };

        _historyService.SaveHistory(_currentAccountManager.Account.Id, Operation.withdraw, money);
        return new WithdrawResult.Success();
    }

    public TransferResult TransferMoney(double money, string username)
    {
        if (_currentAccountManager.Account is null)
        {
            return new TransferResult.NotAuthorized();
        }

        if (_currentAccountManager.Account.Money < money)
        {
            return new TransferResult.NotEnoughMoney();
        }

        Account? accountReceiver = _accountRepository.FindAccountByName(username).Result;
        if (accountReceiver is null)
        {
            return new TransferResult.UserNotFound();
        }

        _accountRepository.UpdateAccount(
            _currentAccountManager.Account.Id,
            _currentAccountManager.Account.Money - money);

        _accountRepository.UpdateAccount(
            accountReceiver.Id,
            accountReceiver.Money + money);

        _currentAccountManager.Account = _currentAccountManager.Account with { Money = _currentAccountManager.Account.Money - money };
        accountReceiver = accountReceiver with { Money = accountReceiver.Money + money };

        _historyService.SaveHistory(_currentAccountManager.Account.Id, Operation.transfer, money);
        _historyService.SaveHistory(accountReceiver.Id, Operation.getTransfer, money);
        return new TransferResult.Success();
    }

    public IEnumerable<History> ShowHistory()
    {
        if (_currentAccountManager.Account is null)
        {
            return new List<History>();
        }

        IAsyncEnumerable<History>? history = _historyRepository.GetAllHistory(_currentAccountManager.Account.Id);

        return history is null ? new List<History>() : history.ToBlockingEnumerable();
    }
}