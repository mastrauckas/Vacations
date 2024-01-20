namespace Maa.Vacations.Dtos;

public record UpdateVacationDto(
    [Required]
    [StringLength(100, MinimumLength = 5)]
    string Name);