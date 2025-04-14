# Feedback - Avaliação Geral

## Front End
### Navegação
  * Pontos positivos:
    - Possui views e rotas definidas no projeto MBADevExpertModulo1.Web
    - Implementação com Razor Pages/Views

### Design
    - Será avaliado na entrega final

### Funcionalidade
  * Pontos positivos:
    - CRUD para Produtos e Categorias implementado
    - Interface web com HTML/CSS básico

## Back End
### Arquitetura
  * Pontos positivos:
    - Estrutura em camadas bem definida na pasta src:
      * MBADevExpertModulo1.Domain
      * MBADevExpertModulo1.Infrastructure
      * MBADevExpertModulo1.Web
      * MBADevExpertModulo1.WebAPI

  * Pontos negativos:
    - Arquitetura mais complexa que o necessário com 4 camadas distintas
    - Recomendação: Uma única camada "Core" já atende muito bem ao propósito

### Funcionalidade
  * Pontos positivos:
    - Suporte a múltiplos bancos de dados (SQL Server/SQLite)
    - Implementação do ASP.NET Identity
    - Configuração de Seed de dados mencionada

  * Pontos negativos:
    - Não é possível verificar a implementação completa das funcionalidades mencionadas

### Modelagem
  * Pontos positivos:
    - Modelos de dados separados em projeto Domain
    - Uso do Entity Framework Core

## Projeto
### Organização
  * Pontos positivos:
    - Estrutura organizada com pasta src na raiz
    - Arquivo solution (MBADevExpertModulo1.sln) na raiz
    - .gitignore e .gitattributes adequados
    - Separação clara dos projetos

### Documentação
  * Pontos positivos:
    - README.md presente com:
      * Apresentação do projeto
      * Tecnologias utilizadas
      * Estrutura do projeto
      * Instruções de execução
    - Documentação da API via Swagger mencionada

  * Pontos negativos:
    - Arquivo FEEDBACK.md mencionado no README mas não presente no repositório
    - Documentação poderia ser mais detalhada em relação às funcionalidades

### Instalação
  * Pontos positivos:
    - Suporte a múltiplos bancos (SQL Server/SQLite)
    - Seed de dados mencionado
    - Instruções básicas de instalação presentes