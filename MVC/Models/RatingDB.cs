using System.ComponentModel.DataAnnotations;

namespace MVC_StudentsRating.Models
{
    public class RatingDB
    {

        [Key]
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public string Month { get; set; } = null!;
        public decimal CurrentRating { get; set; }
        public int TeacherID { get; set; }
    }
}
