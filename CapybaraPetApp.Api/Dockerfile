FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CapybaraPetApp.Api/CapybaraPetApp.Api.csproj", "CapybaraPetApp.Api/"]
RUN dotnet restore "./CapybaraPetApp.Api/CapybaraPetApp.Api.csproj"
COPY . .
WORKDIR "/src/CapybaraPetApp.Api"
RUN dotnet publish "./CapybaraPetApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=build /app/publish .

##This sets the port for incoming requests.
ENV ASPNETCORE_URLS=http://+:8080 
USER app
ENTRYPOINT ["dotnet", "CapybaraPetApp.Api.dll"]