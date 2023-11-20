using System.ComponentModel.DataAnnotations;

namespace MVC_StudentsRating.Models
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; } = null!;
        public int SubjectID { get; set; }
        public Subject Subject { get; set; } = null!;
        public string Month { get; set; } = null!;
        public decimal CurrentRating { get; set; }
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set;} = null!;
    }
}
