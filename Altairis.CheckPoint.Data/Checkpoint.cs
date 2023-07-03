

namespace Altairis.CheckPoint.Data;

public class Checkpoint {

    [Key]
    public Suid Id { get; set; } = Suid.NewSuid();

    [Required, ForeignKey(nameof(Event))]
    public required Suid EventId { get; set; } 

    [ForeignKey(nameof(EventId))]
    public Event? Event { get; set; }

    public int SequenceNumber { get; set; }

    public Point? Location { get; set; }

    public CheckpointType Type { get; set; }

    [Required, MaxLength(50)]
    public required string Name { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Notes { get; set; }

}

public enum CheckpointType {
    Start,
    Finish,
    Mandatory,
    Optional
}