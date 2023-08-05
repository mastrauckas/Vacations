﻿namespace Maa.Vacations.Tests.IntegrationTests;

public class VacationRouteHttpGetIntegrationTest : BaseIntegrationTest<Program>
{
    [Fact]
    public async Task Make_Http_Get_Request_Test()
    {
        await MakeHttpRequest();
    }

    [Theory]
    [VacationIntegrationInlineAutoData("Test Vacation", 1, HttpStatusCode.Created)]
    [VacationIntegrationInlineAutoData("12345", 1, HttpStatusCode.Created)]
    public async Task Http_Get_Create_A_Vacation_Test(string vacationNameExpected, int? expectedId, HttpStatusCode statusCodeExpected)
    {
        CreateVacationDto createVacationDto = new(vacationNameExpected);
        var vacationCreatedDto = await MakeHttpRequest<VacationCreatedDto>(expectedHttpStatusCode: statusCodeExpected, method: HttpMethod.Post, body: createVacationDto);
        Assert.Equal(vacationNameExpected, vacationCreatedDto.Name);
        Assert.Equal(expectedId, vacationCreatedDto.Id);
    }

    [Theory]
    [VacationIntegrationInlineAutoData(null, HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("1", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("12", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("123", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("1234", HttpStatusCode.BadRequest)]
    public async Task Http_Get_BadRequest_A_Vacation_Test(string vacationNameExpected, HttpStatusCode statusCodeExpected)
    {
        CreateVacationDto createVacationDto = new(vacationNameExpected);
        await MakeHttpRequest<VacationCreatedDto>(expectedHttpStatusCode: statusCodeExpected, method: HttpMethod.Post, body: createVacationDto);
    }
}
