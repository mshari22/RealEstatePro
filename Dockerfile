# ── Build Stage ──
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore first (caches NuGet layer)
COPY RealEstatePro.csproj .
RUN dotnet restore

# Copy everything else and publish
COPY . .
RUN dotnet publish -c Release -o /app/publish

# ── Runtime Stage ──
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Render.com sets PORT env var – ASP.NET reads ASPNETCORE_URLS
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

# Expose default (Render overrides via PORT)
EXPOSE 80

# Start the app – bind to Render's PORT at runtime
ENTRYPOINT ["sh", "-c", "dotnet RealEstatePro.dll --urls http://0.0.0.0:${PORT:-80}"]
