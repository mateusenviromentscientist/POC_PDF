using POC_PDF.Context;
using POC_PDF.Dtos;
using POC_PDF.Models.Enum;
using POC_PDF.Repositories;
using POC_PDF.Repositories.Interfaces;
using POC_PDF.Services;
using POC_PDF.Services.Interfaces;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IProductStoreRepository, ProductStoreRepository>();
builder.Services.AddScoped<IProductStoreService, ProductStoreService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<ITextTemplate, TextTemplateService>();
builder.Services.AddScoped<IDataTemplateService, DateTemplateService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("Products", async (IProductStoreService service) =>
{
    var result = await service.GetRecords();
    return Results.Ok(result);
}).WithName("GetProductsStore");

app.MapPost("Products", async (CreateProductDto createProductDto,IProductStoreService service) =>
{
    var result = await service.CreateProducts(createProductDto);
    return Results.Created($"CreateProducts/{result.ProductId}", result);
    
}).WithName("CreateProducts");


app.MapGet("ProductsStoreData", async (RenderType renderType,LanguageEnum cultureInfo,IDataTemplateService service) =>
{
    var result = await service.GetTemplate(renderType, cultureInfo);
    return Results.Ok(result);
}).WithName("GetProductsDataStoreTemplate");


app.Run();
