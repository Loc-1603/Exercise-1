using Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DAL
{
    public class DangKyRepository : IRepository<DangKy>
    {
        private readonly IMongoCollection<DangKy> _collection;

        public DangKyRepository()
        {
            var context = new MongoDBContext();
            _collection = context.GetCollection<DangKy>("dangky");
        }

        public List<DangKy> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public DangKy GetById(string id)
        {
            return _collection.Find(dk => dk.Id == id).FirstOrDefault();
        }

        public List<DangKy> GetBySinhVien(string maSV)
        {
            return _collection.Find(dk => dk.MaSV == maSV).ToList();
        }

        public void Insert(DangKy entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(string id, DangKy entity)
        {
            _collection.ReplaceOne(dk => dk.Id == id, entity);
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(dk => dk.Id == id);
        }
    }
}