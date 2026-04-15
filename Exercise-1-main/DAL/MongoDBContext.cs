using MongoDB.Driver;

namespace DAL
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext()
        {
            // Kết nối MongoDB local
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("QuanLySinhVienDB");
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}