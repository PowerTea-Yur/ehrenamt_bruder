# UML Diagrams (3-Tier Architecture)

Layer-based split for readability. Each diagram focuses on one architectural concern.

## Diagram Index

| # | Diagram | Project | Description |
|---|---------|---------|-------------|
| 01 | [Domain Model](01-domain-model.mermaid) | **DataAccess** | Entities, Value Objects, Relationships (ERM view) |
| 02 | [DTOs](02-dtos.mermaid) | **BusinessLogic** | Response DTOs, Create/Update/Filter Request DTOs |
| 03 | [Services](03-services.mermaid) | **BusinessLogic** | Service Interfaces, Implementations, Dependencies |
| 04 | [Repositories](04-repositories.mermaid) | **DataAccess** | Repository Interfaces, Implementations, UnitOfWork, DbContext |

## Viewing

1. **VS Code**: Install "Mermaid Preview" extension, open `.mermaid` files
2. **Mermaid Live**: https://mermaid.live - paste content
3. **GitHub**: Renders natively in markdown files

## 3-Tier Architecture Mapping

```
┌─────────────────────────────────────────────────────────────┐
│  Presentation Layer: VolunteerPortal.Api                    │
│  Minimal APIs / Controllers, Auth, DI Config                │
│  References: BusinessLogic only                             │
└─────────────────────────┬───────────────────────────────────┘
                          │ references
                          ▼
┌──────────────────────────────────────────────────────────────┐
│  Business Logic Layer: VolunteerPortal.BusinessLogic         │
│  • 02-dtos.mermaid: DTOs (API contracts)                     │
│  • 03-services.mermaid: Service Interfaces + Implementations │
│  • 04-repositories.mermaid: Repository Interfaces (contracts)│
│  • Validators (FluentValidation), Mapping Config (Mapster)   │
│  References: DataAccess only                                 │
└─────────────────────────┬────────────────────────────────────┘
                          │ references
                          ▼
┌─────────────────────────────────────────────────────────────┐
│  Data Access Layer: VolunteerPortal.DataAccess              │
│  • 01-domain-model.mermaid: Entities + Value Objects        │
│  • 04-repositories.mermaid: Repository Implementations      │
│  • DbContext, Migrations, EF Core Configuration             │
└─────────────────────────┬───────────────────────────────────┘
                          │
                          ▼
              Database (SQL Server)
```

## Key 3-Tier Rules Reflected in Diagrams

| Rule | Diagram Representation |
|------|------------------------|
| **Entities in DataAccess** | 01-domain-model shows entities (no behavior, anemic) |
| **Repository Interfaces in BusinessLogic** | 04-repositories shows interfaces (BusinessLogic owns contracts) |
| **Repository Implementations in DataAccess** | 04-repositories shows implementations (DataAccess owns EF) |
| **Services in BusinessLogic** | 03-services shows service implementations |
| **DTOs in BusinessLogic** | 02-dtos shared between Api ↔ BusinessLogic |
| **No Domain Logic in Entities** | Entities have only properties, no methods |
| **Dependency Flow: Api → BusinessLogic → DataAccess** | Arrows only flow downward |

## Domain Model (01) - Core Entities (DataAccess Project)

| Domain | Entities |
|--------|----------|
| **User & Identity** | User, OrganizationMembership |
| **Volunteer** | VolunteerProfile, VolunteerSkill, VolunteerCertification, Availability |
| **Organization** | Organization, ShiftTemplate, ShiftTemplateSkill, ShiftTemplateCertification |
| **Event** | Event, EventSeries, EventOccurrence, Shift, ShiftSlot |
| **Registration** | Registration, AttendanceRecord, WaitlistEntry |

## Value Objects (Immutable, Equality by Value)
`Address`, `Skill`, `Certification`, `ShiftTime`, `EventRecurrence`

## Service Layer (03) - 8 Services (BusinessLogic Project)

| Service | Responsibility |
|---------|----------------|
| `VolunteerService` | Profile, Skills, Certs, Availability, Event Discovery, Registration |
| `OrganizationService` | CRUD, Membership Management |
| `EventService` | Event CRUD, Publishing, Shift Management |
| `ShiftService` | Templates, Requirements (Skills/Certs) |
| `RegistrationService` | Sign-up, Waitlist, Attendance, Check-in/out |
| `SkillService` | Skill Catalog CRUD |
| `CertificationService` | Cert Catalog CRUD, Expiry Tracking |
| `EventSeriesService` | Recurring Events, Occurrence Generation |

## Repository Layer (04) - 17 Repositories + UnitOfWork (DataAccess Project)

Each entity has a dedicated repository interface (in BusinessLogic) + EF Core implementation (in DataAccess).
`UnitOfWork` manages transactions across repositories.

## Diagram Conventions

| Stereotype | Meaning | Project |
|------------|---------|---------|
| `<<valueObject>>` | Immutable, equality by value | DataAccess |
| `<<dto>>` | Data Transfer Object (API response) | BusinessLogic |
| `<<createDto>>` / `<<updateDto>>` / `<<filterDto>>` | Request payloads | BusinessLogic |
| `<<interface>>` | Service/Repository contract | BusinessLogic |
| `<<service>>` | Business logic implementation | BusinessLogic |
| `<<repository>>` | Data access implementation | DataAccess |
| `<<unitOfWork>>` | Transaction boundary | DataAccess |
| `<<dbContext>>` | EF Core DbContext | DataAccess |

