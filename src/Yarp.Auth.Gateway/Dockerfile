FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Yarp.Auth.Gateway/Yarp.Auth.Gateway.csproj", "src/Yarp.Auth.Gateway/"]
RUN dotnet restore "./src/Yarp.Auth.Gateway/Yarp.Auth.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Yarp.Auth.Gateway"
RUN dotnet build "./Yarp.Auth.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Yarp.Auth.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yarp.Auth.Gateway.dll"]