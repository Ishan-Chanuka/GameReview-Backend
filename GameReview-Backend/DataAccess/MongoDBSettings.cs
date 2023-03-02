namespace GameReview_Backend.DataAccess
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string UserCollection { get; set; } = string.Empty;
        public string GameCollection { get; set; } = string.Empty;
        public string ReviewCollection { get; set; } = string.Empty;
    }
}
