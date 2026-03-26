FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AgreementsServices.csproj", "."]
RUN dotnet restore "AgreementsServices.csproj"
COPY . .
RUN dotnet build "AgreementsServices.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app/build .
EXPOSE 8080
ENTRYPOINT ["dotnet", "AgreementsServices.dll"]