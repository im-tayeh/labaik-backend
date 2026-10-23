# Labaik API

Backend for Labaik, an Umrah companion app: companies, groups, supervisors, pilgrims and step-by-step rituals.

## Tech stack
.NET 10, ASP.NET Core Minimal APIs, PostgreSQL 18 (from phase 1), Serilog, OpenTelemetry, Docker, Render, GitHub Actions.

## Architecture
Clean Architecture: `Domain` <- `Application` <- `Infrastructure` <- `Api`, plus `Contracts` for request and response shapes.

## Getting started
```bash
docker compose up -d
dotnet run --project src/Labaik.Api
```
- Scalar: https://localhost:7080/scalar
- Seq: http://localhost:8081
- Telemetry dashboard: http://localhost:18888

## Tests
```bash
dotnet test Labaik.slnx
```
Contract snapshot update: run the tests with `UPDATE_OPENAPI_SNAPSHOT=1`.

## Deployment
Push to `main` -> CI -> CD triggers the Render deploy hook and waits for `/health/live` to report the new commit.

## Project status
v0.1.0: walking skeleton.
