namespace Maa.Vacations.Entities;

public class VactionsContext : DbContext
{
    public VactionsContext(DbContextOptions<VactionsContext> options) 
            : base(options)
    {
    }

    public DbSet<Vacation> Vacations { get; set; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateFields();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateFields();
        return base.SaveChanges();
    }

    private void UpdateFields()
    {
        foreach(var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            if(entity.State == EntityState.Added)
            {
                var entityBase = entity.Entity as dynamic;
                entityBase.LastUpdatedDateTime = entityBase.CreateDateTime = DateTime.UtcNow;
            }
            else if (entity.State == EntityState.Modified)
            {
                var entityBase = entity.Entity as dynamic;
                entityBase.LastUpdatedDateTime = DateTime.UtcNow;
            }
        }
    }
}