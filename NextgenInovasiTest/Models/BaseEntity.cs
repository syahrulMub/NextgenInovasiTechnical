using System.ComponentModel.DataAnnotations;

namespace NextgenInovasiTest.Models;

public class BaseEntity : IEntity
{
    [Key]
    public int Id { get; set; }
    public bool isActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
