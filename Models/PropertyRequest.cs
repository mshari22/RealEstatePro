using System.ComponentModel.DataAnnotations;

namespace RealEstatePro.Models;

public class PropertyRequest
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string? Phone { get; set; }
    
    [MaxLength(1000)]
    public string? Message { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign key
    public int PropertyId { get; set; }
    
    // Navigation property
    public Property Property { get; set; } = null!;
}
