# API ServiceVitrine
---
## 📘 Visão Geral da API

A **Vitrine API** é um microserviço desenvolvido em **.NET 8** com o SDK `Microsoft.NET.Sdk.Web`, responsável por disponibilizar informações sobre produtos de forma estruturada e escalável.

Seu principal objetivo é **centralizar dados de produtos** e disponibilizá-los para outros módulos do ecossistema (como o carrinho, pedidos e estoque), por meio de **endpoints RESTful** padronizados.

A aplicação segue os princípios de **modularidade**, **baixo acoplamento** e **alta coesão**, sendo ideal para integração em arquiteturas baseadas em microserviços.
---
## 🧩 Tipo e Maturidade da API

### 🔷 Tipo de API

Esta aplicação foi desenvolvida como uma **API RESTful**, conforme indicado no arquivo de projeto:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
```

A utilização do SDK `Microsoft.NET.Sdk.Web` demonstra que o projeto foi criado como uma **aplicação ASP.NET Core Web API**, seguindo o padrão **REST**.
A API utiliza o protocolo **HTTP** para comunicação entre cliente e servidor e organiza seus **recursos por URI**, o que favorece uma estrutura clara, escalável e interoperável.

---
### 🔷 Endpoints Principais

A seguir estão os principais endpoints disponíveis no módulo **Vitrine**:

| Método  | Endpoint                             | Descrição                                         |
| :------ | :----------------------------------- | :------------------------------------------------ |
| **GET** | `/api/v1/vitrine/Product`            | Retorna a lista de produtos disponíveis.          |
| **GET** | `/api/v1/vitrine/Product/{id}`       | Retorna os detalhes de um produto específico.     |
| **GET** | `/api/v1/vitrine/Product/{id}/stock` | Retorna o estoque atual de um produto específico. |

Os endpoints seguem uma **estrutura hierárquica e semântica**, refletindo o relacionamento entre **recurso** (`Product`) e **sub-recurso** (`stock`).
---
### 🔷 Modelo de Maturidade REST (Richardson Maturity Model)

A API foi avaliada de acordo com o **Modelo de Maturidade de Richardson**, que classifica o grau de aderência aos princípios REST em quatro níveis:

| Nível | Nome         | Descrição                                                      | Status                |
| :---- | :----------- | :------------------------------------------------------------- | :-------------------- |
| **0** | Swamp of POX | Um único endpoint, sem uso semântico de HTTP.                  | ❌                     |
| **1** | Recursos     | Introduz URIs específicas para cada recurso.                   | ✅                     |
| **2** | Verbos HTTP  | Usa métodos HTTP (GET, POST, PUT, DELETE) e status adequados.  | ✅                     |
| **3** | HATEOAS      | Retorna links dinâmicos guiando o cliente para próximas ações. | 🚧 Em desenvolvimento |

Atualmente, a API se encontra no **Nível 2**, pois:

* Define **recursos específicos** (`Product`, `Stock`);
* Utiliza **métodos HTTP adequados** (`GET`);
* Retorna **status HTTP padronizados** (como 200 e 404).

O próximo passo será atingir o **Nível 3 (HATEOAS)**, retornando links dinâmicos que permitam ao cliente descobrir novas ações sem depender de documentação externa.

**Exemplo de retorno com HATEOAS (em desenvolvimento):**

```json
{
  "id": 1,
  "name": "Produto X",
  "price": 199.90,
  "links": [
    { "rel": "self", "href": "/api/v1/vitrine/Product/1" },
    { "rel": "stock", "href": "/api/v1/vitrine/Product/1/stock" }
  ]
}
```

---

### 🔷 Conclusão

A API **segue o padrão REST**, foi **construída em .NET 8** utilizando o SDK **Microsoft.NET.Sdk.Web**, e atinge o **Nível 2 do Modelo de Maturidade REST**.
Essa arquitetura garante uma **comunicação clara, organizada e compatível** com diferentes tipos de clientes, como aplicações web, mobile e sistemas externos.
