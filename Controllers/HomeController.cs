using MedicalDeviceSafetyPlatform.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalDeviceSafetyPlatform.Controllers;
public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db) => _db = db;
    public async Task<IActionResult> Index() => View(await _db.SafetyNotices.AsNoTracking().ToListAsync());
    public IActionResult Error() => Content("An unexpected error occurred.");
}
