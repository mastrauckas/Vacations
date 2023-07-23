namespace Maa.Vacations.Entities;

public record Vacation : EntityBase<int>
{
    [Required, StringLength(100)]
    public required string Name { get; set; }

    public DateTime? DeletedDateTime { get; set; }

}
