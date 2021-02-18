# PlanetStore

CONTENTS OF THIS FILE
---------------------

 * Introduction
 * Requirements
 * Generate Database
 * Maintainers


INTRODUCTION
------------

The PlanetStore is an E-commerce built with .NetCore 3.1 technology, through distributed systems, with well-defined Bounded Contexts, use of CQRS and MessageBus, user management with Identity, data persistence with EntityFramework and much more...


REQUIREMENTS
------------

.Net Core 3.1
Idenityt
EntityFramework
JWT
EasyNetQ(RabbitMQ)
Swashbuckle
MediatR
More...


GENERATE DATABASE
-----------------

* Migration PSE.Identification.API:
    ApplicationDbContext:  
      dotnet ef migrations add initial_data_Identification --project PSE.Identification.API -s PSE.Identification.API --context ApplicationDbContext --verbose 
      dotnet ef database update initial_data_Identification --project PSE.Identification.API -s PSE.Identification.API --context ApplicationDbContext --verbose

* Migration PSE.Catalog.API:
    CatalogDbContext:
      dotnet ef migrations add initial_data_Catalog --project PSE.Catalog.API -s PSE.Catalog.API --context CatalogDbContext --verbose 
      dotnet ef database update initial_data_Catalog --project PSE.Catalog.API -s PSE.Catalog.API --context CatalogDbContext --verbose

* Migration PSE.Clients.API:
    ClientsDbContext:
      dotnet ef migrations add initial_data_Clients --project PSE.Clients.API -s PSE.Clients.API --context ClientsDbContext --verbose
      dotnet ef database update initial_data_Clients --project PSE.Clients.API -s PSE.Clients.API --context ClientsDbContext --verbose

* Migration PSE.Cart.API:
    CartDbContext:
      dotnet ef migrations add initial_data_Cart --project PSE.Cart.API -s PSE.Cart.API --context CartDbContext --verbose
      dotnet ef database update initial_data_Cart --project PSE.Cart.API -s PSE.Cart.API --context CartDbContext --verbose


MAINTAINERS
-----------

 * Rannyere Almeida
 
