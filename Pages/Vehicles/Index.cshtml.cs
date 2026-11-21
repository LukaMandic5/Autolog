using AutoLog.Data;
using AutoLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoLog.Pages.Vehicles;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Vehicle> Vehicles { get; set; } = new();

    [BindProperty]
    public Vehicle NewVehicle { get; set; } = new();

    public async Task OnGetAsync()
    {
        Vehicles = await _context.Vehicles.OrderBy(v => v.Name).ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        if (!ModelState.IsValid)
        {
            Vehicles = await _context.Vehicles.OrderBy(v => v.Name).ToListAsync();
            return Page();
        }

        _context.Vehicles.Add(NewVehicle);
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle != null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}