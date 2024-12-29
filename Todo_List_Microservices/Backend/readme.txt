docker build -t categories-image -f Categories.WebAPI/Dockerfile .
docker run -d --name todos-api -p 5001:8080 todos-image


// "PostgreSql":"User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=postgres"
// "Server":"host.docker.internal,1433;Initial Catalog=MicroCategoriesDb;Persist Security Info=False;User ID=sa;Password=Udemy#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30"


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Todos.WebAPI/Todos.WebAPI.csproj", "Todos.WebAPI/"]
RUN dotnet restore "Todos.WebAPI/Todos.WebAPI.csproj"
COPY . .
WORKDIR "/src/Todos.WebAPI"
RUN dotnet build "Todos.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Todos.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todos.WebAPI.dll"]
