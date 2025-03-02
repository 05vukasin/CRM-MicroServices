# Koristimo ASP.NET runtime za pokretanje aplikacije (Linux-based)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage - koristimo Linux-based .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CRM.CustomerService.csproj", "."]
RUN dotnet restore "./CRM.CustomerService.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "./CRM.CustomerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage - pripremamo finalnu verziju aplikacije
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CRM.CustomerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage - koristimo ASP.NET runtime (Linux)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRM.CustomerService.dll"]
