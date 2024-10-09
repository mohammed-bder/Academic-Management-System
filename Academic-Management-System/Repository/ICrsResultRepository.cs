using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public interface ICrsResultRepository : IRepository<CrsResult>
    {
        public List<CrsResult> GetCrsDetailsByTraineeId(int traineeId);
    }
}
