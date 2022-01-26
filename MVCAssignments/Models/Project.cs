using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignments.Models
{
    public class Project
    {
        public Project(string course, string name, string message, string uri)
        {
            Course = course;
            Name = name;
            Message = message;
            Uri = uri;
        }

        public string Course { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Uri { get; set; }
    }
}
