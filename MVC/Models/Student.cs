using System.ComponentModel.DataAnnotations;

namespace MVC_StudentsRating.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string Fullname { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; } 
    }
}
