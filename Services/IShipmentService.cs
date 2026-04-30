using LogisticsCore.Models;

namespace LogisticsCore.Services;

public interface IShipmentService
{
    IReadOnlyCollection<Shipment> GetAllShipments();

    Shipment? GetShipment(string id);

    IReadOnlyList<TrackingEvent>? GetTrackingEvents(string id);

    Shipment CreateShipment(CreateShipmentRequest request);
}
