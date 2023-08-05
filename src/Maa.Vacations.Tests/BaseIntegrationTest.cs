namespace Maa.Vacations.Tests;
public abstract class BaseIntegrationTest<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected readonly HttpClient _client;

    public BaseIntegrationTest()
    {
        var databaseName = $"Vacations_{Guid.NewGuid()}";
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<DbContextOptions<VacationsContext>>();
                    services.AddDbContext<VacationsContext>(options => options.UseInMemoryDatabase(databaseName));
                });
            });
        _client = application.CreateClient();
    }
}
