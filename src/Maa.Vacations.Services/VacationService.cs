namespace Maa.Vacations.Services;

public class VacationService : IVacationService
{
    private readonly IVacationRepository _vacationRepository;
    private readonly IMapper _mapper;

    public VacationService(IVacationRepository vacationRepository, IMapper mapper)
    {
        _vacationRepository = vacationRepository;
        _mapper = mapper;
    }

    public async Task<VacationCreatedDto> AddVacationAsync(CreateVacationDto createVacationDto)
    {
        if (createVacationDto is null) throw new ArgumentNullException(nameof(createVacationDto));
        if (createVacationDto.Name is null) throw new ArgumentNullException(nameof(createVacationDto.Name));

        var Vacation = _mapper.Map<Vacation>(createVacationDto);
        await _vacationRepository.AddAsync(Vacation);
        await _vacationRepository.SaveChangesAsync();
        return _mapper.Map<VacationCreatedDto>(Vacation);
    }

    public async Task<VacationUpdatedDto?> UpdateVacationAsync(int id , UpdateVacationDto updateVacationDto)
    {
        if (updateVacationDto is null) throw new ArgumentNullException(nameof(updateVacationDto));
        if (updateVacationDto.Name is null) throw new ArgumentNullException(nameof(updateVacationDto.Name));

        var Vacation = await _vacationRepository.GetByIdAsync(id);

        VacationUpdatedDto? VacationUpdatedDto = null;

        if (Vacation is not null && Vacation.DeletedDateTime is null)
        {
            _mapper.Map(updateVacationDto, Vacation);
            await _vacationRepository.SaveChangesAsync();
            VacationUpdatedDto =  _mapper.Map<VacationUpdatedDto?>(Vacation);
        }

        return VacationUpdatedDto;
    }

    public async Task<DeletedVacationDto?> DeleteVacationAsync(int id)
    {
        var Vacation = await _vacationRepository.GetByIdAsync(id);
        DeletedVacationDto? deletedVacationDto = null;
        if (Vacation is not null && Vacation.DeletedDateTime is null)
        {
            Vacation.DeletedDateTime = DateTime.UtcNow;
            await _vacationRepository.SaveChangesAsync();
            deletedVacationDto = _mapper.Map<DeletedVacationDto?>(Vacation);
        }

        return deletedVacationDto;
    }

    public async Task<IEnumerable<CurrentVacationDto>> GetAllVacationsAsync()
    {
        var Vacations = await _vacationRepository.GetAllVacationsAsync();
        return _mapper.Map<IEnumerable<CurrentVacationDto>>(Vacations);
    }

    public async Task<CurrentVacationDto?> GetVacationByIdAsync(int id)
    {
        var Vacation = await _vacationRepository.GetByIdAsync(id);
        return Vacation is not null && Vacation.DeletedDateTime is null ? _mapper.Map<CurrentVacationDto?>(Vacation) : null;
    }
}