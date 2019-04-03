FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
FROM build AS publish
RUN dotnet publish "WebAdvert.SearchApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebAdvert.SearchApi.dll"]