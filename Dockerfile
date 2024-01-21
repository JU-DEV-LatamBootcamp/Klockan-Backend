FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY KlockanAPI/*.sln .
COPY KlockanAPI/src/KlockanAPI.Application/*.csproj src/KlockanAPI.Application/
COPY KlockanAPI/src/KlockanAPI.Domain/*.csproj src/KlockanAPI.Domain/
COPY KlockanAPI/src/KlockanAPI.Infrastructure/*.csproj src/KlockanAPI.Infrastructure/
COPY KlockanAPI/src/KlockanAPI.Presentation/*.csproj src/KlockanAPI.Presentation/

RUN dotnet restore src/KlockanAPI.Presentation/KlockanAPI.Presentation.csproj

# Copy everything else and build
# COPY KlockanAPI/src src
COPY KlockanAPI/src/KlockanAPI.Application/. src/KlockanAPI.Application/
COPY KlockanAPI/src/KlockanAPI.Domain/. ./src/KlockanAPI.Domain/
COPY KlockanAPI/src/KlockanAPI.Infrastructure/. ./src/KlockanAPI.Infrastructure/
COPY KlockanAPI/src/KlockanAPI.Presentation/. ./src/KlockanAPI.Presentation/

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out/ .

ENTRYPOINT ["dotnet", "KlockanAPI.Presentation.dll"]