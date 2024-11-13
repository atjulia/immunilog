
# Immunilog

O **Immunilog** é uma aplicação projetada para gerenciar de forma eficiente o histórico de imunizações de indivíduos e seus dependentes. O sistema permite que usuários registrem e acompanhem o histórico de vacinas já realizadas para si e seus familiares.


## Funcionalidades

Acesse a documentação nas respectivas páginas:
- [Casos de uso](docs/use-cases.md)
- [Requisitos](docs/requirements.md)
- [Modelos C4](docs/models.md)


## Tecnologias Utilizadas

- **Backend**: 
  - C# com .NET 8
- **Frontend**: 
  - Vue.js, utilizando Vuetify como design system
- **Banco de Dados**: 
  - MySQL
 - **Testes**: 
	  - Cypress
- **Conteinerização**: 
  - Docker
- **Cloud**: 
  - Azure
- **Monitoramento**: 
  - <a href="https://sonarcloud.io/project/overview?id=atjulia_immunilog">SonarCloud</a>


# Instruções para executar o projeto

## Requisitos

- **.NET 8**
- **Node.js 16+**
- **Docker**
- **MySQL**

## Configuração do Banco de Dados

Na sua IDE de preferência, configure a connectionString nos "Segredos de Usuário", especificando o servidor, porta, database, usuário e senha.

Ao rodar o projeto, as migrations irão criar o banco de dados automaticamente.


## Ambiente de desenvolvimento

### Backend

Para rodar o backend em um ambiente de desenvolvimento com .NET 8, siga os seguintes passos:

1. Navegue até a pasta do backend:

    ```bash
    cd backend
    ```

2. Restaure as dependências do projeto:

    ```bash
    dotnet restore
    ```

3. Inicie o servidor:

    ```bash
    dotnet run
    ```

4. O backend estará disponível em `http://localhost:5000` (ou a URL configurada).

### Frontend

1. Navegue até a pasta do frontend:

    ```bash
    cd frontend
    ```

2. Instale as dependências:

    ```bash
    npm install
    ```

3. Execute o projeto:

    ```bash
    npm run dev
    ```
4. O frontend estará disponível em `http://localhost:3000` (ou a URL configurada).


## Licença

Este projeto está licenciado sob a Licença MIT — veja o arquivo [LICENSE](https://github.com/atjulia/immunilog/blob/main/LICENSE) para mais detalhes.
