using System.ComponentModel.DataAnnotations.Schema;

namespace Altairis.CheckPoint.Data;

public class Competitor {

    [Key]
    public Suid Id { get; set; } = Suid.NewSuid();

    [Required, ForeignKey(nameof(Event))]
    public required Suid EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public Event? Event { get; set; }

    public int SequenceNumber { get; set; }

    public DateTime DateCreated { get; set; }

    [Required, MaxLength(50)]
    public required string Name { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Notes { get; set; }

    public int AdditionalSeconds { get; set; }

}
