# Technical Foundations

## Tech Stack (Agreed)

- **Backend**: ASP.NET Core Web API (.NET 9)
- **Frontend**: Blazor WebAssembly (WASM)
- **Authentication**: ASP.NET Core Identity (JWT / Cookies)
- **Data Access**: Entity Framework Core (Code-First)
- **Database**: Microsoft SQL Server

---

## Architecture Decision: 3-Tier Architecture

```
┌─────────────────────────────────────────────────────────────┐
│  Presentation Layer: VolunteerPortal.Api                    │
│  Minimal APIs / Controllers, Auth, DI Config                │
│  References: BusinessLogic only                             │
└─────────────────────────┬───────────────────────────────────┘
                          │ references
                          ▼
┌─────────────────────────────────────────────────────────────┐
│  Business Logic Layer: VolunteerPortal.BusinessLogic        │
│  Services, DTOs, Validators, Repository Interfaces          │
│  References: DataAccess only                                │
└─────────────────────────┬───────────────────────────────────┘
                          │ references
                          ▼
┌─────────────────────────────────────────────────────────────┐
│  Data Access Layer: VolunteerPortal.DataAccess              │
│  Entities, EF Core, Repositories, Migrations                │
└─────────────────────────┬───────────────────────────────────┘
                          │
                          ▼
              Database (SQL Server)
```

### Key Rules

| Rule | Implementation |
|------|----------------|
| **Entities in DataAccess** | Pure C# classes with EF Core annotations |
| **Repository Interfaces in BusinessLogic** | Contracts owned by BusinessLogic |
| **Repository Implementations in DataAccess** | EF Core implementations |
| **Services in BusinessLogic** | Business logic, validation, orchestration |
| **DTOs in BusinessLogic** | Shared between Api ↔ BusinessLogic |
| **No Domain Logic in Entities** | Entities are anemic (properties only) |
| **Dependency Flow** | Api → BusinessLogic → DataAccess |

