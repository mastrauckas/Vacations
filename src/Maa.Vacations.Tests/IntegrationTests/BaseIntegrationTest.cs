﻿namespace Maa.Vacations.Tests.IntegrationTests;

public abstract class BaseIntegrationTest<TProgram> : WebApplicationFactory<TProgram>, IDisposable where TProgram : class
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<TProgram> _webApplicationFactory;
    private const string _baseURL = "http://localhost";

    internal BaseIntegrationTest()
    {
        var databaseName = $"Vacations_{Guid.NewGuid()}";
        _webApplicationFactory = new WebApplicationFactory<TProgram>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<DbContextOptions<VacationsContext>>();
                    services.AddDbContext<VacationsContext>(options => options.UseInMemoryDatabase(databaseName));
                });
            });
        _client = _webApplicationFactory.CreateClient();
    }

    ~BaseIntegrationTest()
    {
        _webApplicationFactory?.Dispose();
        _client?.Dispose();
    }

    protected async Task<TResponseObject> MakeHttpPostRequest<TResponseObject>
    (
        object body,
        HttpStatusCode? expectedHttpStatusCode = null,
        string contentType = "application/json"
    )
    {
        ArgumentNullException.ThrowIfNull(body);
        ArgumentException.ThrowIfNullOrEmpty(contentType);
        var requestUri = _baseURL;
        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        var payload = JsonSerializer.Serialize(body);
        request.Content = new StringContent(payload, Encoding.UTF8, contentType);

        var response = await _client.SendAsync(request);

        if (expectedHttpStatusCode is not null)
        {
            Assert.Equal(response.StatusCode, expectedHttpStatusCode);
        }
        else
        {
            Assert.True(response.IsSuccessStatusCode);
        }

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        var deserializedObject = JsonSerializer.Deserialize<TResponseObject>(content, options);
        Assert.NotNull(deserializedObject);
        return deserializedObject;
    }

    protected async Task MakeHttpDeleteRequest(int id, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent)
    {
        var requestUri = $"{_baseURL}/{id}";
        using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        var response = await _client.SendAsync(request);
        Assert.Equal(response.StatusCode, expectedHttpStatusCode);
    }

    protected async Task MakeHttpGetRequest(HttpStatusCode? expectedHttpStatusCode = null)
    {
        var requestUri = _baseURL;
        using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await _client.SendAsync(request);
        if (expectedHttpStatusCode is not null)
        {
            Assert.Equal(response.StatusCode, expectedHttpStatusCode);
        }
        else
        {
            Assert.True(response.IsSuccessStatusCode);
        }
    }

    protected async Task<TResponseObject> MakeHttpGetRequest<TResponseObject>(HttpStatusCode? expectedHttpStatusCode = null)
    {
        var requestUri = _baseURL;
        using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await _client.SendAsync(request);

        if (expectedHttpStatusCode is not null)
        {
            Assert.Equal(response.StatusCode, expectedHttpStatusCode);
        }
        else
        {
            Assert.True(response.IsSuccessStatusCode);
        }

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
