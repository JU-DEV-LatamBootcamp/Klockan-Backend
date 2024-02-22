FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY KlockanAPI/*.sln .
COPY KlockanAPI/src/KlockanAPI.Application/*.csproj src/KlockanAPI.Application/
COPY KlockanAPI/src/KlockanAPI.Domain/*.csproj src/KlockanAPI.Domain/
COPY KlockanAPI/src/KlockanAPI.Infrastructure/*.csproj src/KlockanAPI.Infrastructure/
COPY KlockanAPI/src/KlockanAPI.Presentation/*.csproj src/KlockanAPI.Presentation/

COPY KlockanAPI/tests/KlockanAPI.Application.Tests/*.csproj tests/KlockanAPI.Application.Tests/
COPY KlockanAPI/tests/KlockanAPI.Infraestructure.Tests/*.csproj tests/KlockanAPI.Infraestructure.Tests/
COPY KlockanAPI/tests/KlockanAPI.Presentation.Tests/*.csproj tests/KlockanAPI.Presentation.Tests/
COPY KlockanAPI/tests/KlockanAPI.IntegrationTests/*.csproj tests/KlockanAPI.IntegrationTests/

RUN dotnet restore src/KlockanAPI.Presentation/KlockanAPI.Presentation.csproj

# Copy everything else and build
COPY KlockanAPI/src/KlockanAPI.Application/. ./src/KlockanAPI.Application/
COPY KlockanAPI/src/KlockanAPI.Domain/. ./src/KlockanAPI.Domain/
COPY KlockanAPI/src/KlockanAPI.Infrastructure/. ./src/KlockanAPI.Infrastructure/
COPY KlockanAPI/src/KlockanAPI.Presentation/. ./src/KlockanAPI.Presentation/

COPY KlockanAPI/tests/KlockanAPI.Application.Tests/. ./tests/KlockanAPI.Application.Tests/
COPY KlockanAPI/tests/KlockanAPI.Infraestructure.Tests/. ./tests/KlockanAPI.Infraestructure.Tests/
COPY KlockanAPI/tests/KlockanAPI.Presentation.Tests/. ./tests/KlockanAPI.Presentation.Tests/
COPY KlockanAPI/tests/KlockanAPI.IntegrationTests/. ./tests/KlockanAPI.IntegrationTests/

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out/ .
COPY KlockanAPI/src/KlockanAPI.Presentation/Certificates/. /app/Certificates

ENTRYPOINT ["dotnet", "KlockanAPI.Presentation.dll"]
