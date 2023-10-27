using POC_PDF.Dtos;
using POC_PDF.Models.Enum;
using SqlKata;
using SqlKata.Compilers;

namespace POC_PDF.Repositories.Queries;

public static class ProductStoreQueries
{
    public static string GetRecords()
    {
        var compiler = new SqlServerCompiler();
        
        var query = new Query("dbo.BIDAISTORE AS BD")
            .Select("BD.ProductId",
                "BD.ProductName",
                "BD.ProductPrice",
                "BD.ProductDiscount",
                "BD.ProductCategory");
        
        return compiler.Compile(query).ToString();
    }

    public static string InsertRecords(CreateProductDto productDto)
    {
        var compiler = new SqlServerCompiler();

        var query = new Query("dbo.BIDAISTORE")
            .AsInsert(new
            {
                productDto.ProductName,
                productDto.ProductPrice,
                productDto.ProductDiscount,
                productDto.ProductCategory
            });
        
        return compiler.Compile(query).ToString();
    }
    
    public static string InsertTemplates(TemplateCreateDto templateCreateDto)
    {
        var compiler = new SqlServerCompiler();

        var query = new Query("dbo.Templates")
            .AsInsert(new
            {
                templateCreateDto.Template,
                templateCreateDto.TipoTemplate
            });
        
        return compiler.Compile(query).ToString();
    }
    
    public static string GetTemplate(int tipoTempate)
    {
        var compiler = new SqlServerCompiler();

       var query = new Query("dbo.Templates AS T")
            .Select("T.Template AS Template").Where("T.TipoTemplate",tipoTempate);
        
        return compiler.Compile(query).ToString();
    }
    
    public static string GetRecordsById(int id)
    {
        var compiler = new SqlServerCompiler();
        
        var query = new Query("dbo.BIDAISTORE AS BD")
            .Select("BD.ProductId",
                "BD.ProductName",
                "BD.ProductPrice",
                "BD.ProductDiscount",
                "BD.ProductCategory")
            .Where("BD.ProductId",id);
        
        return compiler.Compile(query).ToString();
    }
}