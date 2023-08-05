namespace Maa.Vacations.Tests.UnitTests.Attributes;

internal class VacationAutoDataAttribute : AutoDataAttribute
{
    public VacationAutoDataAttribute() : base(
    () =>
    {
        var fixture = new Fixture();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ProfileVacation>());
        var mapper = configuration.CreateMapper();
        fixture.Inject(mapper);
        fixture.Customize(new AutoMoqCustomization());
        return fixture;
    })
    {
    }

    public VacationAutoDataAttribute(string vacationName) : base(
        () =>
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            fixture.Customize<Vacation>
                   (c => c.With(r => r.Name, vacationName));

            return fixture;
        })
    {
    }
}

