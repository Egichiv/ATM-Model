namespace Application.Contracts.Admins;

public abstract record AdminLogoutResult
{
    private AdminLogoutResult() { }

    public sealed record Success : AdminLogoutResult;

    public sealed record Failure : AdminLogoutResult;
}