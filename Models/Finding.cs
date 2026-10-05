using System.ComponentModel.DataAnnotations;

namespace MedicalDeviceSafetyPlatform.Models;

/// <summary>
/// Stores an employee's risk-review finding for one safety/recall notice.
/// Supports creation, storage and management of risk-review findings.
/// </summary>
public class Finding
{
    public int Id { get; set; }

    [Required]
    public int SafetyNoticeId { get; set; }

    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(4000, MinimumLength = 10)]
    public string Analysis { get; set; } = string.Empty;

    [Required]
    public FindingRelevance Relevance { get; set; }

    [Required]
    public FindingStatus Status { get; set; } = FindingStatus.Open;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

    // Optimistic concurrency token.
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    public SafetyNotice? SafetyNotice { get; set; }
    public ApplicationUser? Employee { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

public enum FindingRelevance
{
    Low = 1,
    Medium = 2,
    High = 3
}

public enum FindingStatus
{
    Open = 1,
    UnderReview = 2,
    Resolved = 3
}
