using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.HomeWork
{
    public class Configuration : ResponeValues
    {
        public int? BranchId { get; set; }
        public int? AcademicYearId { get; set; }
        public bool HomeworkLesson { get; set; }
        public bool HomeworkTopic { get; set; }
        public bool AssignmentLesson { get; set; }

    }
    public class ConfigurationCollections : System.Collections.Generic.List<Configuration>
    {
        public ConfigurationCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
