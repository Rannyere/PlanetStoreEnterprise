#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./services/PSE.Identification.API/PSE.Identification.API.csproj", "services/PSE.Identification.API/"]
COPY ["./buildingBlocks/PSE.WebAPI.Core/PSE.WebAPI.Core.csproj", "buildingBlocks/PSE.WebAPI.Core/"]
COPY ["./buildingBlocks/PSE.Core/PSE.Core.csproj", "buildingBlocks/PSE.Core/"]
COPY ["./buildingBlocks/PSE.MessageBus/PSE.MessageBus.csproj", "buildingBlocks/PSE.MessageBus/"]

RUN dotnet restore "services/PSE.Identification.API/PSE.Identification.API.csproj"
COPY . .
WORKDIR "services/PSE.Identification.API"
RUN dotnet build "PSE.Identification.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PSE.Identification.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PSE.Identification.API.dll"]
