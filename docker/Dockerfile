FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src . 
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
CMD ["dotnet", "Desafio.dll", "--urls", "http://0.0.0.0:80"]

# cd C:\Users\acpma\source\repos\Desafio
# docker build -t desafio -f docker/Dockerfile . 