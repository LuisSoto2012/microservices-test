FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Client/Client.API/Client.API.csproj", "src/Services/Client/Client.API/"]
COPY ["src/BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/BuildingBlocks/EventBusRabbitMQ/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/Services/Client/Client.Domain/Client.Domain.csproj", "src/Services/Client/Client.Domain/"]
COPY ["src/Services/Client/Client.Application/Client.Application.csproj", "src/Services/Client/Client.Application/"]
COPY ["src/Services/Client/Client.Infrastructure/Client.Infrastructure.csproj", "src/Services/Client/Client.Infrastructure/"]
RUN dotnet restore "src/Services/Client/Client.API/Client.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Client/Client.API"
RUN dotnet build "Client.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Client.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.API.dll"]
