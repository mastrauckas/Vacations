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
        var Vacation = fixture.Create<CreateVacationDto>();
        var VacationService = fixture.Create<VacationService>();

        await VacationService.AddVacationAsync(Vacation);
        routingRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<Vacation>()), Times.Once());
        routingRepositoryMock.Verify(rr => rr.SaveChangesAsync(), Times.Once());
    }

    [Theory, AutoData]
    public async Task MakeSureVacationIsSaved2Test(CreateVacationDto Vacation)
    {
        await _VacationService.AddVacationAsync(Vacation);
        _routingRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<Vacation>()), Times.Once());
        _routingRepositoryMock.Verify(rr => rr.SaveChangesAsync(), Times.Once());
    }

    [Theory, VacationAutoData()]
    public async Task MakeSureVacationIsSaved3Test
        (
            [Frozen] Mock<IVacationRepository> routingRepositoryMock, //This must be before VacationService
            VacationService VacationService,
            CreateVacationDto Vacation
        )
    {
        await VacationService.AddVacationAsync(Vacation);
        routingRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<Vacation>()), Times.Once());
        routingRepositoryMock.Verify(rr => rr.SaveChangesAsync(), Times.Once());
    }

    [Theory, VacationAutoData(_VacationName)]
    public void CheckNameIsCorrectTest(Vacation Vacation)
    {
        Assert.Equal(_VacationName, Vacation.Name);
    }
}