using Maa.Vacations.Tests.Dtos;

namespace Maa.Vacations.Tests.IntegrationTests;

public class VacationRouteHttpPostIntegrationTest : BaseIntegrationTest<Program>
{
    [Theory]
    [VacationIntegrationInlineAutoData("Test Vacation", 1)]
    [VacationIntegrationInlineAutoData("12345", 1)]
    public async Task Http_Create_A_Vacation_Test(string vacationNameExpected, int? expectedId)
    {
        CreateVacationDto createVacationDto = new(vacationNameExpected);
        var vacationCreatedDto = await MakeHttpPostRequest<VacationCreatedDto>(createVacationDto, HttpStatusCode.Created);
        Assert.Equal(vacationNameExpected, vacationCreatedDto.Name);
        Assert.Equal(expectedId, vacationCreatedDto.Id);
    }

    [Theory]
    [VacationIntegrationInlineAutoData(null, "The Name field is required.")]
    [VacationIntegrationInlineAutoData("1", "The field Name must be a string with a minimum length of 5 and a maximum length of 100.")]
    [VacationIntegrationInlineAutoData("12", "The field Name must be a string with a minimum length of 5 and a maximum length of 100.")]
    [VacationIntegrationInlineAutoData("123", "The field Name must be a string with a minimum length of 5 and a maximum length of 100.")]
    [VacationIntegrationInlineAutoData("1234", "The field Name must be a string with a minimum length of 5 and a maximum length of 100.")]
    public async Task Http_BadRequest_Error_Vacation_Test(string vacationNameExpected, string expectedError)
    {
        CreateVacationDto createVacationDto = new(vacationNameExpected);
        var expectedTitle = "One or more validation errors occurred.";
        var expectedStatus = 400;
        var errorResponse = await MakeHttpPostRequest<ErrorResponse>(createVacationDto, HttpStatusCode.BadRequest);

        Assert.NotNull(errorResponse);
        Assert.Equal(expectedStatus, errorResponse.Status);
        Assert.Equal(expectedTitle, errorResponse.Title);
        Assert.Single(errorResponse.Errors.Name);
        Assert.Equal(expectedError, errorResponse.Errors.Name.Single());
    }
}
