using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDeveloperCore.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }

        public string? Photo { get; set; }

        [Required(ErrorMessage = "Class is required")]
        [MaxLength(50)]
        public string Class { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Subject is required")]
        public string? Subjects { get; set; }

        [Required(ErrorMessage = "Roll number is required")]
        [Range(0, 99999, ErrorMessage = "Roll number cannot exceed 10 digits")]
        public int RollNumber { get; set; }

        public bool? Active { get; set; }

        public DateTime? CreatedOn { get; set; }
        public ICollection<StudentSubjectReference> StudentSubjectReferences { get; set; } = new List<StudentSubjectReference>();

        [NotMapped]
        public IEnumerable<SelectListItem> SubjectList { get; set; } = new List<SelectListItem>();
    }
}