using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        CompanyContext Context;
        public DepartmentRepository(CompanyContext _Context)
        {
            Context = _Context; // new CompanyContext();
        }

        public void Add(Department department)
        {
            Context.Add(department);
        }

        public void Update(Department department)
        {
            Context.Update(department);
        }
        public void Delete(Department department)
        {
            Context.Remove(department);
        }
        public List<Department> GetAll()
        {
            return Context.Department.ToList();
        }

        public Department GetById(int id)
        {
            return Context.Department.FirstOrDefault(x => x.Id == id);
        }
        public List<Department> GetByName(string Name)
        {
            return Context.Department.Where(i => i.Name.Contains(Name)).ToList();
        }
        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
