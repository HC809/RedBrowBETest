#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Imagen de produccion (base / runtime .net)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Imagen build (sdk .net) / copia el archivo de la solucion csproj (restaurar paquetes) / construye la solucion
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RedbrowBackendTest.csproj", "."]
RUN dotnet restore "./RedbrowBackendTest.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RedbrowBackendTest.csproj" -c Release -o /app/build

#Imagen publish
FROM build AS publish
RUN dotnet publish "RedbrowBackendTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

#Mover publish al final (directamente a la base)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedbrowBackendTest.dll"]