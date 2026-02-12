using Microsoft.EntityFrameworkCore;
using RealEstatePro.Data;
using RealEstatePro.DTOs.Property;
using RealEstatePro.Models;

namespace RealEstatePro.Services;

public class PropertyService : IPropertyService
{
    private readonly ApplicationDbContext _context;
    
    public PropertyService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync(PropertyFilterDto? filter = null)
    {
        var query = _context.Properties.Include(p => p.User).AsQueryable();
        
        if (filter != null)
        {
            if (!string.IsNullOrEmpty(filter.PropertyType))
                query = query.Where(p => p.PropertyType == filter.PropertyType);
            
            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(p => p.Category == filter.Category);
            
            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            
            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            
            if (filter.MinBedrooms.HasValue)
                query = query.Where(p => p.Bedrooms >= filter.MinBedrooms.Value);
            
            if (!string.IsNullOrEmpty(filter.Location))
                query = query.Where(p => p.Location.ToLower().Contains(filter.Location.ToLower()));
        }
        
        var properties = await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
        
        return properties.Select(MapToDto);
    }
    
    public async Task<PropertyDto?> GetPropertyByIdAsync(int id)
    {
        var property = await _context.Properties
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return property == null ? null : MapToDto(property);
    }
    
    public async Task<IEnumerable<PropertyDto>> GetUserPropertiesAsync(int userId)
    {
        var properties = await _context.Properties
            .Include(p => p.User)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
        
        return properties.Select(MapToDto);
    }
    
    public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto dto, int userId)
    {
        var property = new Property
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            PropertyType = dto.PropertyType,
            Category = dto.Category,
            Location = dto.Location,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Bedrooms = dto.Bedrooms,
            Bathrooms = dto.Bathrooms,
            Area = dto.Area,
            ImageUrl = dto.ImageUrl,
            ImagePath = dto.ImagePath, // New
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        
        // Reload with User
        await _context.Entry(property).Reference(p => p.User).LoadAsync();
        
        return MapToDto(property);
    }
    
    public async Task<PropertyDto?> UpdatePropertyAsync(int id, UpdatePropertyDto dto, int userId)
    {
        var property = await _context.Properties
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        
        if (property == null) return null;
        
        property.Title = dto.Title;
        property.Description = dto.Description;
        property.Price = dto.Price;
        property.PropertyType = dto.PropertyType;
        property.Category = dto.Category;
        property.Location = dto.Location;
        property.Latitude = dto.Latitude;
        property.Longitude = dto.Longitude;
        property.Bedrooms = dto.Bedrooms;
        property.Bathrooms = dto.Bathrooms;
        property.Area = dto.Area;
        property.ImageUrl = dto.ImageUrl;
        
        await _context.SaveChangesAsync();
        
        return MapToDto(property);
    }
    
    public async Task<bool> DeletePropertyAsync(int id, int userId)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        
        if (property == null) return false;
        
        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    private static PropertyDto MapToDto(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Price = property.Price,
            PropertyType = property.PropertyType,
            Category = property.Category,
            Location = property.Location,
            Latitude = property.Latitude,
            Longitude = property.Longitude,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Area = property.Area,
            ImageUrl = property.ImageUrl,
            ImagePath = property.ImagePath,
            CreatedAt = property.CreatedAt,
            UserId = property.UserId,
            OwnerName = property.User?.FullName ?? "Unknown"
        };
    }
}
