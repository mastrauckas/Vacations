namespace Maa.Vacations.Tests.IntegrationTests;

public class VacationRouteIntegrationTest : BaseIntegrationTest<Program>
{
    [Fact]
    public async Task Make_Http_Get_Request_Test()
    {
        await MakeHttpRequest();
    }

    [Theory]
    [VacationIntegrationInlineAutoData("Test Vacation", 1, HttpStatusCode.Created)]
    [VacationIntegrationInlineAutoData(null, null, HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("1", null, HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("12", null, HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("123", null, HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("1234", null, HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("12345", 1, HttpStatusCode.Created)]
    public async Task Create_A_Vacation_Test(string vactionNameExpected, int? expectedId, HttpStatusCode statusCodeExpected)
    {
        CreateVacationDto createVacationDto = new(vactionNameExpected);
        var vacationCreatedDto = await MakeHttpRequest<VacationCreatedDto>(expectedHttpStatusCode: statusCodeExpected, method: HttpMethod.Post, body: createVacationDto);
        if (statusCodeExpected == HttpStatusCode.Created)
        {
            Assert.Equal(vactionNameExpected, vacationCreatedDto.Name);
            Assert.Equal(expectedId, vacationCreatedDto.Id);
        }
    }
}
