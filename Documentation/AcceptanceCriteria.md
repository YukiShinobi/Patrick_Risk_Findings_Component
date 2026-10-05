# Patrick — Acceptance Criteria

## US08 — Add Employee Findings
**Given** an authenticated employee has selected a valid safety/recall notice,  
**when** they open Add Finding, enter a valid title, analysis, relevance and status, and save,  
**then** a Finding linked to that notice and employee is created.

Invalid or missing required data must be rejected with validation feedback.

## US09 — Store Findings
A successfully created finding must persist in the application database, retain its relationship
to the selected SafetyNotice and employee, and appear in the employee's findings list on later requests.

## US10 — Edit/View Findings
An employee can view the details of a finding they own, including its linked notice and comments.
They can edit title, analysis, relevance and status. Updates persist. A user must not be able to
retrieve or edit another employee's finding through the Findings controller.

## Employee Comments
The owner can add a non-empty comment of no more than 1500 characters to a finding.
The comment is persisted and displayed chronologically.
