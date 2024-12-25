# Bước 1: Image Base (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Bước 2: Image Build (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sao chép tệp .sln vào container
COPY VehicleShowroomManagementSystem.sln .

# Sao chép tất cả các tệp .csproj từ tất cả các thư mục con
COPY ["VehicleShowroom.Management.UI/*.csproj", "VehicleShowroom.Management.UI/"]
COPY ["VehicleShowroom.Management.Application/*.csproj", "VehicleShowroom.Management.Application/"]
COPY ["VehicleShowroom.Management.DataAccess/*.csproj", "VehicleShowroom.Management.DataAccess/"]
COPY ["VehicleShowroom.Management.Domain/*.csproj", "VehicleShowroom.Management.Domain/"]
COPY ["VehicleShowroom.Management.Infrastructure/*.csproj", "VehicleShowroom.Management.Infrastructure/"]

# Khôi phục gói NuGet
RUN dotnet restore VehicleShowroomManagementSystem.sln

# Sao chép toàn bộ mã nguồn vào container
COPY . .

WORKDIR "/src"

# Build ứng dụng
RUN dotnet build VehicleShowroomManagementSystem.sln -c Release -o /app/build

# Bước 3: Publish ứng dụng
FROM build AS publish
RUN dotnet publish VehicleShowroomManagementSystem.sln -c Release -o /app/publish /p:UseAppHost=false

# Bước 4: Final Image (Chạy ứng dụng)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "VehicleShowroomManagementSystem.dll"]

# docker build -t showroom-webapp:1.0.0 -f ./Dockerfile .