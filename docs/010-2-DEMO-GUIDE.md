# Element 010-2 — Individual Component Demonstration

**Module:** MOD003263 Software Engineering  
**Component:** Risk Findings & Employee Comments  
**Related user stories:** US08–US10  
**Application:** Medical Device Safety and Recall Notice Platform

This guide prepares your individual part of the Week 12 team demonstration. Use the integrated team application. Do not claim a feature works unless you have run it successfully in that application.

## Demonstration sequence

### 1. Explain the purpose

> My component lets an authenticated employee record a risk-review finding against a safety or recall notice, revisit and update their own finding, and add comments to support the review. The controller checks ownership so one employee cannot view or edit another employee's finding through these actions.

### 2. Show the findings list

1. Sign in with a test account that has the Employee role.
2. Open the Findings page.
3. Show that the list contains only findings owned by the signed-in employee.
4. Point out the linked notice reference, title, relevance, status and last-updated time.

**Code to explain:** FindingsController.Index, Finding, Views/Findings/Index.cshtml.

### 3. Create a finding

1. Choose an existing safety/recall notice and open the create-finding form.
2. Enter a short title and analysis of at least 10 characters.
3. Choose a relevance level and status.
4. Save, then show the saved details page.
5. If time permits, try an empty title or too-short analysis and show the validation messages.

**Code to explain:** FindingFormViewModel, FindingsController.Create (GET and POST), _FindingForm.cshtml, Finding model.

### 4. Edit a finding

1. Open a finding owned by the test account and select Edit.
2. Change its title or analysis and save.
3. Show the updated value and status.
4. Explain that the POST action checks the route ID, validates the input, scopes the database lookup to the current employee, and handles concurrency/database errors.

**Code to explain:** FindingsController.Edit (GET and POST), RowVersion/timestamp configuration. Confirm the host database provider supports the configured concurrency token before presenting concurrency handling as tested.

### 5. Add an employee comment

1. Open an owned finding.
2. Enter a short comment and submit it.
3. Show the comment, author and timestamp.
4. If time permits, try a blank comment and confirm it is rejected.

**Code to explain:** FindingsController.AddComment, Comment, and the comments section in Views/Findings/Details.cshtml.

### 6. Demonstrate the access-control boundary

Use a second test employee account, if available:

1. Record the ID of a finding owned by the first account.
2. Sign in as the second employee.
3. Attempt to open the first employee's finding URL and its edit URL.
4. Confirm the application returns Not Found and does not disclose the finding.
5. Attempt to add a comment to that finding and confirm it is rejected.

Use test accounts and test data only. Never demonstrate using another person's real account.

**Code to explain:** [Authorize(Roles = "Employee")] and the ownership filters in Index, Details, Edit, and AddComment.

## Likely tutor questions

**Why use a view model?**  
The form accepts only the fields the employee is allowed to edit. It avoids binding a database entity directly from user input, including ownership fields such as EmployeeId.

**How is ownership enforced?**  
The controller reads the current user's identity ID from the authenticated claims and includes it in the database query. A record owned by a different employee is not returned.

**How is input validated?**  
Data annotations validate title and analysis lengths, while controller logic verifies that the selected notice exists and that comments are not empty or over the character limit. Enum values are also validated against the declared enum types.

**Why use asynchronous EF Core calls?**  
Database calls such as ToListAsync, SingleOrDefaultAsync, and SaveChangesAsync avoid synchronously blocking the request thread while waiting for database I/O.

**What is concurrency handling for?**  
It is intended to detect when a finding changes between the time it is loaded and the time an employee saves edits. Its actual behaviour depends on the host database provider and its migration/configuration.

**What would you test?**  
Valid and invalid creation, successful edit, wrong-owner access, comment validation, database failures, and concurrent updates.

## Before Week 12

- [ ] Confirm the component is integrated into the team's main application.
- [ ] Confirm the database has the Findings and Comments mappings and a successfully applied migration.
- [ ] Confirm the SafetyNotice, ApplicationUser, and ApplicationDbContext names and properties match the host project.
- [ ] Confirm test employees have the expected Employee role.
- [ ] Run every demonstration step in the integrated application.
- [ ] Keep screenshots and Git history showing your own contribution.
- [ ] Practise explaining the code in your own words.

## Evidence record

Fill this in after running the demo. Do not mark a step as passed based only on reading the source code.

| Check | Result | Evidence / notes |
|---|---|---|
| Findings list shows only owned findings | Not yet verified | |
| Create and save finding | Not yet verified | |
| Invalid finding input rejected | Not yet verified | |
| Edit persists changes | Not yet verified | |
| Add comment | Not yet verified | |
| Blank/oversized comment rejected | Not yet verified | |
| Wrong-owner access blocked | Not yet verified | |
| Build and automated tests pass | Not yet verified | |
