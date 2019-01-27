FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Rosmery.Security.STS/Rosmery.Security.STS.csproj Rosmery.Security.STS/
COPY Rosmery.Security.Identity/Rosmery.Security.Identity.csproj Rosmery.Security.Identity/
RUN dotnet restore Rosmery.Security.STS/Rosmery.Security.STS.csproj
COPY . .
WORKDIR /src/Rosmery.Security.STS
RUN dotnet build Rosmery.Security.STS.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Rosmery.Security.STS.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Rosmery.Security.STS.dll"]
