namespace Application.Contracts.Accounts;

public abstract record DepositResult
{
    private DepositResult() { }

    public sealed record Success : DepositResult;

    public sealed record NotAuthorized : DepositResult;
}