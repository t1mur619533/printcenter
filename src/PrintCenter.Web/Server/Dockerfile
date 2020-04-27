#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/PrintCenter.Web/Server/PrintCenter.Web.Server.csproj", "src/PrintCenter.Web/Server/"]
COPY ["src/PrintCenter.Web/Client/PrintCenter.Web.Client.csproj", "src/PrintCenter.Web/Client/"]
COPY ["src/PrintCenter.Shared/PrintCenter.Shared.csproj", "src/PrintCenter.Shared/"]
RUN dotnet restore "src/PrintCenter.Web/Server/PrintCenter.Web.Server.csproj"
COPY . .
WORKDIR "/src/src/PrintCenter.Web/Server"
RUN dotnet build "PrintCenter.Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrintCenter.Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrintCenter.Web.Server.dll"]
