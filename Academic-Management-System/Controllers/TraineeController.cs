using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task02_MVC.Models;
using Task02_MVC.Repository;
using Task02_MVC.ViewModel;

namespace Task02_MVC.Controllers
{
    public class TraineeController : Controller
    {
        // CompanyContext context = new CompanyContext();
        ITraineeRepository TraineeRepository;
        IDepartmentRepository DepartmentRepository;
        public TraineeController(ITraineeRepository TraineeRepo , IDepartmentRepository DeptRepo) // inject
        {
            TraineeRepository = TraineeRepo;   
            DepartmentRepository = DeptRepo;
        }
        /************* Get All Trainees *************/
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Trainee> trainees = TraineeRepository.GetAll();
            return View("GetAll" , trainees);  
        }

        /************* Search Trainee by name *************/
        public IActionResult Search(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return BadRequest("Please enter a valid name to search.");
            }

            List<Trainee>? trainees = TraineeRepository.GetByName(Name);  
            if(trainees != null)
            {
                ViewBag.Name = Name;
                return View("GetAll" , trainees);
            }
            else
            {
                return NotFound("Trainee with the given name not found.");
            }
        }

        /************* Add Trainee *************/
        public IActionResult Add()
        {
            List<Department> department = DepartmentRepository.GetAll();
            TraineeWithDeptList traineeViewModel = new TraineeWithDeptList();
            traineeViewModel.DeptList = department;
            return View("Add" , traineeViewModel);
        }

        /************* Save Add Trainee *************/
        [HttpPost]
        public IActionResult SaveAdd(Trainee TraineeFromRequest , IFormFile ImageFile)
        {
            TraineeWithDeptList TraineeViewModel = new TraineeWithDeptList();
            TraineeViewModel.Name = TraineeFromRequest.Name;
            TraineeViewModel.Address = TraineeFromRequest.Address;
            TraineeViewModel.Grade = TraineeFromRequest.Grade;
            TraineeViewModel.DepartmentId = TraineeFromRequest.DepartmentId;

            // Handle Image Upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                // Save the file to the specified directory
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                TraineeFromRequest.ImgURL = fileName;
            }
            TraineeViewModel.ImgURL = TraineeFromRequest.ImgURL;

            if (TraineeViewModel != null)
            {
                TraineeRepository.Add(TraineeFromRequest);
                TraineeRepository.Save();
                return RedirectToAction("GetAll");
            }

            TraineeViewModel.DeptList = DepartmentRepository.GetAll();
            return View("Add" , TraineeViewModel);

        }

        /************* Get Details of Trainee *************/
        public IActionResult GetDetails(int id)
        {
            Trainee? trainee = TraineeRepository.GetById(id);
            if (trainee != null)
            {
                return View("GetDetails" , trainee);
            }
            else
            {
                return Content("This Id not Found");
            }
        }

        /************* Edit Trainee *************/
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Trainee? trainee = TraineeRepository.GetById(id);
            List<Department> DepartmentList = DepartmentRepository.GetAll();
            if (trainee != null)
            {
                TraineeWithDeptList TraineeViewModel = new TraineeWithDeptList
                {
                    Id = trainee.Id,
                    Name = trainee.Name,
                    ImgURL = trainee.ImgURL,  
                    Address = trainee.Address,
                    Grade = trainee.Grade,
                    DepartmentId = trainee.DepartmentId,
                    DeptList = DepartmentList
                };
                return View("Edit", TraineeViewModel);
            }
            else
            {
                return Content("This Id not found");
            }
        }

        /************* Save Edit Trainee *************/
        [HttpPost]
        public ActionResult SaveEdit(Trainee TraineeFromReq, int id, IFormFile ImgUpload)
        {
            if (TraineeFromReq.Name != null && TraineeFromReq.Address != null)
            {
                // Fetch the existing trainee from the database
                Trainee? TraineeFromDB = TraineeRepository.GetById(id);
                if (TraineeFromDB == null)
                {
                    return NotFound("Trainee with the given ID not found."); // Corrected message
                }

                // Update the trainee details
                TraineeFromDB.Name = TraineeFromReq.Name;
                TraineeFromDB.Address = TraineeFromReq.Address;
                TraineeFromDB.Grade = TraineeFromReq.Grade;
                TraineeFromDB.DepartmentId = TraineeFromReq.DepartmentId;

                // Handle the image upload
                if (ImgUpload != null && ImgUpload.Length > 0)
                {
                    var fileName = Path.GetFileName(ImgUpload.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImgUpload.CopyTo(stream);
                    }

                    TraineeFromDB.ImgURL = fileName; // Update image URL in the database
                }

                TraineeRepository.Save(); // Save changes to the database
                return RedirectToAction("GetAll");
            }

            return View("Edit", TraineeFromReq); // Return the correct view model
        }


        /************* Delete Trainee *************/
        public IActionResult Delete(int id)
        {
            Trainee? trainee = TraineeRepository.GetById(id);
            if (trainee != null)
            {
                TraineeRepository.Delete(trainee);
                TraineeRepository.Save();
                return RedirectToAction("GetAll");
            }
            else
            {
                return Content("Error");
            }
        }





    }
}
