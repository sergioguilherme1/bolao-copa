# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/BolaoCopa.API.csproj", "src/"]
RUN dotnet restore "src/BolaoCopa.API.csproj"

COPY src/ src/
RUN dotnet publish "src/BolaoCopa.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

ENTRYPOINT ["dotnet", "BolaoCopa.API.dll"]
