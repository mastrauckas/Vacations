namespace Maa.Vacations.Tests;

public class VacationIntegrationInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public VacationIntegrationInlineAutoDataAttribute(params object[] values) : base(new VacationIntegrationAutoDataAttribute(), values)
    {
    }
}

