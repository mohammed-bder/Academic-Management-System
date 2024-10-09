using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public interface ITraineeRepository : IRepository<Trainee>
    {
        public List<Trainee> GetByName(string Name);
    }
}
