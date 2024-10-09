using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public class CourseRepository : ICourseRepository
    {
        CompanyContext Context;
        public CourseRepository(CompanyContext _Context)
        {
            Context = _Context; // new CompanyContext();
        }

        public void Add(Course course)
        {
            Context.Add(course);
        }

        public void Update(Course course)
        {
            Context.Update(course);
        }
        public void Delete(Course course)
        {
            Context.Remove(course);
        }
        public List<Course> GetAll()
        {
            return Context.Course.ToList();
        }

        public Course GetById(int id)
        {
            return Context.Course.FirstOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public List<Course> GetByName(string Name)
        {
            return Context.Course.Where(i => i.Name.Contains(Name)).ToList();
        }
    }
}
