# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG BUILD_CONFIGURATION=Release

# Copy project file using root-relative path
COPY ["CustomerBackend.csproj", "CustomerBackend/"]
RUN dotnet restore "CustomerBackend/CustomerBackend.csproj"

# Copy everything else
COPY . ./CustomerBackend/
WORKDIR /src/CustomerBackend
RUN dotnet build "CustomerBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "CustomerBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerBackend.dll"]
