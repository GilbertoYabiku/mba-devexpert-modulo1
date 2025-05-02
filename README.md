# **Aplicação de e-commerce simples com MVC e API RESTful**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **[e-Market]**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**.
O objetivo principal desenvolver uma aplicação de e-commerce que permite aos usuários criar, editar, visualizar e excluir produtos e categorias de produtos, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful.

### **Autor**
- **Gilberto Moshim Yabiku Junior**

## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com a plataforma de e-commerce.
- **API RESTful:** Exposição dos recursos da plataforma de e-commerce e para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server/SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:


- src/
  - MBADevExpertModulo1.Core/ - Configuração do EF Core e modelos de dados
  - MBADevExpertModulo1.Web/ - Aplicação web MVC com AspNet Core
  - MBADevExpertModulo1.WebAPI/ - Aplicação WebAPI com AspNet Core
  - README.md - Arquivo de Documentação do Projeto
  - FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
  - .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **CRUD para Categorias de produtos:** Permite criar, editar, visualizar e excluir categorias de produtos para usuários logados no sistema.
- **CRUD para Produtos:** 
	- Permite criação de registros de produtos, desde que o usuário esteja logado no sistema. Possível inclusão de imagem do produto, bem como outras propriedades relevantes como preço e quantidade de estoque;
	- Permite que apenas os vendedores editem seus produtos;
	- Permite que usuários logados e não logados possam visualizar produtos ativos;
	- Permite que apenas os vendedores possam excluir seus produtos.
- **Autenticação e Autorização:** Implementação de sistema para registrar e logar em uma conta no app MVC e no WebAPI para mecanismos de autorização.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 9.0 ou superior
- SQL Server ou SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `https://github.com/GilbertoYabiku/mba-devexpert-modulo1.git`
   - `cd nome-do-repositorio`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server/SQLite.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos

3. **Executar a Aplicação MVC:**
   - `cd src/MBADevExpertModulo1.Web/`
   - `dotnet run`
   - Acesse a aplicação em: https://localhost:7203/

4. **Executar a API:**
   - `cd src/MBADevExpertModulo1.WebAPI/`
   - `dotnet run`
   - Acesse a documentação da API em: https://localhost:7174/swagger

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

https://localhost:7174/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
