namespace Maa.Vacations.Tests.UnitTests.Attributes;

public class VacationUnitTestInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public VacationUnitTestInlineAutoDataAttribute(params object[] values) :
        base(new VacationUnitTestAutoDataAttribute(), values)
    {
    }
}