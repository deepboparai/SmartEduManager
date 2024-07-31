using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SimpleDeveloper.Models;
using SimpleDeveloperCore.IServices;
using SimpleDeveloperCore.Models;
using SimpleDeveloperCore.ViewModels;
using SimpleDeveloperServices.Services;
using System.Reflection;
using System.Runtime.InteropServices.ObjectiveC;

namespace SimpleDeveloper.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<StudentController> _logger;
        private readonly ErrorLogger _errorLogger;
        public StudentController(IStudentService studentService, ITeacherService teacherService, IWebHostEnvironment hostingEnvironment, ILogger<StudentController> logger, ErrorLogger errorLogger)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _errorLogger = errorLogger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var students = await _studentService.GetAllStudentAsyc();
                return View(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting GetAllStudentAsyc.");
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier,
                    Message = ex.Message
                };
                _errorLogger.LogError(ex);
                return View("Error", errorViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentSubjectsAndTeachers()
        {
            try
            {
                var subjectsWithTeachers = await _studentService.StudentSubjectsandTeachersAsync();
                return View("StudentSubjectsandTeachers", subjectsWithTeachers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting StudentSubjectsandTeachers.");

                var errorViewModel = new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier,
                    Message = ex.Message
                };
                _errorLogger.LogError(ex);
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> AddStudent()
        {
            try
            {
                var subjects = await _teacherService.GetSubjectListAsync();
                var subjectList = subjects.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();

                Student teacher = new Student
                {
                    SubjectList = subjectList
                };

                return View(teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting students.");

                var errorViewModel = new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier,
                    Message = ex.Message
                };
                _errorLogger.LogError(ex);
                return View("Error", errorViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student model, IFormFile? photo)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (photo != null && photo.Length > 0)
                    {
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", photo.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await photo.CopyToAsync(stream);
                        }
                        model.Photo = photo.FileName;
                    }
                    else
                    {
                        model.Photo = null; // Ensure Photo field is set to null if no file is uploaded
                    }

                    await _studentService.AddNewStudentAsync(model);

                    return RedirectToAction("Index", "Home");
                }



                // If model state is not valid, reload subjects list
                var subjects = await _teacherService.GetSubjectListAsync();
                model.SubjectList = subjects.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the student.");
               
                
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier,
                    Message = ex.Message
                };
                _errorLogger.LogError(ex);
                return View("Error", errorViewModel);
            }
        }
    }
}
