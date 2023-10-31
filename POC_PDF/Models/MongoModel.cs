using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using POC_PDF.Models.Enum;

namespace POC_PDF.Models;

public class MongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Template { get; set; } = null!;
    public int? TipoTemplate { get; set; } = null!;
    public bool? isHeader { get; set; } = null!;
}