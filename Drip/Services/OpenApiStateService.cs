using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;

namespace Drip.Services;

public class OpenApiStateService
{
    private OpenApiDocument? _currentDocument;
    private string? _documentSource;
    
    public event Action? OnDocumentChanged;
    
    public OpenApiDocument? CurrentDocument => _currentDocument;
    public string? DocumentSource => _documentSource;
    
    public void SetDocument(OpenApiDocument document, string source = "default")
    {
        _currentDocument = document;
        _documentSource = source;
        OnDocumentChanged?.Invoke();
    }
    
    public void ClearDocument()
    {
        _currentDocument = null;
        _documentSource = null;
        OnDocumentChanged?.Invoke();
    }
    
    public bool HasDocument => _currentDocument != null;
    public bool IsUploadedDocument => _documentSource == "uploaded";
} 