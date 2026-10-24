# Labaik API

Backend for Labaik, an Umrah companion app: companies, groups, supervisors, pilgrims and step-by-step rituals.

- Live API: https://labaik-backend-yk5o.onrender.com/api/v1/system/info
- API reference (Scalar): https://labaik-backend-yk5o.onrender.com/scalar

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
Test projects are added in step 0.10. Run them with:
```bash
dotnet test Labaik.slnx
```

## Deployment
Merge a pull request into `main` -> CI (format, build, test, Docker build) -> CD triggers the Render deploy hook and waits until `/health/live` reports the new commit.

## Project status
v0.1.0: walking skeleton.
