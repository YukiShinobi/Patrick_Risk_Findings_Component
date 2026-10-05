// Integration-test outline for Patrick's component.
// Adapt the test fixture to the team's final ApplicationDbContext/authentication setup.

using Xunit;

namespace MedicalDeviceSafetyPlatform.Tests;

public class FindingsControllerTests
{
    [Fact]
    public void US08_CreateFinding_RequiresValidInput()
    {
        // Arrange: authenticated employee + existing SafetyNotice.
        // Act: submit empty/invalid required fields.
        // Assert: ModelState invalid and no Finding is persisted.
    }

    [Fact]
    public void US09_ValidFinding_IsPersisted()
    {
        // Arrange: authenticated employee + existing SafetyNotice.
        // Act: submit valid FindingFormViewModel.
        // Assert: Finding exists in database and links to notice + employee.
    }

    [Fact]
    public void US10_Employee_CanEditOwnedFinding()
    {
        // Arrange: finding owned by authenticated employee.
        // Act: update title/analysis/relevance/status.
        // Assert: values persist and UpdatedAtUtc changes.
    }

    [Fact]
    public void US10_Employee_CannotAccessAnotherEmployeesFinding()
    {
        // Arrange: finding owned by a different employee.
        // Act: request Details/Edit.
        // Assert: NotFound result and data is not disclosed.
    }

    [Fact]
    public void Comment_EmptyBody_IsRejected()
    {
        // Arrange: owned finding.
        // Act: post empty comment.
        // Assert: no Comment is persisted.
    }
}
