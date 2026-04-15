using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Entities
{
    public class SinhVien
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("maSV")]
        public string MaSV { get; set; } = string.Empty;

        [BsonElement("hoTen")]
        public string HoTen { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("ngaySinh")]
        public DateTime NgaySinh { get; set; }
    }
}