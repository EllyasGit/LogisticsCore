# LogisticsCore

**A cloud-native shipment tracking API built with ASP.NET Core, Docker and GitHub Actions.**

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![CI](https://github.com/EllyasGit/LogisticsCore/actions/workflows/dotnet.yml/badge.svg)

## Overview

LogisticsCore is a small backend service that models shipment tracking in a supply chain network. It exposes REST endpoints for shipments, tracking events and service health, and is designed to show practical backend engineering skills in a logistics-focused domain.

The project demonstrates:

- ASP.NET Core minimal APIs
- Domain models for shipments and tracking events
- Validation and error responses
- OpenAPI endpoint metadata
- Health checks
- Docker multi-stage builds
- GitHub Actions for build, tests and container validation

## Tech Stack

- **Backend:** ASP.NET Core 9
- **Language:** C#
- **API style:** REST / Minimal APIs
- **Containerization:** Docker
- **CI/CD:** GitHub Actions
- **Testing:** Lightweight .NET test runner

## API Endpoints

| Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `/` | API status |
| `GET` | `/health/live` | Container and platform health check |
| `GET` | `/shipments` | List all shipments |
| `GET` | `/shipments/{id}` | Get a shipment by id |
| `GET` | `/shipments/{id}/tracking-events` | Get tracking history for a shipment |
| `POST` | `/shipments` | Create a new shipment |

In development, OpenAPI metadata is available at:

```text
/openapi/v1.json
```

## Example Request

```http
POST /shipments
Content-Type: application/json

{
  "customerReference": "ORD-12345",
  "carrier": "Nordic Freight",
  "origin": "Boras",
  "destination": "Stockholm"
}
```

## Example Response

```json
{
  "id": "SHP-20260430-4821",
  "customerReference": "ORD-12345",
  "carrier": "Nordic Freight",
  "origin": "Boras",
  "destination": "Stockholm",
  "status": "Created",
  "estimatedDelivery": "2026-05-03T20:30:00+00:00",
  "trackingEvents": [
    {
      "timestamp": "2026-04-30T20:30:00+00:00",
      "location": "Boras",
      "status": "Created",
      "description": "Shipment registered and awaiting carrier pickup."
    }
  ]
}
```

## Run Locally

```bash
dotnet restore
dotnet run
```

Default local URLs:

```text
http://localhost:5188
https://localhost:7153
```

## Run With Docker

```bash
docker build -t logisticscore .
docker run -p 3000:8080 logisticscore
```

Then open:

```text
http://localhost:3000/shipments
```

## Run Tests

```bash
dotnet build LogisticsCore.sln --configuration Release
dotnet run --project tests/LogisticsCore.Tests/LogisticsCore.Tests.csproj --configuration Release
```

## Project Structure

```text
LogisticsCore/
  Models/
    Shipment.cs
    CreateShipmentRequest.cs
    ApiStatus.cs
    ErrorResponse.cs
  Services/
    IShipmentService.cs
    InMemoryShipmentService.cs
  tests/
    LogisticsCore.Tests/
  Program.cs
  Dockerfile
  .github/workflows/dotnet.yml
```

## Next Improvements

- Add persistent storage with PostgreSQL or SQL Server
- Add an Angular dashboard for shipment monitoring
- Add integration tests for API endpoints
- Deploy the container to Azure Container Apps or Kubernetes
- Add authentication for customer-specific shipment access
