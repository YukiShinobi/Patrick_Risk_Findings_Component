# Patrick — Manual Test Specification

Record the **actual** result, pass/fail, screenshot and Git commit during integration.

| ID | Test | Expected result | Actual result | P/F |
|---|---|---|---|---|
| P01 | Open Add Finding for valid notice | Form opens and displays selected notice | | |
| P02 | Create valid High relevance finding | Finding saved and Details opens | | |
| P03 | Leave title empty | Validation rejects submission | | |
| P04 | Enter analysis shorter than 10 chars | Validation rejects submission | | |
| P05 | Analysis at valid length | Finding saves | | |
| P06 | View My Findings | Employee's stored findings appear | | |
| P07 | View finding details | Correct notice, analysis, relevance and status appear | | |
| P08 | Edit title and analysis | Changes persist after save | | |
| P09 | Change relevance | New relevance persists | | |
| P10 | Change status to Under Review | New status persists | | |
| P11 | Add valid employee comment | Comment persists and displays | | |
| P12 | Add empty comment | Comment rejected | | |
| P13 | Add >1500 character comment | Comment rejected | | |
| P14 | Request nonexistent finding ID | Not Found returned | | |
| P15 | Request another employee's finding | Finding not disclosed / Not Found | | |
| P16 | Selected notice no longer exists during Create | Validation/error prevents orphan finding | | |
| P17 | Database save failure | Friendly error; application does not crash | | |
| P18 | Two edits conflict | Concurrency message shown instead of silent overwrite | | |

## Bug-fix evidence format
For any genuine failed test record:
1. Problem
2. Expected behaviour
3. Actual behaviour
4. Root cause
5. Code change / commit
6. Retest result
