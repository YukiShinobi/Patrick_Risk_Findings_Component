# Team Integration Checklist

Patrick's component depends on names from the final team project. Before integration:

- [ ] Replace namespace `MedicalDeviceSafetyPlatform` if the team's namespace differs.
- [ ] Confirm `SafetyNotice.Id`, `.Reference` and `.DeviceName` property names.
- [ ] Confirm the authentication class is named `ApplicationUser`.
- [ ] Confirm the employee role string is exactly `Employee`.
- [ ] Add `DbSet<Finding>` and `DbSet<Comment>` to the existing ApplicationDbContext.
- [ ] Call `PatrickFindingModelConfiguration.ConfigurePatrickFindingModels(builder)`.
- [ ] Add a migration only after the shared ERD/database design is agreed.
- [ ] Link Priyanshi's Notice Details screen to:
      `asp-controller="Findings" asp-action="Create" asp-route-safetyNoticeId="..."`
- [ ] Confirm shared layout loads Bootstrap and `_ValidationScriptsPartial`.
- [ ] Run Patrick's manual tests after integration.
- [ ] Record real failures/fixes/commits for the individual report.
