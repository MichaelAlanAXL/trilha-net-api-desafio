# Desafio DIO - Trilha .NET - API e Entity Framework - Tivit 2026
www.dio.me

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

## Sobre o Projeto

Este é o meu projeto desenvolvido como parte do desafio da trilha .NET da DIO (Digital Innovation One), em parceria com a Tivit, no ano de 2026. Implementei uma API REST completa para gerenciamento de tarefas, utilizando .NET 9.0, Entity Framework Core e MySQL como banco de dados.

## Funcionalidades Implementadas

### 🔐 Autenticação JWT
- Sistema de login seguro com geração de tokens JWT
- Usuário padrão para testes: `admin` / `123456`
- Todos os endpoints de tarefas são protegidos por autenticação

### 📋 Gerenciamento de Tarefas (CRUD Completo)
- **Criar tarefa**: Adicionar novas tarefas com título, descrição, data e status
- **Listar tarefas**: Obter todas as tarefas ou filtrar por diferentes critérios
- **Atualizar tarefa**: Modificar informações existentes
- **Excluir tarefa**: Remover tarefas do sistema

### 🔍 Filtros e Buscas Avançadas
- Buscar por título (pesquisa parcial)
- Filtrar por data específica
- Filtrar por status (Pendente, Em Andamento, Finalizado, Cancelado, Atrasado)

### 🗄️ Banco de Dados
- Utilização do MySQL como banco de dados
- Migrations do Entity Framework para controle de versão do schema
- Relacionamento entre usuários e tarefas

### 📚 Documentação Interativa
- Swagger UI configurado com autenticação Bearer
- Documentação XML automática dos endpoints
- Exemplos de requisições e respostas

## Tecnologias Utilizadas

- **.NET 9.0** - Framework principal
- **Entity Framework Core 9.0** - ORM para acesso ao banco
- **Pomelo.EntityFrameworkCore.MySql 9.0** - Provider MySQL
- **JWT Bearer Authentication** - Autenticação segura
- **Swashbuckle.AspNetCore** - Documentação Swagger
- **MySQL** - Banco de dados relacional

## Estrutura do Projeto

```
Controllers/
├── AuthController.cs      # Autenticação e geração de tokens
└── TarefaController.cs   # CRUD e filtros de tarefas

Models/
├── Tarefa.cs             # Modelo da entidade Tarefa
├── Usuario.cs            # Modelo da entidade Usuario
├── EnumStatusTarefa.cs   # Enumeração dos status possíveis
└── LoginRequest.cs       # Modelo para requisição de login

Context/
└── OrganizadorContext.cs # Contexto do Entity Framework

Migrations/               # Migrations do banco de dados
```

## Como Executar

### Pré-requisitos
- .NET 9.0 SDK instalado
- MySQL Server rodando localmente
- Criar banco de dados `db_tarefas`

### Configuração
1. Clone o repositório
2. Configure a string de conexão no `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "ConexaoPadrao": "Server=localhost;Port=3306;Database=db_tarefas;User=root;Password=sua_senha;"
     }
   }
   ```

3. Execute as migrations:
   ```bash
   dotnet ef database update
   ```

4. Execute o projeto:
   ```bash
   dotnet run
   ```

5. Acesse o Swagger em: `https://localhost:5001/swagger`

## Exemplos de Uso

### 1. Login
```bash
POST /auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "123456"
}
```

**Resposta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### 2. Criar Tarefa
```bash
POST /Tarefa
Authorization: Bearer {seu_token}
Content-Type: application/json

{
  "titulo": "Estudar .NET",
  "descricao": "Revisar conceitos de API e Entity Framework",
  "data": "2026-01-20T10:00:00",
  "status": "Pendente"
}
```

### 3. Listar Todas as Tarefas
```bash
GET /Tarefa/ObterTodos
Authorization: Bearer {seu_token}
```

### 4. Buscar por Status
```bash
GET /Tarefa/ObterPorStatus?status=Pendente
Authorization: Bearer {seu_token}
```

### 5. Atualizar Tarefa
```bash
PUT /Tarefa/1
Authorization: Bearer {seu_token}
Content-Type: application/json

{
  "titulo": "Estudar .NET - Atualizado",
  "descricao": "Revisar conceitos avançados",
  "data": "2026-01-20T10:00:00",
  "status": "EmAndamento"
}
```

### 6. Excluir Tarefa
```bash
DELETE /Tarefa/1
Authorization: Bearer {seu_token}
```

## Endpoints Disponíveis

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/auth/login` | Realizar login e obter token |
| GET | `/Tarefa/{id}` | Obter tarefa por ID |
| GET | `/Tarefa/ObterTodos` | Listar todas as tarefas |
| GET | `/Tarefa/ObterPorTitulo?titulo={titulo}` | Buscar por título |
| GET | `/Tarefa/ObterPorData?data={data}` | Filtrar por data |
| GET | `/Tarefa/ObterPorStatus?status={status}` | Filtrar por status |
| POST | `/Tarefa` | Criar nova tarefa |
| PUT | `/Tarefa/{id}` | Atualizar tarefa |
| DELETE | `/Tarefa/{id}` | Excluir tarefa |

## Status das Tarefas

- **Pendente**: Tarefa aguardando início
- **EmAndamento**: Tarefa em execução
- **Finalizado**: Tarefa concluída
- **Cancelado**: Tarefa cancelada
- **Atrasado**: Tarefa com data vencida

## Validações Implementadas

- Data da tarefa não pode ser vazia
- Autenticação obrigatória para todos os endpoints de tarefas
- Validação de existência da tarefa antes de atualizar/excluir
- Tratamento adequado de erros (404, 400, etc.)

## Próximos Passos

Este projeto atende completamente aos requisitos do desafio da DIO. Futuramente, posso expandir com:

- Interface frontend (React/Angular)
- Notificações por email
- Categorias para tarefas
- Anexos de arquivos
- API de relatórios

---

**Desenvolvido por [Seu Nome]** como parte do desafio DIO - Trilha .NET 2026