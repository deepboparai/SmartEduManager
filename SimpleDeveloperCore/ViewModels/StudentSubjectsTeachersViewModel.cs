using SimpleDeveloperCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.ViewModels
{
    public class StudentSubjectsTeachersViewModel
    {
        public Student Student { get; set; }
        public List<SubjectWithTeachersViewModel> SubjectsWithTeachers { get; set; }
    }
}
