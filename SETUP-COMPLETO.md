# 🩺 Úlcera Venosa API - Setup Completo

## ✅ Configuração Docker Finalizada

### 📁 Arquivos Criados

1. **docker-compose.yml** - Configuração completa (API + Banco + pgAdmin)
2. **docker-compose.dev.yml** - Apenas banco e pgAdmin para desenvolvimento local
3. **scripts/init.sql** - Script de inicialização do PostgreSQL
4. **README-Docker.md** - Documentação completa do Docker
5. **.env.example** - Exemplo de variáveis de ambiente
6. **Makefile** - Comandos facilitados para desenvolvimento

### 🗄️ Configurações de Banco

#### Desenvolvimento Local
- **Host**: localhost:5432
- **Database**: ulcera_venosa_dev
- **Username**: dev_user
- **Password**: dev_password123

#### Produção (KingHost)
- **Host**: pgsql52-farm1.kinghost.net
- **Database**: mytms2
- **Username**: mytm2s
- **Password**: Rickyelton10@

### 🌐 Serviços Disponíveis

| Serviço | URL | Status |
|---------|-----|--------|
| PostgreSQL Dev | localhost:5432 | ✅ Rodando |
| pgAdmin | http://localhost:5050 | ✅ Rodando |
| API GraphQL | http://localhost:8080/graphql | ⏳ Para executar |

### 🚀 Comandos Rápidos

```bash
# Subir apenas banco (recomendado para desenvolvimento)
make dev-up

# Aplicar migrations (IMPORTANTE: usar connection string explícita)
make db-migrate

# OU aplicar manualmente:
dotnet ef database update \
  --project src/4-Infrastructure/Cn2x.Iryo.UlceraVenosa.Infrastructure \
  --startup-project src/2-Presentation/Cn2x.Iryo.UlceraVenosa.Api \
  --connection "Host=localhost;Database=ulcera_venosa_dev;Username=dev_user;Password=dev_password123;SSL Mode=Disable"

# Executar API localmente
make api-run

# Subir aplicação completa
make full-up

# Parar tudo
make dev-down
```

### 📋 appsettings Atualizados

✅ **appsettings.Development.json** - Configurado para banco Docker local
✅ **appsettings.Production.json** - Mantém configuração KingHost

### 🛠️ Próximos Passos

1. **Aplicar Migrations** (se ainda não foi feito):
   ```bash
   make db-migrate
   ```

2. **Testar API**:
   ```bash
   make api-run
   ```

3. **Acessar pgAdmin**:
   - URL: http://localhost:5050
   - Email: admin@ulceravenosa.com
   - Senha: admin123

4. **Verificar tabelas criadas**:
   ```bash
   docker compose -f docker-compose.dev.yml exec postgres-dev psql -U dev_user -d ulcera_venosa_dev -c "\dt"
   ```

5. **Executar testes**:
   ```bash
   make test
   ```

### 💡 Dicas de Uso

- Use `docker-compose.dev.yml` para desenvolvimento local
- Use `docker-compose.yml` para teste completo da aplicação
- O Makefile facilita todas as operações comuns
- pgAdmin está configurado para gerenciar o banco visualmente

### 🔧 Troubleshooting

Se houver problemas:
```bash
# Ver logs
docker compose -f docker-compose.dev.yml logs

# Resetar tudo
make clean

# Setup inicial
make setup
```

---
**🎉 Setup Docker concluído com sucesso!**
