using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;

namespace Drip.Services;

public class DocumentInfo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public OpenApiDocument Document { get; set; } = new();
    public string Source { get; set; } = string.Empty;
    public DateTime AddedAt { get; set; } = DateTime.Now;
}

public class OpenApiStateService
{
    private OpenApiDocument? _currentDocument;
    private string? _currentDocumentId;
    private readonly Dictionary<string, DocumentInfo> _documents = new();
    
    public event Action? OnDocumentChanged;
    public event Action? OnDocumentsListChanged;
    
    public OpenApiDocument? CurrentDocument => _currentDocument;
    public string? CurrentDocumentId => _currentDocumentId;
    public string? DocumentSource => GetCurrentDocumentInfo()?.Source;
    
    public IEnumerable<DocumentInfo> AllDocuments => _documents.Values.OrderBy(d => d.AddedAt);
    public DocumentInfo? GetCurrentDocumentInfo() => _currentDocumentId != null && _documents.ContainsKey(_currentDocumentId) ? _documents[_currentDocumentId] : null;
    
    public void SetDocument(OpenApiDocument document, string source = "default")
    {
        var documentId = Guid.NewGuid().ToString();
        var documentName = document.Info?.Title ?? "Untitled Document";
        
        var documentInfo = new DocumentInfo
        {
            Id = documentId,
            Name = documentName,
            Document = document,
            Source = source,
            AddedAt = DateTime.Now
        };
        
        _documents[documentId] = documentInfo;
        _currentDocument = document;
        _currentDocumentId = documentId;
        
        OnDocumentChanged?.Invoke();
        OnDocumentsListChanged?.Invoke();
    }
    
    public void AddDocument(OpenApiDocument document, string name, string source = "uploaded")
    {
        var documentId = Guid.NewGuid().ToString();
        
        var documentInfo = new DocumentInfo
        {
            Id = documentId,
            Name = name,
            Document = document,
            Source = source,
            AddedAt = DateTime.Now
        };
        
        _documents[documentId] = documentInfo;
        OnDocumentsListChanged?.Invoke();
    }
    
    public void SwitchToDocument(string documentId)
    {
        if (_documents.ContainsKey(documentId))
        {
            _currentDocument = _documents[documentId].Document;
            _currentDocumentId = documentId;
            OnDocumentChanged?.Invoke();
        }
    }
    
    public void RemoveDocument(string documentId)
    {
        if (_documents.ContainsKey(documentId))
        {
            _documents.Remove(documentId);
            
            // If we're removing the current document, switch to default or first available
            if (_currentDocumentId == documentId)
            {
                var defaultDoc = _documents.Values.FirstOrDefault(d => d.Source == "default");
                if (defaultDoc != null)
                {
                    SwitchToDocument(defaultDoc.Id);
                }
                else if (_documents.Any())
                {
                    SwitchToDocument(_documents.First().Key);
                }
                else
                {
                    _currentDocument = null;
                    _currentDocumentId = null;
                    OnDocumentChanged?.Invoke();
                }
            }
            
            OnDocumentsListChanged?.Invoke();
        }
    }
    
    public void ClearDocument()
    {
        _currentDocument = null;
        _currentDocumentId = null;
        OnDocumentChanged?.Invoke();
    }
    
    public void ClearAllDocuments()
    {
        _documents.Clear();
        _currentDocument = null;
        _currentDocumentId = null;
        OnDocumentChanged?.Invoke();
        OnDocumentsListChanged?.Invoke();
    }
    
    public bool HasDocument => _currentDocument != null;
    public bool IsUploadedDocument => GetCurrentDocumentInfo()?.Source == "uploaded";
    public bool HasMultipleDocuments => _documents.Count > 1;
} 