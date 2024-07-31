using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.ViewModels
{
    public class TeacherViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        [MaxLength(100)]
        public string? PhoneNumber { get; set; }

        public IFormFile? Photo { get; set; } 

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Sex is required")]
        public string Sex { get; set; }
        public bool Active { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public List<string> TeacherSubjects { get; set; } = new List<string>();
        
        public IEnumerable<SelectListItem> SubjectList { get; set; }  = new List<SelectListItem>();
    }
}
