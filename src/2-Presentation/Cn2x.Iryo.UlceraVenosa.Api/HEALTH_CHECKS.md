# Health Checks - Úlcera Venosa API

Este documento descreve a implementação dos health checks na API de Úlcera Venosa, seguindo as melhores práticas de mercado.

## Endpoints Disponíveis

### 1. `/health` - Health Check Geral
Endpoint principal que executa todos os health checks configurados.

**Resposta:**
- **200 OK**: Todos os serviços estão saudáveis
- **200 OK**: Alguns serviços estão degradados (mas funcionais)
- **503 Service Unavailable**: Serviços críticos estão indisponíveis

### 2. `/health/ready` - Readiness Probe
Verifica se a aplicação está pronta para receber tráfego (dependências externas).

**Health Checks incluídos:**
- PostgreSQL Database

### 3. `/health/live` - Liveness Probe
Verifica se a aplicação está viva (sem dependências externas).

**Comportamento:**
- Sempre retorna 200 OK (não executa health checks)
- Usado por orquestradores como Kubernetes para verificar se o processo está rodando

## Health Checks Implementados

### 1. PostgreSQL Database (Custom)
- **Implementação**: Customizada para maior controle e debug
- **Configuração**: 
  - Timeout: 10 segundos
  - Query de teste: `SELECT 1`
  - Status de falha: Degraded
- **Verificações**:
  - Conexão com o banco
  - Execução de query simples
  - Timeout configurável
  - Informações detalhadas de erro
- **Tags**: `database`, `postgresql`

### 2. Memory Usage (Custom)
- **Implementação**: Customizada
- **Configuração**:
  - Máximo: 1024 MB
  - Status de falha: Degraded
- **Verificações**:
  - Uso de memória do processo atual
  - Comparação com limite configurável
- **Tags**: `system`, `memory`

### 3. Disk Storage (Custom)
- **Implementação**: Customizada
- **Configuração**:
  - Mínimo livre: 1 GB no drive C:
  - Status de falha: Degraded
- **Verificações**:
  - Espaço livre em disco
  - Tamanho total do disco
  - Comparação com limite mínimo
- **Tags**: `system`, `disk`

### 4. Application Health (Custom)
- **Implementação**: Customizada
- **Verificações**:
  - Uso de memória da aplicação
  - Coleta de lixo (GC)
  - Thread Pool
  - Informações do sistema
- **Configuração**:
  - Máximo de memória: 500 MB
  - Status de falha: Degraded
- **Tags**: `application`

## Configuração

As configurações dos health checks estão no `appsettings.json`:

```json
{
  "HealthChecks": {
    "PostgreSQL": {
      "Timeout": "00:00:10"
    },
    "Memory": {
      "MaximumMegabytesAllocated": 1024
    },
    "Disk": {
      "MinimumFreeSpaceGB": 1
    },
    "Application": {
      "MaximumMemoryMB": 500
    }
  }
}
```

## Formato da Resposta

Os health checks retornam respostas no formato JSON com a seguinte estrutura:

```json
{
  "status": "Healthy|Degraded|Unhealthy",
  "totalDuration": "00:00:00.1234567",
  "entries": {
    "postgresql": {
      "data": {
        "Database": "mytms2",
        "ServerVersion": "15.1",
        "State": "Open",
        "Timeout": 10
      },
      "duration": "00:00:00.1234567",
      "status": "Healthy",
      "tags": ["database", "postgresql"]
    },
    "memory": {
      "data": {
        "MemoryUsageMB": 256,
        "MaxMemoryMB": 1024,
        "ProcessId": 12345
      },
      "duration": "00:00:00.0001234",
      "status": "Healthy",
      "tags": ["system", "memory"]
    }
  }
}
```

## Troubleshooting do PostgreSQL

### Problemas Comuns

1. **"Exception while reading from stream"**
   - **Causa**: Problemas de conectividade ou configuração de rede
   - **Solução**: Verificar connection string e configurações de SSL

2. **Timeout**
   - **Causa**: Banco lento ou sobrecarregado
   - **Solução**: Aumentar timeout no `appsettings.json`

3. **Erro de autenticação**
   - **Causa**: Credenciais incorretas
   - **Solução**: Verificar username/password na connection string

### Debug

O health check customizado do PostgreSQL fornece informações detalhadas:

```json
{
  "postgresql": {
    "data": {
      "Database": "nome_do_banco",
      "ServerVersion": "versão_do_postgresql",
      "State": "estado_da_conexão",
      "Timeout": 10,
      "ErrorCode": "código_do_erro",
      "Message": "mensagem_detalhada"
    },
    "status": "Degraded"
  }
}
```

## Monitoramento e Alertas

### Kubernetes
Para uso com Kubernetes, configure os probes:

```yaml
livenessProbe:
  httpGet:
    path: /health/live
    port: 80
  initialDelaySeconds: 30
  periodSeconds: 10

readinessProbe:
  httpGet:
    path: /health/ready
    port: 80
  initialDelaySeconds: 5
  periodSeconds: 5
```

### Docker
Para uso com Docker, adicione ao Dockerfile:

```dockerfile
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
  CMD curl -f http://localhost/health || exit 1
```

## Logs e Troubleshooting

Os health checks são logados automaticamente pelo ASP.NET Core. Para debug, verifique:

1. **Logs da aplicação**: Procure por mensagens relacionadas a health checks
2. **Configuração**: Verifique se as connection strings estão corretas
3. **Timeout**: Ajuste os timeouts se necessário
4. **Recursos**: Verifique se há memória e disco suficientes

## Melhorias Implementadas

- ✅ Health check customizado para PostgreSQL com melhor tratamento de erros
- ✅ Timeout configurável e tratamento de exceções específicas
- ✅ Informações detalhadas de debug para troubleshooting
- ✅ Health checks customizados para memória e disco
- ✅ Configuração flexível via appsettings.json
- ✅ Separação clara entre liveness e readiness probes

## Melhorias Futuras

- [ ] Adicionar health check para cache Redis (se implementado)
- [ ] Adicionar health check para serviços externos
- [ ] Implementar métricas customizadas
- [ ] Adicionar dashboard de health checks
- [ ] Configurar alertas automáticos

## Bibliotecas Utilizadas

- `AspNetCore.HealthChecks.NpgSql` - Health check para PostgreSQL (referência)
- `AspNetCore.HealthChecks.UI.Client` - Cliente para UI de health checks
- `Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore` - Health check para EF Core
- `Microsoft.Extensions.Hosting.Systemd` - Suporte para systemd