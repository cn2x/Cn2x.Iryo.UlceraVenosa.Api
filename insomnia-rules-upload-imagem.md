# 🚀 Regras para Insomnia - API GraphQL Upload de Imagens

## 📋 Configuração Inicial

### 1. **Criar Nova Workspace**
- Nome: `Iryo Úlcera Venosa API`
- Descrição: `API GraphQL para gerenciamento de úlceras venosas com upload de imagens`

### 2. **Configurar Environment**
```json
{
  "base_url": "http://localhost:8080",
  "graphql_endpoint": "{{ base_url }}/graphql",
  "api_version": "v1",
  "auth_token": "",
  "test_ulcera_id": "123e4567-e89b-12d3-a456-426614174000",
  "test_profissional_id": "987fcdeb-51a2-43d1-9f12-345678901234"
}
```

## 🎯 Requests para Testar

### **1. Criar Avaliação COM Imagem**

#### **Request Name:** `Create Avaliação with Image`
- **Method:** POST
- **URL:** `{{ graphql_endpoint }}`
- **Headers:**
  ```
  Content-Type: application/json
  Accept: application/json
  ```

#### **Body (JSON):**
```json
{
  "query": "mutation ($input: UpsertAvaliacaoUlceraInput!) { upsertAvaliacaoUlceraAsync(input: $input) { id ulceraId profissionalId dataAvaliacao mesesDuracao descricaoImagem dataCapturaImagem caracteristicas { bordasDefinidas tecidoGranulacao necrose odorFetido } sinaisInflamatorios { calor rubor edema dor perdaDeFuncao eritema } medida { comprimento largura profundidade } } }",
  "variables": {
    "input": {
      "ulceraId": "{{ test_ulcera_id }}",
      "profissionalId": "{{ test_profissional_id }}",
      "dataAvaliacao": "{{ $isoTimestamp }}",
      "mesesDuracao": 6,
      "arquivo": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAABAAEDASIAAhEBAxEB/8QAFQABAQAAAAAAAAAAAAAAAAAAAAv/xAAUEAEAAAAAAAAAAAAAAAAAAAAA/8QAFQEBAQAAAAAAAAAAAAAAAAAAAAX/xAAUEQEAAAAAAAAAAAAAAAAAAAAA/9oADAMBAAIRAxEAPwCdABmX/9k=",
      "descricaoImagem": "Imagem da úlcera venosa na perna direita",
      "dataCapturaImagem": "{{ $isoTimestamp }}",
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
}
```

---

### **2. Criar Avaliação SEM Imagem**

#### **Request Name:** `Create Avaliação without Image`
- **Method:** POST
- **URL:** `{{ graphql_endpoint }}`
- **Headers:** Mesmo do anterior

#### **Body (JSON):**
```json
{
  "query": "mutation ($input: UpsertAvaliacaoUlceraInput!) { upsertAvaliacaoUlceraAsync(input: $input) { id ulceraId profissionalId dataAvaliacao mesesDuracao caracteristicas { bordasDefinidas tecidoGranulacao necrose odorFetido } sinaisInflamatorios { calor rubor edema dor perdaDeFuncao eritema } } }",
  "variables": {
    "input": {
      "ulceraId": "{{ test_ulcera_id }}",
      "profissionalId": "{{ test_profissional_id }}",
      "dataAvaliacao": "{{ $isoTimestamp }}",
      "mesesDuracao": 3,
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
}
```

---

### **3. Atualizar Avaliação Substituindo Imagem**

#### **Request Name:** `Update Avaliação Replace Image`
- **Method:** POST
- **URL:** `{{ graphql_endpoint }}`
- **Headers:** Mesmo do anterior

#### **Body (JSON):**
```json
{
  "query": "mutation ($input: UpsertAvaliacaoUlceraInput!) { upsertAvaliacaoUlceraAsync(input: $input) { id ulceraId profissionalId dataAvaliacao mesesDuracao descricaoImagem } }",
  "variables": {
    "input": {
      "id": "{{ response.body '$.data.upsertAvaliacaoUlceraAsync.id' }}",
      "ulceraId": "{{ test_ulcera_id }}",
      "profissionalId": "{{ test_profissional_id }}",
      "dataAvaliacao": "{{ $isoTimestamp }}",
      "mesesDuracao": 6,
      "arquivo": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==",
      "descricaoImagem": "Nova imagem da úlcera após tratamento"
    }
  }
}
```

---

### **4. Atualizar Avaliação Mantendo Imagem**

#### **Request Name:** `Update Avaliação Keep Image`
- **Method:** POST
- **URL:** `{{ graphql_endpoint }}`
- **Headers:** Mesmo do anterior

#### **Body (JSON):**
```json
{
  "query": "mutation ($input: UpsertAvaliacaoUlceraInput!) { upsertAvaliacaoUlceraAsync(input: $input) { id ulceraId profissionalId dataAvaliacao mesesDuracao caracteristicas { bordasDefinidas tecidoGranulacao } } }",
  "variables": {
    "input": {
      "id": "{{ response.body '$.data.upsertAvaliacaoUlceraAsync.id' }}",
      "ulceraId": "{{ test_ulcera_id }}",
      "profissionalId": "{{ test_profissional_id }}",
      "dataAvaliacao": "{{ $isoTimestamp }}",
      "mesesDuracao": 6,
      "caracteristicas": {
        "bordasDefinidas": false,
        "tecidoGranulacao": true
      }
    }
  }
}
```

---

### **5. Buscar Avaliações**

#### **Request Name:** `Get Avaliações`
- **Method:** POST
- **URL:** `{{ graphql_endpoint }}`
- **Headers:** Mesmo do anterior

#### **Body (JSON):**
```json
{
  "query": "query { avaliacoesUlcera { id ulceraId profissionalId dataAvaliacao mesesDuracao descricaoImagem dataCapturaImagem caracteristicas { bordasDefinidas tecidoGranulacao necrose odorFetido } sinaisInflamatorios { calor rubor edema dor perdaDeFuncao eritema } medida { comprimento largura profundidade } } }"
}
```

---

### **6. Buscar Avaliação por ID**

#### **Request Name:** `Get Avaliação by ID`
- **Method:** POST
- **URL:** `{{ graphql_endpoint }}`
- **Headers:** Mesmo do anterior

#### **Body (JSON):**
```json
{
  "query": "query ($id: ID!) { avaliacaoUlcera(id: $id) { id ulceraId profissionalId dataAvaliacao mesesDuracao descricaoImagem dataCapturaImagem caracteristicas { bordasDefinidas tecidoGranulacao necrose odorFetido } sinaisInflamatorios { calor rubor edema dor perdaDeFuncao eritema } medida { comprimento largura profundidade } } }",
  "variables": {
    "id": "{{ response.body '$.data.upsertAvaliacaoUlceraAsync.id' }}"
  }
}
```

## 🔧 Configurações Avançadas

### **1. Headers Globais**
Crie um **Request Header Preset** com:
```
Content-Type: application/json
Accept: application/json
User-Agent: Insomnia/2023.5.8
```

### **2. Environment Variables**
Adicione estas variáveis ao seu environment:

```json
{
  "test_image_jpeg": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAABAAEDASIAAhEBAxEB/8QAFQABAQAAAAAAAAAAAAAAAAAAAAv/xAAUEAEAAAAAAAAAAAAAAAAAAAAA/8QAFQEBAQAAAAAAAAAAAAAAAAAAAAX/xAAUEQEAAAAAAAAAAAAAAAAAAAAA/9oADAMBAAIRAxEAPwCdABmX/9k=",
  "test_image_png": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==",
  "test_image_gif": "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7"
}
```

### **3. Response Processing**
Configure **Response Body Size Limit** para `10MB` para lidar com respostas grandes.

## 📱 Testando no Frontend

### **1. JavaScript Helper**
```javascript
// Função para converter arquivo em base64
function convertFileToBase64(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onload = () => resolve(reader.result);
    reader.onerror = reject;
    reader.readAsDataURL(file);
  });
}

// Uso
const fileInput = document.getElementById('imageInput');
fileInput.addEventListener('change', async (e) => {
  const file = e.target.files[0];
  if (file) {
    const base64 = await convertFileToBase64(file);
    console.log('Base64:', base64);
    // Use base64 no seu GraphQL mutation
  }
});
```

### **2. React Hook**
```javascript
import { useState } from 'react';

export const useImageUpload = () => {
  const [imageBase64, setImageBase64] = useState(null);
  const [isLoading, setIsLoading] = useState(false);

  const handleImageUpload = async (file) => {
    setIsLoading(true);
    try {
      const base64 = await convertFileToBase64(file);
      setImageBase64(base64);
    } catch (error) {
      console.error('Erro ao converter imagem:', error);
    } finally {
      setIsLoading(false);
    }
  };

  return { imageBase64, isLoading, handleImageUpload };
};
```

## 🧪 Cenários de Teste

### **✅ Teste 1: Upload JPEG**
1. Use `{{ test_image_jpeg }}` no campo `arquivo`
2. Execute a mutation
3. Verifique se retorna `id` e outros campos
4. Verifique se não há erros de validação

### **✅ Teste 2: Upload PNG**
1. Use `{{ test_image_png }}` no campo `arquivo`
2. Execute a mutation
3. Verifique se retorna sucesso

### **✅ Teste 3: Sem Imagem**
1. Deixe o campo `arquivo` como `null`
2. Execute a mutation
3. Verifique se cria avaliação sem imagem

### **✅ Teste 4: Base64 Inválido**
1. Use `"base64-invalido-!@#"` no campo `arquivo`
2. Execute a mutation
3. Verifique se retorna erro apropriado

### **✅ Teste 5: Atualização**
1. Crie uma avaliação com imagem
2. Use o `id` retornado para atualizar
3. Verifique se a imagem foi substituída

## 🚨 Tratamento de Erros

### **Erros Esperados:**
- `400 Bad Request`: Dados inválidos
- `422 Unprocessable Entity`: Validação falhou
- `500 Internal Server Error`: Erro no servidor

### **Validações:**
- `ulceraId` obrigatório
- `profissionalId` obrigatório
- `dataAvaliacao` obrigatório
- `mesesDuracao` obrigatório
- `caracteristicas` obrigatório
- `sinaisInflamatorios` obrigatório

## 📊 Monitoramento

### **1. Response Time**
- ✅ < 1s: Excelente
- ⚠️ 1-3s: Aceitável
- ❌ > 3s: Investigar

### **2. Response Size**
- ✅ < 1MB: Normal
- ⚠️ 1-5MB: Monitorar
- ❌ > 5MB: Investigar

### **3. Success Rate**
- ✅ > 95%: Excelente
- ⚠️ 90-95%: Aceitável
- ❌ < 90%: Investigar

## 🔄 Workflow de Teste

1. **Setup**: Configure environment e headers
2. **Create**: Crie avaliação com imagem
3. **Verify**: Busque e verifique dados
4. **Update**: Atualize avaliação
5. **Cleanup**: Delete dados de teste (se necessário)

## 💡 Dicas

- **Use timestamps dinâmicos**: `{{ $isoTimestamp }}`
- **Teste diferentes formatos**: JPEG, PNG, GIF
- **Monitore performance**: Response time e size
- **Valide dados**: Verifique se todos os campos retornam
- **Teste cenários edge**: Imagens muito grandes, base64 inválido

---

**🎯 Agora você pode testar completamente a API de upload de imagens no Insomnia!**
