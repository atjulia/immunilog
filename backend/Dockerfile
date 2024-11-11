FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY Immunilog.UI/*.csproj ./Immunilog.UI/
COPY Immunilog.Services/*.csproj ./Immunilog.Services/
COPY Immunilog.Repositories/*.csproj ./Immunilog.Repositories/
COPY Immunilog.Domain/*.csproj ./Immunilog.Domain/

RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Immunilog.UI.dll"]
