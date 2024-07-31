using SimpleDeveloperCore.Models;
using SimpleDeveloperCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentAsyc();
        Task<string> AddNewStudentAsync(Student addStudent);
        Task<IEnumerable<Student>> SearchStudentsByNameAsync(string name);
        Task<IEnumerable<IGrouping<string, Student>>> GetStudentsGroupedByClassAsync();
        Task<IEnumerable<StudentSubjectsTeachersViewModel>> StudentSubjectsandTeachersAsync();
    }
}
