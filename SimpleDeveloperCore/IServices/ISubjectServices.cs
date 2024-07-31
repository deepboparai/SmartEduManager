using SimpleDeveloperCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.IServices
{
    public interface ISubjectServices
    {
        Task AddSubjectAsync(Subject addSubject);
    }
}
