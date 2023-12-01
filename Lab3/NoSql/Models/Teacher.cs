using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NoSql.Models
{
    public class Teacher
    {
        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ID { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
