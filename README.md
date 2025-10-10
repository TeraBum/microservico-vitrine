# API VitrineService
## Visão Geral da API
A Vitrine API é um microserviço desenvolvido em .NET 8 com o SDK Microsoft.NET.Sdk.Web. Seu objetivo é centralizar dados de produtos e disponibilizá-los para outros módulos do ecossistema (carrinho, pedidos, estoque) por meio de endpoints RESTful padronizados.

A aplicação segue princípios de modularidade, baixo acoplamento e alta coesão, ideal para integração em arquiteturas de microserviços.

### Tipo de API
A aplicação é uma API RESTful, usando ASP.NET Core Web API e protocolo HTTP. Os recursos são organizados por URI, garantindo estrutura clara, escalável e interoperável.

### Modelo de Maturidade REST (Richardson)
A API está no Nível 2 do Modelo de Richardson:

Nível 0 – Swamp of POX: ❌ Um único endpoint, sem semântica HTTP

Nível 1 – Recursos: ✅ URIs específicas para cada recurso

Nível 2 – Verbos HTTP: ✅ Métodos HTTP adequados (GET, POST, etc.) e status padronizados (200, 404)

Nível 3 – HATEOAS: 🚧 Em desenvolvimento, retornando links dinâmicos

### Endpoints Principais
- GET /api/v1/vitrine/Product – Lista produtos disponíveis
- GET /api/v1/vitrine/Product/{id} – Detalhes de um produto específico
- GET /api/v1/vitrine/Product/{id}/stock – Estoque do produto

Os endpoints seguem estrutura hierárquica e semântica, refletindo a relação recurso/sub-recurso.

## Execução com Docker
Arquivos principais:
- Dockerfile – define a imagem da aplicação
- docker-compose.yml – orquestra serviços e variáveis de ambiente
- .env – contém variáveis sensíveis (host, porta, usuário, senha, banco)

#### Exemplo de serviço no docker-compose.yml:
services:
vitrine-api:
build: .
container_name: vitrine-api
ports:
- "8080:8080"
env_file:
- .env
environment:
- ConnectionStrings__DefaultConnection=Host=${DB_HOST};Port=${DB_PORT};Database=${DB_NAME};Username=${DB_USER};Password=${DB_PASSWORD};Ssl Mode=Require;Trust Server Certificate=true
restart: unless-stopped

#### Como Rodar a Aplicação

- Modo local (.NET CLI):
  dotnet run
  Disponível em: http://localhost:8080/swagger

- Docker Compose:
  docker compose up
  Para rebuildar após mudanças: docker compose up --build

#### Conexão com Banco de Dados (Supabase / RDS)
Configurada via variáveis de ambiente no arquivo .env.

#### Exemplo de .env:
DB_HOST=aws-1-sa-east-1.pooler.supabase.com
DB_PORT=6543
DB_NAME=postgres
DB_USER=postgres.smjdaavxsnbmrdrvejsu
DB_PASSWORD=S3nhaS3gur@:

#### No appsettings.json:
{
"ConnectionStrings": {
"DefaultConnection": "Host=${DB_HOST};Port=${DB_PORT};Database=${DB_NAME};Username=${DB_USER};Password=${DB_PASSWORD};Ssl Mode=Require;Trust Server Certificate=true"
}
}

*Benefícios:*
- Segurança: senhas e dados sensíveis fora do código
- Portabilidade: configuração consistente entre ambientes
- Escalabilidade: fácil em produção com Docker

## Conclusão
- A API segue padrão REST, construída em .NET 8, no Nível 2 de maturidade, executável via .NET CLI ou Docker Compose, garantindo:
- Comunicação clara e organizada com diferentes clientes
- Portabilidade e segurança com variáveis de ambiente
- Facilidade de escalabilidade em produção
