namespace Maa.Vacations.Tests.UnitTests;

public class VacationServiceTest
{
    private const string _vacationName = "Michael Vacation";

    [Theory, VacationAutoData()]
    public async Task MakeSureVacationIsSaved3Test
        (
            [Frozen] Mock<IVacationRepository> routingRepositoryMock, //This must be before VacationService
            VacationService vacationService,
            CreateVacationDto createVacationDto
        )
    {
        var vactionCreatedDto = await vacationService.AddVacationAsync(createVacationDto);
        routingRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<Vacation>()), Times.Once());
        routingRepositoryMock.Verify(rr => rr.SaveChangesAsync(), Times.Once());
        Assert.Equal(createVacationDto.Name, vactionCreatedDto.Name);
    }

    [Theory, VacationAutoData(_vacationName)]
    public void CheckNameIsCorrectTest(Vacation vacation)
    {
        Assert.Equal(_vacationName, vacation.Name);
    }
}