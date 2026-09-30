public class DomainResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public IReadOnlyCollection<string> Messages { get; }

    protected DomainResult(bool isSuccess, IReadOnlyCollection<string> messages)
    {
        IsSuccess = isSuccess;
        Messages = messages;
    }

    public static DomainResult Success(string? successMessage = "OperationDoneSuccessfully") => new(true, [successMessage ?? "OperationDoneSuccessfully"]);

    public static DomainResult Failure(List<string> errorMessages) => new(false, errorMessages);

    public static DomainResult<TValue> Success<TValue>(TValue value, string? successMessage = "Operation Done Successfully") => new(value, true, [successMessage ?? "Operation Done Successfully"]);

    public static DomainResult<TValue> Failure<TValue>(List<string> errorMessages) => new(default, false, errorMessages);
}


public class DomainResult<TValue> : DomainResult
{
    public TValue? Value { get; }

    protected internal DomainResult(
        TValue? value,
        bool isSuccess,
        IReadOnlyCollection<string> messages)
        : base(isSuccess, messages)
    {
        Value = value;
    }
}
