namespace GameReview_Backend.Models.RequestModels
{
    public class GameRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
        public double Rating { get; set; }
    }
}
