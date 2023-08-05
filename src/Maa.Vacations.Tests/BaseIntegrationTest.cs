namespace Maa.Vacations.Tests;

public abstract class BaseIntegrationTest<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly HttpClient _client;

    internal BaseIntegrationTest()
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

    protected async Task MakeHttpRequest(HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK, HttpMethod method = null, object body = null, string contentType = "application/json")
    {
        var requestUri = $"http://localhost/vacations";
        using var request = new HttpRequestMessage(method ?? HttpMethod.Get, requestUri);
        if (body is not null)
        {
            var payload = JsonSerializer.Serialize(body);
            request.Content = new StringContent(payload, Encoding.UTF8, contentType);
        }

        var response = await _client.SendAsync(request);
        Assert.Equal(response.StatusCode, expectedHttpStatusCode);
    }

    protected async Task<TResponseObject> MakeHttpRequest<TResponseObject>(HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK, HttpMethod method = null, object body = null, string contentType = "application/json")
    {
        var requestUri = $"http://localhost/vacations";
        using var request = new HttpRequestMessage(method ?? HttpMethod.Get, requestUri);
        if (body is not null)
        {
            var payload = JsonSerializer.Serialize(body);
            request.Content = new StringContent(payload, Encoding.UTF8, contentType);
        }

        var response = await _client.SendAsync(request);
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
