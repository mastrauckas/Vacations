namespace Maa.Vacations.Tests;

public class VacationRouteTest : BaseIntegrationTest<Program>
{
    [Fact]
    public async Task Make_Http_Get_Request_Test()
    {
        await MakeHttpRequest();
    }

    [Theory]
    [VacationIntegrationInlineAutoData("Test Vacation", HttpStatusCode.Created)]
    [VacationIntegrationInlineAutoData(null, HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("1", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("12", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("123", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("1234", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("12345", HttpStatusCode.Created)]
    public async Task Create_A_Vacation_Test(string vactionNameExpected, HttpStatusCode statusCodeExpected)
    {
        CreateVacationDto createVacationDto = new(vactionNameExpected);
        var vacationCreatedDto = await MakeHttpRequest<VacationCreatedDto>(expectedHttpStatusCode: statusCodeExpected, method: HttpMethod.Post, body: createVacationDto);
        if (statusCodeExpected == HttpStatusCode.Created)
        {
            Assert.Equal(createVacationDto.Name, vacationCreatedDto.Name);
            Assert.Equal(1, vacationCreatedDto.Id);
        }
    }
}
