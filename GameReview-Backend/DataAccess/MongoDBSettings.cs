namespace GameReview_Backend.DataAccess
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ProjectCollection { get; set; } = string.Empty;
    }
}
