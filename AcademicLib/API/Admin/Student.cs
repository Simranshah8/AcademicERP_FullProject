using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{

    public class Student
    {
        public Student()
        {
            ClassColl = new List<ClassDetail>();
            StudentColl = new List<StudentDetail>();
            ResponseMSG = "";

        }
        public List<ClassDetail> ClassColl { get; set; }
        public List<StudentDetail> StudentColl { get; set; }

        public dynamic ClassWiseStudentColl { get; set; }

        public int TotalStudent
        {
            get
            {
                try
                {
                    return StudentColl.Count;
                }
                catch
                {
                    return 0;
                }
                
            }
        }
        public int TotalNew
        {
            get
            {
                try
                {
                    return StudentColl.Count(p1 => p1.IsNew == true);
                }
                catch
                {
                    return 0;
                }

                
            }
        }
        public int TotalOld
        {
            get
            {
                try
                {
                    return StudentColl.Count(p1 => p1.IsNew == false);
                }
                catch
                {
                    return 0;
                }
            }
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ClassDetail
    {
        public int SNo { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }


        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }


        public int ClassOrderNo { get; set; }
        public int SectionOrderNo { get; set; }
        public int BatchOrderNo { get; set; }
        public int SemesterOrderNo { get; set; }
        public int ClassYearOrderNo { get; set; }


    }
    public class StudentDetail
    {
        public int SNo { get; set; }
        public int StudentId { get; set; } 
        public int UserId { get; set; }
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public string Name { get; set; } = "";
        public int RollNo { get; set; }
        public string RegNo { get; set; } = "";
        public string FatherName { get; set; } = "";
        public string ContactNo { get; set; } = "";
        public bool IsNew { get; set; }
        public string PhotoPath { get; set; } = "";
        public string Address { get; set; } = "";
        public string CurrentAddress { get; set; } = "";

        public string Level { get; set; } = "";
        public string Faculty { get; set; } = "";
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string Batch { get; set; } = "";

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }

        public string MotherName { get; set; } = "";
        public string MotherContactNo { get; set; } = "";
        public string LedgerPanaNo { get; set; } = "";

        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
    }
   
}
