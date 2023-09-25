using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using POC_PDF.Models.Enum;
using POC_PDF.Services.Interfaces;
using POC_PDF.ViewModels;

namespace POC_PDF.Services;

public class DateTemplateService : IDataTemplateService
{
    public DateTemplateService(IProductStoreService productStoreService, ITemplateService templateService)
    {
        _productStoreService = productStoreService;
        _templateService = templateService;
    }

    private readonly IProductStoreService _productStoreService;
    private readonly ITemplateService _templateService;
    
    public async Task<string> GetTemplate(RenderType renderType, LanguageEnum cultureInfo)
    {
        try
        {
            var template = "";
            var resultString = "";
            const string pattern = "\r\n";
            var model = await _productStoreService.GetRecords();
            var rm = Resources.translate.ResourceManager;
            var templateModel = new ProductViewModel().GetListProducts(model, GetCultureInfo(cultureInfo));
            
            switch (renderType)
            {
                case RenderType.Html:
                    template = await _templateService.RenderTemplate("ProductView",
                        new ProducsViewModels(templateModel, GetViewProduct(rm, GetCultureInfo(cultureInfo))));
                    break;
                case RenderType.Pdf:
                {
                    var modelTemplate = await _templateService.RenderTemplate("ProductView",
                        new ProducsViewModels(templateModel, GetViewProduct(rm, GetCultureInfo(cultureInfo))));
                    resultString = await _templateService.GetPdf(modelTemplate);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(renderType), renderType, null);
            }
            
            if(renderType == RenderType.Html) resultString = Regex.Replace(template, pattern, string.Empty);
            return resultString;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private static CultureInfo GetCultureInfo(LanguageEnum languageEnum)
    {
        return languageEnum switch
        {
            LanguageEnum.Portuguse => new CultureInfo("pt-BR"),
            LanguageEnum.English => new CultureInfo("en-US"),
            LanguageEnum.Spaninsh => new CultureInfo("es-ES"),
            _ => new CultureInfo("pt-BR")
        };
    }
    
    private static ViewProduct GetViewProduct(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        return new ViewProduct
        {
            Preco = resourceManager.GetString("Preço", cultureInfo),
            Categoria = resourceManager.GetString("Categoria", cultureInfo),
            Desconto = resourceManager.GetString("Desconto" , cultureInfo),
            Nome = resourceManager.GetString("Nome" , cultureInfo),
            ProdutoId = resourceManager.GetString("ProdutoID",cultureInfo),
            RelatorioContas = resourceManager.GetString("Relatorio_Contas", cultureInfo)
        };
    }
}