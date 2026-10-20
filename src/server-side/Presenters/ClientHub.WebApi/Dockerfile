FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/server-side/Core/ClientHub.Domain/*.csproj src/server-side/Core/ClientHub.Domain/
COPY src/server-side/Core/ClientHub.UseCases/*.csproj src/server-side/Core/ClientHub.UseCases/

COPY src/server-side/Infra/ClientHub.Caching/*.csproj src/server-side/Infra/ClientHub.Caching/
COPY src/server-side/Infra/ClientHub.Persistense/*.csproj src/server-side/Infra/ClientHub.Persistense/
COPY src/server-side/Infra/ClientHub.Logging/*.csproj src/server-side/Infra/ClientHub.Logging/

COPY src/server-side/Presenters/ClientHub.WebApi/*.csproj src/server-side/Presenters/ClientHub.WebApi/

RUN dotnet restore src/server-side/Presenters/ClientHub.WebApi/*.csproj

COPY src/server-side/ src/server-side/

WORKDIR /src/src/server-side/Presenters/ClientHub.WebApi
RUN dotnet publish -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ClientHub.WebApi.dll"]