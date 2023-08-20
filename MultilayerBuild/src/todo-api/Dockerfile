# Setup
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /api
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build
COPY . .

RUN dotnet restore "todo-api.csproj"
RUN dotnet build "todo-api.csproj" -o /app/build

FROM build AS publish
RUN dotnet publish "todo-api.csproj" -c release -o /app/publish

# Runtime stage
FROM base AS final
WORKDIR /api
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "todo-api.dll"]
