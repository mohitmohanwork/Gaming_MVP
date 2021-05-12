FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["GameMVP.Web.csproj", ""]
RUN dotnet restore "./GameMVP.Web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "GameMVP.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameMVP.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameMVP.Web.dll"]