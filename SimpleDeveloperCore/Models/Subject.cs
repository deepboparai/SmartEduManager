using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Class is required")]
        public string Class { get; set; }

        public string Language { get; set; }
        public bool Active { get; set; }
        public ICollection<SubjectTeacherReference> SubjectTeacherReferences { get; set; }
        public ICollection<StudentSubjectReference> StudentSubjectReferences { get; set; }
    }
}
