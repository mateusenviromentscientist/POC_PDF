using POC_PDF.Dtos;
using POC_PDF.Models;

namespace POC_PDF.Services.Interfaces;

public interface IProductStoreService
{
    Task<List<Product>> GetRecords();
    Task<Product> CreateProducts(CreateProductDto productDto);
}