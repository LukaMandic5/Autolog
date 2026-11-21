using AutoLog.Data;
using AutoLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoLog.Pages.Expenses;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Expense> Expenses { get; set; } = new();
    public SelectList VehicleOptions { get; set; } = null!;
    public decimal FilteredTotal { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SelectedCategory { get; set; }

    [BindProperty]
    public Expense NewExpense { get; set; } = new();

    private async Task LoadDropdownAsync()
    {
        var vehicles = await _context.Vehicles.OrderBy(v => v.Name).ToListAsync();
        VehicleOptions = new SelectList(vehicles, nameof(Vehicle.Id), nameof(Vehicle.Name));
    }

    public async Task OnGetAsync()
    {
        await LoadDropdownAsync();

        var query = _context.Expenses.Include(e => e.Vehicle).AsQueryable();
        if (!string.IsNullOrWhiteSpace(SelectedCategory) && SelectedCategory != "Sve")
        {
            query = query.Where(e => e.Category == SelectedCategory);
        }

        Expenses = await query.OrderByDescending(e => e.ExpenseDate).ToListAsync();
        FilteredTotal = Expenses.Sum(e => e.Amount);
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownAsync();
            Expenses = await _context.Expenses.Include(e => e.Vehicle).ToListAsync();
            return Page();
        }

        _context.Expenses.Add(NewExpense);
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }
}