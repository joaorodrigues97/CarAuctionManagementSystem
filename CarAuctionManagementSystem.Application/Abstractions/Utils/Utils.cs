using CarAuctionManagementSystem.Domain.Abstractions;
using FluentValidation.Results;

namespace CarAuctionManagementSystem.Application.Abstractions.Utils;

public static class Utils
{
    public static List<Error> ConvertToError(this List<ValidationFailure> validationFailures)
    {
        return validationFailures
            .Select(validation => new Error(validation.ErrorCode, validation.ErrorMessage))
            .ToList();
    }
}