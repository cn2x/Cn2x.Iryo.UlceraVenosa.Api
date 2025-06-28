# Cn2x.Iryo.UlceraVenosa.Api

API completa em .NET 9.0 seguindo Clean Architecture para gerenciamento de úlceras venosas.

## 🏗️ Arquitetura

A solução segue os princípios da Clean Architecture com as seguintes camadas:

- **Domain**: Entidades, interfaces e modelos do domínio
- **Application**: GraphQL, queries, mutations e lógica de aplicação
- **Infrastructure**: DbContext, repositórios e configurações de banco
- **API**: Controllers, middleware e configurações da API

## 🚀 Tecnologias

- **.NET 9.0**
- **Entity Framework Core** com PostgreSQL
- **HotChocolate 15.x** para GraphQL
- **Clean Architecture**
- **Repository Pattern** com Assembly Scanning
- **Response Compression** (Brotli + Gzip)
- **Memory Cache**
- **Otimizações de Kestrel**

## 📋 Pré-requisitos

- .NET 9.0 SDK
- PostgreSQL
- Docker (opcional)

## 🔧 Configuração

### 1. Configuração do Banco de Dados

Edite o arquivo `appsettings.json` e configure a connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ulcera_venosa_db;Username=postgres;Password=your_password"
  }
}
```

### 2. Executando a Aplicação

```bash
# Restaurar dependências
dotnet restore

# Executar em desenvolvimento
dotnet run --project Cn2x.Iryo.UlceraVenosa.Api

# Executar com Docker
docker build -t ulcera-venosa-api .
docker run -p 5000:80 ulcera-venosa-api
```

## 📚 Endpoints

### GraphQL
- **Playground**: `http://localhost:5000/graphql`
- **Banana Cake Pop**: `http://localhost:5000/graphql/`

### REST API
- **Health Check**: `GET /health`
- **Swagger**: `http://localhost:5000/swagger` (apenas desenvolvimento)

## 🔒 CORS

Configurado para os seguintes domínios:
- `http://localhost:3000`
- `https://localhost:3000`
- `http://localhost:9000`
- `https://localhost:9000`
- `https://firebasestorage.googleapis.com`
- `https://cdn.jsdelivr.net`

## 🧪 Testes

```bash
# Executar testes
dotnet test

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage"
```

## 📦 Deploy

### Azure App Service
1. Configure as variáveis de ambiente
2. Deploy via Azure DevOps ou GitHub Actions
3. Configure a connection string no Azure

### Docker
```bash
docker build -t ulcera-venosa-api .
docker run -p 5000:80 -e ConnectionStrings__DefaultConnection="your_connection_string" ulcera-venosa-api
```

## 🔧 Configurações de Performance

- **Garbage Collector**: Otimizado para performance
- **Thread Pool**: Configurado automaticamente
- **Kestrel**: Limites de conexão configurados
- **Response Compression**: Brotli e Gzip habilitados
- **Memory Cache**: Configurado para queries frequentes

## 📝 Logs

Logs estruturados configurados para:
- Console (desenvolvimento)
- Debug (desenvolvimento)
- Arquivo (produção)

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature
3. Commit suas mudanças
4. Push para a branch
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT.
