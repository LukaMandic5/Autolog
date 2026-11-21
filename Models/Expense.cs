using System.ComponentModel.DataAnnotations;

namespace AutoLog.Models;

public class Expense
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Vozilo")]
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    [Required(ErrorMessage = "Izaberite ili unesite kategoriju troška.")]
    [StringLength(50)]
    [Display(Name = "Kategorija")]
    public string Category { get; set; } = "Gorivo";

    [DataType(DataType.Date)]
    [Display(Name = "Datum")]
    public DateTime ExpenseDate { get; set; } = DateTime.Today;

    [Range(1, 1000000)]
    [Display(Name = "Iznos (RSD)")]
    public decimal Amount { get; set; }

    [StringLength(200)]
    [Display(Name = "Opis")]
    public string? Description { get; set; }
}