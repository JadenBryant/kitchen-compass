# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG BUILD_CONFIGURATION=Release

# Copy project file and restore dependencies
COPY ["CustomerBackend.csproj", "."]
RUN dotnet restore "CustomerBackend.csproj"

# Copy the rest of the source code
COPY . .
RUN dotnet build "CustomerBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "CustomerBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Railway automatically injects PORT, so we bind to it dynamically
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}

EXPOSE 8080

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerBackend.dll"]
