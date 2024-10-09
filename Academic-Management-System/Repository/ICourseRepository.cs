using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        public List<Course> GetByName(string Name);
    }
}
