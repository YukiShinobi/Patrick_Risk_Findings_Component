using System.ComponentModel.DataAnnotations;

namespace MedicalDeviceSafetyPlatform.Models;

public class Finding
{
    public int Id { get; set; }

    [Range(1, int.MaxValue)]
    public int SafetyNoticeId { get; set; }

    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(4000, MinimumLength = 10)]
    public string Analysis { get; set; } = string.Empty;

    [EnumDataType(typeof(FindingRelevance))]
    public FindingRelevance Relevance { get; set; } = FindingRelevance.Medium;

    [EnumDataType(typeof(FindingStatus))]
    public FindingStatus Status { get; set; } = FindingStatus.Open;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

    // Configure the database provider's concurrency token behaviour during integration.
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    public SafetyNotice? SafetyNotice { get; set; }
    public ApplicationUser? Employee { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

public enum FindingRelevance { Low = 1, Medium = 2, High = 3 }
public enum FindingStatus { Open = 1, UnderReview = 2, Resolved = 3 }
