using Microsoft.AspNetCore.Components.Forms;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Models;

namespace Drip.Services;

public class OpenApiService
{
    public async Task<(OpenApiDocument? Document, OpenApiDiagnostic? Diagnostic, string? Error)> ParseOpenApiDocumentAsync(IBrowserFile file)
    {
        try
        {
            using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10MB max
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            
            var openApiReader = new OpenApiStringReader();
            var document = openApiReader.Read(content, out var diagnostic);
            
            foreach (var (path, pathItem) in document.Paths)
            {
                foreach (var (operationType, operation) in pathItem.Operations)
                {
                    if (string.IsNullOrEmpty(operation.OperationId))
                        operation.OperationId = Guid.NewGuid().ToString();

                    for (var i = operation.Tags.Count - 1; i >= 0; i--)
                    {
                        var tag = operation.Tags[i];
                        
                        var existingTag = document.Tags.FirstOrDefault(t => t.Name == tag.Name);

                        if (existingTag is not null)
                        {
                            operation.Tags[i] = existingTag;
                        }
                        else
                        {
                            document.Tags.Add(tag);
                        }
                    }
                }
            }

            if (diagnostic.Errors.Any())
            {
                var errorMessage = string.Join("; ", diagnostic.Errors.Select(e => e.Message));
                return (null, diagnostic, $"OpenAPI parsing errors: {errorMessage}");
            }
            
            return (document, diagnostic, null);
        }
        catch (Exception ex)
        {
            return (null, null, $"Error parsing OpenAPI document: {ex.Message}");
        }
    }
    
    public async Task<OpenApiDocument?> LoadDefaultDocumentAsync(HttpClient httpClient, string fileName = "petstore.json")
    {
        try
        {
            var stream = await httpClient.GetStreamAsync(fileName);
            var openApiReader = new OpenApiStreamReader();
            var document = openApiReader.Read(stream, out var diagnostic);
            
            if (diagnostic.Errors.Any())
            {
                // Log errors but don't fail completely
                Console.WriteLine($"Warning: OpenAPI parsing errors for {fileName}: {string.Join("; ", diagnostic.Errors.Select(e => e.Message))}");
            }
            
            return document;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading default OpenAPI document: {ex.Message}");
            return null;
        }
    }
} 