using Task02_MVC.Models;

namespace Task02_MVC.ViewModel
{
    public class InstWithCourseListAndDeptList
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImgURL { get; set; }
        public int Salary { get; set; }
        public string? Address { get; set; }
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
        public List<Course>? CourseList { get; set; }
        public List<Department>? DeptList { get; set; }

    }
}
