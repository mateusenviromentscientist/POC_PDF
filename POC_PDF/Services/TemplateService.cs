using POC_PDF.Services.Interfaces;
using RazorLight;
using WkHtmlToPdfDotNet;

namespace POC_PDF.Services;

public class TemplateService : ITemplateService
{
    private readonly RazorLightEngine _engine;
    private readonly ITextTemplate _template;

    public TemplateService(ITextTemplate template)
    {
        _engine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(System.Reflection.Assembly.GetEntryAssembly())
            .UseMemoryCachingProvider()
            .Build();
        _template = template;
    }
    public async Task<string> RenderTemplate<TData>(string template, TData data)
    {
        
        var content = await _template.RetrieveAsync(template);

        var cachedTemplate = _engine.Handler.Cache.RetrieveTemplate(template);
        if (cachedTemplate.Success)
        {
            return await _engine.RenderTemplateAsync(cachedTemplate.Template.TemplatePageFactory(), data);
        }

        return await _engine.CompileRenderStringAsync(template,content,data,null);
    }

    public Task<string> GetPdf(string html)
    {
        try
        {
            var converter = new BasicConverter(new PdfTools());
            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Plus,
                    // Out = @"D:\dev\POC_PDF\POC_PDF\teste.pdf",
                },
                Objects = {
                    new ObjectSettings {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };
            
            var pdf = Convert.ToBase64String(converter.Convert(doc));
            
            return Task.FromResult(pdf);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}