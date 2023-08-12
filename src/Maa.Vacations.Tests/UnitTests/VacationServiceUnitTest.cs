namespace Maa.Vacations.Tests.UnitTests;

public class VacationServiceUnitTest
{
    [Theory, VacationUnitTestAutoData("Michaels Vacation")]
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
        Assert.Equal(createVacationDto.Name + 2, vactionCreatedDto.Name);
    }
}