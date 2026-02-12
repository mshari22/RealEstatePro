using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure.Storage.Blobs;
using Azure.Identity;
using Microsoft.AspNetCore.Http;

namespace RealEstatePro.Models;

public class Property
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string PropertyType { get; set; } = "sale"; // sale, rent
    
    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = "apartment"; // apartment, villa, land, commercial
    
    [Required]
    [MaxLength(255)]
    public string Location { get; set; } = string.Empty;
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public int Bedrooms { get; set; }
    
    public int Bathrooms { get; set; }
    
    public double Area { get; set; }
    
    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    
    [MaxLength(500)]
    public string? ImagePath { get; set; } // New: Local file path
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign key
    public int UserId { get; set; }
    
    // Navigation property
    public User User { get; set; } = null!;
    
    public ICollection<PropertyRequest> Requests { get; set; } = new List<PropertyRequest>();

// Methods removed for clarity and correctness
}
