FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SpeedReading.Api/SpeedReading.Api.csproj", "SpeedReading.Api/"]
COPY ["SpeedReading.Persistent/SpeedReading.Persistent.csproj", "SpeedReading.Persistent/"]
COPY ["SpeedReading.Application/SpeedReading.Application.csproj", "SpeedReading.Application/"]
COPY ["SpeedReading.Domain/SpeedReading.Domain.csproj", "SpeedReading.Domain/"]
RUN dotnet restore "SpeedReading.Api/SpeedReading.Api.csproj"
COPY . .
WORKDIR "/src/SpeedReading.Api"
RUN dotnet publish "SpeedReading.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SpeedReading.Api.dll"]