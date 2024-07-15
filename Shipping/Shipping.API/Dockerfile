FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Shipping/Shipping.API/Shipping.API.csproj", "src/Services/Shipping/Shipping.API/"]
RUN dotnet restore "src/Services/Shipping/Shipping.API/Shipping.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Shipping/Shipping.API"
RUN dotnet build "Shipping.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shipping.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shipping.API.dll"]
