using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameReview_Backend.Models
{
    public class Reviews
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReviewId { get; set; } = string.Empty;

        [BsonElement("userName")]
        public string UserName { get; set; } = string.Empty;

        [BsonElement("rating")]
        public double UserRating { get; set; }

        [BsonElement("location")]
        public string Location { get; set; } = string.Empty;

        [BsonElement("review")]
        public string Review { get; set; } = string.Empty;

        [BsonElement("deletedFlag")]
        public bool DeletedFlag { get; set; } = false;
    }
}
