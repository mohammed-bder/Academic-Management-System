using Microsoft.EntityFrameworkCore;
using Task02_MVC.Models;

namespace Task02_MVC.Repository
{
    public class CrsResultRepository : ICrsResultRepository
    {
        CompanyContext Context;
        public CrsResultRepository(CompanyContext _Context)
        {
            Context = _Context; // new CompanyContext();
        }

        public void Add(CrsResult crsResult)
        {
            Context.Add(crsResult);
        }

        public void Update(CrsResult crsResult)
        {
            Context.Update(crsResult);
        }
        public void Delete(CrsResult crsResult)
        {
            Context.Remove(crsResult);
        }
        public List<CrsResult> GetAll()
        {
            return Context.CrsResult.ToList();
        }

        public CrsResult GetById(int id)
        {
            return Context.CrsResult.FirstOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public List<CrsResult> GetCrsDetailsByTraineeId(int traineeId)
        {
            var crsDetailsList = Context.CrsResult
                .Include(c => c.Course)   
                .Include(c => c.Trainee)  
                .Where(c => c.TraineeId == traineeId) 
                .ToList();  
            return crsDetailsList;
        }

    }
}
