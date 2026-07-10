# AGENTS.md

## Project Context

You are assisting with the development of the Volunteer Portal project.

The project is a portfolio-quality C#/.NET web application used to learn and demonstrate professional software engineering practices.

---

## Documentation

The `/docs` directory is the source of truth for project knowledge.

Review relevant documentation before making decisions.

Keep documentation synchronized with the implementation.

Update documentation when changes affect:

- Requirements
- Domain concepts
- Architecture
- API behavior
- Technical decisions

Important technical decisions should be documented as ADRs:

`docs/decisions/ADR-XXX-title.md`

---

## Decision Making

**Before making major technical decisions (architecture, patterns, libraries, database design):**

1. Read relevant documentation in `/docs`
2. Check existing ADRs in `/docs/decisions`
3. Review the current domain model and technical foundations
4. Document the decision as an ADR if it's significant

This prevents re-work and ensures consistency with established patterns.

---

## Planning

When creating plans:

1. Review existing documentation.
2. Identify affected requirements and domain concepts.
3. Consider architecture impact.
4. Ask questions if requirements are unclear.
5. Provide a plan before implementation.

Do not make assumptions about undefined behavior.

---

## Development Principles

Follow these principles:

- Prefer simple, maintainable solutions.
- Follow modern C#/.NET practices.
- Keep responsibilities separated.
- Avoid unnecessary complexity.
- Design for future extension.

Prefer:

- Dependency Injection
- Clear domain models
- Testable code
- Separation of concerns

## Workflow

For larger changes:

1. Understand the requirement.
2. Review documentation.
3. Update documentation if needed.
4. Implement the change.
5. Add tests where appropriate.
6. Verify documentation consistency.

---

## Learning Objective

This is a learning project.

When introducing new concepts, explain:

- What problem it solves.
- Why it is used.
- Alternatives and trade-offs.

Optimize for understanding and maintainability.
