# Etapa 1: Build (Usa o SDK completo para compilar)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["BolaoCopa.API.csproj", "./"]
RUN dotnet restore "BolaoCopa.API.csproj"

COPY . .
RUN dotnet publish "BolaoCopa.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime (Usa apenas a imagem otimizada para rodar)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

ENTRYPOINT ["dotnet", "BolaoCopa.API.dll"]
