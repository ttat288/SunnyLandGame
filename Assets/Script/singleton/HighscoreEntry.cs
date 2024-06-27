using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Assets.Script.singleton
{
    public class HighscoreEntry
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string playerId { get; set; }

        [BsonElement("playerScore"), BsonRepresentation(BsonType.Int32)]
        public int playerScore { get; set; }
    }
}
