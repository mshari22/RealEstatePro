using System.ComponentModel.DataAnnotations;

namespace RealEstatePro.DTOs.Request;

public class CreateRequestDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Phone]
    public string? Phone { get; set; }
    
    [MaxLength(1000)]
    public string? Message { get; set; }
    
    [Required]
    public int PropertyId { get; set; }
}

public class RequestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Message { get; set; }
    public int PropertyId { get; set; }
    public string PropertyTitle { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
