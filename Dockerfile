FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DOG.RepoNook.csproj",""]
RUN dotnet restore "./DOG.RepoNook"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DOG.RepoNook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DOG.RepoNook.csproj" -c Release -o /app/publish

# Make sure the app binds to port 80
ENV ASPNETCORE_URLS http://*:80

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DOG.RepoNook.dll"]
