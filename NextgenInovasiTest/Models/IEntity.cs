using System.ComponentModel.DataAnnotations;

namespace NextgenInovasiTest.Models;

public interface IEntity
{
    int Id { get; set; }
    bool isActive { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    int CreatedBy { get; set; }
    int UpdatedBy { get; set; }
}
