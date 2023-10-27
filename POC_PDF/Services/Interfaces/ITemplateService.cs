namespace POC_PDF.Services.Interfaces;

public interface ITemplateService
{
    Task<string> RenderTemplate<TData>(string template, TData data, int tipoTemplateEnum);
    Task<string> GetPdf(string html);
}