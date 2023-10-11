@ECHO OFF
SET /P MSG="Enter migration text "

echo 0 - All
echo 1 - Application
echo 2 - Accounting
echo 3 - Identity
echo 4 - Catalog
echo 5 - People
echo 6 - Inventory
echo 7 - Organization
echo 8 - Sales

SET /P OPTOIN="Select Project to apply migration "

2>NUL CALL :CASE_%OPTOIN% # jump to :CASE_red, :CASE_blue, etc.
IF ERRORLEVEL 1 CALL :DEFAULT_CASE # If label doesn't exist


ECHO Done.
EXIT /B

:CASE_0
	cd ./Shared/Shared.Infrastructure
	dotnet ef migrations add "1-%MSG%" --startup-project ../../API -o Persistence/Migrations/ --context ApplicationDbContext
	dotnet ef database update --startup-project ../../API --context ApplicationDbContext
	cd ../../
	cd ./Modules/Accounting/Modules.Accounting.Infrastructure
	dotnet ef migrations add "2-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context AccountingDbContext
	dotnet ef database update --startup-project ../../../API --context AccountingDbContext
	cd ../../../

	cd ./Modules/Identity/Modules.Identity.Infrastructure
	dotnet ef migrations add "3-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context IdentityDbContext
	dotnet ef database update --startup-project ../../../API --context IdentityDbContext
	cd ../../../

	cd ./Modules/Catalog/Modules.Catalog.Infrastructure
	dotnet ef migrations add "4-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context CatalogDbContext
	dotnet ef database update --startup-project ../../../API --context CatalogDbContext
	cd ../../../

	cd ./Modules/People/Modules.People.Infrastructure
	dotnet ef migrations add "5-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context PeopleDbContext
	dotnet ef database update --startup-project ../../../API --context PeopleDbContext
	cd ../../../

	cd ./Modules/Inventory/Modules.Inventory.Infrastructure
	dotnet ef migrations add "6-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context InventoryDbContext
	dotnet ef database update --startup-project ../../../API --context InventoryDbContext
	cd ../../../

	cd ./Modules/Organization/Modules.Organization.Infrastructure
	dotnet ef migrations add "7-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context OrganizationDbContext
	dotnet ef database update --startup-project ../../../API --context OrganizationDbContext
	cd ../../../

	cd ./Modules/Invoicing/Modules.Invoicing.Infrastructure
	dotnet ef migrations add "8-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context SalesDbContext
	dotnet ef database update --startup-project ../../../API --context SalesDbContext
	cd ../../../
	GOTO END_CASE
:CASE_1
  cd ./Shared/Shared.Infrastructure
  dotnet ef migrations add "1-%MSG%" --startup-project ../../API -o Persistence/Migrations/ --context ApplicationDbContext
  dotnet ef database update --startup-project ../../API --context ApplicationDbContext
  cd ../../
  GOTO END_CASE
:CASE_2
  cd ./Modules/Accounting/Modules.Accounting.Infrastructure
  dotnet ef migrations add "2-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context ApplicationDbContext
  dotnet ef database update --startup-project ../../../API --context ApplicationDbContext
  cd ../../../
  GOTO END_CASE
:CASE_3
	cd ./Modules/Identity/Modules.Identity.Infrastructure
	dotnet ef migrations add "3-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context IdentityDbContext
	dotnet ef database update --startup-project ../../../API --context IdentityDbContext
	cd ../../../
  GOTO END_CASE
:CASE_4
	cd ./Modules/Catalog/Modules.Catalog.Infrastructure
	dotnet ef migrations add "4-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context CatalogDbContext
	dotnet ef database update --startup-project ../../../API --context CatalogDbContext
	cd ../../../
  GOTO END_CASE
:CASE_5
	cd ./Modules/People/Modules.People.Infrastructure
	dotnet ef migrations add "5-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context PeopleDbContext
	dotnet ef database update --startup-project ../../../API --context PeopleDbContext
	cd ../../../
  GOTO END_CASE
:CASE_6
	cd ./Modules/Inventory/Modules.Inventory.Infrastructure
	dotnet ef migrations add "6-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context InventoryDbContext
	dotnet ef database update --startup-project ../../../API --context InventoryDbContext
	cd ../../../
  GOTO END_CASE
:CASE_7
	cd ./Modules/Organization/Modules.Organization.Infrastructure
	dotnet ef migrations add "7-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context OrganizationDbContext
	dotnet ef database update --startup-project ../../../API --context OrganizationDbContext
	cd ../../../
  GOTO END_CASE
:CASE_8
	cd ./Modules/Invoicing/Modules.Invoicing.Infrastructure
	dotnet ef migrations add "8-%MSG%" --startup-project ../../../API -o Persistence/Migrations/ --context SalesDbContext
	dotnet ef database update --startup-project ../../../API --context SalesDbContext
	cd ../../../
  GOTO END_CASE
:DEFAULT_CASE
  ECHO Unknown color "%OPTOIN%"
  GOTO END_CASE
:END_CASE
  VER > NUL # reset ERRORLEVEL
  GOTO :EOF # return from CALL
  
  pause;
