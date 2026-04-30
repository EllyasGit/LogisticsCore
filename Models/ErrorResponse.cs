namespace LogisticsCore.Models;

public sealed record ErrorResponse(string Message);

public sealed record ValidationErrorResponse(IReadOnlyCollection<string> Errors);
