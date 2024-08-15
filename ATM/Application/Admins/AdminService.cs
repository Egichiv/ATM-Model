using Application.Abstractions.Repositories;
using Application.Contracts.Admins;
using Application.Models.Admins;

namespace Application.Admins;

public class AdminService : IAdminService
{
    private readonly CurrentAdminManager _currentAdminManager;
    private readonly IAdminRepository _adminRepository;
    private readonly IAccountRepository _accountRepository;

    public AdminService(
        CurrentAdminManager currentAdminManager,
        IAdminRepository adminRepository,
        IAccountRepository accountRepository)
    {
        _currentAdminManager = currentAdminManager;
        _adminRepository = adminRepository;
        _accountRepository = accountRepository;
    }

    public AdminLoginResult Login(string username, string password)
    {
        Admin? admin = _adminRepository.FindAdminByUserName(username).Result;

        if (admin is null)
        {
            return new AdminLoginResult.AdminNotFound();
        }

        if (admin.Password != password)
        {
            return new AdminLoginResult.IncorrectPassword();
        }

        _currentAdminManager.Admin = admin;
        return new AdminLoginResult.Success();
    }

    public AdminLogoutResult Logout()
    {
        if (_currentAdminManager.Admin is null)
        {
            return new AdminLogoutResult.Failure();
        }

        _currentAdminManager.Admin = null;
        return new AdminLogoutResult.Success();
    }

    public void RegisterAccount(string username, string password)
    {
        _accountRepository.RegisterAccount(username, password);
    }

    public void RegisterNewAdmin(string username, string password)
    {
        _adminRepository.RegisterNewAdmin(username, password);
    }
}