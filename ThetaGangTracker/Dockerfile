FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 4040

ENV ASPNETCORE_URLS=http://+:4040

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ThetaGangTracker/ThetaGangTracker.csproj", "ThetaGangTracker/"]
RUN dotnet restore "ThetaGangTracker/ThetaGangTracker.csproj"
COPY . .
WORKDIR "/src/ThetaGangTracker"
RUN dotnet build "ThetaGangTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThetaGangTracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThetaGangTracker.dll"]
