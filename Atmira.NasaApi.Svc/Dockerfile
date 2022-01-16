#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Atmira.NasaApi.Svc/Atmira.NasaApi.Svc.csproj", "Atmira.NasaApi.Svc/"]
RUN dotnet restore "Atmira.NasaApi.Svc/Atmira.NasaApi.Svc.csproj"
COPY . .
WORKDIR "/src/Atmira.NasaApi.Svc"
RUN dotnet build "Atmira.NasaApi.Svc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Atmira.NasaApi.Svc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Atmira.NasaApi.Svc.dll"]