#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/ContentService/ContentService.Api/ContentService.Api.csproj", "src/Services/ContentService/ContentService.Api/"]
COPY ["src/BuildingBlocks/LocalMessageTable/LocalMessage/LocalMessage.csproj", "src/BuildingBlocks/LocalMessageTable/LocalMessage/"]
COPY ["src/StudyOnline.Common/StudyOnline.Common.csproj", "src/StudyOnline.Common/"]
COPY ["src/Services/MediaService/MediaService.Model/MediaService.Model.csproj", "src/Services/MediaService/MediaService.Model/"]
COPY ["src/Services/OrderService/OrderService.Api/OrderService.Api.csproj", "src/Services/OrderService/OrderService.Api/"]
COPY ["src/Services/OrderService/OrderService.Domain/OrderService.Domain.csproj", "src/Services/OrderService/OrderService.Domain/"]
COPY ["src/Services/OrderService/OrderService.Model/OrderService.Model.csproj", "src/Services/OrderService/OrderService.Model/"]
COPY ["src/Services/ContentService/ContentService.Model/ContentService.Model.csproj", "src/Services/ContentService/ContentService.Model/"]
RUN dotnet restore "src/Services/ContentService/ContentService.Api/ContentService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/ContentService/ContentService.Api"
RUN dotnet build "ContentService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContentService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContentService.Api.dll"]