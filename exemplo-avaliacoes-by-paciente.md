# Exemplo de Uso: avaliacoesByPaciente

## Nova Query GraphQL

A consulta `avaliacoesByPaciente` retorna todas as avaliações de úlceras de um paciente específico, incluindo todos os dados da úlcera e seus objetos relacionados.

### Estrutura de Retorno

```graphql
type AvaliacaoUlcera {
  id: ID!
  ulceraId: UUID!
  profissionalId: UUID
  dataAvaliacao: DateTime!
  mesesDuracao: Int!
  caracteristicas: Caracteristicas!
  sinaisInflamatorios: SinaisInflamatorios!
  medida: Medida
  imagens: [ImagemAvaliacaoUlcera!]!
  exsudatos: [ExsudatoDaAvaliacao!]!
  ulcera: Ulcera!
  profissional: Profissional
}
```

### Exemplo de Query Completa

```graphql
query GetAvaliacoesByPaciente($pacienteId: UUID!) {
  avaliacoesByPaciente(pacienteId: $pacienteId) {
    id
    ulceraId
    profissionalId
    dataAvaliacao
    mesesDuracao
    caracteristicas {
      bordasDefinidas
      tecidoGranulacao
      necrose
      odorFetido
    }
    sinaisInflamatorios {
      calor
      rubor
      edema
      dor
      perdaDeFuncao
      eritema
    }
    medida {
      comprimento
      largura
      profundidade
    }
    imagens {
      id
      descricao
      dataCaptura
      imagem {
        id
        nomeArquivo
        url
        tipoConteudo
      }
    }
    exsudatos {
      id
      exsudato {
        id
        nome
        descricao
      }
    }
    ulcera {
      id
      pacienteId
      desativada
      paciente {
        id
        nome
        cpf
      }
      ceap {
        clinica
        etiologica
        anatomica
        patofisiologica
      }
      topografia {
        # Campos específicos da topografia (Perna ou Pe)
        ... on TopografiaPerna {
          lateralidade {
            id
            nome
          }
          segmentacao {
            id
            nome
          }
          regiaoAnatomica {
            id
            nome
          }
        }
        ... on TopografiaPe {
          lateralidade {
            id
            nome
          }
          regiaoTopograficaPe {
            id
            nome
          }
        }
      }
    }
    profissional {
      id
      nome
      crm
      especialidade
    }
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
    "avaliacoesByPaciente": [
      {
        "id": "11111111-1111-1111-1111-111111111111",
        "ulceraId": "22222222-2222-2222-2222-222222222222",
        "profissionalId": "33333333-3333-3333-3333-333333333333",
        "dataAvaliacao": "2025-01-15T10:30:00Z",
        "mesesDuracao": 6,
        "caracteristicas": {
          "bordasDefinidas": false,
          "tecidoGranulacao": true,
          "necrose": false,
          "odorFetido": false
        },
        "sinaisInflamatorios": {
          "calor": true,
          "rubor": true,
          "edema": false,
          "dor": true,
          "perdaDeFuncao": false,
          "eritema": true
        },
        "medida": {
          "comprimento": 2.5,
          "largura": 1.8,
          "profundidade": 0.3
        },
        "imagens": [
          {
            "id": "44444444-4444-4444-4444-444444444444",
            "descricao": "Foto da úlcera no momento da avaliação",
            "dataCaptura": "2025-01-15T10:30:00Z",
            "imagem": {
              "id": "55555555-5555-5555-5555-555555555555",
              "nomeArquivo": "ulcera_20250115_103000.jpg",
              "url": "https://storage.googleapis.com/ulceras/ulcera_20250115_103000.jpg",
              "tipoConteudo": "image/jpeg"
            }
          }
        ],
        "exsudatos": [
          {
            "id": "66666666-6666-6666-6666-666666666666",
            "exsudato": {
              "id": "77777777-7777-7777-7777-777777777777",
              "nome": "Seroso",
              "descricao": "Transparente ou levemente amarelo, aquoso, fluido."
            }
          }
        ],
        "ulcera": {
          "id": "22222222-2222-2222-2222-222222222222",
          "pacienteId": "123e4567-e89b-12d3-a456-426614174000",
          "desativada": false,
          "paciente": {
            "id": "123e4567-e89b-12d3-a456-426614174000",
            "nome": "João Silva",
            "cpf": "123.456.789-00"
          },
          "ceap": {
            "clinica": "C4",
            "etiologica": "E",
            "anatomica": "A",
            "patofisiologica": "P"
          },
          "topografia": {
            "id": "88888888-8888-8888-8888-888888888888",
            "lateralidade": {
              "id": "99999999-9999-9999-9999-999999999999",
              "nome": "Direita"
            },
            "segmentacao": {
              "id": "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
              "nome": "Terço Médio"
            },
            "regiaoAnatomica": {
              "id": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
              "nome": "Face Medial"
            }
          }
        },
        "profissional": {
          "id": "33333333-3333-3333-3333-333333333333",
          "nome": "Dr. Carlos Santos",
          "crm": "12345-SP",
          "especialidade": "Angiologia"
        }
      }
    ]
  }
}
```

### Características da Query

✅ **Retorna todas as avaliações** de um paciente específico
✅ **Inclui dados completos da úlcera** com todas as propriedades
✅ **Carrega objetos relacionados** (Paciente, Profissional, Topografia, etc.)
✅ **Ordenação por data** (mais recente primeiro)
✅ **Filtro automático** para úlceras não desativadas
✅ **Performance otimizada** com carregamento eficiente (Include/ThenInclude)

### Casos de Uso

- 📊 **Dashboard do paciente**: Visualizar histórico completo de avaliações
- 🏥 **Relatórios médicos**: Análise temporal da evolução da úlcera
- 📈 **Acompanhamento**: Comparar medidas e características ao longo do tempo
- 🖼️ **Galeria de imagens**: Acessar todas as fotos das avaliações
- 📋 **Documentação**: Histórico completo para auditoria médica
