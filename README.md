# Ecom Pilot API

## Introduction

This repository contains the **Ecom Pilot API**, a backend service built using .NET and Entity Framework with PostgreSQL as the database.

---

## Prerequisites

Make sure the following dependencies are installed on your system:

* **.NET SDK (10.x)**
  [https://dotnet.microsoft.com/en-us/download/dotnet/10.0](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

* **Entity Framework CLI**

  ```bash
  dotnet tool install --global dotnet-ef
  ```

* **PostgreSQL (Latest Version)**
  [https://www.enterprisedb.com/downloads/postgres-postgresql-downloads](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)

---

## Installation

Follow these steps to set up the project locally:

1. Ensure all prerequisites are installed correctly.
2. Navigate to the root directory of the project.
3. Pull the latest code:

   ```bash
   git pull
   ```
4. Build the solution:

   ```bash
   dotnet build
   ```

   Ensure the build completes without errors.

---

## Database Migration

To apply database changes using Entity Framework migrations:

1. Create a new migration:

   ```bash
   dotnet ef migrations add [MIGRATION_NAME] \
     --project EcomApi.Api \
     --startup-project EcomApi.Api
   ```

   Replace `[MIGRATION_NAME]` with a meaningful and unique name.

   **Example:**

   ```bash
   dotnet ef migrations add business_dto_changes \
     --project EcomApi.Api \
     --startup-project EcomApi.Api
   ```

2. Update the database:

   ```bash
   dotnet ef database update [MIGRATION_NAME]
   ```

   This command synchronizes the database with your local changes. Ensure the command completes without errors.

---

## Notes

* Always verify database connection settings before running migrations.
* Keep migration names descriptive for easier tracking and maintenance.
