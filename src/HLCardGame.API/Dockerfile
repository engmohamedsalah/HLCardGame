#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/nightly/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/nightly/aspnet:3.1 AS build
WORKDIR /src
COPY ["HLCardGame.API/HLCardGame.API.csproj", "HLCardGame.API/"]
COPY ["HLCardGame.Application/HLCardGame.Application.csproj", "HLCardGame.Application/"]
COPY ["HLCardGame.Domain/HLCardGame.Domain.csproj", "HLCardGame.Domain/"]
COPY ["HLCardGame.Infrastructure/HLCardGame.Infrastructure.csproj", "HLCardGame.Infrastructure/"]
RUN dotnet restore "HLCardGame.API/HLCardGame.API.csproj"
COPY . .
WORKDIR "/src/HLCardGame.API"
RUN dotnet build "HLCardGame.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HLCardGame.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HLCardGame.API.dll"]