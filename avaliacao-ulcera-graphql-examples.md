# 📸 **Exemplos GraphQL para Avaliação de Úlcera com Upload de Imagem Real**

## 🎯 **Resumo: Upload Real é Definitivamente Melhor!**

### **✅ Benefícios da Abordagem:**
- **Eficiência**: Upload direto de arquivo (multipart) vs Base64
- **Performance**: Sem overhead de codificação/decodificação
- **Padrão**: Segue as melhores práticas do GraphQL para uploads
- **Simplicidade**: API limpa e direta

### **🔧 Implementação Simplificada:**
```csharp
public class UpsertAvaliacaoUlceraInput
{
    // ... outros campos ...
    public IFile? Arquivo { get; set; } // O arquivo real para upload
    public string? DescricaoImagem { get; set; } // Metadados da imagem
    public DateTime? DataCapturaImagem { get; set; } // Data de captura
}
```

### **📋 Regras de Comportamento das Imagens:**

#### **🆕 Criação (Insert):**
- Se `Arquivo` for fornecido: cria nova avaliação com imagem
- Se `Arquivo` for null: cria avaliação sem imagem

#### **🔄 Atualização (Update):**
- **Se `Arquivo` for fornecido**: 
  - ✅ **Substitui** a imagem existente pela nova
  - ✅ **Apaga** a imagem anterior
  - ✅ **Processa** o upload da nova imagem
- **Se `Arquivo` for null**: 
  - ✅ **Mantém** a imagem existente
  - ✅ **Não faz alterações** na imagem
  - ✅ **Preserva** o histórico de imagens

### **🚀 Exemplo de Mutation GraphQL:**

```graphql
mutation UpsertAvaliacaoUlcera($input: UpsertAvaliacaoUlceraInput!, $arquivo: Upload!) {
  upsertAvaliacaoUlceraAsync(input: $input, arquivo: $arquivo) {
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

### **📤 Variáveis JSON:**
```json
{
  "input": {
    "ulceraId": "123e4567-e89b-12d3-a456-426614174000",
    "profissionalId": "123e4567-e89b-12d3-a456-426614174001",
    "dataAvaliacao": "2024-01-15T10:00:00Z",
    "mesesDuracao": 3,
    "descricaoImagem": "Foto da úlcera no terço inferior da perna direita",
    "dataCapturaImagem": "2024-01-15T10:00:00Z",
    "caracteristicas": {
      "dor": {
        "intensidade": "MODERADA",
        "caracteristica": "CONTINUA"
      },
      "localizacao": {
        "regiaoAnatomica": "PERNA",
        "segmento": "TERCO_INFERIOR"
      }
    },
    "sinaisInflamatorios": {
      "edema": true,
      "eritema": false,
      "dor": true,
      "calor": false,
      "exsudatoPurulento": false
    },
    "exsudatos": [
      {
        "tipo": "SEROSO",
        "quantidade": "MODERADA"
      }
    ]
  }
}
```

**File Part (separate from JSON variables):**
```
----------------------------boundary
Content-Disposition: form-data; name="arquivo"; filename="ulcera_foto.jpg"
Content-Type: image/jpeg
[Binary content of ulcera_foto.jpg]
----------------------------boundary--
```

### **🔄 Exemplo de Update (Sem Nova Imagem):**

```graphql
mutation UpsertAvaliacaoUlcera($input: UpsertAvaliacaoUlceraInput!) {
  upsertAvaliacaoUlceraAsync(input: $input) {
    id
    ulceraId
    profissionalId
    dataAvaliacao
    mesesDuracao
  }
}
```

```json
{
  "input": {
    "id": "avaliacao-existente-id",
    "ulceraId": "123e4567-e89b-12d3-a456-426614174000",
    "profissionalId": "123e4567-e89b-12d3-a456-426614174001",
    "dataAvaliacao": "2024-01-16T14:00:00Z",
    "mesesDuracao": 4
    // Sem arquivo = mantém imagem existente
  }
}
```

### **💡 Casos de Uso:**

1. **📸 Primeira Avaliação com Imagem**: Envia `Arquivo` + metadados
2. **🔄 Atualização com Nova Imagem**: Envia `Arquivo` + metadados (substitui anterior)
3. **📝 Atualização sem Imagem**: Não envia `Arquivo` (mantém imagem existente)
4. **🗑️ Remoção de Imagem**: Envia `Arquivo` vazio ou implementa mutation específica

### **🔒 Validações:**
- ✅ Formato de imagem suportado (JPEG, PNG, GIF, BMP)
- ✅ Tamanho máximo de arquivo
- ✅ Metadados obrigatórios quando há arquivo
- ✅ Permissões de usuário para upload

### **📱 Frontend Integration:**
```javascript
// Exemplo com FormData
const formData = new FormData();
formData.append('operations', JSON.stringify({
  query: mutation,
  variables: { input: inputData, arquivo: null }
}));
formData.append('map', JSON.stringify({ "0": ["variables.arquivo"] }));
formData.append('0', fileInput.files[0]);

fetch('/graphql', {
  method: 'POST',
  body: formData
});
```
