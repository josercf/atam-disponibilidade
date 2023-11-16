FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CustomerProfileAPI/CustomerProfileAPI.csproj", "CustomerProfileAPI/"]
RUN dotnet restore "CustomerProfileAPI/CustomerProfileAPI.csproj"
COPY . .
WORKDIR "/src/CustomerProfileAPI"
RUN dotnet build "CustomerProfileAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerProfileAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerProfileAPI.dll"]
