using Microsoft.OpenApi.Reader;

namespace Drip;

public sealed class OpenApiDocumentLoader(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ReadResult> LoadLocalDocumentAsync(string fileName = "example.json")
    {
        var reader = new OpenApiJsonReader();
        await using var stream = await _httpClient.GetStreamAsync(fileName);
        return await reader.ReadAsync(stream, new OpenApiReaderSettings());
    }
}
