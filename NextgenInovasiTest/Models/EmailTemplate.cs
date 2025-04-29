using System.ComponentModel.DataAnnotations;

namespace NextgenInovasiTest.Models;

public class EmailTemplate
{
    [Key]
    public int Id { get; set; }
    public string? EmailCode { get; set; }
    public string? Subject { get; set; }
    [StringLength(10000)]
    public string? Body { get; set; }

}
