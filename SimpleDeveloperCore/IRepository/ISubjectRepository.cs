using SimpleDeveloperCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.IRepository
{
    public interface ISubjectRepository
    {
        Task AddSubjectAsync(Subject addSubject);
    }
}
