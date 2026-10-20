# Manual Test Plan — Risk Findings & Employee Comments

Use a non-production environment with test notices and at least two test employees. Record actual results and evidence after execution.

| ID | Scenario | Steps | Expected result | Actual result |
|---|---|---|---|---|
| RF-01 | Findings list | Sign in as Employee A and open Findings | Only A's findings are listed | Not yet run |
| RF-02 | Create valid finding | Select an existing notice, enter valid title/analysis, select relevance/status, save | Finding persists and details page opens | Not yet run |
| RF-03 | Missing notice | Submit a notice ID that does not exist | Validation error; no finding created | Not yet run |
| RF-04 | Invalid title | Submit blank title or title longer than 150 characters | Validation error; no finding created | Not yet run |
| RF-05 | Short analysis | Submit analysis shorter than 10 characters | Validation error; no finding created | Not yet run |
| RF-06 | Invalid enum value | Submit a numeric value not defined by the relevance/status enum | Validation error; no invalid enum value persisted | Not yet run |
| RF-07 | Edit owned finding | Employee A changes their own finding and saves | Changes persist and update time changes | Not yet run |
| RF-08 | Wrong-owner details | Employee B requests Employee A's finding ID | Not Found; finding details are not disclosed | Not yet run |
| RF-09 | Wrong-owner edit | Employee B requests/posts an edit for Employee A's finding | Not Found; no data changed | Not yet run |
| RF-10 | Add comment | Employee A posts a valid comment on their finding | Comment persists with A as author and a timestamp | Not yet run |
| RF-11 | Empty comment | Employee A submits whitespace-only comment | Rejected; no comment persisted | Not yet run |
| RF-12 | Oversized comment | Employee A submits more than 1500 characters | Rejected; no comment persisted | Not yet run |
| RF-13 | Wrong-owner comment | Employee B posts a comment against Employee A's finding | Not Found; no comment persisted | Not yet run |
| RF-14 | Concurrency conflict | Two sessions edit the same finding; save session one, then session two | Stale update is rejected and a useful message appears, if provider token support is configured | Not yet run |
| RF-15 | Database failure | Simulate a safe test-environment persistence failure | User sees a generic error; exception details are logged server-side | Not yet run |

## Execution record

- Environment / branch:
- Date:
- Tester:
- Build command and result:
- Test command and result:
- Screenshot or log references:
- Failures and follow-up commits:

Do not change a result from Not yet run to Passed until the scenario has been executed in the integrated host application.
