using System.Diagnostics.CodeAnalysis;

namespace CarAuctionManagementSystem.Domain.Abstractions;

public class Result
{
    protected Result(bool isSuccess, List<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; }
    
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, []);
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, [error]);
    public static Result<TValue> Failure<TValue>(List<Error> errors) => new(default, false, errors);

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}

public sealed class Result<TValue>(TValue? value, bool isSuccess, List<Error> errors) : Result(isSuccess, errors)
{
    [NotNull]
    public TValue Value => IsSuccess
        ? value!
        : throw new InvalidOperationException("Cannot access value of a failure result.");
}