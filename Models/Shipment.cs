namespace LogisticsCore.Models;

public sealed record Shipment(
    string Id,
    string CustomerReference,
    string Carrier,
    string Origin,
    string Destination,
    ShipmentStatus Status,
    DateTimeOffset EstimatedDelivery,
    IReadOnlyList<TrackingEvent> TrackingEvents);

public sealed record TrackingEvent(
    DateTimeOffset Timestamp,
    string Location,
    ShipmentStatus Status,
    string Description);

public enum ShipmentStatus
{
    Created,
    InTransit,
    Delayed,
    OutForDelivery,
    Delivered
}
