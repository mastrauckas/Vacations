using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Maa.Vacations.Tests;

public class VacationRouteTest : WebApplicationFactory<Program>
{
    private readonly HttpClient _client;

    public VacationRouteTest()
    {
        var databaseName = $"Vacations_{Guid.NewGuid()}";
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll<DbContextOptions<VacationsContext>>();
                        services.AddDbContext<VacationsContext>(options => options.UseInMemoryDatabase(databaseName));
                    });
            });
        _client = application.CreateClient();
    }

    [Fact]
    public async Task Make_Http_Get_Request_Test()
    {
        await MakeHttpRequest(_client);
    }

    [Theory]
    [VacationIntegrationInlineAutoData("Test Vacation", HttpStatusCode.Created)]
    [VacationIntegrationInlineAutoData(null, HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("1", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("12", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("123", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("1234", HttpStatusCode.BadRequest)]
    [VacationIntegrationInlineAutoData("12345", HttpStatusCode.Created)]
    public async Task Create_A_Vacation_Test(string vactionName, HttpStatusCode statusCode)
    {
        CreateVacationDto createVacationDto = new(vactionName);
        var vacationCreatedDto = await MakeHttpRequest<VacationCreatedDto>(_client, expectedHttpStatusCode: statusCode, method: HttpMethod.Post, body: createVacationDto);
        if (statusCode == HttpStatusCode.Created)
        {
            Assert.Equal(createVacationDto.Name, vacationCreatedDto.Name);
            Assert.Equal(1, vacationCreatedDto.Id);
        }
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
