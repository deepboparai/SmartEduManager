using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleDeveloper.Models;
using SimpleDeveloperCore.IServices;
using SimpleDeveloperCore.Models;
using SimpleDeveloperCore.ViewModels;

namespace SimpleDeveloper.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly SchoolContext _schoolContext;
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;
        private readonly ErrorLogger _errorLogger;

        public TeacherController(IWebHostEnvironment hostingEnvironment, SchoolContext schoolContext, ITeacherService teacherService, ILogger<TeacherController> logger, ErrorLogger errorLogger)
        {
            _hostingEnvironment = hostingEnvironment;
            _schoolContext = schoolContext;
            _teacherService = teacherService;
            _logger = logger;
            _errorLogger = errorLogger;
        }

        [HttpGet]
        public async Task<IActionResult> AddTeacher()
        {
            try
            {
                var subjects = await _teacherService.GetSubjectListAsync();
                var subjectList = subjects.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();

                TeacherViewModel teacher = new TeacherViewModel
                {
                    SubjectList = subjectList
                };

                return View(teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting teachers.");
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
        public async Task<IActionResult> AddTeacher(TeacherViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;
                    var subject = model.TeacherSubjects;

                    if (model.Photo != null)
                    {
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.Photo.CopyToAsync(fileStream);
                        }
                    }

                    Teacher teacher = new Teacher
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Photo = uniqueFileName,
                        Age = model.Age,
                        Sex = model.Sex,
                        PhoneNumber = model.PhoneNumber,
                        SubjectId = string.Join(", ", model.TeacherSubjects),
                        Active = model.Active
                    };

                    await _teacherService.AddTeachertAsync(teacher);
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
                _logger.LogError(ex, "An error occurred while creating teacher.");
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
