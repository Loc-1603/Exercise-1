using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class MonHoc
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("maMH")]
        public string MaMH { get; set; } = string.Empty;

        [BsonElement("tenMH")]
        public string TenMH { get; set; } = string.Empty;

        [BsonElement("soTinChi")]
        public int SoTinChi { get; set; }
    }
}