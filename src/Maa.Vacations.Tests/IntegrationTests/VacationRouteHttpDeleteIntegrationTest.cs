namespace Maa.Vacations.Tests.IntegrationTests;

public class VacationRouteHttpDeleteIntegrationTest : BaseIntegrationTest<Program>
{
    [Theory]
    [VacationIntegrationAutoData]
    public async Task Check_If_Vacation_Can_Be_Deleted_Test(CreateVacationDto createVacationDto)
    {
        var vacationCreatedDto = await MakeHttpPostRequest<VacationCreatedDto>(createVacationDto);
        await MakeHttpDeleteRequest(vacationCreatedDto.Id);
    }
}
