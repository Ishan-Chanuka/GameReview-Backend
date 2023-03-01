using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameReview_Backend.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int UserId { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; } = string.Empty;

        [BsonElement("email")]
        public string UserEmail { get; set; } = string.Empty;

        [BsonElement("role")]
        public string UserRole { get; set; } = string.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;
    }
}
