using System.ComponentModel.DataAnnotations.Schema;

namespace Task02_MVC.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int CourseHours { get; set; }

        public List<Instructor> Instructors { get; set; }
        public List<CrsResult> CrsResults { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
