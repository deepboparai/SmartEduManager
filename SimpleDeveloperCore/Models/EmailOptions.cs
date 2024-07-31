using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.Models
{
    public class EmailOptions
    {
        public string Password { get; set; }
        public string Host { get; set; }
        public string FromMail { get; set; }
        public string ToMail { get; set; }
        public string ApiKey { get; set; }
    }
}
