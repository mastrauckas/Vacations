namespace Maa.Vacations.Repositories;

public interface IBaseRepository<TKeyType, TEntity, TDbContext> where TKeyType : struct
                                                                where TEntity : class
                                                                where TDbContext : DbContext
{ 
    Task<TEntity> GetByIdAsync(TKeyType id);

    Task AddAsync(TEntity entity);

    Task<int> SaveChangesAsync();
}
