namespace POC_PDF.Context;

public class MongoContext
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string TemplatesCollectionName { get; set; } = null!;
}