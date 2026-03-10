🧪 Lupo Química - Sistema de Gestão de Produtos

Este é um sistema Full Stack desenvolvido para a empresa Lupo Química Industrial. O projeto consiste em uma vitrine de produtos para clientes e um painel administrativo seguro para gerenciamento de estoque e categorias.



🚀 Funcionalidades

🌐 Área Pública (Vitrine)

Visualização de Produtos: Listagem dinâmica de produtos químicos e industriais.



Detalhes do Produto: Informações técnicas e princípios ativos.



Integração WhatsApp: Botão de orçamento direto que já envia o nome do produto no texto.



🔐 Área Administrativa (Painel de Controle)

Autenticação JWT: Sistema de login seguro com Tokens.



Gestão de Produtos (CRUD): Interface para criar, editar, listar e excluir produtos.



Gestão de Categorias: Sistema dinâmico para cadastrar novas categorias no banco de dados.



Modais Interativos: Formulários que não recarregam a página, proporcionando uma UX moderna.



🛠️ Tecnologias Utilizadas

Front-end

Blazor WebAssembly (.NET 10)



Bootstrap 5: Estilização e componentes responsivos.



Blazored LocalStorage: Para persistência do token de autenticação.



Bootstrap Icons: Biblioteca de ícones.



Back-end (API)

ASP.NET Core Web API



Entity Framework Core: ORM para comunicação com o banco de dados.



SQL Server: Armazenamento de dados.



JWT (JSON Web Tokens): Segurança e autorização de rotas.



📦 Como rodar o projeto

Clonar o repositório:



Bash

git clone https://github.com/SEU\_USUARIO/LupoQuimica.git

Configurar o Banco de Dados:



No arquivo appsettings.json da API, ajuste a sua DefaultConnection.



Rode as migrations no Console do Gerenciador de Pacotes:



PowerShell

Update-Database

Executar:



Inicie o projeto da API e o projeto Client simultaneamente pelo Visual Studio.

