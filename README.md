# Risk Findings and Employee Comments

ASP.NET Core MVC component for recording and managing employee risk-review findings associated with medical-device safety and recall notices.

## Functionality

- Create a finding for a selected safety/recall notice.
- Persist findings through Entity Framework Core.
- List and view findings owned by the authenticated employee.
- Edit finding title, analysis, relevance and status.
- Add employee comments to findings.
- Validate submitted data, including the allowed relevance and status enum values.
- Restrict finding access by authenticated employee ownership.
- Handle database update errors and edit concurrency conflicts, subject to host database-provider support.

## Integration

The component is intended for the shared ASP.NET Core MVC application and expects the shared SafetyNotice, ApplicationUser, ApplicationDbContext, ASP.NET Core Identity, and an Employee role. Align namespaces/property names, add the Finding/Comment mappings to the existing DbContext, create the EF Core migration, and run the documented tests against the integrated application.

This repository is a component, not a standalone executable application. Its final build and runtime behaviour must be verified inside the team's host solution.

## Assessment preparation

For the MOD003263 Software Engineering Element 010-2 individual component demonstration:

- [Demo guide](docs/010-2-DEMO-GUIDE.md)
- [Integration checklist](docs/INTEGRATION-CHECKLIST.md)
- [Manual test plan](docs/MANUAL-TEST-PLAN.md)

The form-validation test file contains executable unit tests for data annotations. Controller/database integration tests still require a test fixture aligned with the final host application's database and authentication setup.

## Scope and maintenance — October 2026

An ASP.NET Core MVC component designed for integration into a host application's employee risk-review workflow. It is not a standalone clinical or recall-management system; verify shared identity, EF Core migrations and user ownership controls during integration.

Related projects: [Project Creation](https://github.com/YukiShinobi/project-creation), [Veyra](https://github.com/YukiShinobi/YukiShinobi-veyra), [Simply Management](https://github.com/YukiShinobi/SIMPLYMANAGEMENT-WEBSITE). Documentation is not an assertion of a verified live deployment.
