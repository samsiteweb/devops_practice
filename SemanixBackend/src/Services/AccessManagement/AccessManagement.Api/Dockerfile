# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/AccessManagement/AccessManagement.Api/AccessManagement.Api.csproj", "src/Services/AccessManagement/AccessManagement.Api/"]
COPY ["src/Services/AccessManagement/AccessManagement.Application/AccessManagement.Application.csproj", "src/Services/AccessManagement/AccessManagement.Application/"]
COPY ["src/Services/AccessManagement/AccessManagement.Domain/AccessManagement.Domain.csproj", "src/Services/AccessManagement/AccessManagement.Domain/"]
COPY ["src/Services/AccessManagement/AccessManagement.Infrastructure/AccessManagement.Infrastructure.csproj", "src/Services/AccessManagement/AccessManagement.Infrastructure/"]
RUN dotnet restore "src/Services/AccessManagement/AccessManagement.Api/AccessManagement.Api.csproj"
COPY ["src/Services/AccessManagement/AccessManagement.Api/", "src/Services/AccessManagement/AccessManagement.Api/"]
COPY ["src/Services/AccessManagement/AccessManagement.Application/", "src/Services/AccessManagement/AccessManagement.Application/"]
COPY ["src/Services/AccessManagement/AccessManagement.Domain/", "src/Services/AccessManagement/AccessManagement.Domain/"]
COPY ["src/Services/AccessManagement/AccessManagement.Infrastructure/", "src/Services/AccessManagement/AccessManagement.Infrastructure/"]
RUN dotnet build "src/Services/AccessManagement/AccessManagement.Api/AccessManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Services/AccessManagement/AccessManagement.Api/AccessManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccessManagement.Api.dll"]