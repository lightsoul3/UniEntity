using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NoSql.Models
{
    public class Subject
    {
        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int MaxRating { get; set; }
        public int CurrentRating { get; set; }
        public List<Teacher> Teacher { get; set; } = null!;
    }
}
