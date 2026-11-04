using System.ComponentModel.DataAnnotations;
namespace MedicalDeviceSafetyPlatform.Models;
public class SafetyNotice
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Reference { get; set; } = "";
    [Required, StringLength(200)] public string DeviceName { get; set; } = "";
}
