# 🚢 Comex API

API desenvolvida em **.NET 8** para gerenciamento de **Escalas**, **Manifestos** e seus **Vínculos**.  
Permite cadastrar, listar e excluir escalas e manifestos, além de criar vínculos entre eles.

---

## 📌 Tecnologias utilizadas
- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

---

## ⚙️ Estrutura do Projeto
- **Program.cs** → Configuração da aplicação, injeção de dependências, CORS e Swagger.  
- **Data/AppDbContext.cs** → Contexto do banco de dados e configuração do EF Core.  
- **Models** → Entidades (`Escala`, `Manifesto`, `Vinculo`) e enums (`StatusEscala`, `TiposManifesto`).  
- **Services** → Lógica de negócio e regras de manipulação do banco.  
- **Controllers** → Endpoints da API (Escalas, Manifestos e Vinculos).

---

## 🚀 Como rodar o projeto

### 1️⃣ Clonar o repositório
```bash
git clone https://github.com/seu-repo/comex-api.git
cd comex-api
```

### 2️⃣ Configurar o banco de dados
No arquivo `appsettings.json`, configure a connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ComexDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3️⃣ Criar o banco e aplicar migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4️⃣ Rodar a API
```bash
dotnet run
```

Por padrão, a API sobe em:
- **HTTP:** `http://localhost:5252`
- **HTTPS:** `https://localhost:7002`

Swagger disponível em:
```
http://localhost:5252/swagger
```

---

## 📚 Endpoints principais

### 🔹 Escalas (`/api/escalas`)
- `GET /api/escalas` → Lista todas as escalas  
- `GET /api/escalas/{id}` → Busca uma escala por ID  
- `POST /api/escalas` → Cria uma nova escala  
- `DELETE /api/escalas/{id}` → Remove uma escala  

### 🔹 Manifestos (`/api/manifestos`)
- `GET /api/manifestos` → Lista todos os manifestos  
- `GET /api/manifestos/{id}` → Busca um manifesto por ID  
- `POST /api/manifestos` → Cria um novo manifesto  
- `DELETE /api/manifestos/{id}` → Remove um manifesto  

### 🔹 Vínculos (`/api/vinculos`)
- `GET /api/vinculos` → Lista todos os vínculos  
- `GET /api/vinculos/manifesto/{manifestoId}` → Lista escalas de um manifesto  
- `GET /api/vinculos/escala/{escalaId}` → Lista manifestos de uma escala  
- `POST /api/vinculos/{manifestoId}/{escalaId}` → Cria vínculo entre manifesto e escala  
- `DELETE /api/vinculos/{manifestoId}/{escalaId}` → Remove vínculo  

---

## 🛠️ Regras de Negócio
- Um **manifesto** só pode ser vinculado a uma **escala** se:
  - O **navio** for o mesmo.
  - A escala **não estiver cancelada**.
  - Não existir vínculo duplicado.
- Ao excluir um **manifesto** ou uma **escala**, todos os vínculos relacionados são removidos.

---

## 🧑‍💻 Contribuição
1. Fork o projeto  
2. Crie uma branch (`git checkout -b feature/nova-funcionalidade`)  
3. Commit suas alterações (`git commit -m 'Adiciona nova funcionalidade'`)  
4. Push para a branch (`git push origin feature/nova-funcionalidade`)  
5. Abra um Pull Request 🚀  

---

## 📄 Licença
Este projeto está sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
