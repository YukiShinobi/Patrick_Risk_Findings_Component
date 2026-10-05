# Patrick — Software Design Contribution

## Findings wireframe

```text
+---------------------------------------------------------------+
| Finding Details                                  [Edit Finding]|
| FDA reference · Device name                                   |
+---------------------------------------------------------------+
| Relevance: High              Status: Under Review             |
|                                                               |
| Risk-review analysis                                          |
| [ Employee's analysis text................................. ] |
| [ ........................................................ ] |
+---------------------------------------------------------------+
| Employee Comments                                             |
| Employee | date/time                                          |
| Existing comment                                              |
|                                                               |
| [ Add a comment............................................ ] |
|                                           [Add Comment]        |
+---------------------------------------------------------------+
```

## ERD contribution

```mermaid
erDiagram
    APPLICATION_USER ||--o{ FINDING : creates
    SAFETY_NOTICE ||--o{ FINDING : has
    FINDING ||--o{ COMMENT : contains
    APPLICATION_USER ||--o{ COMMENT : writes

    FINDING {
        int Id PK
        int SafetyNoticeId FK
        string EmployeeId FK
        string Title
        string Analysis
        int Relevance
        int Status
        datetime CreatedAtUtc
        datetime UpdatedAtUtc
        rowversion RowVersion
    }

    COMMENT {
        int Id PK
        int FindingId FK
        string AuthorId FK
        string Body
        datetime CreatedAtUtc
    }
```

## MVC contribution

```mermaid
flowchart LR
    V1[Findings/Index View] --> C[FindingsController]
    V2[Findings/Create View] --> C
    V3[Findings/Details View] --> C
    V4[Findings/Edit View] --> C
    C --> VM[FindingFormViewModel]
    C --> M1[Finding Model]
    C --> M2[Comment Model]
    M1 --> DB[(ApplicationDbContext)]
    M2 --> DB
    M1 --> SN[Team SafetyNotice Model]
    M1 --> U[Team ApplicationUser Model]
```

## Design decisions
- A ViewModel is used for Create/Edit to avoid binding the database entity directly.
- Findings are filtered by authenticated employee ID for ownership protection.
- Data Annotations provide server-side validation and work with MVC client validation.
- `AsNoTracking()` is used for read-only queries.
- A row-version token supports optimistic concurrency on edits.
- Database exceptions are handled without exposing internal exception details to the user.
