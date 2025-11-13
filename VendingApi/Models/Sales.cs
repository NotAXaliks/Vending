using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingApi.Models;

public class Sales
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
    public int Id { get; set; }
    
    public int MachineId { get; set; }
    
    public int ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Amount { get; set; }
    
    [Column(TypeName = "timestamp")]
    public DateTime Date { get; set; }
    
    public PaymentMethod PaymentMethod  { get; set; }
    
    public ICollection<Machines> Machines { get; set; }
    public ICollection<Products> Products { get; set; }
}

public enum PaymentMethod
{
    Cash,
    Card,
    QR
}
