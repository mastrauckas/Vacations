namespace Maa.Vacations.Tests.IntegrationTests.Attributes;

public class VacationIntegrationAutoDataAttribute : AutoDataAttribute
{
    public VacationIntegrationAutoDataAttribute() : base(
                                                         () =>
                                                         {
                                                             Fixture fixture = new();
                                                             MapperConfiguration configuration =
                                                                 new(cfg =>
                                                                         cfg.AddProfile<ProfileVacation>());
                                                             var mapper = configuration.CreateMapper();
                                                             fixture.Inject(mapper);
                                                             fixture.Customize(new AutoNSubstituteCustomization());

                                                             return fixture;
                                                         })
    {
    }
}