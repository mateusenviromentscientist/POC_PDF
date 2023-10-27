using POC_PDF.Models.Enum;

namespace POC_PDF.Models;

public class TemplateModel
{
    public int Id { get; set; }
    public string Template { get; set; }
    public TipoTemplateEnum TipoTemplate { get; set; }
}