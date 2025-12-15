Introduction 
API for Ecom Pilot

Prerequisite:
dotnet(10.x): https://dotnet.microsoft.com/en-us/download/dotnet/10.0
entity framework: `dotnet tool install --global dotnet-ef`
postgres(latest): https://www.enterprisedb.com/downloads/postgres-postgresql-downloads

How to install?
- Make sure you have prerequisites installed.
- Run the following commands on the root directory
- `git pull`
-- `dotnet build`
-- Make sure there are no errors.
-- `dotnet ef migrations add [MIGRATION NAME] --project EcomApi.Api --startup-project EcomApi.Api`
-- replace [MIGRATION NAME] with a unique meaningful name for migration for example, 
   `dotnet ef migrations add business_dto_changes --project EcomApi.Api --startup-project EcomApi.Api`
-- `dotnet ef database update [MIGRATION NAME]`
   This will sync database with your local changes, replace [MIGRATION NAME] with unique migration name that you used in the previous command. Make sure there are no errors.
