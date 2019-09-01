FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["APILyfr/APILyfr.csproj", "APILyfr/"]
RUN dotnet restore "APILyfr/APILyfr.csproj"
COPY . .
WORKDIR "/src/APILyfr"
RUN dotnet build "APILyfr.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "APILyfr.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "APILyfr.dll"]