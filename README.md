# Countries Management System API

A robust RESTful API built as a technical assessment to manage Countries and their associated Cities. Engineered with modern software design principles focusing on maintainability, scalability, and separation of concerns.

---

## 🚀 Tech Stack & Architecture

- **Framework:** .NET 10 / ASP.NET Core Web API
- **Database:** SQL Server & Entity Framework Core
- **Architecture:** Clean Architecture
- **Patterns Used:**
  - **CQRS-style Organization** — Clear separation of Commands and Queries per feature
  - **Result Pattern** — Internal `DomainResult` wrapper for safe business logic execution without exceptions
  - **Repository & Unit of Work** — Abstracts data access and manages transaction boundaries
  - **Standardized API Responses** — Uniform `ApiResponse<T>` wrapper for all HTTP responses
  - **FluentValidation** — Strongly-typed, decoupled request validation pipeline

---

## 📁 Project Structure

```
CountriesManagementSystem/
├── CountriesManagementSystem.API           # Controllers, Middlewares, Program.cs
├── CountriesManagementSystem.Application   # Services, DTOs, Validators
├── CountriesManagementSystem.Domain        # Entities, Interfaces, DomainResult
├── CountriesManagementSystem.Infrastructure # DbContext, Repositories, Migrations, Seeding
└── CountriesManagementSystem.Shared        # ApiResponse, PageListResult, SearchListDto
```

---

## ⚙️ Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server (LocalDB or a standard instance)

---

## 🛠 Installation & Setup

**1. Clone the repository**
```bash
git clone https://github.com/mohamednour2019/CountriesManagementSystem-Crud--The-Bridge-BFC-Assessment.git
```

**2. Configure the Database Connection**

Open `CountriesManagementSystem.API/appsettings.json` and update the connection string:
```json
"ConnectionStrings": {
  "default": "server=.; Initial Catalog=CountriesDB; Integrated Security=True; TrustServerCertificate=true;"
}
```

**3. Run the Application**
```bash
cd CountriesManagementSystem/CountriesManagementSystem.API
dotnet run --launch-profile https
```

> ✅ On startup, the application **automatically** creates the database, applies all migrations, and seeds initial data (Countries & Cities). No manual migration commands needed.

**4. Explore the API**

Navigate to Swagger UI in your browser:
```
For HTTP
http://localhost:5221/swagger
OR
For HTTPS
https://localhost:7291/swagger

```

---

## 📌 API Endpoints

### Countries

| Method | Endpoint | Description | Validations |
|--------|----------|-------------|-------------|
| `GET` | `/api/Countries` | Get paginated list of countries | — |
| `GET` | `/api/Countries/{id}` | Get country by ID (includes its cities) | `id` required · Country must exist |
| `POST` | `/api/Countries` | Add a new country | `Name` required · 3–50 chars · Must be unique globally |
| `PUT` | `/api/Countries/{id}` | Update a country | `Name` required · 3–50 chars · Must be unique (excluding self) · Country must exist |
| `DELETE` | `/api/Countries/{id}` | Delete a country | Country must exist · Cannot delete if it has associated cities |

### Cities

| Method | Endpoint | Description | Validations |
|--------|----------|-------------|-------------|
| `GET` | `/api/Cities` | Get paginated list of cities | — |
| `GET` | `/api/Cities/{id}` | Get city by ID | `id` required · City must exist |
| `GET` | `/api/Cities/by-country/{countryId}` | Get paginated cities by country | Country must exist |
| `POST` | `/api/Cities` | Add a new city | `Name` required · 3–50 chars · `CountryId` required · Country must exist · Name must be unique within the same country |
| `PUT` | `/api/Cities/{id}` | Update a city | `Name` required · 3–50 chars · `CountryId` required · Country must exist · Name must be unique within the same country (excluding self) · City must exist |
| `DELETE` | `/api/Cities/{id}` | Delete a city | City must exist |

### 📄 Pagination

All paginated endpoints (`GET /api/Countries`, `GET /api/Cities`, `GET /api/Cities/by-country/{countryId}`) accept the following query parameters:

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Paginator.CurrentPage` | `int` | `1` | Page number to retrieve (min: 1) |
| `Paginator.PageSize` | `int` | `10` | Number of records per page (min: 1) |

> If `CurrentPage` is less than 1, it defaults to **1**. If `PageSize` is 0 or negative, it defaults to **10**.

---



