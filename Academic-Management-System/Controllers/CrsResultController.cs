using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using Task02_MVC.Models;
using Task02_MVC.Repository;
using Task02_MVC.ViewModel;

namespace Task02_MVC.Controllers
{
    public class CrsResultController : Controller
    {
        // CompanyContext context = new CompanyContext();
        ITraineeRepository TraineeRepository;
        ICrsResultRepository CrsResultRepository;

        public CrsResultController(ITraineeRepository TraineeRepo , ICrsResultRepository CrsResultRepo)
        {
            TraineeRepository = TraineeRepo;
            CrsResultRepository = CrsResultRepo;

        }
        public IActionResult ShowAllResults(int traineeId)
        {
            // Fetch trainee details
            var trainee = TraineeRepository.GetById(traineeId);
            if (trainee == null)
            {
                return NotFound("Trainee not found");
            }

            // Fetch the course results for the trainee
            var crsDetailsList = CrsResultRepository.GetCrsDetailsByTraineeId(traineeId);
                

            if (!crsDetailsList.Any())
                return Content("No course results found for this trainee.");

            // Create the view model
            var model = new TraineeWithCourseResultsViewModel
            {
                Name = trainee.Name,
                Address = trainee.Address,
                ImgURL = trainee.ImgURL,
                Grade = trainee.Grade,
                DepartmentId = trainee.DepartmentId,
                CourseResults = crsDetailsList.Select(c => new TraineeCrsViewModel
                {
                    CourseName = c.Course.Name,
                    Degree = c.Degree,
                    State = c.Degree >= c.Course.MinDegree ? "Success" : "Failed"
                }).ToList()
            };

            return View("TraineeResults", model);
        }

    }
}
