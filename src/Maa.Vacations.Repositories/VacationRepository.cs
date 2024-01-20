namespace Maa.Vacations.Repositories;

public class VacationRepository : BaseRepository<int, Vacation, VacationsContext>, IVacationRepository
{
    public VacationRepository(VacationsContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Vacation>> GetAllVacationsAsync()
    {
        return await _dbSet
                     .Where(r => !r.DeletedDateTime.HasValue)
                     .ToListAsync();
    }
}