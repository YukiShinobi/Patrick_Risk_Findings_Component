// Unit tests for the component's validation rules.
// Requires the team's test project to reference this component and xUnit.
// Controller/database integration tests still need the team's actual DbContext and Identity setup.
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MedicalDeviceSafetyPlatform.Models;
using MedicalDeviceSafetyPlatform.ViewModels;
using Xunit;

namespace MedicalDeviceSafetyPlatform.Tests;

public class FindingValidationTests
{
    private static List<ValidationResult> Validate(object instance)
    {
        var errors = new List<ValidationResult>();
        Validator.TryValidateObject(instance, new ValidationContext(instance), errors, validateAllProperties: true);
        return errors;
    }

    private static FindingFormViewModel ValidForm() => new()
    {
        SafetyNoticeId = 1,
        Title = "Risk review",
        Analysis = "A sufficiently detailed risk assessment.",
        Relevance = FindingRelevance.High,
        Status = FindingStatus.Open
    };

    [Fact]
    public void US08_ValidFindingForm_PassesValidation()
        => Assert.Empty(Validate(ValidForm()));

    [Fact]
    public void US08_EmptyTitle_IsRejected()
    {
        var form = ValidForm();
        form.Title = "";
        Assert.NotEmpty(Validate(form));
    }

    [Fact]
    public void US08_ShortAnalysis_IsRejected()
    {
        var form = ValidForm();
        form.Analysis = "short";
        Assert.NotEmpty(Validate(form));
    }

    [Fact]
    public void US08_InvalidNoticeId_IsRejected()
    {
        var form = ValidForm();
        form.SafetyNoticeId = 0;
        Assert.NotEmpty(Validate(form));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(99)]
    public void US08_InvalidRelevance_IsRejected(int value)
    {
        var form = ValidForm();
        form.Relevance = (FindingRelevance)value;
        Assert.NotEmpty(Validate(form));
    }

    [Fact]
    public void US10_InvalidStatus_IsRejected()
    {
        var form = ValidForm();
        form.Status = (FindingStatus)999;
        Assert.NotEmpty(Validate(form));
    }

    [Fact]
    public void Comment_EmptyBody_IsRejected()
    {
        var comment = new Comment { FindingId = 1, AuthorId = "employee-1", Body = "" };
        Assert.NotEmpty(Validate(comment));
    }

    [Fact]
    public void Comment_OverlongBody_IsRejected()
    {
        var comment = new Comment { FindingId = 1, AuthorId = "employee-1", Body = new string('x', 1501) };
        Assert.NotEmpty(Validate(comment));
    }
}
