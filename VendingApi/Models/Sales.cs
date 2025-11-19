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
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset Date { get; set; }
    
    public PaymentMethod PaymentMethod  { get; set; }
    
    [ForeignKey(nameof(MachineId))]
    public Machines Machine { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Products Product { get; set; }
}

public enum PaymentMethod
{
    Cash,
    Card,
    QR
}
