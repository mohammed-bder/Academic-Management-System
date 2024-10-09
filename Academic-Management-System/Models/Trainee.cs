using System.ComponentModel.DataAnnotations.Schema;

namespace Task02_MVC.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgURL { get; set; }

        public string? Address { get; set; }
        public int Grade { get; set; }

        public List<CrsResult> CrsResults { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
