FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /App
COPY . .
RUN dotnet publish ./RichardH-P0/ -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run
WORKDIR /App
COPY --from=build /App/out/ .
COPY ./RichardH-P0/P0-DB.txt .
ENTRYPOINT ["dotnet", "RichardH-P0.App.dll"]