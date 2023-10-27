using POC_PDF.Dtos;
using POC_PDF.Models;
using POC_PDF.Models.Enum;

namespace POC_PDF.Services.Interfaces;

public interface ITextTemplate
{
    Task<TemplateModel> RetrieveAsync(int tipoTemplateEnum);
    Task<int> CreateTemplate(TemplateCreateDto templateCreateDto, string name, TipoTemplateEnum tipoTemplateEnum);
}