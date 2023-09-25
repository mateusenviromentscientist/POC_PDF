using POC_PDF.Models.Enum;

namespace POC_PDF.Dtos;

public class CreateProductDto
{
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductDiscount { get; set; }
    public ProductCategoryEnum ProductCategory { get; set; }
}