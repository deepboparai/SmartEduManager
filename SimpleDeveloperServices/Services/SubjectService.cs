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
    public class SubjectService : ISubjectServices
    {
        private readonly ISubjectRepository subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
                this.subjectRepository = subjectRepository;
        }

        public async Task AddSubjectAsync(Subject addSubject)
        {
            try
            {
                await subjectRepository.AddSubjectAsync(addSubject);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
