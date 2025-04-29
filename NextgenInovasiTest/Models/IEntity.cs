using System.ComponentModel.DataAnnotations;

namespace NextgenInovasiTest.Models;

public interface IEntity
{
    int Id { get; set; }
    bool isActive { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    string CreatedBy { get; set; }
    string UpdatedBy { get; set; }
}
