FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
	WORKDIR /backend
	COPY nuget.config .
	COPY backend/Immunilog.sln .

	COPY backend/*/*.csproj ./
	RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done

	RUN --mount=type=cache,id=nuget_xml,target=/root/.nuget/packages\
		dotnet restore Immunilog.sln

FROM restore AS build
	ARG Version=1.0.0

	COPY backend/ .

	RUN 	--mount=type=cache,id=nuget_xml,target=/root/.nuget/packages\
			dotnet build Immunilog.sln -c Release /p:Version=$Version --no-restore

FROM build AS publish
	ARG Version=1.0.0
	WORKDIR /backend/Immunilog.UI
	RUN --mount=type=cache,id=nuget_xml,target=/root/.nuget/packages\
		dotnet publish /p:Version=$Version -c Release -o /app --no-build --no-restore

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
	WORKDIR /backend
	COPY --from=publish /app .

	USER $APP_UID
	EXPOSE 8080
	EXPOSE 8081
	ENTRYPOINT ["dotnet", "Immunilog.UI.dll"]

