FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["../gamemvp.bgservice.startup/gamemvp.bgservice.startup.csproj", "../gamemvp.bgservice.startup/"]
COPY ["gamemvp.bgservice.csproj", ""]
COPY ["../../gamemvp.common/gamemvp.common.csproj", "../../gamemvp.common/"]
RUN dotnet restore "../gamemvp.bgservice.startup/gamemvp.bgservice.startup.csproj"
COPY . .
WORKDIR "/src/../gamemvp.bgservice.startup"
RUN dotnet build "gamemvp.bgservice.startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gamemvp.bgservice.startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gamemvp.bgservice.startup.dll"]