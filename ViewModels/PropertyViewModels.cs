using System.ComponentModel.DataAnnotations;
using RealEstatePro.Models;

namespace RealEstatePro.ViewModels;

public class PropertyListViewModel
{
    public IEnumerable<Property> Properties { get; set; } = new List<Property>();
    public string? CurrentCategory { get; set; }
    public string? SearchTerm { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}

public class PropertyCreateViewModel
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
    [Display(Name = "Property Type")]
    public string PropertyType { get; set; } = "sale"; // sale, rent

    [Required]
    public string Category { get; set; } = "apartment"; // apartment, villa, land, commercial

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
    
    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }
}
