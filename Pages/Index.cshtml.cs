using AutoLog.Data;
using AutoLog.Models;
using AutoLog.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoLog.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly VehicleTrackerService _tracker;

    public IndexModel(AppDbContext context, VehicleTrackerService tracker)
    {
        _context = context;
        _tracker = tracker;
    }

    public List<VehicleStatusItem> VehiclesStatus { get; set; } = new();
    public decimal TotalExpenses { get; set; }
    public decimal TotalServicesCost { get; set; }

    public async Task OnGetAsync()
    {
        var vehicles = await _context.Vehicles.ToListAsync();
        TotalExpenses = await _context.Expenses.SumAsync(e => (decimal?)e.Amount) ?? 0;
        TotalServicesCost = await _context.ServiceRecords.SumAsync(s => (decimal?)s.Cost) ?? 0;

        VehiclesStatus = vehicles.Select(v => new VehicleStatusItem
        {
            Vehicle = v,
            KmLeft = _tracker.GetKmUntilNextService(v),
            DaysLeft = _tracker.GetDaysUntilRegistrationExpiry(v),
            StatusText = _tracker.GetServiceStatus(v)
        }).ToList();
    }

    public class VehicleStatusItem
    {
        public Vehicle Vehicle { get; set; } = null!;
        public int KmLeft { get; set; }
        public int DaysLeft { get; set; }
        public string StatusText { get; set; } = string.Empty;
    }
}