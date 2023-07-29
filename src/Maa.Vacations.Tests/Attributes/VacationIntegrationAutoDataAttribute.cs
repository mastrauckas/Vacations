using Microsoft.AspNetCore.Mvc.Testing;

namespace Maa.Vacations.Tests.Attributes;

public class VacationIntegrationAutoDataAttribute : AutoDataAttribute
{
    public VacationIntegrationAutoDataAttribute() : base(
        () =>
        {
            var application = new WebApplicationFactory<Program>();
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
