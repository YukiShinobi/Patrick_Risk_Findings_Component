# Risk Findings and Employee Comments

ASP.NET Core MVC component for recording and managing employee risk-review findings associated with medical-device safety and recall notices.

## Functionality
- Create a finding for a selected safety/recall notice.
- Persist findings through Entity Framework Core.
- List and view findings owned by the authenticated employee.
- Edit finding title, analysis, relevance and status.
- Add employee comments to findings.
- Validate submitted data.
- Restrict finding access by authenticated employee ownership.
- Handle database update errors and edit concurrency conflicts.

## Integration
The component is intended for the shared ASP.NET Core MVC application and expects the shared `SafetyNotice`, `ApplicationUser`, `ApplicationDbContext`, ASP.NET Core Identity, and an `Employee` role. Align namespaces/property names, add the Finding/Comment mappings to the existing DbContext, create the EF Core migration, and run the documented tests against the integrated application.
