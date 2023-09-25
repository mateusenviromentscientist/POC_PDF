using POC_PDF.Models.Enum;

namespace POC_PDF.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductPrice { get; set; }
    public int ProductDiscount { get; set; }
    public ProductCategoryEnum ProductCategory { get; set; }
}