using Microsoft.AspNetCore.Mvc;
using Task02_MVC.ViewModel;
using Task02_MVC.Models;
using Task02_MVC.Repository;

namespace Task02_MVC.Controllers
{
    public class InstructorController : Controller
    {
        //CompanyContext Context = new CompanyContext();
        IInstructorRepository InstructorRepository;
        IDepartmentRepository DepartmentRepository;
        ICourseRepository CourseRepository;
        public InstructorController(IInstructorRepository InstRepo , IDepartmentRepository DeptRepo , ICourseRepository CrsRepo ) // inject
        {
            InstructorRepository = InstRepo; //new InstructorRepository();
            DepartmentRepository = DeptRepo; //new DepartmentRepository();
            CourseRepository     = CrsRepo;  //new CourseRepository();
        }

        // ************** Get All Instructor **************
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Instructor> instructors = InstructorRepository.GetAll();
            return View("GetAll", instructors);
        }

        // ************** Get Details of Instructor **************
        [HttpGet]
        public IActionResult GetDetails(int id)
        {
            Instructor? instructor = InstructorRepository.GetById(id);
            if(instructor != null)
            {
                return View("GetDetails", instructor);
            }
            return Content("This id Not Found!!");
            
        }

        // ************** Add Instructor **************
        public IActionResult Add()
        {
            List<Department> DepartmentList = DepartmentRepository.GetAll();
            List<Course> CourseList = CourseRepository.GetAll();
            InstWithCourseListAndDeptList instView = new InstWithCourseListAndDeptList();
            instView.DeptList = DepartmentList;
            instView.CourseList = CourseList;

            return View("Add" , instView);
        }

        // ************** Save Add Instructor info **************
        [HttpPost]
        public IActionResult SaveAdd(Instructor instructor, IFormFile ImageFile)
        {
            InstWithCourseListAndDeptList instViewModel = new InstWithCourseListAndDeptList();
            instViewModel.Name = instructor.Name;
            instViewModel.Salary = instructor.Salary;
            instViewModel.Address = instructor.Address;
            instViewModel.DepartmentId = instructor.DepartmentId;
            instViewModel.CourseId = instructor.CourseId?? 0;

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

                instructor.ImgURL = fileName;
            }
            instViewModel.ImgURL = instructor.ImgURL;

            if (instructor.Name != null)
            {
                InstructorRepository.Add(instructor);
                InstructorRepository.Save();
                return RedirectToAction("GetAll");
            }

            instViewModel.DeptList = DepartmentRepository.GetAll();
            instViewModel.CourseList = CourseRepository.GetAll();

            return View("Add", instViewModel);
        }


        // ************** Edit Instructor  info **************
        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            Instructor? instModel = InstructorRepository.GetById(id);
            if (instModel == null)
            {
                return NotFound(); 
            }


            List<Department> DepartmentList = DepartmentRepository.GetAll();
            List<Course> CourseList = CourseRepository.GetAll();

            
            InstWithCourseListAndDeptList instViewModel = new InstWithCourseListAndDeptList
            {
                Id = instModel.Id,
                Name = instModel.Name,
                ImgURL = instModel.ImgURL,
                Salary = instModel.Salary,
                Address = instModel.Address,
                DepartmentId = instModel.DepartmentId,
                CourseId = instModel.CourseId ?? 0,
                DeptList = DepartmentList,
                CourseList = CourseList
            };

            
            return View("Edit", instViewModel);
        }

        // ************** Save Edit Instructor info **************
        [HttpPost]
        public IActionResult SaveEdit(Instructor instfromrequest, int id, IFormFile? ImgUpload)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", instfromrequest);
            }

            if (instfromrequest.Name != null && instfromrequest.Address != null)
            {
                // Fetch the existing instructor from the database
                Instructor? instFromDB = InstructorRepository.GetById(id);
                if (instFromDB == null)
                {
                    return NotFound("Instructor with the given ID not found.");
                }

                // Update the instructor details
                instFromDB.Name = instfromrequest.Name;
                instFromDB.Salary = instfromrequest.Salary;
                instFromDB.Address = instfromrequest.Address;
                instFromDB.CourseId = instfromrequest.CourseId;
                instFromDB.DepartmentId = instfromrequest.DepartmentId;

                // Handle the image upload
                if (ImgUpload != null && ImgUpload.Length > 0)
                {
                    var fileName = Path.GetFileName(ImgUpload.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImgUpload.CopyTo(stream);
                    }

                    instFromDB.ImgURL = fileName;
                }

                InstructorRepository.Save();
                return RedirectToAction("GetAll");
            }

            return View("Edit", instfromrequest);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Instructor? inst = InstructorRepository.GetById(id);
            if (inst != null)
            {   
                InstructorRepository.Delete(inst);
                InstructorRepository.Save();
                return RedirectToAction("GetAll");
            }
            else
            {
                return Content("This ID was not found.");
            }
        }

        public IActionResult Search(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return BadRequest("Please enter a valid name to search.");
            }

            List<Instructor>? inst = InstructorRepository.GetByName(Name);  
            if (inst != null)
            {
                ViewBag.Name = Name;
                return View("GetAll" , inst);
            }
            else
            {
                return NotFound("Instructor with the given name not found.");
            }
        }


    }
}
