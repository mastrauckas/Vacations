namespace Maa.Vacations.Tests.IntegrationTests;

public class VacationRouteHttpGetIntegrationTest : BaseIntegrationTest<Program>
{
    [Fact]
    public async Task Make_Http_Request_Test()
    {
        await MakeHttpGetRequest();
    }
}