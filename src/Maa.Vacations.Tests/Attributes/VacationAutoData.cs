namespace Maa.Vacations.Tests.Attributes;

internal class VacationAutoDataAttribute : AutoDataAttribute
{
    public VacationAutoDataAttribute() : base(
    () =>
    {
        var fixture = new Fixture();
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

    //public VacationAutoDataAttribute(string vacationName, int howManyDeliveries) : base(
    //    () =>
    //    {
    //        var fixture = new Fixture();
    //        fixture.Customize(new AutoMoqCustomization());

    //        fixture.Customize<Vacation>
    //               (c =>
    //                   c.With(r => r.Name, vacationName)
    //                    .With(r => r.Deliveries, fixture.CreateMany<Delivery>(howManyDeliveries))
    //               );

    //        return fixture;
    //    })
    //{
    //}

    //public VacationAutoDataAttribute(int howManyDeliveries) : base(
    //    () =>
    //    {
    //        var fixture = new Fixture();
    //        fixture.Customize(new AutoMoqCustomization());

    //        fixture.Customize<Vacation>(c => c.With(r => r.Deliveries, fixture.CreateMany<Delivery>(howManyDeliveries)));

    //        return fixture;
    //    })
    //{
    //}
}

