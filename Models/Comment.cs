using System.ComponentModel.DataAnnotations;

namespace MedicalDeviceSafetyPlatform.Models;

/// <summary>
/// Employee comment attached to a stored finding.
/// </summary>
public class Comment
{
    public int Id { get; set; }

    [Required]
    public int FindingId { get; set; }

    [Required]
    public string AuthorId { get; set; } = string.Empty;

    [Required, StringLength(1500, MinimumLength = 1)]
    public string Body { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public Finding? Finding { get; set; }
    public ApplicationUser? Author { get; set; }
}
