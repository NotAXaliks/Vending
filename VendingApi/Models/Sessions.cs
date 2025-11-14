using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingApi.Models;

public class Sessions
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
    public int Id { get; set; }

    public int UserId { get; set; }

    public required string RefreshToken { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    
    public required string? IP  { get; set; }
    public required string? DeviceName { get; set; }
    public required string? UserAgent { get; set; }
    
    public bool IsActive => RevokedAt == null && DateTimeOffset.UtcNow <= ExpiresAt;
    
    [ForeignKey(nameof(UserId))]
    public Users User { get; set; }
}
