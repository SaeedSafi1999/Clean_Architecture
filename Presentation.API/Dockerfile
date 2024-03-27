#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation.API/Presentation.API.csproj", "Presentation.API/"]
COPY ["Application/Core.Application.csproj", "Application/"]
COPY ["Domain/Core.Domain.csproj", "Domain/"]
COPY ["Infrestructure.Persistance/Infrestructure.Persistance.csproj", "Infrestructure.Persistance/"]
COPY ["WebFramework/WebFramework.csproj", "WebFramework/"]
RUN dotnet restore "./Presentation.API/Presentation.API.csproj"
RUN dotnet dev-certs https
COPY . .
WORKDIR "/src/Presentation.API"
RUN dotnet build "./Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.API.dll"]