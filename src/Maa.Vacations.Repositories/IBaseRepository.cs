namespace Maa.Vacations.Repositories;

public interface IBaseRepository<in TKeyType, TEntity, TDbContext> where TKeyType : struct
                                                                   where TEntity : class
                                                                   where TDbContext : DbContext
{ 
    Task<TEntity> GetByIdAsync(TKeyType id);

    Task AddAsync(TEntity entity);

    Task<int> SaveChangesAsync();
}
