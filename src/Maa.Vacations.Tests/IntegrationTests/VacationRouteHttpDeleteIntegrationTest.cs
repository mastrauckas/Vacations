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

    [Fact]
    public async Task Check_Delete_Id_Does_Not_Exists_Test()
    {
        await MakeHttpDeleteRequest(100, HttpStatusCode.NotFound);
    }

    [Theory]
    [VacationIntegrationAutoData]
    public async Task Check_If_Vacation_Multiple_Deletes_Test(CreateVacationDto createVacationDto)
    {
        var vacationCreatedDto = await MakeHttpPostRequest<VacationCreatedDto>(createVacationDto);
        await MakeHttpDeleteRequest(vacationCreatedDto.Id);
        await MakeHttpDeleteRequest(vacationCreatedDto.Id, HttpStatusCode.NotFound);
    }
}