using System.ComponentModel.DataAnnotations;
using MedicalDeviceSafetyPlatform.Models;

namespace MedicalDeviceSafetyPlatform.ViewModels;

/// <summary>
/// Input model used by Create and Edit so database entities are not bound directly.
/// </summary>
public class FindingFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Select a valid safety or recall notice.")]
    public int SafetyNoticeId { get; set; }

    [Required, StringLength(150)]
    [Display(Name = "Finding title")]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(4000, MinimumLength = 10)]
    [Display(Name = "Risk-review analysis")]
    public string Analysis { get; set; } = string.Empty;

    [EnumDataType(typeof(FindingRelevance), ErrorMessage = "Select a valid relevance level.")]
    [Display(Name = "Relevance")]
    public FindingRelevance Relevance { get; set; }

    [EnumDataType(typeof(FindingStatus), ErrorMessage = "Select a valid finding status.")]
    [Display(Name = "Status")]
    public FindingStatus Status { get; set; } = FindingStatus.Open;

    // Required for edits when the host database provider supports the configured
    // timestamp/row-version concurrency token. Create requests do not have one yet.
    public byte[]? RowVersion { get; set; }

    // Display-only information supplied by the controller.
    public string NoticeReference { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
}
