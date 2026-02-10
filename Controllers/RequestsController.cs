using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstatePro.Data;
using RealEstatePro.DTOs.Request;
using RealEstatePro.Models;

namespace RealEstatePro.Controllers;

public class RequestsController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public RequestsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateRequestDto createDto)
    {
        if (!ModelState.IsValid)
        {
            // In a real app we'd redirect back with errors, but for simplicity:
            return RedirectToAction("Details", "Properties", new { id = createDto.PropertyId });
        }
        
        var request = new PropertyRequest
        {
            Name = createDto.Name,
            Email = createDto.Email,
            Phone = createDto.Phone,
            Message = createDto.Message,
            PropertyId = createDto.PropertyId,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.PropertyRequests.Add(request);
        await _context.SaveChangesAsync();
        
        TempData["SuccessMessage"] = "Your request has been sent successfully!";
        
        return RedirectToAction("Details", "Properties", new { id = createDto.PropertyId });
    }
}
