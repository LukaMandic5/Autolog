using System.ComponentModel.DataAnnotations;

namespace AutoLog.Models;

public class ServiceRecord
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Vozilo")]
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    [Required(ErrorMessage = "Unesite vrstu servisa (npr. Mali servis, Kočnice).")]
    [StringLength(100)]
    [Display(Name = "Vrsta servisa")]
    public string ServiceType { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Datum")]
    public DateTime ServiceDate { get; set; } = DateTime.Today;

    [Range(0, 2000000)]
    [Display(Name = "Kilometraža")]
    public int Mileage { get; set; }

    [Range(0, 500000)]
    [Display(Name = "Cena (RSD)")]
    public decimal Cost { get; set; }

    [StringLength(200)]
    [Display(Name = "Napomena")]
    public string? Notes { get; set; }
}