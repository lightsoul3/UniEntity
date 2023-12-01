using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NoSql.Models
{
    public class Rating
    {
        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ID { get; set; } = null!;
        public List<Student> Student { get; set; } = null!;
        public string Month { get; set; } = null!;

    }
}
