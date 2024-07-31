using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.ViewModels
{
    public class SubjectViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Class is required")]
        public string Class { get; set; }
        public List<string>? Languages { get; set; } = new List<string>();
        public bool Active { get; set; }
    }

}
