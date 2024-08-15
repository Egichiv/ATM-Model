namespace Application.Contracts.Accounts;

public abstract record LogoutResult
{
    private LogoutResult() { }

    public sealed record Success : LogoutResult;

    public sealed record Failure : LogoutResult;
}