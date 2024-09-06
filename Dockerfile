# Use an official .NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80  # This exposes port 80, where your application will run

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TMDB2.csproj", "./"]  # Copy your project file
RUN dotnet restore "./TMDB2.csproj"  # Restore dependencies
COPY . .  # Copy everything else
WORKDIR "/src/TMDB2"
RUN dotnet build "TMDB2.csproj" -c Release -o /app/build  # Build the app

FROM build AS publish
RUN dotnet publish "TMDB2.csproj" -c Release -o /app/publish  # Publish the app

# Use the runtime image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TMDB2.dll"]