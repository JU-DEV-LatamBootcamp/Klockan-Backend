﻿# Klockan-Backend

## Folder Structure

```
|-- KlockanAPI
    |-- src
        |-- KlockanAPI.Application
            |-- CrossCutting
                |-- Login
                |-- Security
                |-- Extensions
                |-- Utilities
            |-- DTOs
            |-- Mappings
            |-- Services
                |-- Intefaces
                |-- [+] Implementations
            |-- Validators
                |-- Intefaces
                |-- [+] Implementations
            |-- KlockanAPI.Application.csproj

        |-- KlockanAPI.Domain
            |-- Models
            |-- Entities
            |-- Enums
            |-- Events
            |-- Exceptions
            |-- Interfaces
            |-- KlockanAPI.Domain.csproj

        |-- KlockanAPI.Infrastructure
            |-- CrossCutting
            |-- Data
                |-- [+] Migrations
                KlockanContext.cs
            |-- Repositories
                |-- Intefaces
                |-- [+] Implementations
            |-- KlockanAPI.Infrastructure.csproj

        |-- KlockanAPI.Presentation
            |-- Certificates
            |-- Controllers
            |-- Http
            |-- Middlewares
            |-- Properties
                |-- launchSettings.json
            |-- Registrations
            |-- KlockanAPI.Presentation.csproj
            |-- Program.cs

    |-- tests
        |-- KlockanAPI.Application.Tests
            |-- Services
            |-- KlockanAPI.Application.Tests.csproj

        |-- KlockanAPI.Infrastructure.Tests
            |-- Repositories
            |-- KlockanAPI.Infrastructure.Tests.csproj

        |-- KlockanAPI.Presentation.Tests
            |-- Controllers
            |-- KlockanAPI.Presentation.Tests.csproj

    |-- KlockanAPI.sln
```

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

1. Move to `KlockanAPI.Infrastructure` directory

   ```bash
   cd KlockanAPI/src/KlockanAPI.Infrastructure/
   ```

1. Run the following command to add a new migration:

   ```bash
   dotnet ef migrations add InitialCreate --output-dir Data/Migrations --project ../KlockanAPI.Infrastructure/ --startup-project ../KlockanAPI.Presentation/
   ```

1. Update the database:
   ```bash
   dotnet ef database update --project ../KlockanAPI.Infrastructure/ --startup-project ../KlockanAPI.Presentation/
   ```

## Configuration

### REST Client (.http files Compiler)

#### VS Code

Add the following configurations to the project's `VS Code` settings file, `.vscode/settings.json`.

```json
{
  "rest-client.environmentVariables": {
    "$shared": {
      "token": "Bearer <authentication_token>",
      "presentation_host": "https://localhost:5001"
    }
  }
}
```
