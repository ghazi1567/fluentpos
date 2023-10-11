# Documentation

### Switching Database Providers

fluentpos currently supports MSSQL & Postgres Dbs. Due to issues with schemas in EFCore's MySQL implementation, MySQL is not being used here.

Firstly, you need to make sure that valid connection strings are mentioned in the appSetting.json
Next, set either to true in appSetting under `PersistenceSettings`.

`"UseMsSql": true,`

`"UsePostgres": true,`

Note: If you set both to true, Postgres will be used by default.

### Important 
- While Switching DBProviders via EFcore, make sure to delete all the migrations, and re-add migrations via the below CLI Command.
- Make sure that you drop the existing database if any.

## Steps 
- Navigate to each of the Infrastructure project per module and shared(Shared.Infrastructure)
- Open the directory in terminal mode. PFB the attached screenshot. You just have to right the Infrastructure project in Visual Studio and select `Open in Terminal`.

![image](https://user-images.githubusercontent.com/31455818/122291148-1d211380-cf12-11eb-9f28-35e5ec0989e5.png)
- Run the EF commands. You can find the EF Commands below in the next section with additional steps ;)

![image](https://user-images.githubusercontent.com/31455818/122291196-2d38f300-cf12-11eb-965e-27267fd1fc76.png)
- That's it!



### Application

Navigate terminal to Shared.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../API -o Persistence/Migrations/ --context ApplicationDbContext`
`dotnet ef database update --startup-project ../../API --context ApplicationDbContext`


### Accounting

Navigate terminal to Shared.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context AccountingDbContext`
`dotnet ef database update --startup-project ../../../API --context AccountingDbContext`
### Identity

Navigate terminal to Modules.Identity.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context IdentityDbContext`
`dotnet ef database update --startup-project ../../../API --context IdentityDbContext`
### Catalog

Navigate terminal to Modules.Catalog.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context CatalogDbContext`
`dotnet ef database update --startup-project ../../../API --context CatalogDbContext`
### People

Navigate terminal to Modules.People.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context PeopleDbContext`
`dotnet ef database update --startup-project ../../../API --context PeopleDbContext`
### Inventory

Navigate terminal to Modules.People.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context InventoryDbContext`
`dotnet ef database update --startup-project ../../../API --context InventoryDbContext`
### Organization

Navigate terminal to Modules.People.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context OrganizationDbContext`
`dotnet ef database update --startup-project ../../../API --context OrganizationDbContext`
### Sales

Navigate terminal to Modules.People.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context SalesDbContext`
`dotnet ef database update --startup-project ../../../API --context SalesDbContext`



cd ./Shared/Shared.Infrastructure
dotnet ef migrations add "migration" --startup-project ../../API -o Persistence/Migrations/ --context ApplicationDbContext
dotnet ef database update --startup-project ../../API --context ApplicationDbContext
cd ../../
cd ./Modules/Accounting/Modules.Accounting.Infrastructure
dotnet ef migrations add "migration" --startup-project ../../../API -o Persistence/Migrations/ --context AccountingDbContext
dotnet ef database update --startup-project ../../../API --context AccountingDbContext
cd ../../../

cd ./Modules/Identity/Modules.Identity.Infrastructure
dotnet ef migrations add "migration" --startup-project ../../../API -o Persistence/Migrations/ --context IdentityDbContext
dotnet ef database update --startup-project ../../../API --context IdentityDbContext
cd ../../../

cd ./Modules/Catalog/Modules.Catalog.Infrastructure
dotnet ef migrations add "migration" --startup-project ../../../API -o Persistence/Migrations/ --context CatalogDbContext
dotnet ef database update --startup-project ../../../API --context CatalogDbContext
cd ../../../

cd ./Modules/People/Modules.People.Infrastructure
dotnet ef migrations add "migration" --startup-project ../../../API -o Persistence/Migrations/ --context PeopleDbContext
dotnet ef database update --startup-project ../../../API --context PeopleDbContext
cd ../../../

cd ./Modules/Inventory/Modules.Inventory.Infrastructure
dotnet ef migrations add "migration" --startup-project ../../../API -o Persistence/Migrations/ --context InventoryDbContext
dotnet ef database update --startup-project ../../../API --context InventoryDbContext
cd ../../../

cd ./Modules/Organization/Modules.Organization.Infrastructure
dotnet ef migrations add "migration" --startup-project ../../../API -o Persistence/Migrations/ --context OrganizationDbContext
dotnet ef database update --startup-project ../../../API --context OrganizationDbContext
cd ../../../

cd ./Modules/Invoicing/Modules.Invoicing.Infrastructure
dotnet ef migrations add "migration" --startup-project ..../../API -o Persistence/Migrations/ --context SalesDbContext
dotnet ef database update --startup-project ../../../API --context SalesDbContext
cd ../../../