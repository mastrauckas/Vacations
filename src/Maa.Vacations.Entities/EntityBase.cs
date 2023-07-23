namespace Maa.Vacations.Entities;

public abstract record EntityBase<T>
{
    [Required]
    public required T Id { get; set; }

    [Required]
    public required DateTime CreateDateTime { get; set; }

    [Required]
    public required DateTime LastUpdatedDateTime { get; set; }

}
