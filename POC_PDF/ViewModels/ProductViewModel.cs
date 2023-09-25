using System.Globalization;
using POC_PDF.Models;
using POC_PDF.Models.Enum;

namespace POC_PDF.ViewModels;

public class ProductViewModel
{
    public string Category { get; set; }
    public List<Product> Products { get; set; }
    public CultureInfo CultureInfo { get; set; }

    public List<ProductViewModel> GetListProducts(List<Product> products, CultureInfo cultureInfo)
    {
        var listProducts = new List<ProductViewModel>();
        
        var sellersProduct = products.Select(x => x).Where(z => z.ProductCategory == ProductCategoryEnum.Seller).ToList();
        
        var formulaProduct = products.Select(x => x).Where(z => z.ProductCategory == ProductCategoryEnum.Formula).ToList();

        if (sellersProduct.Any())
        {
            var model = new ProductViewModel
            {
                Category = sellersProduct.FirstOrDefault().ProductCategory.ToString(),
                Products = sellersProduct,
                CultureInfo = cultureInfo
            };
            listProducts.Add(model);
        }

        if (!formulaProduct.Any()) return listProducts;
        {
            var model = new ProductViewModel
            {
                Category = formulaProduct.FirstOrDefault().ProductCategory.ToString(),
                Products = formulaProduct,
                CultureInfo = cultureInfo
            };
            listProducts.Add(model);
        }

        return listProducts;
    }
}