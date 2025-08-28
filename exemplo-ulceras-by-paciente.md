# Exemplo de Uso: ulcerasByPaciente com Total de Avaliações

## Nova Query GraphQL

A consulta `ulcerasByPaciente` agora retorna úlceras com o total de avaliações para cada uma.

### Estrutura de Retorno

```graphql
type UlceraWithTotalAvaliacoes {
  ulcera: Ulcera!
  totalAvaliacoes: Int!
}
```

### Exemplo de Query

```graphql
query GetUlcerasByPaciente($pacienteId: UUID!) {
  ulcerasByPaciente(pacienteId: $pacienteId) {
    ulcera {
      id
      pacienteId
      paciente {
        nome
        cpf
      }
      topografia {
        # campos específicos da topografia
      }
      avaliacoes {
        id
        dataAvaliacao
      }
      desativada
    }
    totalAvaliacoes
  }
}
```

### Variáveis

```json
{
  "pacienteId": "123e4567-e89b-12d3-a456-426614174000"
}
```

### Exemplo de Resposta

```json
{
  "data": {
    "ulcerasByPaciente": [
      {
        "ulcera": {
          "id": "11111111-1111-1111-1111-111111111111",
          "pacienteId": "123e4567-e89b-12d3-a456-426614174000",
          "paciente": {
            "nome": "João Silva",
            "cpf": "123.456.789-00"
          },
          "topografia": {
            "lateralidade": "Direita",
            "segmentacao": "Terço Médio",
            "regiaoAnatomica": "Face Posterior"
          },
          "avaliacoes": [
            {
              "id": "22222222-2222-2222-2222-222222222222",
              "dataAvaliacao": "2025-08-22T10:00:00Z"
            }
          ],
          "desativada": false
        },
        "totalAvaliacoes": 1
      },
      {
        "ulcera": {
          "id": "33333333-3333-3333-3333-333333333333",
          "pacienteId": "123e4567-e89b-12d3-a456-426614174000",
          "paciente": {
            "nome": "João Silva",
            "cpf": "123.456.789-00"
          },
          "topografia": {
            "lateralidade": "Esquerda",
            "regiaoTopograficaPe": "Dorso"
          },
          "avaliacoes": [
            {
              "id": "44444444-4444-4444-4444-444444444444",
              "dataAvaliacao": "2025-08-21T14:30:00Z"
            },
            {
              "id": "55555555-5555-5555-5555-555555555555",
              "dataAvaliacao": "2025-08-20T09:15:00Z"
            }
          ],
          "desativada": false
        },
        "totalAvaliacoes": 2
      }
    ]
  }
}
```

## Benefícios da Nova Implementação

1. **Contagem Eficiente**: O total de avaliações é calculado no banco de dados
2. **Dados Relacionados**: Inclui todas as informações da úlcera e paciente
3. **Flexibilidade**: Permite acessar tanto os dados da úlcera quanto o total de avaliações
4. **Performance**: Usa `AsNoTracking()` para melhor performance em consultas de leitura

## Uso no Frontend

```typescript
interface UlceraWithTotalAvaliacoes {
  ulcera: {
    id: string;
    pacienteId: string;
    paciente: {
      nome: string;
      cpf: string;
    };
    topografia: any;
    avaliacoes: Array<{
      id: string;
      dataAvaliacao: string;
    }>;
    desativada: boolean;
  };
  totalAvaliacoes: number;
}

// Exemplo de uso
const ulceras = await graphqlClient.query({
  query: GET_ULCERAS_BY_PACIENTE,
  variables: { pacienteId: "123e4567-e89b-12d3-a456-426614174000" }
});

ulceras.data.ulcerasByPaciente.forEach(item => {
  console.log(`Úlcera ${item.ulcera.id}: ${item.totalAvaliacoes} avaliações`);
});
```
