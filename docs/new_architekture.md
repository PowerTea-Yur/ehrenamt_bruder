# Architekturvorschlag für unsere Blazor-WebAssembly-Anwendung

## Ziel

Die bestehende Architektur soll **nicht vollständig ersetzt**, sondern schrittweise verbessert werden.

Ziele:

- Klare Verantwortlichkeiten pro Projekt
- Weniger Kopplung zwischen den Schichten
- Höhere Testbarkeit
- Einfachere Wartbarkeit
- Keine unnötige Komplexität durch eine "reine" Clean Architecture

---

# Architektur

```text
Client
Host
Services
Abstractions
Model
ModelMigrations
```

Diese Struktur bleibt bestehen.

Es werden lediglich die Verantwortlichkeiten der einzelnen Projekte geschärft.

---

# Projektverantwortlichkeiten

## Client

**Aufgabe**

Das Blazor-WebAssembly-Frontend.

Enthält:

- Pages
- Components
- State Management
- API Clients
- Validierung der UI
- Layout
- Navigation

Soll **keine Geschäftslogik** enthalten.

---

## Host

**Aufgabe**

ASP.NET Core Einstiegspunkt.

Enthält:

- Program.cs
- Dependency Injection
- Controller / Minimal APIs
- Authentication
- Authorization
- Middleware
- Konfiguration

Host soll möglichst wenig Logik besitzen.

---

## Services

**Aufgabe**

Enthält die eigentliche Geschäftslogik.

Hier werden Use Cases implementiert.

Beispiele:

- Kunden anlegen
- Auftrag abschließen
- Rechnung erzeugen

Services sollten:

- Business Rules enthalten
- Prozesse orchestrieren
- Interfaces aus Abstractions verwenden

Services sollen **keine technischen Details kennen**.

---

## Abstractions

**Aufgabe**

Enthält ausschließlich Schnittstellen.

Beispiele:

```csharp
ICustomerRepository

IEmailSender

IFileStorage

ICurrentUser

IClock
```

Hier liegen keine Implementierungen.

---

## Model

**Aufgabe**

Enthält ausschließlich fachliche Modelle.

Zum Beispiel:

- Entities
- Value Objects
- Enums

Keine:

- DbContext
- EF Core Konfigurationen
- API Controller
- HTTP Logik

Falls DTOs verwendet werden, sollten diese möglichst klar von Domain Models getrennt werden.

---

## ModelMigrations

**Aufgabe**

Enthält ausschließlich Persistenz.

Zum Beispiel:

- DbContext
- EF Core Migrationen
- Entity Configurations
- Repository Implementierungen

Hier liegen alle EF-spezifischen Klassen.

---

# Architekturregeln

## Regel 1

Geschäftslogik gehört ausschließlich in Services.

Nicht in:

- Controller
- Razor Components
- DbContext

---

## Regel 2

Technische Implementierungen liegen außerhalb der Geschäftslogik.

Beispiele:

- SQL Server
- Entity Framework
- SMTP
- Dateisystem
- REST Clients

werden über Interfaces aus Abstractions verwendet.

---

## Regel 3

Model kennt keine Infrastruktur.

Model darf keine Referenzen besitzen auf:

- Entity Framework
- ASP.NET
- Blazor
- SQL Server

---

## Regel 4

Host dient ausschließlich zum Verdrahten der Anwendung.

Businesslogik gehört nicht in:

- Program.cs
- Controller
- Middleware

---

## Regel 5

UI enthält keine Businesslogik.

Razor-Komponenten sollen:

- Daten anzeigen
- Eingaben entgegennehmen
- Services aufrufen

Nicht jedoch Geschäftsregeln implementieren.

---

# Empfohlene Ordnerstruktur

## Client

```text
Features/
    Customer/
    Orders/
    Products/

Shared/

Infrastructure/

Pages/
```

---

## Services

```text
Customer/
    CreateCustomer.cs
    UpdateCustomer.cs
    DeleteCustomer.cs

Orders/

Products/
```

Anwendungsfälle sollen nach Fachbereich organisiert werden und nicht nach technischen Kategorien wie:

```text
Repositories/
Managers/
Helpers/
Utilities/
```

---

## Model

```text
Customer/

Order/

Invoice/

Common/
```

---

# Dependency Rules

```text
Client
    ↓
Host
    ↓
Services
    ↓
Abstractions
    ↓
Model

ModelMigrations
    ↓
Model
```

Wichtig:

Model besitzt keine Abhängigkeiten auf technische Frameworks.

---

# Langfristige Ziele

- Klare Trennung zwischen Fachlogik und Technik
- Weniger Kopplung
- Bessere Unit Tests
- Einfachere Erweiterbarkeit
- Austausch technischer Komponenten ohne Auswirkungen auf Businesslogik

---

# Nicht-Ziele

Wir möchten **keine vollständige Clean Architecture** einführen.

Insbesondere möchten wir vermeiden:

- unnötig viele Projekte
- übermäßige Anzahl an Interfaces
- Boilerplate-Code
- komplexe CQRS-/Mediator-Strukturen ohne Mehrwert

Unser Ziel ist eine **pragmatische Schichtenarchitektur**, die bewährte Prinzipien der Clean Architecture übernimmt, ohne deren gesamte Komplexität einzuführen.

---

# Mögliche Arbeitspakete

- [ ] Verantwortlichkeiten der Projekte dokumentieren
- [ ] Architekturregeln im Wiki festhalten
- [ ] Services auf Businesslogik überprüfen
- [ ] EF Core ausschließlich in ModelMigrations verwenden
- [ ] Technische Implementierungen hinter Interfaces kapseln
- [ ] Feature-basierte Ordnerstruktur im Client einführen
- [ ] Feature-basierte Ordnerstruktur in Services einführen
- [ ] Domain Models von DTOs trennen
- [ ] Architekturregeln im Code Review berücksichtigen
