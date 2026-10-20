using System.ComponentModel.DataAnnotations;
using MedicalDeviceSafetyPlatform.Models;
using MedicalDeviceSafetyPlatform.ViewModels;
using Xunit;

namespace MedicalDeviceSafetyPlatform.Tests;

/// <summary>
/// Executable unit tests for form-level validation. Controller/database integration
/// tests still need a fixture based on the final host application's DbContext and auth setup.
/// </summary>
public class FindingFormViewModelValidationTests
{
    [Fact]
    public void ValidForm_HasNoValidationErrors()
    {
        var model = new FindingFormViewModel
        {
            SafetyNoticeId = 1,
            Title = "Packaging risk review",
            Analysis = "The packaging may be damaged during transport and requires review.",
            Relevance = FindingRelevance.High,
            Status = FindingStatus.Open
        };

        Assert.Empty(Validate(model));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void BlankTitle_IsRejected(string title)
    {
        var model = ValidModel();
        model.Title = title;

        Assert.Contains(Validate(model), error => error.MemberNames.Contains(nameof(model.Title)));
    }

    [Fact]
    public void TitleLongerThan150Characters_IsRejected()
    {
        var model = ValidModel();
        model.Title = new string('x', 151);

        Assert.Contains(Validate(model), error => error.MemberNames.Contains(nameof(model.Title)));
    }

    [Fact]
    public void AnalysisShorterThan10Characters_IsRejected()
    {
        var model = ValidModel();
        model.Analysis = "Too short";

        Assert.Contains(Validate(model), error => error.MemberNames.Contains(nameof(model.Analysis)));
    }

    [Fact]
    public void AnalysisLongerThan4000Characters_IsRejected()
    {
        var model = ValidModel();
        model.Analysis = new string('x', 4001);

        Assert.Contains(Validate(model), error => error.MemberNames.Contains(nameof(model.Analysis)));
    }

    [Fact]
    public void UndefinedRelevanceValue_IsRejected()
    {
        var model = ValidModel();
        model.Relevance = (FindingRelevance)999;

        Assert.Contains(Validate(model), error => error.MemberNames.Contains(nameof(model.Relevance)));
    }

    [Fact]
    public void UndefinedStatusValue_IsRejected()
    {
        var model = ValidModel();
        model.Status = (FindingStatus)999;

        Assert.Contains(Validate(model), error => error.MemberNames.Contains(nameof(model.Status)));
    }

    [Fact]
    public void MissingSafetyNoticeId_IsRejected()
    {
        var model = ValidModel();
        model.SafetyNoticeId = 0;

        Assert.Contains(Validate(model), error => error.MemberNames.Contains(nameof(model.SafetyNoticeId)));
    }

    private static FindingFormViewModel ValidModel() => new()
    {
        SafetyNoticeId = 1,
        Title = "Packaging risk review",
        Analysis = "The packaging may be damaged during transport and requires review.",
        Relevance = FindingRelevance.Medium,
        Status = FindingStatus.Open
    };

    private static List<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);
        return results;
    }
}
