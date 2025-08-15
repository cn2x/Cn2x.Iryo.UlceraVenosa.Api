# Exemplos de Upload de Imagem via GraphQL

## Visão Geral

A API agora suporta upload de imagens via **strings base64** dentro das mutations GraphQL. Isso permite enviar imagens junto com outros dados em uma única requisição.

## Cenários de Teste

### 1. Criar Avaliação com Imagem

**Mutation:**
```graphql
mutation ($input: UpsertAvaliacaoUlceraInput!) {
    upsertAvaliacaoUlceraAsync(input: $input) {
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
    }
}
```

**Variables:**
```json
{
    "input": {
        "ulceraId": "123e4567-e89b-12d3-a456-426614174000",
        "profissionalId": "987fcdeb-51a2-43d1-9f12-345678901234",
        "dataAvaliacao": "2025-01-15T10:30:00Z",
        "mesesDuracao": 6,
        "arquivo": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAABAAEDASIAAhEBAxEB/8QAFQABAQAAAAAAAAAAAAAAAAAAAAv/xAAUEAEAAAAAAAAAAAAAAAAAAAAA/8QAFQEBAQAAAAAAAAAAAAAAAAAAAAX/xAAUEQEAAAAAAAAAAAAAAAAAAAAA/9oADAMBAAIRAxEAPwCdABmX/9k=",
        "descricaoImagem": "Imagem da úlcera venosa na perna direita",
        "dataCapturaImagem": "2025-01-15T10:25:00Z",
        "caracteristicas": {
            "bordasDefinidas": true,
            "tecidoGranulacao": false,
            "necrose": true,
            "odorFetido": false
        },
        "sinaisInflamatorios": {
            "calor": true,
            "rubor": false,
            "edema": true,
            "dor": 2,
            "perdaDeFuncao": false,
            "eritema": true
        },
        "medida": {
            "comprimento": 5.5,
            "largura": 3.2,
            "profundidade": 1.0
        }
    }
}
```

### 2. Criar Avaliação SEM Imagem

**Mutation:**
```graphql
mutation ($input: UpsertAvaliacaoUlceraInput!) {
    upsertAvaliacaoUlceraAsync(input: $input) {
        id
        ulceraId
        profissionalId
        dataAvaliacao
        mesesDuracao
    }
}
```

**Variables:**
```json
{
    "input": {
        "ulceraId": "123e4567-e89b-12d3-a456-426614174000",
        "profissionalId": "987fcdeb-51a2-43d1-9f12-345678901234",
        "dataAvaliacao": "2025-01-15T10:30:00Z",
        "mesesDuracao": 6,
        "caracteristicas": {
            "bordasDefinidas": true,
            "tecidoGranulacao": false,
            "necrose": true,
            "odorFetido": false
        },
        "sinaisInflamatorios": {
            "calor": true,
            "rubor": false,
            "edema": true,
            "dor": 2,
            "perdaDeFuncao": false,
            "eritema": true
        }
    }
}
```

### 3. Atualizar Avaliação Substituindo Imagem

**Mutation:**
```graphql
mutation ($input: UpsertAvaliacaoUlceraInput!) {
    upsertAvaliacaoUlceraAsync(input: $input) {
        id
        ulceraId
        profissionalId
        dataAvaliacao
        mesesDuracao
        descricaoImagem
    }
}
```

**Variables:**
```json
{
    "input": {
        "id": "456e7890-e89b-12d3-a456-426614174000",
        "ulceraId": "123e4567-e89b-12d3-a456-426614174000",
        "profissionalId": "987fcdeb-51a2-43d1-9f12-345678901234",
        "dataAvaliacao": "2025-01-15T10:30:00Z",
        "mesesDuracao": 6,
        "arquivo": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==",
        "descricaoImagem": "Nova imagem da úlcera após tratamento"
    }
}
```

### 4. Atualizar Avaliação Mantendo Imagem Existente

**Mutation:**
```graphql
mutation ($input: UpsertAvaliacaoUlceraInput!) {
    upsertAvaliacaoUlceraAsync(input: $input) {
        id
        ulceraId
        profissionalId
        dataAvaliacao
        mesesDuracao
        caracteristicas {
            bordasDefinidas
            tecidoGranulacao
        }
    }
}
```

**Variables:**
```json
{
    "input": {
        "id": "456e7890-e89b-12d3-a456-426614174000",
        "ulceraId": "123e4567-e89b-12d3-a456-426614174000",
        "profissionalId": "987fcdeb-51a2-43d1-9f12-345678901234",
        "dataAvaliacao": "2025-01-15T10:30:00Z",
        "mesesDuracao": 6,
        "caracteristicas": {
            "bordasDefinidas": false,
            "tecidoGranulacao": true
        }
    }
}
```

## Formatos de Imagem Suportados

- **JPEG/JPG**: `image/jpeg`
- **PNG**: `image/png`
- **GIF**: `image/gif`
- **BMP**: `image/bmp`

## Formatos de Base64

### 1. Base64 Simples
```
iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==
```

### 2. Base64 com Header (Recomendado)
```
data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAABAAEDASIAAhEBAxEB/8QAFQABAQAAAAAAAAAAAAAAAAAAAAv/xAAUEAEAAAAAAAAAAAAAAAAAAAAA/8QAFQEBAQAAAAAAAAAAAAAAAAAAAAX/xAAUEQEAAAAAAAAAAAAAAAAAAAAA/9oADAMBAAIRAxEAPwCdABmX/9k=
```

## Como Converter Imagem para Base64

### JavaScript (Frontend)
```javascript
function convertImageToBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result);
        reader.onerror = reject;
        reader.readAsDataURL(file);
    });
}

// Uso
const fileInput = document.getElementById('imageInput');
const file = fileInput.files[0];
const base64 = await convertImageToBase64(file);
console.log(base64); // data:image/jpeg;base64,/9j/4AAQ...
```

### C# (Backend)
```csharp
public static string ConvertImageToBase64(string imagePath)
{
    byte[] imageBytes = File.ReadAllBytes(imagePath);
    string base64String = Convert.ToBase64String(imageBytes);
    string contentType = GetContentType(imagePath);
    return $"data:{contentType};base64,{base64String}";
}

private static string GetContentType(string filePath)
{
    string extension = Path.GetExtension(filePath).ToLower();
    return extension switch
    {
        ".jpg" or ".jpeg" => "image/jpeg",
        ".png" => "image/png",
        ".gif" => "image/gif",
        ".bmp" => "image/bmp",
        _ => "application/octet-stream"
    };
}
```

## Vantagens da Abordagem Base64

1. **Simplicidade**: Uma única requisição HTTP
2. **Compatibilidade**: Funciona com qualquer cliente GraphQL
3. **Transação**: Dados e imagem são processados juntos
4. **Validação**: Validação centralizada no servidor
5. **Cache**: Pode ser cacheado pelo GraphQL

## Limitações

1. **Tamanho**: Base64 aumenta o tamanho em ~33%
2. **Memória**: Imagens ficam em memória durante o processamento
3. **Performance**: Para imagens muito grandes, considere upload separado

## Recomendações

1. **Use base64 com header** para melhor compatibilidade
2. **Valide o tamanho** da imagem no frontend
3. **Comprima imagens** antes de converter para base64
4. **Limite o tamanho** máximo (ex: 5MB)
5. **Use formatos eficientes** como JPEG para fotos

## Testes Automatizados

Os testes unitários cobrem:
- ✅ Processamento de base64 válido
- ✅ Remoção de headers de base64
- ✅ Validação de formatos de imagem
- ✅ Criação de comandos com/sem imagem
- ✅ Diferentes formatos de base64
