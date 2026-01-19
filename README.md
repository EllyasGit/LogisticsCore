# LogisticsCore 📦
**A Cloud-Native Supply Chain Microservice**

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

## 📖 Overview
LogisticsCore is a backend Web API built with **.NET 9**. It simulates real-time package tracking data, designed as a microservice for a distributed supply chain system.

## 🛠 Tech Stack
* **Framework:** ASP.NET Core 9.0
* **Containerization:** Docker (Multi-stage builds)
* **CI/CD:** GitHub Actions
* **DevOps:** Automated build and test workflows

## 🚀 Getting Started
To run this project via Docker:
```bash
docker build -t logisticscore .
docker run -p 3000:8080 logisticscore
