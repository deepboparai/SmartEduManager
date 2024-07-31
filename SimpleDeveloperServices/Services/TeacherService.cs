using SimpleDeveloperCore.IRepository;
using SimpleDeveloperCore.IServices;
using SimpleDeveloperCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperServices.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository teacherRepository;
        public TeacherService(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task AddTeachertAsync(Teacher addTeacher)
        {
            try
            {
                await teacherRepository.AddNewTeacherAsync(addTeacher);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Subject>> GetSubjectListAsync()
        {
            try
            {
                return await teacherRepository.GetSubjectListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
