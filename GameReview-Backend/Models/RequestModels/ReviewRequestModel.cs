namespace GameReview_Backend.Models.RequestModels
{
    public class ReviewRequestModel
    {
        public string UserName { get; set; } = string.Empty;
        public double UserRating { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Review { get; set; } = string.Empty;
    }
}
