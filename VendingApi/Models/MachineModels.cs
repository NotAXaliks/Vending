using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingApi.Models;

// Готово
public class MachineModels
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    [MinLength(1)]
    [MaxLength(100)]
    public required string Manufacturer { get; set; }
    
    [MinLength(2)]
    [MaxLength(100)]
    public required string Country { get; set; }
    
    public virtual ICollection<Machines> Machines { get; set; } = [];
}