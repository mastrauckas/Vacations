namespace Maa.Vacations.Tests.IntegrationTests.Attributes;

public class VacationIntegrationInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public VacationIntegrationInlineAutoDataAttribute(params object[] values) :
        base(new VacationIntegrationAutoDataAttribute(), values)
    {
    }
}