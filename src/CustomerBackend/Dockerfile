# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the csproj first (Docker caching optimization)
COPY CustomerBackend.csproj ./
RUN dotnet restore

# Copy all files for build
COPY . .
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/out .
EXPOSE 8080

ENTRYPOINT ["dotnet", "CustomerBackend.dll"]
