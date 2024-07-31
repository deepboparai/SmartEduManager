using Microsoft.AspNetCore.Mvc;
using SimpleDeveloper.Models;
using SimpleDeveloperCore.IServices;
using SimpleDeveloperCore.Models;
using System.Diagnostics;

namespace SimpleDeveloper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentService _studentService;
        private readonly ErrorLogger _errorLogger;
        public HomeController(ILogger<HomeController> logger, IStudentService studentService, ErrorLogger errorLogger)
        {
            _logger = logger;
            _studentService = studentService;
            _errorLogger = errorLogger;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {                
                IEnumerable<IGrouping<string, Student>> groupedStudents;
                if (!string.IsNullOrEmpty(searchString))
                {
                    var students = await _studentService.SearchStudentsByNameAsync(searchString);
                    groupedStudents = students.GroupBy(s => s.Class);
                }
                else
                {
                    groupedStudents = await _studentService.GetStudentsGroupedByClassAsync();
                }
                return View(groupedStudents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting GetStudentsGroupedByClassAsync.");
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier,
                    Message = ex.Message
                };
                _errorLogger.LogError(ex);
                return View("Error", errorViewModel);
            }
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
