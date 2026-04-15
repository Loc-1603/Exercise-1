using Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DAL
{
    public class MonHocRepository : IRepository<MonHoc>
    {
        private readonly IMongoCollection<MonHoc> _collection;

        public MonHocRepository()
        {
            var context = new MongoDBContext();
            _collection = context.GetCollection<MonHoc>("monhoc");
        }

        public List<MonHoc> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public MonHoc GetById(string id)
        {
            return _collection.Find(mh => mh.Id == id).FirstOrDefault();
        }

        public MonHoc GetByMaMH(string maMH)
        {
            return _collection.Find(mh => mh.MaMH == maMH).FirstOrDefault();
        }

        public void Insert(MonHoc entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(string id, MonHoc entity)
        {
            _collection.ReplaceOne(mh => mh.Id == id, entity);
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(mh => mh.Id == id);
        }
    }
}