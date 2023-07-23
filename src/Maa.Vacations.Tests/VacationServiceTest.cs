namespace Maa.Vacations.Tests;

public class VacationServiceTest
{
    private readonly Mock<IVacationRepository> _routingRepositoryMock;
    private readonly VacationService _VacationService;
    private const string _VacationName = "Michael Vacation";
    private const int _howManyDeliveries = 25;

    public VacationServiceTest()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ProfileVacation>());
        var mapper = configuration.CreateMapper();
        _routingRepositoryMock = new Mock<IVacationRepository>();
        _VacationService = new VacationService(_routingRepositoryMock.Object, mapper);
    }

    [Fact]
    public async Task MakeSureVacationIsSaved1Test()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization());

        var routingRepositoryMock = fixture.Freeze<Mock<IVacationRepository>>();
        var vacation = fixture.Create<CreateVacationDto>();
        var vacationService = fixture.Create<VacationService>();

        await vacationService.AddVacationAsync(vacation);
        routingRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<Vacation>()), Times.Once());
        routingRepositoryMock.Verify(rr => rr.SaveChangesAsync(), Times.Once());
    }

    [Theory, AutoData]
    public async Task MakeSureVacationIsSaved2Test(CreateVacationDto vacation)
    {
        await _VacationService.AddVacationAsync(vacation);
        _routingRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<Vacation>()), Times.Once());
        _routingRepositoryMock.Verify(rr => rr.SaveChangesAsync(), Times.Once());
    }

    [Theory, VacationAutoData()]
    public async Task MakeSureVacationIsSaved3Test
        (
            [Frozen] Mock<IVacationRepository> routingRepositoryMock, //This must be before VacationService
            VacationService vacationService,
            CreateVacationDto vacation
        )
    {
        await vacationService.AddVacationAsync(vacation);
        routingRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<Vacation>()), Times.Once());
        routingRepositoryMock.Verify(rr => rr.SaveChangesAsync(), Times.Once());
    }

    [Theory, VacationAutoData(_VacationName)]
    public void CheckNameIsCorrectTest(Vacation vacation)
    {
        Assert.Equal(_VacationName, vacation.Name);
    }
}