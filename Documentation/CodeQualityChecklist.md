# Code Quality Checklist

- [ ] Project builds with zero compilation errors.
- [ ] Remove unused `using` directives and dead code.
- [ ] Remove obsolete commented-out implementation code.
- [ ] Keep naming consistent with the shared application.
- [ ] Confirm Create/Edit use the validated ViewModel.
- [ ] Confirm anti-forgery validation is active on POST actions.
- [ ] Confirm employee ownership restrictions on Index, Details, Edit and comments.
- [ ] Confirm database errors return usable feedback rather than an unhandled exception.
- [ ] Confirm concurrency conflicts do not silently overwrite another update.
- [ ] Confirm all Finding/Comment relationships match the final ERD.
- [ ] Run and record manual tests against the integrated application.
- [ ] Record genuine failed tests, fixes and retest outcomes.
- [ ] Capture relevant Git commits and screenshots for individual evidence.
