using LogisticsCore.Models;
using LogisticsCore.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<IShipmentService, InMemoryShipmentService>();
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => Results.Ok(new ApiStatus(
        Name: "LogisticsCore",
        Status: "Running",
        Description: "Cloud-native shipment tracking API",
        Timestamp: DateTimeOffset.UtcNow)))
    .WithName("GetApiStatus")
    .WithTags("System")
    .WithOpenApi();

app.MapHealthChecks("/health/live");

var shipments = app.MapGroup("/shipments")
    .WithTags("Shipments");

shipments.MapGet("/", (IShipmentService shipmentService) =>
    Results.Ok(shipmentService.GetAllShipments()))
    .WithName("GetShipments")
    .WithOpenApi();

shipments.MapGet("/{id}", (string id, IShipmentService shipmentService) =>
    shipmentService.GetShipment(id) is { } shipment
        ? Results.Ok(shipment)
        : Results.NotFound(new ErrorResponse($"Shipment '{id}' was not found.")))
    .WithName("GetShipmentById")
    .WithOpenApi();

shipments.MapGet("/{id}/tracking-events", (string id, IShipmentService shipmentService) =>
    shipmentService.GetTrackingEvents(id) is { } events
        ? Results.Ok(events)
        : Results.NotFound(new ErrorResponse($"Shipment '{id}' was not found.")))
    .WithName("GetShipmentTrackingEvents")
    .WithOpenApi();

shipments.MapPost("/", (CreateShipmentRequest request, IShipmentService shipmentService) =>
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.BadRequest(new ValidationErrorResponse(validationErrors));
        }

        var shipment = shipmentService.CreateShipment(request);
        return Results.Created($"/shipments/{shipment.Id}", shipment);
    })
    .WithName("CreateShipment")
    .WithOpenApi();

app.Run();
