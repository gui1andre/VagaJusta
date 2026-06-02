FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 5090

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["VagaJusta.API/VagaJusta.API.csproj", "VagaJusta.API/"]
COPY ["VagaJusta.Application/VagaJusta.Application.csproj", "VagaJusta.Application/"]
COPY ["VagaJusta.Domain/VagaJusta.Domain.csproj", "VagaJusta.Domain/"]
COPY ["VagaJusta.Infrastructure/VagaJusta.Infrastructure.csproj", "VagaJusta.Infrastructure/"]
RUN dotnet restore "VagaJusta.API/VagaJusta.API.csproj"
COPY . .
WORKDIR "/src/VagaJusta.API"
RUN dotnet publish "VagaJusta.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "VagaJusta.API.dll"]
