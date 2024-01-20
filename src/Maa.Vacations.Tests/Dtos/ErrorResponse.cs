namespace Maa.Vacations.Tests.Dtos;

internal record ErrorResponse(
    string              Type,
    string              Title,
    int                 Status,
    ErrorDetailResponse Errors
);