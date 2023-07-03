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

    public ICollection<Competitor> Competitors { get; set; } = new HashSet<Competitor>();

    public ICollection<Checkpoint> Checkpoints { get; set; } = new HashSet<Checkpoint>();

}
