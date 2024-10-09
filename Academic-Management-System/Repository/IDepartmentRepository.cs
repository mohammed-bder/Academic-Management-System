using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        public List<Department> GetByName(string Name);
    }
}
