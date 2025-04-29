using System.ComponentModel.DataAnnotations;

namespace NextgenInovasiTest.Models;

public class BaseEntity : IEntity
{
    [Key]
    public int Id { get; set; }
    public bool isActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}
