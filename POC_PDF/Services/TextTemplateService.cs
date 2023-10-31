using MongoDB.Bson;
using POC_PDF.Dtos;
using POC_PDF.Models;
using POC_PDF.Models.Enum;
using POC_PDF.Repositories.Interfaces;
using POC_PDF.Services.Interfaces;

namespace POC_PDF.Services;

public class TextTemplateService : ITextTemplate
{
    public TextTemplateService(IProductStoreRepository productStoreRepository)
    {
        _productStoreRepository = productStoreRepository;
    }

    private readonly IProductStoreRepository _productStoreRepository;


    public async Task<TemplateModel> RetrieveAsync(int tipoTemplateEnum) =>
        await _productStoreRepository.ObterTemplate(tipoTemplateEnum);

    public async Task<string> GetTemplateMongo(int tipoTemplateEnum, bool isHeader) =>
        await _productStoreRepository.GetTemplateMongo(tipoTemplateEnum, isHeader);
    

    public async Task<int> CreateTemplate(TemplateCreateDto templateCreateDto, string name, TipoTemplateEnum tipoTemplateEnum)
    {
        var template = "";
        try
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
            var filePath = Path.Combine(dir, $"{name}.cshtml");

            await using var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true);
            using StreamReader reader = new StreamReader(sourceStream);
            template = await reader.ReadToEndAsync();
            var dto = new TemplateCreateDto
            {
                Template = template,
                TipoTemplate = TipoTemplateEnum.Store
            };
            var dtoMongo = new MongoModel
            {
                Id = new ObjectId().ToString(),
                Template = template,
                isHeader = false,
                TipoTemplate = (int)TipoTemplateEnum.Store
            }; 
            
            await _productStoreRepository.CrateTemplateMongo(dtoMongo);
           return await _productStoreRepository.CreateTemplate(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}