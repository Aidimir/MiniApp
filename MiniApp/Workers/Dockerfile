FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
ENV DOTNET_URLS=http://+:5000
WORKDIR /app
EXPOSE 8090
EXPOSE 8091

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workers/Workers.csproj", "Workers/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Dal/Dal.csproj", "Dal/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "Workers/Workers.csproj"
COPY . .
WORKDIR "/src/Workers"
RUN dotnet build "Workers.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Workers.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

COPY Workers/workers_appsettings.json /app/publish/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workers.dll"]
