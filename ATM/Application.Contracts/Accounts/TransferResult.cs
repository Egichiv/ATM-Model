namespace Application.Contracts.Accounts;

public abstract record TransferResult
{
    private TransferResult() { }

    public sealed record Success : TransferResult;

    public sealed record NotEnoughMoney : TransferResult;

    public sealed record NotAuthorized : TransferResult;

    public sealed record UserNotFound : TransferResult;
}