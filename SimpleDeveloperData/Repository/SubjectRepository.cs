using Microsoft.EntityFrameworkCore;
using SimpleDeveloperCore.IRepository;
using SimpleDeveloperCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperData.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolContext _schoolContext;
        public SubjectRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }
        public async Task AddSubjectAsync(Subject addSubject)
        {

            _schoolContext.Add(addSubject);
            await _schoolContext.SaveChangesAsync();
        }
    }
}
