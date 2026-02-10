using RealEstatePro.DTOs.Auth;
using RealEstatePro.Models;

namespace RealEstatePro.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto registerDto);
    Task<User?> LoginAsync(string email, string password);
    Task<User?> GetUserByIdAsync(int userId);
}
