namespace GameReview_Backend.Models.ResponseModels
{
    public class LoginResponseModel
    {
        public string UserRole { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
