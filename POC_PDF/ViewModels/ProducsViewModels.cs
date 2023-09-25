using System.Globalization;

namespace POC_PDF.ViewModels;

public class ProducsViewModels
{
    public ProducsViewModels(List<ProductViewModel> productViewModels, ViewProduct viewProduct)
    {
        ProductViewModels = productViewModels;
        ViewProduct = viewProduct;
    }

    public List<ProductViewModel> ProductViewModels { get; set; }
    public ViewProduct ViewProduct { get; set; }
}

public class ViewProduct
{
    public string Categoria { get; set; }
    public string Desconto { get; set; }
    public string Nome { get; set;}
    public string ProdutoId { get; set;}
    public string Preco { get; set; }
    public string RelatorioContas { get; set; }
}