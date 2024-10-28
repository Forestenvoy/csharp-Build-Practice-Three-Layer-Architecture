# 此階段用於以快速模式從 VS 執行時 (偵錯設定的預設值)
FROM mcr.microsoft.com/dotnet/aspnet:8.0.8-alpine3.19 AS base
WORKDIR /app
EXPOSE 80
RUN apk add --no-cache tzdata icu-libs vim busybox-extras bind-tools curl
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# 此階段是用來組建服務專案
FROM mcr.microsoft.com/dotnet/sdk:8.0.401-1-alpine3.19 AS build
WORKDIR /src
COPY ["src/Practice.Web/Practice.Web.csproj", "Practice.Web/"]
COPY ["src/Practice.Common/Practice.Common.csproj", "Practice.Common/"]
COPY ["src/Practice.Repository/Practice.Repository.csproj", "Practice.Repository/"]
COPY ["src/Practice.Service/Practice.Service.csproj", "Practice.Service/"]
RUN dotnet restore "Practice.Web/Practice.Web.csproj"
COPY src .
WORKDIR "/src/Practice.Web"
RUN dotnet build "Practice.Web.csproj" -c Release -o /app/build

# 此階段可用來發佈要複製到最終階段的服務專案
FROM build AS publish
RUN dotnet publish "Practice.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 此階段用於生產環境，或以一般模式從 VS 執行時 (未使用偵錯設定時的預設值)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Practice.Web.dll"]