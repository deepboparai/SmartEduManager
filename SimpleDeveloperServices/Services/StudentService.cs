using SimpleDeveloperCore.IRepository;
using SimpleDeveloperCore.IServices;
using SimpleDeveloperCore.Models;
using SimpleDeveloperCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleDeveloperServices.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public async Task<string> AddNewStudentAsync(Student addStudent)
        {
            try
            {
                var response = studentRepository.AddNewStudentAsync(addStudent);
                return response.Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentAsyc()
        {
            try
            {
                return await studentRepository.GetAllStudentAsyc();
            }
            catch (Exception)
            {

                throw;
            }
        }        

        public async Task<IEnumerable<IGrouping<string, Student>>> GetStudentsGroupedByClassAsync()
        {
            try
            {
                return await studentRepository.GetStudentsGroupedByClassListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Student>> SearchStudentsByNameAsync(string name)
        {
            try
            {
                return await studentRepository.SearchStudentsByNameAsync(name);
            }
            catch (Exception)
            {
                return new List<Student>();
            }
        }


        public async Task<IEnumerable<StudentSubjectsTeachersViewModel>> StudentSubjectsandTeachersAsync()
        {
            return await studentRepository.GetStudentSubjectsandTeachersListAsync();
        }
    }
}
