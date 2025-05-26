using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameScoreboard.Models;

public class Score
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }

    public string PlayerName { get; set; } = string.Empty;

    public int Points { get; set; }

    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}