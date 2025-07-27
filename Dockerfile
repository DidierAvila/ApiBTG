# Use the official .NET 8.0 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 8.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files
COPY ["ApiBTG/ApiBTG.csproj", "ApiBTG/"]
COPY ["ApiBTG.Application/ApiBTG.Application.csproj", "ApiBTG.Application/"]
COPY ["ApiBTG.Domain/ApiBTG.Domain.csproj", "ApiBTG.Domain/"]
COPY ["ApiBTG.Infrastructure/ApiBTG.Infrastructure.csproj", "ApiBTG.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "ApiBTG/ApiBTG.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/ApiBTG"
RUN dotnet build "ApiBTG.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ApiBTG.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Create a non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

ENTRYPOINT ["dotnet", "ApiBTG.dll"] 