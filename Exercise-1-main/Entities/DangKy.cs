using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Entities
{
    public class DangKy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("maSV")]
        public string MaSV { get; set; } = string.Empty;

        [BsonElement("maMH")]
        public string MaMH { get; set; } = string.Empty;

        [BsonElement("ngayDangKy")]
        public DateTime NgayDangKy { get; set; }

        [BsonElement("diem")]
        public double? Diem { get; set; }
    }
}