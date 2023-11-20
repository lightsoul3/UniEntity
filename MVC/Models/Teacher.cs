using System.ComponentModel.DataAnnotations;

namespace MVC_StudentsRating.Models
{
    public class Teacher
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = null!;
    }
}
