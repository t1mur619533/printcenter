#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/PrintCenter.Api/PrintCenter.Api.csproj", "src/PrintCenter.Api/"]
COPY ["src/PrintCenter.Data/PrintCenter.Data.csproj", "src/PrintCenter.Data/"]
COPY ["src/PrintCenter.Domain/PrintCenter.Domain.csproj", "src/PrintCenter.Domain/"]
COPY ["src/PrintCenter.Infrastructure/PrintCenter.Infrastructure.csproj", "src/PrintCenter.Infrastructure/"]
RUN dotnet restore "src/PrintCenter.Api/PrintCenter.Api.csproj"
COPY . .
WORKDIR "/src/src/PrintCenter.Api"
RUN dotnet build "PrintCenter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrintCenter.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrintCenter.Api.dll"]
