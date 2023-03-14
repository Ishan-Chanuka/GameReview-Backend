using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameReview_Backend.Models.ResponseModels
{
    public class GamesResponseModel
    {
        public string GameId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[] ImageUrl { get; set; } = new byte[0];
        public double Rating { get; set; }
        public List<Reviews> Reviews { get; set; } = new List<Reviews>();
        public bool DeletedFlag { get; set; } = false;
    }
}
