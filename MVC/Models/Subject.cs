using System.ComponentModel.DataAnnotations;

namespace MVC_StudentsRating.Models
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = null!; 
        public int Rating { get; set; }
    }
}
