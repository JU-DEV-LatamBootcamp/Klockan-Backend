# Klockan-Backend
 
Demo example for the CI/CD

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

## Collecting Code Coverage

To collect code coverage metrics for your .NET tests, use the following command in your terminal:

```sh
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults
```

This command will execute all tests in your solution and collect code coverage data using the cross-platform code coverage tool.

**_The results will be stored in the `./TestResults` directory._**

## Generating Coverage Reports

After collecting code coverage data, you can generate readable reports using ReportGenerator. Use the command below:

```sh
reportgenerator -reports:"<.../coverage.cobertura.xml>;<.../coverage.cobertura.xml>;<.../coverage.cobertura.xml>" -targetdir:"CoverletReports" -reporttypes:"HtmlInline_AzurePipelines;Cobertura" -historydir:./TestResults -filefilters:-*\KlockanAPI.Infrastructure\Data\Migrations*
```

Replace `<.../coverage.cobertura.xml>` with the path to your coverage files.

This command compiles the coverage data into an HTML report.

## Command Summary

```sh
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/Coverage/ ; reportgenerator -reports:"./TestResults/*/*.xml" -targetdir:"./TestResults/CoverletReports" -reporttypes:"HtmlInline_AzurePipelines_Dark;Cobertura" -historydir:./TestResults -filefilters:-*\KlockanAPI.Infrastructure\Data\Migrations* ; start ./TestResults/CoverletReports/index.html
```

## Viewing Coverage Reports

Once you have generated the coverage reports, you can view a detailed HTML report by opening the `index.html` file located in the `CoverletReports` directory.

To open the report, navigate to the `CoverletReports` directory and double-click on the `index.html` file.

Alternatively, you can open it directly from the command line with the following command, depending on your operating system:

For Windows:

```powershell
start CoverletReports\index.html
```

For macOS:

```sh
open CoverletReports/index.html
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

