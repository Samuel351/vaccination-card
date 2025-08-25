# Cartão de Vacinação – Angular + .NET + MySQL

> Sistema para gerenciar o cartão de vacinação de uma pessoa, com cadastro, consulta, atualização e exclusão de registros. Backend em .NET (Clean Architecture + CQRS + MediatR + FluentValidation + Result Pattern + EF Core code‑first + Repository Pattern) e frontend em Angular (micro components).

## Visão Geral

O objetivo do sistema é permitir que pessoas tenham seus cartões de vacinação gerenciados via uma API REST, com um frontend em Angular para consumo/gestão. O sistema registra vacinas, pessoas, e vacinações (aplicações), garantindo regras como validação de dose e integridade referencial.

**Principais funcionalidades:**

* Cadastrar vacina (nome, quantidade de doses requeridas, identificador único).
* Cadastrar pessoa (nome, CPF, email, genero, telefone, idade e identificador único).
* Remover pessoa (remove também seu cartão e registros associados – *cascade* lógico conforme decisão de implementação).
* Registrar vacinação para uma pessoa (vacina + dose + data de aplicação; validação da dose).
* Consultar cartão de vacinação (histórico completo por pessoa).
* Excluir registro de vacinação específico.

---

## Arquitetura

**Clean Architecture** com camadas separando domínio, aplicação e infraestrutura.

```
Frontend (Angular)
        ↓ HTTP/JSON
API (Web API)
├─ Application (CQRS + MediatR + FluentValidation)
├─ Domain (Entidades + Regras de Negócio)
└─ Infrastructure (EF Core + MySQL + Repository Pattern)
```

**Padrões e abordagens:**

* **CQRS**: comandos/consultas separados, handlers específicos.
* **MediatR**: orquestração de handlers e behaviours (ex.: validação).
* **FluentValidation**: validações declarativas por request.
* **Result Pattern**: retorno padronizado (sucesso/erro) entre camadas e handlers.
* **Repository Pattern**: abstração de acesso a dados.
* **EF Core (code-first)**: migrações e mapeamentos para MySQL.
* **Angular micro components**: componentes pequenos, reutilizáveis e focados.

---

## Tecnologias

* **Backend**: .NET 8+, ASP.NET Core Web API, MediatR, FluentValidation, Entity Framework Core, Pomelo.EntityFrameworkCore.MySql.
* **Frontend**: Angular 17+ (ou compatível), RxJS, HTTPClient.
* **Banco**: MySQL 8+.
---

## Domínio e Regras

**Entidades**

* **Vaccine**: `Id`, `Name`, `RequiredDoses`.
* **Person**: `Id`, `Name`, `CPF`, `Email`, `PhoneNumber`. `Gender`, `Age`.
* **Vaccination**: `Id`, `PersonId`, `VaccineId`, `DoseNumber`, `AppliedAt`.

## Estrutura dos Projetos

```
/ (repo root)
├─ backend/
│  ├─ src/
│  │  ├─ Api/                  # Controllers/Endpoints, DI, Middlewares
│  │  ├─ Application/          # CQRS (Commands/Queries), DTOs, Validators, Behaviours
│  │  ├─ Domain/               # Entidades, Interfaces (Repos), Regras de negócio
│  └─ Infrastructure/       # EF Core, Migrations, MySQL, Repositories
└─ frontend/
   ├─ src/
   │  ├─ app/
   │  │  ├─ core/
   │  │  ├─ shared/             # micro components, pipes, directives
   │  │  ├─ features/
   │  │  │  ├─ vaccines/
   │  │  │  ├─ person/
   │  │  │  └─ vaccinations/
            └─ pages/
```

---

## Validações e Pipeline

* **FluentValidation** por *command/query* (ex.: `CreatePersonCommandValidator`, `CreateVaccinationCommandValidator`).
* **MediatR Pipeline Behavior** executando validações antes do *handler*; se inválido, retorna `Result.Failure(...)` imediatamente.
* **Result Pattern** padroniza respostas internas e facilita mapeamento para HTTP.

Exemplos de regras:

* `Person` ➜ `Name` não vazio, `CPF` com formato válido e único.
* `Vaccine` ➜ `Name` não vazio/único.
* `Vaccination` ➜ `DoseNumber` >= 1, `AppliedAt` ≤ `DateTime.UtcNow`, combinação única `(PersonId,VaccineId,DoseNumber)`.

---

## Padrões de Commit e Git

* **Commits descritivos** (ex.: *feat: register vaccination command with validator*).
* **Branches**: `feat/*`, `fix/*`, `chore/*`, `docs/*`, `test/*`.

Sugestão de *Conventional Commits*:

* `feat:`, `fix:`, `refactor:`, `perf:`, `docs:`, `test:`, `build:`, `ci:`, `chore:`.

---

## Roadmap

* [ ] Soft delete e trilhas de auditoria (CreatedBy/UpdatedBy, DeletedAt).
* [ ] Autenticação e autorização por perfil (Admin/Operador/Leitura)
* [ ] Paginação, ordenação e filtros avançados no GET.
* [ ] Rate limiting e *caching*.
* [ ] Observabilidade (Serilog, OpenTelemetry).
* [ ] Docker Compose (API + MySQL + Adminer + Frontend).
* [ ] Documentação OpenAPI/Swagger completa + Postman Collection.

---

## Decisões Arquiteturais (Resumo)

* **Clean Architecture** para testabilidade e isolamento de camadas.
* **CQRS** para clareza de intenção e escalabilidade de queries/commands.
* **Result Pattern** para padronizar retornos e facilitar *error handling* sem exceções de controle de fluxo.
* **Repository Pattern** para desacoplar domínio de EF Core (substituível por outro provider).
* **Micro Components (Angular)** para reuso, mantenabilidade, testabilidade e coesão em UI.
