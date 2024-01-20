namespace Maa.Vacations.Services;

public interface IVacationService
{
    Task<VacationCreatedDto> AddVacationAsync(CreateVacationDto route);

    Task<VacationUpdatedDto?> UpdateVacationAsync(int id, UpdateVacationDto updateRouteDto);

    Task<DeletedVacationDto?> DeleteVacationAsync(int id);

    Task<IEnumerable<CurrentVacationDto>> GetAllVacationsAsync();

    Task<CurrentVacationDto?> GetVacationByIdAsync(int id);
}