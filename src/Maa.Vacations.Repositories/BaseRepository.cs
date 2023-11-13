namespace Maa.Vacations.Repositories;

public abstract class BaseRepository<TKeyType, TEntity, TDbContext> : IBaseRepository<TKeyType, TEntity, TDbContext>
                                                                    where TKeyType : struct
                                                                    where TEntity : class
                                                                    where TDbContext : DbContext

{
    private readonly TDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(TDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetByIdAsync(TKeyType id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
