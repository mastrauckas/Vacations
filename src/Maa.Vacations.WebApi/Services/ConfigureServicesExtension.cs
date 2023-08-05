namespace Maa.Vacations.WebApi.Services;

public static class ConfigureServicesExtension
{
    public static void ConfigureService<TDbContext>(this IServiceCollection services, string? connectionString) where TDbContext : DbContext
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString);
        services.AddDbContext<TDbContext>(c => c.UseSqlite(connectionString));
        services.AddTransient<IVacationService, VacationService>();
        services.AddTransient<IVacationRepository, VacationRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}