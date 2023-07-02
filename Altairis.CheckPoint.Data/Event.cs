using System.ComponentModel.DataAnnotations;

namespace Altairis.CheckPoint.Data;

public class Event {

    [Key]
    public Suid Id { get; set; } = Suid.NewSuid();

    [Required, MaxLength(50)]
    public required string Name { get; set; }

    [Required, DataType(DataType.DateTime)]
    public DateTime DateStart { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Notes { get; set; }

}
