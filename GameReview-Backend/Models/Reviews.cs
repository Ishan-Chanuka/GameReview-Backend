using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameReview_Backend.Models
{
    public class Reviews
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int ReviewId { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; } = string.Empty;

        [BsonElement("rating")]
        public int UserRating { get; set; }

        [BsonElement("location")]
        public string Location { get; set; } = string.Empty;

        [BsonElement("review")]
        public string Review { get; set; } = string.Empty;
    }
}
