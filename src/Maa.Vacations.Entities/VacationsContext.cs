namespace Maa.Vacations.Entities;

public class VacationsContext : DbContext
{
    public VacationsContext(DbContextOptions<VacationsContext> options)
        : base(options)
    {
    }

    public DbSet<Vacation> Vacations { get; set; }

    public override Task<int> SaveChangesAsync(bool              acceptAllChangesOnSuccess,
                                               CancellationToken cancellationToken = default)
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
        foreach (var entity in ChangeTracker.Entries()
                                            .Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            if (entity.State == EntityState.Added)
            {
                dynamic entityBase                                         = entity.Entity;
                entityBase.LastUpdatedDateTime = entityBase.CreateDateTime = DateTime.UtcNow;
            }
            else if (entity.State == EntityState.Modified)
            {
                dynamic entityBase = entity.Entity;
                entityBase.LastUpdatedDateTime = DateTime.UtcNow;
            }
        }
    }
}