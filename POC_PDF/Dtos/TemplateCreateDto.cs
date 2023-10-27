using POC_PDF.Models.Enum;

namespace POC_PDF.Dtos;

public class TemplateCreateDto
{
    public string Template { get; set; }
    public TipoTemplateEnum TipoTemplate { get; set; }
}