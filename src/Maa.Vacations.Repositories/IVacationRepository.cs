namespace Maa.Vacations.Repositories;

public interface IVacationRepository : IBaseRepository<int, Vacation, VactionsContext>
{
    Task<IEnumerable<Vacation>> GetAllVacationsAsync();
}
