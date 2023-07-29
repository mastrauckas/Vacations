using Microsoft.AspNetCore.Mvc.Testing;

namespace Maa.Vacations.Tests;

public class VacationIntegrationAutoData : AutoDataAttribute
{
    public VacationIntegrationAutoData() : base(
        () =>
        {
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
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

