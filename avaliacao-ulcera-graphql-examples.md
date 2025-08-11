# Exemplos de GraphQL para AvaliacaoUlcera

## Mutation para Upsert de AvaliacaoUlcera

### 1. Criar Nova Avaliação (Create)

```graphql
mutation CreateAvaliacaoUlcera {
  upsertAvaliacaoUlcera(input: {
    ulceraId: "11111111-1111-1111-1111-111111111111"
    profissionalId: "66666666-6666-6666-6666-666666666666"
    dataAvaliacao: "2024-01-15T10:00:00Z"
    mesesDuracao: 3
    caracteristicas: {
      ceap: {
        clinica: "C6"
        etiologica: "Es"
        anatomica: "As"
        patofisiologica: "Pr"
      }
      dor: {
        intensidade: "MODERADA"
        tipo: "CONTINUA"
      }
    }
    sinaisInflamatorios: {
      dor: true
      calor: false
      rubor: true
      edema: false
      perdaFuncao: false
    }
    medida: {
      comprimento: 2.5
      largura: 1.8
      profundidade: 0.5
    }
  }) {
    id
    ulceraId
    profissionalId
    dataAvaliacao
    mesesDuracao
    caracteristicas {
      ceap {
        clinica
        etiologica
        anatomica
        patofisiologica
      }
      dor {
        intensidade
        tipo
      }
    }
    sinaisInflamatorios {
      dor
      calor
      rubor
      edema
      perdaFuncao
    }
    medida {
      comprimento
      largura
      profundidade
    }
  }
}
```

### 2. Atualizar Avaliação Existente (Update)

```graphql
mutation UpdateAvaliacaoUlcera {
  upsertAvaliacaoUlcera(input: {
    id: "22222222-2222-2222-2222-222222222222"
    ulceraId: "11111111-1111-1111-1111-111111111111"
    profissionalId: "66666666-6666-6666-6666-666666666666"
    dataAvaliacao: "2024-01-20T14:30:00Z"
    mesesDuracao: 4
    caracteristicas: {
      ceap: {
        clinica: "C5"
        etiologica: "Es"
        anatomica: "As"
        patofisiologica: "Pr"
      }
      dor: {
        intensidade: "LEVE"
        tipo: "INTERMITENTE"
      }
    }
    sinaisInflamatorios: {
      dor: false
      calor: false
      rubor: false
      edema: false
      perdaFuncao: false
    }
    medida: {
      comprimento: 2.0
      largura: 1.5
      profundidade: 0.3
    }
  }) {
    id
    ulceraId
    profissionalId
    dataAvaliacao
    mesesDuracao
    caracteristicas {
      ceap {
        clinica
        etiologica
        anatomica
        patofisiologica
      }
      dor {
        intensidade
        tipo
      }
    }
    sinaisInflamatorios {
      dor
      calor
      rubor
      edema
      perdaFuncao
    }
    medida {
      comprimento
      largura
      profundidade
    }
  }
}
```

### 3. Query para Buscar Avaliações

```graphql
query GetAvaliacoesUlcera {
  avaliacoesUlcera(ulceraId: "11111111-1111-1111-1111-111111111111") {
    id
    ulceraId
    profissionalId
    dataAvaliacao
    mesesDuracao
    caracteristicas {
      ceap {
        clinica
        etiologica
        anatomica
        patofisiologica
      }
      dor {
        intensidade
        tipo
      }
    }
    sinaisInflamatorios {
      dor
      calor
      rubor
      edema
      perdaFuncao
    }
    medida {
      comprimento
      largura
      profundidade
    }
    profissional {
      id
      nome
    }
    ulcera {
      id
      paciente {
        nome
      }
    }
  }
}
```

## Campos Obrigatórios

- **ulceraId**: ID da úlcera (obrigatório)
- **profissionalId**: ID do profissional (obrigatório)
- **dataAvaliacao**: Data da avaliação (obrigatório)
- **mesesDuracao**: Duração em meses desde o surgimento (obrigatório)

## Campos Opcionais

- **id**: Se fornecido, atualiza a avaliação existente; se não fornecido, cria nova
- **caracteristicas**: Características da úlcera (CEAP, dor, etc.)
- **sinaisInflamatorios**: Sinais inflamatórios presentes
- **medida**: Medidas da úlcera (comprimento, largura, profundidade)
- **imagens**: Lista de imagens da avaliação
- **exsudatos**: Lista de exsudatos da avaliação

## Resposta

A mutation retorna a entidade `AvaliacaoUlcera` completa com todos os campos populados, incluindo as relações com `Profissional` e `Ulcera`.

## Exemplo de Resposta de Sucesso

```json
{
  "data": {
    "upsertAvaliacaoUlcera": {
      "id": "22222222-2222-2222-2222-222222222222",
      "ulceraId": "11111111-1111-1111-1111-111111111111",
      "profissionalId": "66666666-6666-6666-6666-666666666666",
      "dataAvaliacao": "2024-01-20T14:30:00Z",
      "mesesDuracao": 4,
      "caracteristicas": {
        "ceap": {
          "clinica": "C5",
          "etiologica": "Es",
          "anatomica": "As",
          "patofisiologica": "Pr"
        },
        "dor": {
          "intensidade": "LEVE",
          "tipo": "INTERMITENTE"
        }
      },
      "sinaisInflamatorios": {
        "dor": false,
        "calor": false,
        "rubor": false,
        "edema": false,
        "perdaFuncao": false
      },
      "medida": {
        "comprimento": 2.0,
        "largura": 1.5,
        "profundidade": 0.3
      }
    }
  }
}
```

## Validações

- O campo `profissionalId` é obrigatório
- O campo `ulceraId` deve referenciar uma úlcera existente
- O campo `profissionalId` deve referenciar um profissional existente
- A `dataAvaliacao` não pode ser no futuro
- Os `mesesDuracao` devem ser um número positivo
