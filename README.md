# KPI Platform Backend

A multi-tenant KPI (Key Performance Indicator) management backend built with ASP.NET Core, designed to support scalable and configurable enterprise performance management workflows.

This system was developed based on real-world business processes previously handled through Excel and SharePoint, with the goal of reducing operational friction while preserving familiar user workflows.

---

## Overview

The platform provides a structured backend foundation for managing KPIs across multiple tenants, roles, and organizational units. It emphasizes maintainability, extensibility, and clear separation of concerns, allowing the system to evolve without large-scale refactoring.

---

## Core Capabilities

- Multi-tenant architecture with tenant-specific configuration
- Role-based access control with per-tenant role assignment
- Configurable and customizable menu structure
- Centralized authentication via Keycloak (OpenID Connect)
- Background processing for scheduled and asynchronous tasks
- Structured and centralized logging
- Clean Architecture implementation
- Docker-ready for containerized deployment
- Designed for incremental feature growth

---

## Architecture

The solution follows Clean Architecture principles to ensure clear responsibility boundaries and long-term maintainability.

```text
├── Core
│   ├── Domain entities
│   └── Contracts and abstractions
│
├── Application
│   ├── Business rules
│   └── Use case orchestration
│
├── Infrastructure
│   ├── Data access and persistence
│   ├── External service integrations
│   └── Authentication and background processing
│
├── Presentation
│   ├── HTTP API endpoints
│   ├── Middleware and filters
│   └── Application startup configuration

Technology Stack
ASP.NET Core (.NET 10)
PostgreSQL
Entity Framework Core
Dapper
Keycloak (OIDC)
Hangfire
Serilog
Docker
Redis (optional)

Development Setup
Prerequisites
.NET SDK 10 or later
PostgreSQL instance
Keycloak instance
Docker (optional)

Running Locally
bash
Copy code
dotnet restore
dotnet build
dotnet run --project src/Presentation
Authentication and Authorization
Authentication is handled through Keycloak using OpenID Connect.
Authorization is enforced within the application layer, allowing roles and permissions to be evaluated independently of the identity provider implementation.

This approach enables:
Centralized identity management
Flexible role assignment per tenant
Clear separation between identity and business logic
Configuration Management
Sensitive configuration values are excluded from source control and provided via environment variables or external configuration files.

Example configuration structure:
json
{
  "ConnectionStrings": {
    "Default": "YOUR_CONNECTION_STRING"
  }
}

Design Considerations
The system prioritizes:
Explicit dependency management
Fail-fast configuration validation
Predictable runtime behavior
Evolutionary architecture over rigid upfront design
Design decisions favor clarity and adaptability rather than premature optimization.

Project Status
The core architectural foundation is established.
Feature development continues iteratively based on validated requirements and user feedback.

License
This repository is intended for educational and portfolio demonstration purposes.