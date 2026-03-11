# 🧪 Lupo Química - Sistema de Gestão Industrial

O projeto **Lupo Química** é uma aplicação Full Stack robusta desenvolvida para gerenciar uma vitrine de produtos químicos e industriais. O sistema conta com uma interface pública para clientes e uma área administrativa restrita e segura.

---

## 🚀 Funcionalidades Principais

### 🌐 Área do Cliente (Vitrine)
* **Vitrine Dinâmica:** Listagem de produtos filtrados por categorias.
* **Detalhes Técnicos:** Visualização de princípios ativos e especificações.
* **Conversão via WhatsApp:** Botão de orçamento integrado que já preenche a mensagem para o vendedor.

### 🔐 Painel Administrativo (Exclusivo)
* **Segurança JWT:** Proteção de rotas com autenticação por Token.
* **Gestão de Inventário:** CRUD completo (Criar, Ler, Atualizar e Excluir) de produtos.
* **Categorias Dinâmicas:** Gerenciamento de categorias em tempo real para expansão do catálogo.

---

## 🛠️ Stack Tecnológica

| Camada | Tecnologia |
| :--- | :--- |
| **Front-end** | Blazor WebAssembly (.NET 10) |
| **Back-end** | ASP.NET Core Web API |
| **Banco de Dados** | SQL Server com Entity Framework Core |
| **Segurança** | Autenticação JWT (JSON Web Tokens) |
| **UI/UX** | Bootstrap 5 & Blazored LocalStorage |

---

## 📦 Como Instalar e Rodar

1. **Clone o repositório:**
   ```bash
   git clone [https://github.com/SEU_USUARIO/LupoQuimica.git](https://github.com/SEU_USUARIO/LupoQuimica.git)

2. **Configure o Banco:**

    No appsettings.json, insira sua ConnectionString.

4. **Aplique as Migrations:**
   ```bash
   Update-Database
