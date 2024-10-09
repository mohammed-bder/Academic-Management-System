using Task02_MVC.Models;

namespace Task02_MVC.ViewModel
{
    public class CourseWithDeptList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int CourseHours { get; set; }
        public int DepartmentId { get; set; }
        public List<Department>? DeptList { get; set; }
    }
}
