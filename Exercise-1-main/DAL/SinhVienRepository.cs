using Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DAL
{
    public class SinhVienRepository : IRepository<SinhVien>
    {
        private readonly IMongoCollection<SinhVien> _collection;

        public SinhVienRepository()
        {
            var context = new MongoDBContext();
            _collection = context.GetCollection<SinhVien>("sinhvien");
        }

        public List<SinhVien> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public SinhVien GetById(string id)
        {
            return _collection.Find(sv => sv.Id == id).FirstOrDefault();
        }

        public SinhVien GetByMaSV(string maSV)
        {
            return _collection.Find(sv => sv.MaSV == maSV).FirstOrDefault();
        }

        public void Insert(SinhVien entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(string id, SinhVien entity)
        {
            _collection.ReplaceOne(sv => sv.Id == id, entity);
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(sv => sv.Id == id);
        }
    }
}