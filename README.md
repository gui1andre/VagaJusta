# VagaJusta

Sistema de gerenciamento de matrículas escolares desenvolvido como desafio técnico para a **Spassu**. A aplicação permite o controle completo de escolas, turmas, alunos e matrículas, com suporte a fila de espera quando as turmas estão lotadas.

---

## Sumário

- [Sobre o Projeto](#sobre-o-projeto)
- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Funcionalidades](#funcionalidades)
- [Pré-requisitos](#pré-requisitos)
- [Como Executar](#como-executar)
  - [Com Docker (recomendado)](#com-docker-recomendado)
  - [Localmente (para desenvolvimento)](#localmente-para-desenvolvimento)
- [Variáveis de Ambiente](#variáveis-de-ambiente)
- [Endpoints da API](#endpoints-da-api)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Testes](#testes)

---

## Sobre o Projeto

O **VagaJusta** é uma plataforma web para gerenciar o processo de matrícula em instituições de ensino. O sistema controla a capacidade máxima de alunos por turma e, quando essa capacidade é atingida, o aluno é automaticamente colocado em fila de espera, aguardando a disponibilidade de uma vaga.

O projeto foi construído seguindo boas práticas de engenharia de software, com separação clara de responsabilidades, validações robustas e cobertura de testes.

---

## Arquitetura

O backend segue os princípios da **Clean Architecture**, dividido em quatro camadas bem definidas:

```
VagaJusta/
├── Backend/
│   ├── VagaJusta.Domain          # Entidades, Value Objects, Interfaces, Enums
│   ├── VagaJusta.Application     # Casos de uso, Commands, Queries, DTOs, Validators
│   ├── VagaJusta.Infrastructure  # EF Core, Repositórios, Identity, JWT, Migrations
│   └── VagaJusta.API             # Controllers, Middleware, Configuração da aplicação
└── Frontend/
    └── vaga-justa-react          # SPA em React + TypeScript
```

### Decisões de Design

| Padrão | Aplicação |
|--------|-----------|
| **Clean Architecture** | Separação em Domain → Application → Infrastructure → API, sem dependências invertidas |
| **CQRS com MediatR** | Commands para escrita e Queries para leitura, processados via pipeline MediatR |
| **Repository + Unit of Work** | Abstração do acesso a dados e garantia de consistência transacional |
| **Value Object** | CPF implementado como objeto de valor imutável com validação no domínio |
| **Pipeline Behavior** | `ValidationBehaviour` executa FluentValidation automaticamente antes de cada handler |
| **Global Exception Handler** | `ExceptionMiddleware` mapeia exceções de domínio para respostas HTTP padronizadas |

---

## Tecnologias

### Backend
- **[.NET 8 / ASP.NET Core](https://dotnet.microsoft.com/)** — Framework web
- **[Entity Framework Core 8](https://learn.microsoft.com/ef/core/)** — ORM com Npgsql para PostgreSQL
- **[ASP.NET Core Identity](https://learn.microsoft.com/aspnet/core/security/authentication/identity)** — Gerenciamento de usuários
- **[MediatR](https://github.com/jbogard/MediatR)** — Mediator para CQRS
- **[FluentValidation](https://fluentvalidation.net/)** — Validações expressivas na camada de aplicação
- **[JWT Bearer](https://learn.microsoft.com/aspnet/core/security/authentication/jwt-authn)** — Autenticação stateless via tokens
- **[Swagger / Swashbuckle](https://swagger.io/)** — Documentação interativa da API

### Frontend
- **[React 19](https://react.dev/)** — Biblioteca de UI
- **[TypeScript](https://www.typescriptlang.org/)** — Tipagem estática
- **[Vite](https://vitejs.dev/)** — Build tool e dev server
- **[React Router DOM 7](https://reactrouter.com/)** — Roteamento SPA

### Infraestrutura
- **[PostgreSQL 16](https://www.postgresql.org/)** — Banco de dados relacional
- **[Docker + Docker Compose](https://docs.docker.com/)** — Containerização e orquestração
- **[Nginx](https://nginx.org/)** — Servidor estático para o frontend em produção

### Testes
- **[xUnit](https://xunit.net/)** — Framework de testes
- **[coverlet](https://github.com/coverlet-coverage/coverlet)** — Cobertura de código

---

## Funcionalidades

- **Autenticação** — Login via e-mail e senha com retorno de JWT
- **Escolas** — Cadastro, listagem, edição e remoção de escolas
- **Turmas** — Criação de turmas por escola com configuração de série, categoria, capacidade máxima e faixa etária permitida
- **Alunos** — Atualização de dados do aluno (nome, CPF, data de nascimento)
- **Matrículas** — Solicitação de matrícula com validação automática de:
  - Compatibilidade de idade do aluno com a faixa etária da turma
  - Disponibilidade de vagas
  - Matrícula duplicada na mesma turma
- **Fila de Espera** — Quando a turma está lotada, o aluno entra automaticamente na fila de espera
- **Seed de Dados** — Banco populado automaticamente na inicialização com dados de exemplo

---

## Pré-requisitos

Para executar com Docker:
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e em execução

Para executar localmente:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/) e npm
- [PostgreSQL 16](https://www.postgresql.org/download/) rodando localmente **ou** Docker (apenas para o banco)

---

## Como Executar

### Com Docker (recomendado)

Sobe toda a stack (banco, backend e frontend) com um único comando:

```bash
docker compose up --build
```

Aguarde os health checks passarem. Os serviços estarão disponíveis em:

| Serviço | URL |
|---------|-----|
| Frontend | http://localhost:5173 |
| Backend API | http://localhost:5090 |
| Swagger | http://localhost:5090/swagger |
| PostgreSQL | localhost:5432 |

Para parar todos os containers:

```bash
docker compose down
```

Para parar e remover também os volumes (apaga o banco):

```bash
docker compose down -v
```

---

### Localmente (para desenvolvimento)

No modo de desenvolvimento é recomendado subir apenas o banco via Docker e rodar backend e frontend diretamente na máquina, o que permite o uso do debugger e hot reload.

#### 1. Subir apenas o banco de dados

```bash
docker compose up postgres -d
```

#### 2. Executar o Backend

```bash
cd Backend
dotnet run --project VagaJusta.API
```

As migrations e o seed de dados são executados automaticamente na inicialização.

O Swagger estará disponível em: http://localhost:5090/swagger

#### 3. Executar o Frontend

```bash
cd Frontend/vaga-justa-react
npm install
npm run dev
```

A aplicação estará disponível em: http://localhost:5173

---

## Variáveis de Ambiente

### Backend (`appsettings.json`)

| Variável | Descrição | Padrão |
|----------|-----------|--------|
| `ConnectionStrings__DefaultConnection` | Connection string do PostgreSQL | `Host=localhost;Port=5432;Database=VagaJusta;...` |
| `Jwt__SecretKey` | Chave secreta para assinar os tokens JWT | — |
| `Jwt__Issuer` | Emissor do token JWT | `VagaJusta.API` |
| `Jwt__Audience` | Audiência do token JWT | `VagaJusta.Client` |
| `Jwt__ExpiracaoHoras` | Tempo de expiração do token em horas | `30` |

### Frontend

| Variável | Descrição | Padrão |
|----------|-----------|--------|
| `VITE_API_URL` | URL base da API do backend | `http://localhost:5090` |

---

## Endpoints da API

A documentação interativa completa está disponível via Swagger em `/swagger` quando a aplicação está em execução.

### Autenticação

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|------|
| `POST` | `/api/auth/login` | Realiza login e retorna JWT | Público |

### Escolas

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|------|
| `GET` | `/api/escola` | Lista todas as escolas | Requerida |
| `GET` | `/api/escola/{id}` | Busca uma escola pelo ID | Requerida |
| `POST` | `/api/escola` | Cria uma nova escola | Requerida |
| `PUT` | `/api/escola/{id}` | Atualiza dados de uma escola | Requerida |
| `DELETE` | `/api/escola/{id}` | Remove uma escola | Requerida |

### Turmas

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|------|
| `POST` | `/api/turma` | Cria uma nova turma | Requerida |
| `DELETE` | `/api/turma/{id}` | Remove uma turma | Requerida |
| `GET` | `/api/turma/{id}/alunos` | Lista alunos matriculados na turma | Requerida |

### Matrículas

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|------|
| `POST` | `/api/matricula` | Solicita matrícula para um aluno | Requerida |
| `GET` | `/api/matricula/fila-espera/{turmaId}` | Lista a fila de espera de uma turma | Requerida |

### Alunos

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|------|
| `PUT` | `/api/aluno/{id}` | Atualiza dados de um aluno | Requerida |

> **Autenticação:** Todos os endpoints protegidos requerem o header `Authorization: Bearer {token}`.

---

## Estrutura do Projeto

```
VagaJusta/
│
├── Backend/
│   ├── VagaJusta.Domain/
│   │   ├── Entities/          # Aluno, Escola, Turma, Matricula, BaseEntity
│   │   ├── Enums/             # StatusMatriculaEnum, SerieEnum, CategoriaSerieEnum
│   │   ├── Exceptions/        # DomainException
│   │   ├── Interfaces/        # IRepository, IAlunoRepository, IUnitOfWork, ...
│   │   └── ValueObjects/      # CPF
│   │
│   ├── VagaJusta.Application/
│   │   ├── Commands/          # CreateSchool, CreateTurma, RequestMatricula, Login, ...
│   │   ├── Queries/           # ListarEscola, ObterFilaDeEspera, ObterAlunosDaTurma, ...
│   │   ├── DTOs/              # Responses, IdentityUserDto
│   │   ├── Validators/        # FluentValidation por Command/Query
│   │   ├── Behaviors/         # ValidationBehaviour (pipeline MediatR)
│   │   └── Mappers/           # Mapeamentos entre entidades e DTOs
│   │
│   ├── VagaJusta.Infrastructure/
│   │   ├── Data/              # DBContext, Entity Configurations
│   │   ├── Migrations/        # Migrations do EF Core
│   │   ├── Repositories/      # Implementações dos repositórios
│   │   ├── Identity/          # UsuarioIdentity, IdentityService, TokenService
│   │   └── Seed/              # DatabaseSeeder
│   │
│   ├── VagaJusta.API/
│   │   ├── Controllers/       # AuthController, EscolaController, TurmaController, ...
│   │   ├── MiddleWare/        # ExceptionMiddleware
│   │   ├── Properties/        # launchSettings.json
│   │   ├── appsettings.json
│   │   └── Program.cs
│   │
│   ├── VagaJusta.Tests/            # Testes unitários (xUnit)
│   └── VagaJusta.IntegrationTests/ # Testes de integração (xUnit)
│
├── Frontend/
│   └── vaga-justa-react/
│       ├── src/
│       │   ├── api/           # client.ts, auth.ts, escolas.ts, turmas.ts, ...
│       │   ├── components/    # Navbar, PrivateRoute
│       │   ├── pages/         # Login, Escolas, CriarEscola, TurmaDetalhe, ...
│       │   └── types/         # Tipagens TypeScript dos responses da API
│       ├── Dockerfile
│       └── nginx.conf
│
├── docker-compose.yml
└── README.md
```

---

## Testes

O projeto contém testes unitários cobrindo as principais regras de negócio do domínio.

```bash
cd Backend
dotnet test
```

### Cobertura

| Área | Casos testados |
|------|----------------|
| **Aluno** | Validação de nome e CPF, criação, cálculo de idade (com anos bissextos) |
| **Matrícula** | Transições de status (Aprovada, Rejeitada, AguardandoVaga), validação de motivo de rejeição |
| **Escola** | Criação e validações de negócio |
| **Turma** | Criação, validação de capacidade e compatibilidade de idade |

---

## Fluxo de Matrícula

```
Aluno solicita matrícula na Turma
         │
         ▼
Validação de idade compatível com a turma?
    │               │
   Não             Sim
    │               │
    ▼               ▼
 Rejeita     Há vagas disponíveis?
                │           │
               Sim          Não
                │           │
                ▼           ▼
            Aprovada   Fila de Espera
```

---

Desenvolvido por **Guilherme Andre** como desafio técnico para a **Spassu**.
