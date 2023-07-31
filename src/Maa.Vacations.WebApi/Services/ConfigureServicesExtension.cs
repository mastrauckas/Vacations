namespace Maa.Vacations.WebApi.Services;

public static class ConfigureServicesExtension
{
    public static void ConfigureService<TDbContext>(this IServiceCollection services, string? connectionString) where TDbContext : DbContext
    {
        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
        // services.AddDbContext<VactionsContext>(c => c.UseSqlite(connectionString));
        services.AddDbContext<TDbContext>(c => c.UseSqlite(connectionString));
        services.AddTransient<IVacationService, VacationService>();
        services.AddTransient<IVacationRepository, VacationRepository>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}