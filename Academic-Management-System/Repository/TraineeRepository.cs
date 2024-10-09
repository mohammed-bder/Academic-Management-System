using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public class TraineeRepository : ITraineeRepository
    {
        CompanyContext Context;
        public TraineeRepository(CompanyContext _Context)
        {
            Context = _Context; // new CompanyContext();
        }

        public void Add(Trainee trainee)
        {
            Context.Add(trainee);
        }

        public void Update(Trainee trainee)
        {
            Context.Update(trainee);
        }
        public void Delete(Trainee trainee)
        {
            Context.Remove(trainee);
        }
        public List<Trainee> GetAll()
        {
            return Context.Trainee.ToList();
        }

        public Trainee GetById(int id)
        {
            return Context.Trainee.FirstOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public List<Trainee> GetByName(string Name)
        {
            return Context.Trainee.Where(i => i.Name.Contains(Name)).ToList();
        }
    }
}
