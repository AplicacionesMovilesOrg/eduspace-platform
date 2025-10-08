FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FULLSTACKFURY.EduSpace.API.csproj", "./"]
RUN dotnet restore "FULLSTACKFURY.EduSpace.API.csproj"
COPY . .
RUN dotnet build "FULLSTACKFURY.EduSpace.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FULLSTACKFURY.EduSpace.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FULLSTACKFURY.EduSpace.API.dll"]