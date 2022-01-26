using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignments.Models
{
    public class ProjectModel
    {
        public ProjectModel(string course, string project, string message, string uri)
        {
            Course = course;
            Project = project;
            Message = message;
            Uri = uri;
        }

        public string Course { get; set; }
        public string Project { get; set; }
        public string Message { get; set; }
        public string Uri { get; set; }
    }
}
