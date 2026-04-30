using LogisticsCore.Models;
using LogisticsCore.Services;

var tests = new (string Name, Action Test)[]
{
    ("Seed data contains realistic shipments", SeedDataContainsRealisticShipments),
    ("Shipments can be looked up case-insensitively", ShipmentsCanBeLookedUpCaseInsensitively),
    ("Tracking events are returned for existing shipments", TrackingEventsAreReturnedForExistingShipments),
    ("Create shipment validates required fields", CreateShipmentValidatesRequiredFields),
    ("Create shipment adds a new created shipment", CreateShipmentAddsNewCreatedShipment)
};

var failedTests = 0;

foreach (var test in tests)
{
    try
    {
        test.Test();
        Console.WriteLine($"PASS: {test.Name}");
    }
    catch (Exception exception)
    {
        failedTests++;
        Console.WriteLine($"FAIL: {test.Name}");
        Console.WriteLine(exception.Message);
    }
}

return failedTests == 0 ? 0 : 1;

static void SeedDataContainsRealisticShipments()
{
    var service = new InMemoryShipmentService();
    var shipments = service.GetAllShipments();

    Assert(shipments.Count >= 3, "Expected at least three seeded shipments.");
    Assert(shipments.All(shipment => shipment.TrackingEvents.Count > 0), "Every shipment should include tracking events.");
}

static void ShipmentsCanBeLookedUpCaseInsensitively()
{
    var service = new InMemoryShipmentService();

    var shipment = service.GetShipment("shp-1001");

    Assert(shipment is not null, "Expected SHP-1001 to be found regardless of casing.");
    Assert(shipment!.Id == "SHP-1001", "Expected the seeded shipment id to match.");
}

static void TrackingEventsAreReturnedForExistingShipments()
{
    var service = new InMemoryShipmentService();

    var events = service.GetTrackingEvents("SHP-1002");

    Assert(events is not null, "Expected tracking events for SHP-1002.");
    Assert(events!.Count >= 2, "Expected SHP-1002 to include multiple tracking events.");
}

static void CreateShipmentValidatesRequiredFields()
{
    var request = new CreateShipmentRequest(
        CustomerReference: "",
        Carrier: "Global Parcel",
        Origin: null,
        Destination: "Stockholm");

    var errors = request.Validate();

    Assert(errors.Contains("CustomerReference is required."), "Expected CustomerReference validation error.");
    Assert(errors.Contains("Origin is required."), "Expected Origin validation error.");
}

static void CreateShipmentAddsNewCreatedShipment()
{
    var service = new InMemoryShipmentService();
    var beforeCount = service.GetAllShipments().Count;

    var shipment = service.CreateShipment(new CreateShipmentRequest(
        CustomerReference: "ORD-12345",
        Carrier: "Nordic Freight",
        Origin: "Boras",
        Destination: "Stockholm"));

    Assert(shipment.Status == ShipmentStatus.Created, "Expected new shipment to start as Created.");
    Assert(service.GetAllShipments().Count == beforeCount + 1, "Expected the new shipment to be stored.");
    Assert(service.GetShipment(shipment.Id) is not null, "Expected created shipment to be retrievable.");
}

static void Assert(bool condition, string message)
{
    if (!condition)
    {
        throw new InvalidOperationException(message);
    }
}
