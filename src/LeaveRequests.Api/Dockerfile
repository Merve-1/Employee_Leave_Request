FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["LeaveRequests.Api.csproj", "./"]

RUN dotnet restore "LeaveRequests.Api.csproj"

COPY . .

RUN dotnet build "LeaveRequests.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish 

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "LeaveRequests.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish  /p:UseAppHost=false

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LeaveRequests.Api.dll"]
