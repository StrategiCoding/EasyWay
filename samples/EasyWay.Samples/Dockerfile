#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["source/Directory.Packages.props", "source/"]
COPY ["source/Directory.Build.props", "source/"]
COPY ["samples/EasyWay.Samples/EasyWay.Samples.csproj", "samples/EasyWay.Samples/"]
COPY ["source/EasyWay.EntityFrameworkCore/EasyWay.EntityFrameworkCore.csproj", "source/EasyWay.EntityFrameworkCore/"]
COPY ["source/EasyWay/EasyWay.csproj", "source/EasyWay/"]
RUN dotnet restore "./samples/EasyWay.Samples/./EasyWay.Samples.csproj"
COPY . .
WORKDIR "/src/samples/EasyWay.Samples"
RUN dotnet build "./EasyWay.Samples.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EasyWay.Samples.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyWay.Samples.dll"]