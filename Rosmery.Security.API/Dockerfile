FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Rosmery.Security.API/Rosmery.Security.API.csproj Rosmery.Security.API/
COPY Rosmery.Security.Identity/Rosmery.Security.Identity.csproj Rosmery.Security.Identity/
RUN dotnet restore Rosmery.Security.API/Rosmery.Security.API.csproj
COPY . .
WORKDIR /src/Rosmery.Security.API
RUN dotnet build Rosmery.Security.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Rosmery.Security.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Rosmery.Security.API.dll"]
