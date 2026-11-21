using System.ComponentModel.DataAnnotations;

namespace AutoLog.Models;

public class Vehicle
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Unesite naziv ili model vozila.")]
    [StringLength(80)]
    [Display(Name = "Vozilo")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Unesite registarsku oznaku.")]
    [StringLength(20)]
    [Display(Name = "Registracija")]
    public string LicensePlate { get; set; } = string.Empty;

    [Range(0, 2000000, ErrorMessage = "Kilometraža mora biti pozitivan broj.")]
    [Display(Name = "Trenutna km")]
    public int CurrentMileage { get; set; }

    [Range(1000, 100000, ErrorMessage = "Interval servisa mora biti između 1.000 i 100.000 km.")]
    [Display(Name = "Interval servisa (km)")]
    public int ServiceIntervalKm { get; set; } = 10000;

    [Display(Name = "Poslednji servis (km)")]
    public int LastServiceMileage { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Registracija važi do")]
    public DateTime RegistrationExpiry { get; set; } = DateTime.Today.AddMonths(6);

    public List<ServiceRecord> ServiceRecords { get; set; } = new();
    public List<Expense> Expenses { get; set; } = new();
}