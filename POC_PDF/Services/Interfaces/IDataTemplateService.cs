using System.Globalization;
using POC_PDF.Models.Enum;

namespace POC_PDF.Services.Interfaces;

public interface IDataTemplateService
{
    Task<string> GetTemplate(RenderType renderType, LanguageEnum languageEnum);
}