# Individual component — next-week evidence and demonstration

## Source-code inventory
- Controller: `Controllers/FindingsController.cs` — Index, Details, Create (GET/POST), Edit (GET/POST), AddComment (POST).
- Views: `Views/Findings/Index.cshtml`, `Create.cshtml`, `Details.cshtml`, `Edit.cshtml`, `_FindingForm.cshtml`.
- Models: `Finding`, `Comment`, and `FindingFormViewModel`.
- ERD: `docs/Final-ERD.md`.
- Validation tests: `Tests/FindingsControllerTests.cs` (xUnit; requires team test project).

## What is not yet verified
- A successful build or running demonstration: this repository is a component, not a complete ASP.NET Core application.
- Shared `ApplicationDbContext`, `ApplicationUser`, `SafetyNotice` compatibility.
- EF Core migration and persisted records.
- Authentication, authorization and employee role configuration.
- Automated integration tests, real test output, and screenshots.
- Overall 70% functional completion: this cannot be claimed from source count.

## Integration sequence (team application)
1. Copy component source into the agreed project namespaces, resolving conflicts rather than replacing existing team files.
2. Verify shared `SafetyNotice.Id`, `Reference`, `DeviceName`, `ApplicationUser`, and role `Employee`.
3. Add `DbSet<Finding>` and `DbSet<Comment>` to the shared `ApplicationDbContext`.
4. Call `PatrickFindingModelConfiguration.ConfigurePatrickFindingModels(modelBuilder)` in `OnModelCreating` after `base.OnModelCreating(modelBuilder)`.
5. Confirm the final ERD and database provider; configure the `RowVersion` concurrency token appropriately.
6. Generate and review an EF Core migration, then apply it to the development database.
7. Link a safety notice's Details page to `Findings/Create?safetyNoticeId=<id>`.
8. Build, launch, sign in as an Employee, and test create → list → details → edit → comment.
9. Verify that another employee cannot access the finding by changing the ID in the URL.
10. Capture screenshots, actual test results, and commit/PR links for the assessment.

## Demonstration checklist
- [ ] Explain the ERD's four entities and their relationships.
- [ ] Show the controller and at least one Razor view.
- [ ] Run the integrated application and create a finding.
- [ ] Show persistence by returning to the findings list.
- [ ] Edit a finding and add a comment.
- [ ] Explain ownership filtering and input validation.
- [ ] Show actual tests and evidence of personal contribution.

Do not claim unrun tests as passed.
