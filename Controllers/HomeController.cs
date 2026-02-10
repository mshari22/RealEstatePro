using Microsoft.AspNetCore.Mvc;
using RealEstatePro.Services;
using RealEstatePro.ViewModels;

namespace RealEstatePro.Controllers;

public class HomeController : Controller
{
    private readonly IPropertyService _propertyService;

    public HomeController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<IActionResult> Index()
    {
        // Get featured/recent properties for home page
        var properties = await _propertyService.GetAllPropertiesAsync();
        
        // Take top 6 for display
        var featuredProperties = properties.Take(6).Select(p => new Models.Property
        {
            Id = p.Id,
            Title = p.Title,
            Price = p.Price,
            Location = p.Location,
            Bedrooms = p.Bedrooms,
            Bathrooms = p.Bathrooms,
            Area = p.Area,
            ImageUrl = p.ImageUrl,
            PropertyType = p.PropertyType,
            Category = p.Category
        });

        return View(featuredProperties);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
