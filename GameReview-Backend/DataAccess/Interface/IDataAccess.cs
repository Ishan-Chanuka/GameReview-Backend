using MongoDB.Driver;

namespace GameReview_Backend.DataAccess.Interface
{
    public interface IDataAccess
    {
        IMongoCollection<T> ConnectToMongo<T>(in string collection);
    }
}
