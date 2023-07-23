namespace Maa.Vacations.Dtos;

public record CreateVacationDto([Required, StringLength(100, MinimumLength = 5)] string Name);