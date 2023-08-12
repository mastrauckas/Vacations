namespace Maa.Vacations.Tests.IntegrationTests.Attributes;

public class VacationIntegrationAutoDataAttribute : AutoDataAttribute
{
    public VacationIntegrationAutoDataAttribute() : base(
        () =>
        {
            var fixture = new Fixture();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ProfileVacation>());
            var mapper = configuration.CreateMapper();
            fixture.Inject(mapper);
            fixture.Customize(new AutoNSubstituteCustomization());
            return fixture;
        })
    {
    }
}
