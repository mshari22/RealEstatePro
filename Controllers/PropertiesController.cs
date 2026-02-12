using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePro.DTOs.Property;
using RealEstatePro.Services;
using RealEstatePro.ViewModels;

namespace RealEstatePro.Controllers;

public class PropertiesController : Controller
{
    private readonly IPropertyService _propertyService;
    private readonly ITranslationService _translationService;
    
    public PropertiesController(IPropertyService propertyService, ITranslationService translationService)
    {
        _propertyService = propertyService;
        _translationService = translationService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(PropertyFilterDto filter)
    {
        var dtos = await _propertyService.GetAllPropertiesAsync(filter);
        
        // Map DTOs to Model for View
        var properties = dtos.Select(p => new Models.Property
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
            Category = p.Category,
            Description = p.Description
        });
        
        var model = new PropertyListViewModel
        {
            Properties = properties,
            CurrentCategory = filter.Category,
            MinPrice = filter.MinPrice,
            MaxPrice = filter.MaxPrice
        };
        
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var property = await _propertyService.GetPropertyByIdAsync(id);
        
        if (property == null)
        {
            return NotFound();
        }
        
        return View(property);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> MyProperties()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var properties = await _propertyService.GetUserPropertiesAsync(userId);
        return View(properties);
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(PropertyCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(model.ImageFile.FileName).ToLowerInvariant();
            
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("ImageFile", "Only .jpg, .jpeg, .png, and .gif files are allowed.");
                return View(model);
            }
            
            var fileName = $"{Guid.NewGuid()}{extension}";
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
                
            var filePath = Path.Combine(uploadPath, fileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }
            
            model.ImageUrl = $"/images/{fileName}";
        }

        var createDto = new CreatePropertyDto
        {
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            PropertyType = model.PropertyType,
            Category = model.Category,
            Location = model.Location,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            Bedrooms = model.Bedrooms,
            Bathrooms = model.Bathrooms,
            Area = model.Area,
            ImageUrl = model.ImageUrl,
            ImagePath = model.ImageUrl // Using ImageUrl for consistency, or use filePath if physical path needed
        };
        
        var property = await _propertyService.CreatePropertyAsync(createDto, userId);
        return RedirectToAction(nameof(Details), new { id = property.Id });
    }
}
