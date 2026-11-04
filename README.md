# Risk Findings and Employee Comments — runnable individual demo

This branch contains a **standalone local demonstration** of the individual ASP.NET Core MVC component, in addition to the original integration-ready controller, models, and views. It is not the team's combined application.

## Requirements
- Windows 10/11, macOS, or Linux
- .NET 8 SDK (not only the runtime): https://dotnet.microsoft.com/download/dotnet/8.0
- Git or GitHub ZIP download

## Run locally
Open a terminal in the downloaded repository folder:

```powershell
dotnet --info
dotnet restore MedicalDeviceSafetyPlatform.csproj
dotnet build MedicalDeviceSafetyPlatform.csproj
dotnet run --project MedicalDeviceSafetyPlatform.csproj --urls http://localhost:5077
```

Visit **http://localhost:5077**. Click **Register**, enter an email and password (8+ characters with upper/lowercase, digit, and symbol), then select **Add finding** beside one of the seeded safety notices. Create, view, edit, and comment on a finding. Use **My Findings** to show saved records.

The SQLite database `findings-demo.db` is created on first launch and excluded from Git. To reset the **local demo** database, stop the app and delete that file. Do not delete a shared/team database.

## Run validation tests
```powershell
dotnet test Tests/MedicalDeviceSafetyPlatform.Tests.csproj
```

## ERD and assessment evidence
- `docs/Final-ERD.md` — entity relationships
- `Documentation/AcceptanceCriteria.md` — US08–US10
- `Documentation/Design.md` — wireframe and MVC diagram
- `docs/Next-Week-Demo.md` — live demo checklist
- `Documentation/ManualTests.md` — scenarios to execute and record

## Important limitations
- This branch has not yet been built or tested in a .NET-capable environment by the assistant. **Do not claim tests pass until you run them.**
- The standalone demo provides local Identity accounts and example SafetyNotice records so the component can be demonstrated independently. They are not replacements for the team's actual models.
- `EnsureCreated` is for this local demo only. Use EF Core migrations and the team's real Identity/SafetyNotice models when integrating.
- The ERD and controller/view source are prepared; the exact 70% completion threshold is determined by your lecturer and the integrated system.
- Before merging this branch into the team's repository, review conflicts and avoid copying the demo's `ApplicationUser`, `SafetyNotice`, `ApplicationDbContext`, `Program.cs` or `AccountController` over team equivalents.
