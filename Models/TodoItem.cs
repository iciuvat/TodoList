using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models;

public class TodoItem
{
    //document's primary key
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? name { get; set; }

    public bool isComplete { get; set; }
}