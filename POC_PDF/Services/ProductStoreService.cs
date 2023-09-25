using POC_PDF.Dtos;
using POC_PDF.Models;
using POC_PDF.Repositories.Interfaces;
using POC_PDF.Services.Interfaces;

namespace POC_PDF.Services;

public class ProductStoreService : IProductStoreService
{
    public ProductStoreService(IProductStoreRepository productStoreRepository)
    {
        _productStoreRepository = productStoreRepository;
    }

    private readonly IProductStoreRepository _productStoreRepository;

    public async Task<List<Product>> GetRecords() => await _productStoreRepository.GetProducts();

    public async Task<Product> CreateProducts(CreateProductDto productDto)
    {
        await _productStoreRepository.CreateProduct(productDto);
        var result = await _productStoreRepository.GetProducts();
        return result.Last();
    }

}