using LogisticsCore.Models;

namespace LogisticsCore.Services;

public sealed class InMemoryShipmentService : IShipmentService
{
    private readonly List<Shipment> _shipments =
    [
        new(
            Id: "SHP-1001",
            CustomerReference: "ORD-78421",
            Carrier: "Nordic Freight",
            Origin: "Boras",
            Destination: "Stockholm",
            Status: ShipmentStatus.InTransit,
            EstimatedDelivery: DateTimeOffset.UtcNow.AddDays(2),
            TrackingEvents:
            [
                new(DateTimeOffset.UtcNow.AddHours(-18), "Boras", ShipmentStatus.Created, "Shipment registered in the transport network."),
                new(DateTimeOffset.UtcNow.AddHours(-10), "Jonkoping", ShipmentStatus.InTransit, "Shipment departed the sorting terminal.")
            ]),
        new(
            Id: "SHP-1002",
            CustomerReference: "ORD-90133",
            Carrier: "Global Parcel",
            Origin: "Malmo",
            Destination: "Copenhagen",
            Status: ShipmentStatus.OutForDelivery,
            EstimatedDelivery: DateTimeOffset.UtcNow.AddHours(8),
            TrackingEvents:
            [
                new(DateTimeOffset.UtcNow.AddDays(-1), "Malmo", ShipmentStatus.Created, "Shipment registered for pickup."),
                new(DateTimeOffset.UtcNow.AddHours(-5), "Copenhagen", ShipmentStatus.OutForDelivery, "Shipment loaded on final delivery route.")
            ]),
        new(
            Id: "SHP-1003",
            CustomerReference: "ORD-55618",
            Carrier: "BlueLine Logistics",
            Origin: "Gothenburg",
            Destination: "Oslo",
            Status: ShipmentStatus.Delayed,
            EstimatedDelivery: DateTimeOffset.UtcNow.AddDays(4),
            TrackingEvents:
            [
                new(DateTimeOffset.UtcNow.AddDays(-2), "Gothenburg", ShipmentStatus.Created, "Shipment registered with carrier."),
                new(DateTimeOffset.UtcNow.AddHours(-7), "Customs Hub", ShipmentStatus.Delayed, "Shipment delayed during customs processing.")
            ])
    ];

    public IReadOnlyCollection<Shipment> GetAllShipments() =>
        _shipments
            .OrderBy(shipment => shipment.EstimatedDelivery)
            .ToList();

    public Shipment? GetShipment(string id) =>
        _shipments.FirstOrDefault(shipment =>
            string.Equals(shipment.Id, id, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<TrackingEvent>? GetTrackingEvents(string id) =>
        GetShipment(id)?.TrackingEvents;

    public Shipment CreateShipment(CreateShipmentRequest request)
    {
        var now = DateTimeOffset.UtcNow;

        var shipment = new Shipment(
            Id: $"SHP-{now:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}",
            CustomerReference: request.CustomerReference!.Trim(),
            Carrier: request.Carrier!.Trim(),
            Origin: request.Origin!.Trim(),
            Destination: request.Destination!.Trim(),
            Status: ShipmentStatus.Created,
            EstimatedDelivery: now.AddDays(3),
            TrackingEvents:
            [
                new(now, request.Origin!.Trim(), ShipmentStatus.Created, "Shipment registered and awaiting carrier pickup.")
            ]);

        _shipments.Add(shipment);

        return shipment;
    }
}
