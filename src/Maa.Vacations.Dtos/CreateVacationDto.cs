using System.ComponentModel.DataAnnotations;

namespace Maa.Vacations.Dtos;
public class CreateVacationDto
{
    [Required, StringLength(100, MinimumLength = 5)]
    public string Name { get; set; }
}
