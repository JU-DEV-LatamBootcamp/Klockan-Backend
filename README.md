# Klockan-Backend

---

## Database Connection

### Step 1: Install PostgreSQL

Install PostgreSQL on your system. You can download it from the [official PostgreSQL website](https://www.postgresql.org/download/).

### Step 2: Create Database

Once PostgreSQL is installed, create the required database: `klockandb`.

### Step 3: Install Necessary Packages

Inside the `KlockanAPI.Infrastructure` directory, run the following commands to install the necessary packages:

```bash
Install-Package Microsoft.EntityFrameworkCore -Version 8.0.1
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL -Version 8.0.0
Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.1
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.1
```

### Step 4: Update Password in `appsettings.json`

In the Presentation layer, update the password in `DefaultConnection`.

### Step 5: Database Migrations

Inside `KlockanAPI.Infrastructure`, follow these steps:

1. Set the default project to Infrastructure.
2. Run the following command to add a new migration:

   ```bash
   Add-Migration InitialCreate -OutputDir Data/Migrations
   ```

3. Update the database:
   ```bash
   Update-Database
   ```

#### Alternatively, you can use these .NET Core CLI commands:

1. Run the following command to add a new migration:

   ```bash
   dotnet ef migrations add InitialCreate --output-dir Data/Migrations
   ```

2. Update the database:
   ```bash
   dotnet ef database update
   ```

---

## Folder Structure

# Klockan

## Core

- **Domain**
  - _Models_
  - _Entities_
  - _Enums_
  - _Events_
  - _Exceptions_
  - _Interfaces_

## Infrastructure

- **Data**
  - _Context_
  - _Repositories_
  - _Configurations_
- **ExternalServices**
- **Migrations**

## Application

- **Services**
  - _Implementations_
  - _Interfaces_
- **DTOs**
- **Mappings**
- **CrossCuttingConcerns**
  - _Logging_
  - _Security_
  - _Extensions_
  - _Utilities_

## Presentation

- **API**
  - _Controllers_
  - _ViewModels_
- **Web**
  - _Views_
  - _Controllers_
  - _ViewModels_

## Tests

- **UnitTests**
- **IntegrationTests**

-

![Folder Structure](./assets/folderStructure.png)



