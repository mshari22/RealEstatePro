using RealEstatePro.DTOs.Property;

namespace RealEstatePro.Services;

public interface IPropertyService
{
    Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync(PropertyFilterDto? filter = null);
    Task<PropertyDto?> GetPropertyByIdAsync(int id);
    Task<IEnumerable<PropertyDto>> GetUserPropertiesAsync(int userId);
    Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto dto, int userId);
    Task<PropertyDto?> UpdatePropertyAsync(int id, UpdatePropertyDto dto, int userId);
    Task<bool> DeletePropertyAsync(int id, int userId);
}
