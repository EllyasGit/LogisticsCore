namespace LogisticsCore.Models;

public sealed record CreateShipmentRequest(
    string? CustomerReference,
    string? Carrier,
    string? Origin,
    string? Destination)
{
    public IReadOnlyCollection<string> Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(CustomerReference))
        {
            errors.Add("CustomerReference is required.");
        }

        if (string.IsNullOrWhiteSpace(Carrier))
        {
            errors.Add("Carrier is required.");
        }

        if (string.IsNullOrWhiteSpace(Origin))
        {
            errors.Add("Origin is required.");
        }

        if (string.IsNullOrWhiteSpace(Destination))
        {
            errors.Add("Destination is required.");
        }

        return errors;
    }
}
