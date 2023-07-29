namespace Maa.Vacations.Tests;

public class VacationRouteTest
{
    [Theory, VacationIntegrationAutoData]
    public void MakeSureVacationIsSaved2Test(HttpClient client)
    {
        var i = 0;
        i++;

        Assert.NotNull(client);
    }
}
