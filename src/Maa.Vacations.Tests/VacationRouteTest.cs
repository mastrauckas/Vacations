using System.Net;
using System.Text;
using System.Text.Json;

namespace Maa.Vacations.Tests;

public class VacationRouteTest
{
    [Theory, VacationIntegrationAutoData]
    public async Task Make_Http_Get_Request_Test(HttpClient client)
    {
        await MakeHttpRequest(client);
    }

    [Theory, VacationIntegrationAutoData]
    public async Task Create_A_Vacation_Test(HttpClient client)
    {
        CreateVacationDto createVacationDto = new("Test Vacation");
        var vacationCreatedDto = await MakeHttpRequest<VacationCreatedDto>(client, expectedHttpStatusCode: HttpStatusCode.Created, method: HttpMethod.Post, body: createVacationDto);
        Assert.Equal(createVacationDto.Name, vacationCreatedDto.Name);
        Assert.True(vacationCreatedDto.Id > 0);
    }

    private async Task MakeHttpRequest(HttpClient client, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK, HttpMethod method = null, object body = null, string contentType = "application/json")
    {
        Assert.NotNull(client);
        var requestUri = $"http://localhost/vacations";
        using var request = new HttpRequestMessage(method ?? HttpMethod.Get, requestUri);
        if (body is not null)
        {
            var payload = JsonSerializer.Serialize(body);
            request.Content = new StringContent(payload, Encoding.UTF8, contentType);
        }

        var response = await client.SendAsync(request);
        Assert.Equal(response.StatusCode, expectedHttpStatusCode);
    }

    private async Task<TResponseObject> MakeHttpRequest<TResponseObject>(HttpClient client, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK, HttpMethod method = null, object body = null, string contentType = "application/json")
    {
        Assert.NotNull(client);
        var requestUri = $"http://localhost/vacations";
        using var request = new HttpRequestMessage(method ?? HttpMethod.Get, requestUri);
        if (body is not null)
        {
            var payload = JsonSerializer.Serialize(body);
            request.Content = new StringContent(payload, Encoding.UTF8, contentType);
        }

        var response = await client.SendAsync(request);
        Assert.Equal(response.StatusCode, expectedHttpStatusCode);

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var deserializedObject = JsonSerializer.Deserialize<TResponseObject>(content, options);
        Assert.NotNull(deserializedObject);
        return deserializedObject;
    }
}
