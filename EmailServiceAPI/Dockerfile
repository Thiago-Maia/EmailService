#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
USER app
WORKDIR /app
COPY ./publish /app
#EXPOSE 8080
#EXPOSE 8081

#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["./EmailServiceAPI.csproj", "EmailServiceAPI/"]
#RUN dotnet restore "./EmailServiceAPI/EmailServiceAPI.csproj"
#COPY . .
#WORKDIR "/src/EmailServiceAPI"
#RUN dotnet build "./EmailServiceAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./EmailServiceAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "EmailServiceAPI.dll"]