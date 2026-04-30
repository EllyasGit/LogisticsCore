namespace LogisticsCore.Models;

public sealed record ApiStatus(
    string Name,
    string Status,
    string Description,
    DateTimeOffset Timestamp);
