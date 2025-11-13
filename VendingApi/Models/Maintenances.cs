using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingApi.Models;

public class Maintenances
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
    public int Id { get; set; }
    
    public int MachineId { get; set; }
    
    [Column(TypeName = "timestamp")]
    public DateTime Date { get; set; }
    
    [MaxLength(1500)]
    public required string Description { get; set; }
    
    [MaxLength(700)]
    public required string? Issues { get; set; }
    
    public int PerformedById { get; set; }
    
    [ForeignKey(nameof(PerformedById))]
    public Users PerformedBy { get; set; }
    
    [ForeignKey(nameof(MachineId))]
    public Machines Machine { get; set; }
}