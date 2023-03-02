using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameReview_Backend.Models
{
    public class Games
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GameId { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [BsonElement("rating")]
        public int Rating { get; set; }

        [BsonElement("reviews")]
        public List<Reviews> Reviews { get; set; } = new List<Reviews>();
    }
}
