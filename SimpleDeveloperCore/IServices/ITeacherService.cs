using SimpleDeveloperCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.IServices
{
    public interface ITeacherService
    {
        Task AddTeachertAsync(Teacher addTeacher);
        Task<List<Subject>> GetSubjectListAsync(); 
    }
}
