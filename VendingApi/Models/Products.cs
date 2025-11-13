using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingApi.Models;

public class Products
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
    public int Id { get; set; }
    
    [MaxLength(70)]
    public required string Name { get; set; }
    
    [MaxLength(200)]
    public required string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int InStock { get; set; }
    
    public int MinStock { get; set; }
    
    // TODO: Средняя дневная статистика продаж за месяц
    
    public virtual ICollection<Sales> Sales { get; set; } = [];
}