# Integration Checklist — Risk Findings & Employee Comments

This repository is a component intended for a host ASP.NET Core MVC application, not a standalone application. Complete these checks against the team's real solution before calling the component finished.

## Host project compatibility

- [ ] Confirm the host solution's target .NET version and EF Core version.
- [ ] Confirm the host namespace and actual definitions for ApplicationDbContext, ApplicationUser, and SafetyNotice.
- [ ] Confirm SafetyNotice exposes the Id, Reference, and DeviceName properties used by the component, or adapt the component to the actual names.
- [ ] Confirm the signed-in employee's ID is available through ClaimTypes.NameIdentifier.
- [ ] Confirm the expected Employee role exists and is assigned to the demo account.
- [ ] Confirm MVC views have the required tag helpers and validation scripts enabled.

## Entity Framework Core

- [ ] Add DbSet<Finding> and DbSet<Comment> to the existing shared context.
- [ ] Call PatrickFindingModelConfiguration.ConfigurePatrickFindingModels(builder) from OnModelCreating, after base.OnModelCreating(builder).
- [ ] Review foreign-key delete behaviours against the host schema.
- [ ] Create and apply a migration in the host solution.
- [ ] Confirm the provider supports the configured [Timestamp]/RowVersion concurrency token. SQL Server rowversion semantics are provider-specific; do not assume identical behaviour with SQLite, PostgreSQL, or another provider.
- [ ] Run a create, read, edit and comment round trip against a non-production database.

## Security and validation

- [ ] Confirm all controller actions require the expected authentication and role.
- [ ] Confirm the current employee ID is assigned server-side; it must never come from a posted form field.
- [ ] Confirm details/edit/comment lookups enforce ownership in the database query.
- [ ] Confirm POST forms include antiforgery tokens.
- [ ] Confirm invalid enum values, invalid notice IDs, empty comments and over-limit comments are rejected.
- [ ] Confirm logs contain useful exception context without exposing internal errors to end users.

## Build and tests

- [ ] Build the host solution with the component included.
- [ ] Run existing unit and integration tests.
- [ ] Add/execute tests for valid creation, invalid input, edit, wrong-owner access, comments, and concurrency.
- [ ] Manually run the steps in 010-2-DEMO-GUIDE.md.
- [ ] Record the exact build/test commands, results, and any known limitations.

## Important status note

A source review does not prove successful integration. At the last recorded status, the component had not yet been verified inside the final team application. Mark each item above only after it has been checked in the host repository.
