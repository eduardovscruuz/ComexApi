# ComexApi

Este projeto é uma API desenvolvida em **.NET 8** para gerenciar **Manifestos** e **Escalas**, incluindo o relacionamento entre eles.

## 🚀 Tecnologias Utilizadas

- .NET 8 (Web API)
- Entity Framework Core
- SQL Server (ou outro banco configurado)
- Newtonsoft.Json (para desserialização de seeders)

---

## 📂 Estrutura do Projeto

```
ComexApi/
│-- Controllers/        # Controllers da API
│-- Data/               # Contexto do banco + Seeders
│-- Models/             # Modelos de dados (Manifesto, Escala, Vínculo)
│-- Migrations/         # Histórico das migrations
│-- Program.cs          # Configuração da aplicação
```

---

## ⚙️ Configuração do Projeto

### 1. Clonar o repositório
```bash
git clone https://github.com/seu-usuario/ComexApi.git
cd ComexApi
```

### 2. Criar a base de dados via Migration
```bash
dotnet ef database update
```

### 3. Popular o banco de dados (Seeder)
Os arquivos JSON com dados mockados estão em `Data/Seed/`.

O seeding roda automaticamente no **startup da aplicação** (em `Program.cs`):
```csharp
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(dbContext);
}
```

Ou seja, na primeira execução da API (`dotnet run`), o banco será populado.

---

## ▶️ Executar a Aplicação

```bash
dotnet run
```

A API ficará disponível em:
```
https://localhost:7002 (HTTPS)
http://localhost:5252 (HTTP)
```

---

## 📌 Endpoints (Exemplos)

### Manifestos
- `GET /api/manifestos` → Lista todos os manifestos
- `GET /api/manifestos/{id}` → Detalhes de um manifesto
- `POST /api/manifestos` → Cria um manifesto

### Escalas
- `GET /api/escalas` → Lista todas as escalas
- `GET /api/escalas/{id}` → Detalhes de uma escala
- `POST /api/escalas` → Cria uma escala

### Vínculos (Manifesto ↔ Escala)
- `POST /api/vinculos` → Vincula um manifesto a uma escala
- `GET /api/vinculos` → Lista vínculos existentes

---

## 🛠️ Observações

- Os arquivos **JSON em `Data/Seed/` não devem ser excluídos**, pois servem como fonte de dados mockados para testes.  
- Caso queira limpar e recriar o banco, basta rodar:
  ```bash
  dotnet ef database drop -f
  dotnet ef database update
  dotnet run
  ```
  Assim o **Seeder** será executado novamente.

---

## 📜 Licença

Projeto desenvolvido para fins de **teste técnico**.
