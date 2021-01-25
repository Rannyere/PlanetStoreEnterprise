# PlanetStoreEnterprise
E-commerce with AspNet Core 3.1


Commands Generate Database with EntityFramework:

Migration PSE.Identification.API:
ApplicationDbContext:  
dotnet ef migrations add initial_data_Identification --project PSE.Identification.API -s PSE.Identification.API --context ApplicationDbContext --verbose 
dotnet ef database update initial_data_Identification --project PSE.Identification.API -s PSE.Identification.API --context ApplicationDbContext --verbose

Migration PSE.Catalog.API:
CatalogDbContext:
dotnet ef migrations add initial_data_Catalog --project PSE.Catalog.API -s PSE.Catalog.API --context CatalogDbContext --verbose 
dotnet ef database update initial_data_Catalog --project PSE.Catalog.API -s PSE.Catalog.API --context CatalogDbContext --verbose

Migration PSE.Catalog.API:
CatalogDbContext:
dotnet ef migrations add initial_data_Clients --project PSE.Clients.API -s PSE.Clients.API --context ClientsDbContext --verbose
dotnet ef database update initial_data_Clients --project PSE.Clients.API -s PSE.Clients.API --context ClientsDbContext --verbose

