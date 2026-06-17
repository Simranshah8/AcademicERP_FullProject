using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class AttendanceSummary
    {
        public AttendanceSummary()
        {
            ClassName = "";
            SectionName = "";
            SubjectName = "";
        }
        public DateTime DT_AD { get; set; }
        public string DT_BS { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? SubjectId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public double NoOfStudent { get; set; }
        public double Present { get; set; }
        public double Absent { get; set; }
        public double Late { get; set; }
        public double Leave { get; set; }

        public string ClassSection
        {
            get
            {
                string val = "";

                if (!string.IsNullOrEmpty(ClassName))
                    val = ClassName;

                if (!string.IsNullOrEmpty(SectionName))
                    val = val +" - "+ SectionName;

                if (!string.IsNullOrEmpty(Semester))
                    val = val + " - " + Semester;

                if (!string.IsNullOrEmpty(ClassYear))
                    val = val + " - " + ClassYear;

                if (!string.IsNullOrEmpty(Batch))
                    val = val + " - " + Batch;
                return val;
            }
        }

        public double PresentPer { get; set; }
        public double AbsentPer { get; set; }
        public double LatePer { get; set; }
        public double LeavePer { get; set; }
        public double Holiday { get; set; }
        public bool IsWeekEnd { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }
    public class AttendanceSummaryCollections : System.Collections.Generic.List<AttendanceSummary>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
