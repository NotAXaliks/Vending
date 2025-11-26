using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VendingApi.Models;

// Готово
[Index(nameof(Date), IsUnique = true)]
public class Maintenances
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
    public int Id { get; set; }

    public int MachineId { get; set; }

    public DateTimeOffset Date { get; set; }

    [Range(1, 20)]
    public int DurationHours { get; set; }
    public int ActualDurationSeconds { get; set; }

    [MaxLength(1500)] public required string Description { get; set; }

    [MaxLength(700)] public required string? Issues { get; set; }

    public int PerformedById { get; set; }

    [ForeignKey(nameof(PerformedById))] public Users PerformedBy { get; set; }

    [ForeignKey(nameof(MachineId))] public Machines Machine { get; set; }
}