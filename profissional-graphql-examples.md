# Exemplos de uso do GraphQL para Profissional

## Queries

### Obter todos os profissionais
```graphql
query {
  profissionais {
    id
    nome
    criadoEm
    atualizadoEm
  }
}
```

### Obter um profissional específico
```graphql
query {
  profissional(id: "12345678-1234-1234-1234-123456789012") {
    id
    nome
    criadoEm
    atualizadoEm
    avaliacoes {
      id
      dataAvaliacao
      mesesDuracao
    }
  }
}
```

## Mutations

### Criar um novo profissional
```graphql
mutation {
  upsertProfissional(
    nome: "Dr. João Silva"
  )
}
```

### Atualizar um profissional existente
```graphql
mutation {
  upsertProfissional(
    id: "12345678-1234-1234-1234-123456789012"
    nome: "Dr. João Silva Santos"
  )
}
```

### Deletar um profissional
```graphql
mutation {
  deleteProfissional(
    id: "12345678-1234-1234-1234-123456789012"
  )
}
```

## Relacionamento com Avaliação

### Obter avaliações de um profissional
```graphql
query {
  profissional(id: "12345678-1234-1234-1234-123456789012") {
    id
    nome
    avaliacoes {
      id
      dataAvaliacao
      mesesDuracao
      caracteristicas {
        bordasDefinidas
        tecidoGranulacao
        necrose
        odorFetido
      }
      sinaisInflamatorios {
        eritema
        calor
        rubor
        edema
        dor
        perdaDeFuncao
      }
      medida {
        comprimento
        largura
        profundidade
      }
    }
  }
}
```

## Observações

- O comando `upsertProfissional` pode criar um novo profissional (quando `id` é null) ou atualizar um existente (quando `id` é fornecido)
- O comando `deleteProfissional` faz soft delete (marca como desativado)
- Cada avaliação pode ter um profissional associado através do campo `profissionalId`
- O relacionamento é opcional: uma avaliação pode não ter profissional associado














