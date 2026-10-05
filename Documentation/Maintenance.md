# Patrick — Maintenance Considerations

Over a two-year maintenance period, the Findings component would require:

1. **Schema compatibility:** if the team's SafetyNotice dataset/model changes, the foreign-key
   relationship and display fields used by Findings must be reviewed and migrated safely.
2. **Validation changes:** finding lengths, statuses and relevance categories should be kept aligned
   with company risk-review processes.
3. **Database migrations:** schema changes should use reviewed EF Core migrations with backups and
   rollback planning for production data.
4. **Security/ownership:** access-control tests should be rerun after authentication or role changes
   to ensure employees cannot view or edit findings they do not own.
5. **Concurrency and defects:** monitor edit conflicts and database failures; preserve logs without
   exposing sensitive data to users.
6. **Performance:** if findings grow substantially, review indexes for `EmployeeId`,
   `SafetyNoticeId` and frequently queried date fields.
7. **Automated regression tests:** retain tests around create/store/view/edit/comment behaviour when
   the wider application changes.
