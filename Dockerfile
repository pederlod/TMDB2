# Use an official .NET 8.0 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY TMDB2.csproj ./
RUN dotnet restore "TMDB2.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet build "TMDB2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TMDB2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TMDB2.dll"]
