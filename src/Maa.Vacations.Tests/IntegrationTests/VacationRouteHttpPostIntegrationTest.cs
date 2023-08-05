namespace Maa.Vacations.Tests.IntegrationTests;

public class VacationRouteHttpPostIntegrationTest : BaseIntegrationTest<Program>
{
    [Theory]
    [VacationIntegrationInlineAutoData("Test Vacation", 1)]
    [VacationIntegrationInlineAutoData("12345", 1)]
    public async Task Http_Create_A_Vacation_Test(string vacationNameExpected, int? expectedId)
    {
        CreateVacationDto createVacationDto = new(vacationNameExpected);
        var vacationCreatedDto = await MakeHttpPostRequest<VacationCreatedDto>(createVacationDto, HttpStatusCode.Created);
        Assert.Equal(vacationNameExpected, vacationCreatedDto.Name);
        Assert.Equal(expectedId, vacationCreatedDto.Id);
    }

    [Theory]
    [VacationIntegrationInlineAutoData(null)]
    [VacationIntegrationInlineAutoData("1")]
    [VacationIntegrationInlineAutoData("12")]
    [VacationIntegrationInlineAutoData("123")]
    [VacationIntegrationInlineAutoData("1234")]
    public async Task Http_BadRequest_A_Vacation_Test(string vacationNameExpected)
    {
        CreateVacationDto createVacationDto = new(vacationNameExpected);
        await MakeHttpPostRequest<VacationCreatedDto>(createVacationDto, HttpStatusCode.BadRequest);
    }
}
