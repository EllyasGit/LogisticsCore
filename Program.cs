var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 1. "Status" Endpoint - To check if the server is running
app.MapGet("/", () => "Centiro Logistics Core is Running!");

// 2. "Tracking" Endpoint - Simulates finding a package
app.MapGet("/track-package", () =>
{
    var statuses = new[] { "In Transit", "Out for Delivery", "Delivered", "Delayed at Customs", "Sorting Facility" };
    var cities = new[] { "Malmö", "Stockholm", "Gothenburg", "Borås", "Copenhagen" };

    // Create a fake package update
    var packageUpdate = new
    {
        PackageId = "CN-" + Random.Shared.Next(1000, 9999),
        Status = statuses[Random.Shared.Next(statuses.Length)],
        Location = cities[Random.Shared.Next(cities.Length)],
        EstimatedDelivery = DateTime.Now.AddDays(Random.Shared.Next(1, 5)),
        LastScanned = DateTime.Now
    };

    return packageUpdate;
});

app.Run();