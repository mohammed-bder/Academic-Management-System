using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        CompanyContext Context;
        public InstructorRepository(CompanyContext _Context)
        {
            Context = _Context; // new CompanyContext();
        }

        public void Add(Instructor instructor)
        {
            Context.Add(instructor);
        }

        public void Update(Instructor instructor)
        {
            Context.Update(instructor);
        }
        public void Delete(Instructor instructor)
        {
            Context.Remove(instructor);
        }
        public List<Instructor> GetAll()
        {
            return Context.Instructor.ToList();
        }

        public Instructor GetById(int id)
        {
           return Context.Instructor.FirstOrDefault(x => x.Id == id); 
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public List<Instructor> GetByName(string Name)
        {
           return Context.Instructor.Where(i => i.Name.Contains(Name)).ToList();
        }

    }
}
