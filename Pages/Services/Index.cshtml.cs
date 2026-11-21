using AutoLog.Data;
using AutoLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoLog.Pages.Services;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<ServiceRecord> Records { get; set; } = new();
    public SelectList VehicleOptions { get; set; } = null!;

    [BindProperty]
    public ServiceRecord NewRecord { get; set; } = new();

    private async Task LoadDropdownAsync()
    {
        var vehicles = await _context.Vehicles.OrderBy(v => v.Name).ToListAsync();
        VehicleOptions = new SelectList(vehicles, nameof(Vehicle.Id), nameof(Vehicle.Name));
    }

    public async Task OnGetAsync()
    {
        await LoadDropdownAsync();
        Records = await _context.ServiceRecords
            .Include(s => s.Vehicle)
            .OrderByDescending(s => s.ServiceDate)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownAsync();
            Records = await _context.ServiceRecords.Include(s => s.Vehicle).ToListAsync();
            return Page();
        }

        _context.ServiceRecords.Add(NewRecord);

        var vehicle = await _context.Vehicles.FindAsync(NewRecord.VehicleId);
        if (vehicle != null)
        {
            vehicle.LastServiceMileage = NewRecord.Mileage;
            if (NewRecord.Mileage > vehicle.CurrentMileage)
            {
                vehicle.CurrentMileage = NewRecord.Mileage;
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToPage();
    }
}