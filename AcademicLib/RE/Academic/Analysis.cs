using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class Analysis
    {
        public Analysis()
        {
            Class = "";
            Section = "";
            Gender = "";
            Caste = "";
            House = "";
            Medium = "";
            Board = "";
            Route = "";
            Point = "";
            Room = "";
            TravelType = "";
            Shift = "";
            Disability = "";
            StudentType = "";
            Age = "";
        }
        public int NoOfStudent { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Gender { get; set; }
        public string Caste { get; set; }
        public string House { get; set; }
        public string Medium { get; set; }
        public string Board { get; set; }
        public bool IsNew { get; set; }
        public string Route { get; set; }
        public string Point { get; set; }
        public string Room { get; set; }
        public string TravelType { get; set; }
        public string Shift { get; set; }
        public string Disability { get; set; }
        public string StudentType { get; set; }
        public string Age { get; set; }
        public string Batch { get; set; } = "";
        public string Faculty { get; set; } = "";
        public string Level { get; set; } = "";
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";
    }
    public class AnalysisCollections : System.Collections.Generic.List<Analysis>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
