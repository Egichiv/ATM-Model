using Application.Models.Histories;

namespace Application.Contracts.Accounts;

public interface IAccountService
{
    AccountLoginResult Login(string username, string password);

    LogoutResult Logout();
    double ShowMoney();
    DepositResult AddMoney(double money);
    WithdrawResult WithdrawMoney(double money);
    TransferResult TransferMoney(double money, string username);
    IEnumerable<History> ShowHistory();
}