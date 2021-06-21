#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./services/PSE.Order.API/PSE.Order.API.csproj", "services/PSE.Order.API/"]
COPY ["./buildingBlocks/PSE.Core/PSE.Core.csproj", "buildingBlocks/PSE.Core/"]
COPY ["./buildingBlocks/PSE.WebAPI.Core/PSE.WebAPI.Core.csproj", "buildingBlocks/PSE.WebAPI.Core/"]
COPY ["./buildingBlocks/PSE.MessageBus/PSE.MessageBus.csproj", "buildingBlocks/PSE.MessageBus/"]
COPY ["./services/PSE.Order.Infra/PSE.Order.Infra.csproj", "services/PSE.Order.Infra/"]
COPY ["./services/PSE.Order.Domain/PSE.Order.Domain.csproj", "services/PSE.Order.Domain/"]

RUN dotnet restore "services/PSE.Order.API/PSE.Order.API.csproj"
COPY . .
WORKDIR "services/PSE.Order.API"
RUN dotnet build "PSE.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PSE.Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PSE.Order.API.dll"]
