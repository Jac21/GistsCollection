FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY RestfulJobPattern/RestfulJobPattern.csproj RestfulJobPattern/
RUN dotnet restore RestfulJobPattern/RestfulJobPattern.csproj
COPY . .
WORKDIR /src/RestfulJobPattern
RUN dotnet build RestfulJobPattern.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RestfulJobPattern.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestfulJobPattern.dll"]
