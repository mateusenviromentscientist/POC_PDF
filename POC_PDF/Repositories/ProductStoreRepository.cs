using Dapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using POC_PDF.Context;
using POC_PDF.Dtos;
using POC_PDF.Models;
using POC_PDF.Repositories.Interfaces;
using POC_PDF.Repositories.Queries;

namespace POC_PDF.Repositories;

public class ProductStoreRepository : IProductStoreRepository
{
    private readonly DapperContext _dapperContext;
    private readonly IMongoCollection<MongoModel> _mongoContext;
    
    public ProductStoreRepository(DapperContext dapperContext, IOptions<MongoContext> mongoDatabaseSettings)
    {
        _dapperContext = dapperContext;
        var mongoClient = new MongoClient(mongoDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);
        _mongoContext = mongoDatabase.GetCollection<MongoModel>(mongoDatabaseSettings.Value.TemplatesCollectionName);
    }


    public async Task<List<Product>> GetProducts()
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            var model = await connection.QueryAsync<Product>(ProductStoreQueries.GetRecords(), commandTimeout: 0);
            if (model.Any()) return model.ToList();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return new List<Product>();
    }

    public async Task<int> CreateProduct(CreateProductDto productDto)
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            var model = await connection.ExecuteAsync(ProductStoreQueries.InsertRecords(productDto), commandTimeout: 0);
            return model;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }

    public async Task<Product> GetById(int id)
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            var model = await connection.QueryFirstAsync<Product>(ProductStoreQueries.GetRecordsById(id), commandTimeout: 0);
            return model;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    public async Task<int> CreateTemplate(TemplateCreateDto templateCreateDto)
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            var model = await connection.ExecuteAsync(ProductStoreQueries.InsertTemplates(templateCreateDto), commandTimeout: 0);
            return model;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task CrateTemplateMongo(MongoModel templateCreateDto)
    {
        try
        {
            await _mongoContext.InsertOneAsync(templateCreateDto);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> GetTemplateMongo(int tipoTemplateEnum, bool isHeader)
    {
        try
        {
            var model = await _mongoContext.Find(x => x.TipoTemplate == tipoTemplateEnum && x.isHeader == isHeader).FirstOrDefaultAsync();
            return model.Template;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<TemplateModel> ObterTemplate(int tipoTemplate)
    {
        try
        {
            using var connection = _dapperContext.CreateConnection();
            var model = await connection.QueryFirstAsync<TemplateModel>(ProductStoreQueries.GetTemplate(tipoTemplate), commandTimeout: 0);
            return model;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}