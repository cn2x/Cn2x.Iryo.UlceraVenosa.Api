# Úlcera Venosa API - Docker Setup

## Desenvolvimento Local

### Pré-requisitos
- Docker e Docker Compose instalados
- .NET 9.0 SDK (para executar a API localmente)

### Configuração Rápida

#### 1. Subir apenas o banco de dados para desenvolvimento local:
```bash
# Subir banco PostgreSQL + pgAdmin
docker compose -f docker-compose.dev.yml up -d

# Verificar se os serviços estão rodando
docker compose -f docker-compose.dev.yml ps
```

#### 2. Aplicar migrations e executar API:
```bash
# Aplicar migrations no banco de desenvolvimento (IMPORTANTE: usar connection string explícita)
dotnet ef database update --project src/4-Infrastructure/Cn2x.Iryo.UlceraVenosa.Infrastructure --startup-project src/2-Presentation/Cn2x.Iryo.UlceraVenosa.Api --connection "Host=localhost;Database=ulcera_venosa_dev;Username=dev_user;Password=dev_password123;SSL Mode=Disable"

# Executar a API
dotnet run --project src/2-Presentation/Cn2x.Iryo.UlceraVenosa.Api
```

#### 3. Subir toda a aplicação com Docker:
```bash
# Subir API + Banco + pgAdmin
docker compose up -d

# Ver logs
docker compose logs -f
```

### Serviços Disponíveis

| Serviço | URL | Credenciais |
|---------|-----|-------------|
| API GraphQL | http://localhost:8080/graphql | - |
| pgAdmin | http://localhost:5050 | admin@ulceravenosa.com / admin123 |
| PostgreSQL | localhost:5432 | dev_user / dev_password123 |

### Configuração do pgAdmin

1. Acesse http://localhost:5050
2. Login: `admin@ulceravenosa.com` / `admin123`
3. Adicionar servidor:
   - Name: `Desenvolvimento`
   - Host: `postgres-dev` (se rodando via docker-compose) ou `localhost` (se rodando dev local)
   - Port: `5432`
   - Database: `ulcera_venosa_dev`
   - Username: `dev_user`
   - Password: `dev_password123`

### ⚠️ Aplicando Migrations

**IMPORTANTE**: O Entity Framework pode não detectar automaticamente o ambiente Development. Por isso, é necessário usar a connection string explícita:

```bash
# Comando completo para aplicar migrations
dotnet ef database update \
  --project src/4-Infrastructure/Cn2x.Iryo.UlceraVenosa.Infrastructure \
  --startup-project src/2-Presentation/Cn2x.Iryo.UlceraVenosa.Api \
  --connection "Host=localhost;Database=ulcera_venosa_dev;Username=dev_user;Password=dev_password123;SSL Mode=Disable"
```

**Ou use o Makefile**:
```bash
make db-migrate  # Comando simplificado
```

**Verificar se as tabelas foram criadas**:
```bash
# Conectar no banco via Docker
docker compose -f docker-compose.dev.yml exec postgres-dev psql -U dev_user -d ulcera_venosa_dev

# Listar tabelas
\dt

# Sair
\q
```

### Comandos Úteis

```bash
# Parar todos os serviços
docker compose down

# Parar e remover volumes (limpar dados)
docker compose down -v

# Rebuild da API
docker compose build ulcera-venosa-api

# Ver logs de um serviço específico
docker compose logs postgres-dev

# Executar comando no banco
docker compose exec postgres-dev psql -U dev_user -d ulcera_venosa_dev
```

### Estrutura dos Ambientes

#### Desenvolvimento Local (docker-compose.dev.yml)
- PostgreSQL na porta 5432
- pgAdmin na porta 5050
- API executada via `dotnet run`

#### Produção (appsettings.Production.json)
- Connection String: `Host=pgsql52-farm1.kinghost.net;Database=mytms2;Username=mytm2s;Password=Rickyelton10@;SSL Mode=Disable`

### Troubleshooting

#### Erro de conexão com banco:
```bash
# Verificar se o banco está rodando
docker compose ps

# Ver logs do PostgreSQL
docker compose logs postgres-dev

# Testar conexão
docker compose exec postgres-dev pg_isready -U dev_user
```

#### Resetar banco de desenvolvimento:
```bash
# Parar serviços e remover volumes
docker compose -f docker-compose.dev.yml down -v

# Subir novamente
docker compose -f docker-compose.dev.yml up -d

# Aguardar banco inicializar (10 segundos)
sleep 10

# Aplicar migrations (IMPORTANTE: usar connection string explícita)
dotnet ef database update --project src/4-Infrastructure/Cn2x.Iryo.UlceraVenosa.Infrastructure --startup-project src/2-Presentation/Cn2x.Iryo.UlceraVenosa.Api --connection "Host=localhost;Database=ulcera_venosa_dev;Username=dev_user;Password=dev_password123;SSL Mode=Disable"
```
