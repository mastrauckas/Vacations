namespace Maa.Vacations.Tests;

public class VacationServiceTest
{
    private readonly Fixture _fixture;
    private readonly Mock<IVacationRepository> _routingRepositoryMock;
    private readonly VacationService _VacationService;
    private const string _VacationName = "Michael Vacation";

    public VacationServiceTest()
    {
        _fixture = new Fixture();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ProfileVacation>());
        var mapper = configuration.CreateMapper();
        _routingRepositoryMock = new Mock<IVacationRepository>();
        _fixture.Inject(mapper);
        _VacationService = new VacationService(_routingRepositoryMock.Object, mapper);
    }

    [Fact]
    public async Task MakeSureVacationIsSaved1Test()
    {
        _fixture.Customize(new AutoMoqCustomization());

        var routingRepositoryMock = _fixture.Freeze<Mock<IVacationRepository>>();
        var createVacationDto = _fixture.Create<CreateVacationDto>();
        var vacationService = _fixture.Create<VacationService>();

        var vactionCreatedDto = await vacationService.AddVacationAsync(createVacationDto);
        routingRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<Vacation>()), Times.Once());
        routingRepositoryMock.Verify(rr => rr.SaveChangesAsync(), Times.Once());
        Assert.Equal(createVacationDto.Name, vactionCreatedDto.Name);
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
            CreateVacationDto createVacationDto
        )
    {
        var vactionCreatedDto = await vacationService.AddVacationAsync(createVacationDto);
        routingRepositoryMock.Verify(rr => rr.AddAsync(It.IsAny<Vacation>()), Times.Once());
        routingRepositoryMock.Verify(rr => rr.SaveChangesAsync(), Times.Once());
        Assert.Equal(createVacationDto.Name, vactionCreatedDto.Name);
    }

    [Theory, VacationAutoData(_VacationName)]
    public void CheckNameIsCorrectTest(Vacation vacation)
    {
        Assert.Equal(_VacationName, vacation.Name);
    }
}