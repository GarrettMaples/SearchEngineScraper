﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SearchEngineScraper.Api/SearchEngineScraper.Api.csproj", "SearchEngineScraper.Api/"]
RUN dotnet restore "SearchEngineScraper.Api/SearchEngineScraper.Api.csproj"
COPY . .
WORKDIR "/src/SearchEngineScraper.Api"
RUN dotnet build "SearchEngineScraper.Api.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR /src
COPY ["SearchEngineScraper.UnitTests/SearchEngineScraper.UnitTests.csproj", "SearchEngineScraper.UnitTests/"]
COPY ["SearchEngineScraper.IntegrationTests/SearchEngineScraper.IntegrationTests.csproj", "SearchEngineScraper.IntegrationTests/"]

WORKDIR 'SearchEngineScraper.UnitTests/'
RUN dotnet test

WORKDIR ../'SearchEngineScraper.IntegrationTests/'
RUN dotnet test

FROM build AS publish
RUN dotnet publish "SearchEngineScraper.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchEngineScraper.Api.dll"]
