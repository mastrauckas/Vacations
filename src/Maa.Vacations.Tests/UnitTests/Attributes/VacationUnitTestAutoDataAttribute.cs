using AutoFixture.AutoNSubstitute;

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
            fixture.Customize<Vacation>(c => c.With(r => r.Name, vacationName));
            return fixture;
        })
    {
    }

    static private Fixture CreateFixture()
    {
        var fixture = new Fixture();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ProfileVacation>());
        var mapper = configuration.CreateMapper();
        fixture.Inject(mapper);
        fixture.Customize(new AutoNSubstituteCustomization());
        return fixture;
    }
}

