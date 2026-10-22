# syntax=docker/dockerfile:1

# ---------- build ----------
# NET11: sdk:11.0
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1) restore layer: only project files, so code changes keep the NuGet cache
COPY global.json Directory.Build.props Directory.Packages.props BannedSymbols.txt .editorconfig ./
COPY src/Labaik.Domain/Labaik.Domain.csproj                 src/Labaik.Domain/
COPY src/Labaik.Application/Labaik.Application.csproj       src/Labaik.Application/
COPY src/Labaik.Infrastructure/Labaik.Infrastructure.csproj src/Labaik.Infrastructure/
COPY src/Labaik.Contracts/Labaik.Contracts.csproj           src/Labaik.Contracts/
COPY src/Labaik.Api/Labaik.Api.csproj                       src/Labaik.Api/
RUN dotnet restore src/Labaik.Api/Labaik.Api.csproj

# 2) build layer
COPY src/ src/
RUN dotnet publish src/Labaik.Api/Labaik.Api.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false

# ---------- runtime ----------
# chiseled: no shell, no package manager, non-root by default. "extra" adds ICU and tzdata.
# NET11: aspnet:11.0-noble-chiseled-extra (verify the tag on MCR)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled-extra AS final
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
COPY --from=build /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "Labaik.Api.dll"]
