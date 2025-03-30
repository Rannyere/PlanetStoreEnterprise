# PlanetStore

## Contents

- [Introduction](#introduction)
- [Requirements](#requirements)
- [Environment Setup](#environment-setup)
  - [Installing RabbitMQ with MassTransit](#installing-rabbitmq-with-masstransit)
- [Database: Creating and Updating Migrations](#database-creating-and-updating-migrations)
  - [Using CLI](#using-cli)
  - [Using Package Manager Console in Visual Studio](#using-package-manager-console-in-visual-studio)

## Introduction

**PlanetStore** is an e-commerce platform built with **.NET 9**, leveraging **microservices architecture** and **distributed systems**. The project follows **Bounded Contexts**, **CQRS**, and **Message Bus** principles, featuring **Identity** for user management and **Entity Framework** for data persistence, among other technologies.

## Requirements

Before running the project, ensure you have the following dependencies installed:

- **.NET 9**
- **SQL Server**
- **MassTransit with RabbitMQ** (for asynchronous communication between microservices)

The following technologies are used in the project:

- **Identity** (for user management and authentication)
- **Entity Framework Core**
- **JWT** (JSON Web Token for secure authentication)
- **Swashbuckle (Swagger)** (for API documentation)
- **MediatR** (for CQRS pattern implementation)

## Environment Setup

### Installing RabbitMQ with MassTransit

**MassTransit** uses **RabbitMQ** as a message broker. To run RabbitMQ in a Docker container, use the following command:

```sh
docker run -d --name rabbit-planetstore -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```

- **-d**: Runs the container in detached mode (in the background).
- **--name rabbit-planetstore**: Names the container.
- **-p 15672:15672**: Exposes the RabbitMQ web management interface.
- **-p 5672:5672**: RabbitMQ communication port.

The management interface can be accessed at [http://localhost:15672](http://localhost:15672).

Default credentials:

- **Username**: `guest`
- **Password**: `guest`

## Database: Creating and Updating Migrations

Each **PlanetStore** microservice has its own **DbContext** and database. Follow the instructions below to create and update migrations.

### Using CLI

#### **PSE.Identification.API**

```sh
cd PSE.Identification.API
dotnet ef migrations add initial_data_Identification --project PSE.Identification.API -s PSE.Identification.API --context ApplicationDbContext --verbose
dotnet ef database update --project PSE.Identification.API -s PSE.Identification.API --context ApplicationDbContext --verbose
```

#### **PSE.Catalog.API**

```sh
cd PSE.Catalog.API
dotnet ef migrations add initial_data_Catalog --project PSE.Catalog.API -s PSE.Catalog.API --context CatalogDbContext --verbose
dotnet ef database update --project PSE.Catalog.API -s PSE.Catalog.API --context CatalogDbContext --verbose
```

#### **PSE.Order.API**

```sh
cd PSE.Order.Infra
dotnet ef migrations add initial_data_Orders --project PSE.Order.Infra -s PSE.Order.API --context OrderDbContext --verbose
dotnet ef database update --project PSE.Order.Infra -s PSE.Order.API --context OrderDbContext --verbose
```

### Using Package Manager Console in Visual Studio

If you are using **Visual Studio**, you can also run migrations directly from the **Package Manager Console**.

#### Steps:

1. Open **Visual Studio** and load your solution.
2. Navigate to **Tools > NuGet Package Manager > Package Manager Console**.
3. In the **Package Manager Console**, select the **Default Project** dropdown to choose the project where your **DbContext** is located.
4. Run the following commands for each microservice:

##### **Example for PSE.Identification.API**

```powershell
Add-Migration initial_data_Identification -Context ApplicationDbContext
Update-Database -Context ApplicationDbContext
```

##### **Example for PSE.Order.API**

For **PSE.Order.API**, the `OrderDbContext` is located in the `PSE.Order.Infra` project, so you should set **PSE.Order.Infra** as the **Default Project** and **PSE.Order.API** as the **Startup Project** in Visual Studio.

```powershell
Add-Migration initial_data_Orders -StartupProject PSE.Order.API -Context OrderDbContext
Update-Database -StartupProject PSE.Order.API -Context OrderDbContext
```
