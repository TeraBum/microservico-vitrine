# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código e compila
COPY . .
RUN dotnet publish -c Release -o /app

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia a saída da etapa de build
COPY --from=build /app .

# Configura variáveis de ambiente (connection string etc.)
# ⚠️ Aqui você pode sobrescrever valores ao rodar o container com -e
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Variáveis específicas do Supabase
ENV DB_HOST=aws-1-sa-east-1.pooler.supabase.com
ENV DB_PORT=6543
ENV DB_NAME=postgres
ENV DB_USER=postgres.smjdaavxsnbmrdrvejsu
ENV DB_PASSWORD=S3nhaS3gur@:(

# Expor a porta
EXPOSE 8080

# Rodar a API
ENTRYPOINT ["dotnet", "Vitrine.dll"]
