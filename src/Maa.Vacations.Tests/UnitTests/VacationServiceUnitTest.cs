using NSubstitute;

namespace Maa.Vacations.Tests.UnitTests;

public class VacationServiceUnitTest
{
    [Theory, VacationUnitTestAutoData("Michaels Vacation")]
    public async Task MakeSureVacationIsSaved3Test
        (
            [Frozen] IVacationRepository routingRepositoryMock, //This must be before VacationService
            VacationService vacationService,
            CreateVacationDto createVacationDto
        )
    {
        var vactionCreatedDto = await vacationService.AddVacationAsync(createVacationDto);
        await routingRepositoryMock.Received(1).AddAsync(Arg.Any<Vacation>());
        await routingRepositoryMock.Received(1).SaveChangesAsync();
        Assert.Equal(createVacationDto.Name, vactionCreatedDto.Name);
    }
}