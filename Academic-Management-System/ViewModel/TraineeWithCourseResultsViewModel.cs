namespace Task02_MVC.ViewModel
{
    public class TraineeWithCourseResultsViewModel
    {
        // Trainee's personal details
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ImgURL { get; set; }
        public int? Grade { get; set; }
        public int DepartmentId { get; set; }

        // List of course results
        public List<TraineeCrsViewModel>? CourseResults { get; set; }
    }

    public class TraineeCrsViewModel
    {
        public string? CourseName { get; set; }
        public int Degree { get; set; }
        public string? State { get; set; }
    }
}
