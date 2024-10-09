using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Task02_MVC.Migrations;
using Task02_MVC.Models;
using Task02_MVC.Repository;
using Task02_MVC.ViewModel;

namespace Task02_MVC.Controllers
{
    public class CourseController : Controller
    {
        // CompanyContext Context = new CompanyContext();
        ICourseRepository CourseRepository;
        IDepartmentRepository DepartmentRepository;
        public CourseController(ICourseRepository CrsRepo , IDepartmentRepository DeptRepo)
        {
            CourseRepository     = CrsRepo;         // new CourseRepository();
            DepartmentRepository = DeptRepo;    // new DepartmentRepository();
        }

        /******************* Get All Courses *******************/
        public IActionResult GetAll()
        {
            List<Course> courses = CourseRepository.GetAll();
            return View("GetAll", courses);
        }

        /******************* Search Course by Name *******************/
        public IActionResult Search(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return BadRequest("Please enter a valid name to search.");
            }
            List<Course>? courses = CourseRepository.GetByName(Name);
            if (courses != null)
            {
                ViewBag.Name = Name;
                return View("GetAll", courses);
            }
            else
            {
                return NotFound("Instructor with the given name not found.");
            }
        }

        /******************* Add Course *******************/
        public ActionResult Add()
        {
            List<Department> departments = DepartmentRepository.GetAll();

            CourseWithDeptList CourseView = new CourseWithDeptList();
            CourseView.DeptList = departments;
            return View("Add", CourseView);
        }
        [HttpPost]
        public ActionResult SaveAdd(Course CourseFromRequest)
        {
            CourseWithDeptList CourseViewModel = new CourseWithDeptList();
            CourseViewModel.Name = CourseFromRequest.Name;
            CourseViewModel.Degree = CourseFromRequest.Degree;
            CourseViewModel.MinDegree = CourseFromRequest.MinDegree;
            CourseViewModel.CourseHours = CourseFromRequest.CourseHours;
            CourseViewModel.DepartmentId = CourseFromRequest.DepartmentId;

            if (CourseViewModel != null)
            {
                CourseRepository.Add(CourseFromRequest);
                CourseRepository.Save();
                return RedirectToAction("GetAll");
            }
            CourseViewModel.DeptList = DepartmentRepository.GetAll();
            return View("Add", CourseViewModel);
        }


        /******************* Get Details *******************/
        public ActionResult GetDetails(int id)
        {
            Course? course = CourseRepository.GetById(id);
            if(course != null)
            {
                return View("GetDetails" , course);
            }
            else
            {
                return Content("This Id not found");
            }

        }

        
        /******************* Edit Course *******************/
        public IActionResult Edit(int id)
        {
            Course? course = CourseRepository.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            List<Department> DepartmentList = DepartmentRepository.GetAll();

            // Prepare the CourseWithDeptList view model
            CourseWithDeptList CourseModel = new CourseWithDeptList
            {
                Id = course.Id,
                Name = course.Name,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                CourseHours = course.CourseHours,
                DepartmentId = course.DepartmentId,
                DeptList = DepartmentList
            };

            return View("Edit", CourseModel);
        }

        /******************* Save Edit Course *******************/
        [HttpPost]
        public ActionResult SaveEdit(CourseWithDeptList CourseFromReq, int id)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate the department list in case of validation errors
                CourseFromReq.DeptList = DepartmentRepository.GetAll();
                return View("Edit", CourseFromReq);
            }

            if (CourseFromReq.Name != null)
            {
                // Fetch the existing course from the database
                Course? CourseFromDB = CourseRepository.GetById(id);
                if (CourseFromDB == null)
                {
                    return NotFound("Course with the given ID not found.");
                }

                // Update the course details
                CourseFromDB.Name = CourseFromReq.Name;
                CourseFromDB.Degree = CourseFromReq.Degree;
                CourseFromDB.MinDegree = CourseFromReq.MinDegree;
                CourseFromDB.CourseHours = CourseFromReq.CourseHours;
                CourseFromDB.DepartmentId = CourseFromReq.DepartmentId;

                CourseRepository.Save();
                return RedirectToAction("GetAll");
            }

            // In case CourseFromReq.Name is null
            CourseFromReq.DeptList = DepartmentRepository.GetAll();
            return View("Edit", CourseFromReq);
        }

        [HttpPost]
        /******************* Delete Course *******************/
        public ActionResult Delete(int id)
        {
            Course? course = CourseRepository.GetById(id);
            if(course != null)
            {
                CourseRepository.Delete(course);
                CourseRepository.Save();
                return RedirectToAction("GetAll");
            }
            else
            {
                return Content("This Id Not Found");
            }
        }


    }
}
