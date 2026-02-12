using System.ComponentModel.DataAnnotations;

namespace RealEstatePro.DTOs.Property;

public class PropertyDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string PropertyType { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public double Area { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
}

public class CreatePropertyDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    
    [Required]
    public string PropertyType { get; set; } = "sale";
    
    [Required]
    public string Category { get; set; } = "apartment";
    
    [Required]
    [MaxLength(255)]
    public string Location { get; set; } = string.Empty;
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    [Range(0, 100)]
    public int Bedrooms { get; set; }
    
    [Range(0, 100)]
    public int Bathrooms { get; set; }
    
    [Range(0, double.MaxValue)]
    public double Area { get; set; }
    
    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    public string? ImagePath { get; set; }
}

public class UpdatePropertyDto : CreatePropertyDto
{
}

public class PropertyFilterDto
{
    public string? PropertyType { get; set; }
    public string? Category { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinBedrooms { get; set; }
    public string? Location { get; set; }
}
