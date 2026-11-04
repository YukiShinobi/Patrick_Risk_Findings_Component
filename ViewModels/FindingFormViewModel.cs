using System.ComponentModel.DataAnnotations;
using MedicalDeviceSafetyPlatform.Models;

namespace MedicalDeviceSafetyPlatform.ViewModels;

public class FindingFormViewModel
{
    public int? Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Select a valid safety notice.")]
    public int SafetyNoticeId { get; set; }

    [Required, StringLength(150)]
    [Display(Name = "Finding title")]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(4000, MinimumLength = 10)]
    [Display(Name = "Risk-review analysis")]
    public string Analysis { get; set; } = string.Empty;

    [EnumDataType(typeof(FindingRelevance))]
    public FindingRelevance Relevance { get; set; } = FindingRelevance.Medium;

    [EnumDataType(typeof(FindingStatus))]
    public FindingStatus Status { get; set; } = FindingStatus.Open;

    public byte[]? RowVersion { get; set; }

    // Display-only fields; supplied by the controller, never trusted as submitted data.
    public string NoticeReference { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
}
