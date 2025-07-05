using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Reader;

namespace Drip;

public sealed class OpenApiDocumentLoader(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<OpenApiDocument?> LoadLocalDocumentAsync(string fileName = "star-trek.json")
    {
        var reader = new OpenApiJsonReader();
        await using var stream = await _httpClient.GetStreamAsync(fileName);
        var (document, _) = await reader.ReadAsync(stream, new OpenApiReaderSettings());
        return document;
    }
}
