using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        public List<Instructor> GetByName(string Name);
    }
}
