namespace Maa.Vacations.Tests.UnitTests.Attributes;

internal class VacationUnitTestAutoDataAttribute : AutoDataAttribute
{
    public VacationUnitTestAutoDataAttribute() : base(CreateFixture)
    {
    }

    public VacationUnitTestAutoDataAttribute(string vacationName) : base(
                                                                         () =>
                                                                         {
                                                                             var fixture = CreateFixture();
                                                                             fixture.Customize<Vacation>(c =>
                                                                                          c.With(r => r.Name,
                                                                                                   vacationName));

                                                                             return fixture;
                                                                         })
    {
    }

    private static Fixture CreateFixture()
    {
        Fixture             fixture       = new();
        MapperConfiguration configuration = new(cfg => cfg.AddProfile<ProfileVacation>());
        var                 mapper        = configuration.CreateMapper();
        fixture.Inject(mapper);
        fixture.Customize(new AutoNSubstituteCustomization());

        return fixture;
    }
}