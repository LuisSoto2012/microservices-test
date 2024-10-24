﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Account/Account.API/Account.API.csproj", "src/Services/Account/Account.API/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/Services/Account/Account.Domain/Account.Domain.csproj", "src/Services/Account/Account.Domain/"]
COPY ["src/Services/Account/Account.Application/Account.Application.csproj", "src/Services/Account/Account.Application/"]
COPY ["src/Services/Account/Account.Infrastructure/Account.Infrastructure.csproj", "src/Services/Account/Account.Infrastructure/"]
COPY ["src/BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/BuildingBlocks/EventBusRabbitMQ/"]
RUN dotnet restore "src/Services/Account/Account.API/Account.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Account/Account.API"
RUN dotnet build "Account.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Account.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.API.dll"]
