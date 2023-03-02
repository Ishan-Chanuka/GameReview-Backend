namespace GameReview_Backend.Models.ResponseModels
{
    public class LoginResponseModel
    {
        public string UserRole { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
