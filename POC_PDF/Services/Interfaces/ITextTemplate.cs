namespace POC_PDF.Services.Interfaces;

public interface ITextTemplate
{
    Task<string> RetrieveAsync(string name);
}