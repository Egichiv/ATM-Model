using Application.Contracts.Admins;

namespace Application.Admins;

public class CurrentAdminManager : ICurrentAdminService
{
    public Models.Admins.Admin? Admin { get; set; }
}