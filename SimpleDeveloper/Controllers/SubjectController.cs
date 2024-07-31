using Microsoft.AspNetCore.Mvc;
using SimpleDeveloper.Models;
using SimpleDeveloperCore.IServices;
using SimpleDeveloperCore.Models;
using SimpleDeveloperCore.ViewModels;

namespace SimpleDeveloper.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectServices _subjectService;
        private readonly ILogger<TeacherController> _logger;
        private readonly ErrorLogger _errorLogger;
        public SubjectController(ISubjectServices subjectService, ILogger<TeacherController> logger, ErrorLogger errorLogger)
        {
            _subjectService = subjectService;
            _logger = logger;
            _errorLogger = errorLogger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddSubjectAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubjectAsync(SubjectViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Example of how to use the selected languages
                    var selectedLanguages = model.Languages;

                    // Create a new Subject entity or perform your logic
                    var subject = new Subject
                    {
                        Name = model.Name,
                        Class = model.Class,
                        Language = string.Join(", ", selectedLanguages), // Save languages as a comma-separated string if needed
                        Active = model.Active
                    };

                    await _subjectService.AddSubjectAsync(subject);

                    return RedirectToAction("Index", "Home"); // Redirect or return a view as needed
                }

                return View(model); // Return the view with the model if validation fails
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating subject.");
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
