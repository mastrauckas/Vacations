namespace Maa.Vacations.Repositories;

public interface IVacationRepository : IBaseRepository<int, Vacation, VacationsContext>
{
    Task<IEnumerable<Vacation>> GetAllVacationsAsync();
}
