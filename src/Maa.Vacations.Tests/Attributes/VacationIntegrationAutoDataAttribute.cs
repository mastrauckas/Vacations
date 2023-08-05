using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Maa.Vacations.Tests.Attributes;

public class VacationIntegrationAutoDataAttribute : AutoDataAttribute
{
    public VacationIntegrationAutoDataAttribute() : base(
        () =>
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                     {
                         services.RemoveAll<DbContextOptions<VacationsContext>>();
                         services.AddDbContext<VacationsContext>(options => options.UseInMemoryDatabase("Vacations"));
                     });
                });
            var client = application.CreateClient();
            var fixture = new Fixture();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ProfileVacation>());
            var mapper = configuration.CreateMapper();
            fixture.Inject(mapper);
            fixture.Inject(client);
            fixture.Customize(new AutoMoqCustomization());
            return fixture;
        })
    {
    }
}
