using SimpleDeveloperCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.ViewModels
{
    public class SubjectWithTeachersViewModel
    {
        public Subject Subject { get; set; }
        public List<Teacher> Teachers { get; set; }
    }

}
