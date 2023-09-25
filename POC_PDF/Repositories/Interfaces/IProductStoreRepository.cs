using POC_PDF.Dtos;
using POC_PDF.Models;
using POC_PDF.Services;

namespace POC_PDF.Repositories.Interfaces;

public interface IProductStoreRepository
{
    Task<List<Product>> GetProducts();
    Task<int> CreateProduct(CreateProductDto productDto);
    Task<Product> GetById(int id);
}