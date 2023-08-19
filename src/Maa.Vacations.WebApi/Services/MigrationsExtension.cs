namespace Maa.Vacations.WebApi.Services;

public static class MigrationsExtension
{
    public static async Task RunMigrations<TDbContext>(this IServiceProvider serviceProvider) where TDbContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);
        ArgumentNullException.ThrowIfNull(scope.ServiceProvider);
        var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        await context.Database.MigrateAsync();
    }
}
