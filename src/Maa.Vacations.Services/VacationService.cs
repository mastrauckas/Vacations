namespace Maa.Vacations.Services;

public class VacationService : IVacationService
{
    private readonly IMapper             _mapper;
    private readonly IVacationRepository _vacationRepository;

    public VacationService(IVacationRepository vacationRepository, IMapper mapper)
    {
        _vacationRepository = vacationRepository;
        _mapper             = mapper;
    }

    public async Task<VacationCreatedDto> AddVacationAsync(CreateVacationDto createVacationDto)
    {
        if (createVacationDto is null)
        {
            throw new ArgumentNullException(nameof(createVacationDto));
        }

        if (createVacationDto.Name is null)
        {
            throw new ArgumentNullException(nameof(createVacationDto.Name));
        }

        var vacation = _mapper.Map<Vacation>(createVacationDto);
        ArgumentException.ThrowIfNullOrEmpty(vacation.Name);

        await _vacationRepository.AddAsync(vacation);
        await _vacationRepository.SaveChangesAsync();

        return _mapper.Map<VacationCreatedDto>(vacation);
    }

    public async Task<VacationUpdatedDto?> UpdateVacationAsync(int id, UpdateVacationDto updateVacationDto)
    {
        if (updateVacationDto is null)
        {
            throw new ArgumentNullException(nameof(updateVacationDto));
        }

        if (updateVacationDto.Name is null)
        {
            throw new ArgumentNullException(nameof(updateVacationDto.Name));
        }

        var vacation = await _vacationRepository.GetByIdAsync(id);

        VacationUpdatedDto? vacationUpdatedDto = null;

        if (vacation is null || vacation.DeletedDateTime is not null)
        {
            return vacationUpdatedDto;
        }

        _mapper.Map(updateVacationDto, vacation);
        await _vacationRepository.SaveChangesAsync();
        vacationUpdatedDto = _mapper.Map<VacationUpdatedDto?>(vacation);

        return vacationUpdatedDto;
    }

    public async Task<DeletedVacationDto?> DeleteVacationAsync(int id)
    {
        var                 vacation           = await _vacationRepository.GetByIdAsync(id);
        DeletedVacationDto? deletedVacationDto = null;
        if (vacation is null || vacation.DeletedDateTime is not null)
        {
            return deletedVacationDto;
        }

        vacation.DeletedDateTime = DateTime.UtcNow;
        await _vacationRepository.SaveChangesAsync();
        deletedVacationDto = _mapper.Map<DeletedVacationDto?>(vacation);

        return deletedVacationDto;
    }

    public async Task<IEnumerable<CurrentVacationDto>> GetAllVacationsAsync()
    {
        IEnumerable<Vacation>? vacations = await _vacationRepository.GetAllVacationsAsync();

        return _mapper.Map<IEnumerable<CurrentVacationDto>>(vacations);
    }

    public async Task<CurrentVacationDto?> GetVacationByIdAsync(int id)
    {
        var vacation = await _vacationRepository.GetByIdAsync(id);

        return vacation is not null && vacation.DeletedDateTime is null
            ? _mapper.Map<CurrentVacationDto?>(vacation)
            : null;
    }
}