namespace Application.Contracts.Admins;

public interface IAdminService
{
    AdminLoginResult Login(string username, string password);

    AdminLogoutResult Logout();
    void RegisterAccount(string username, string password);

    void RegisterNewAdmin(string username, string password);
}