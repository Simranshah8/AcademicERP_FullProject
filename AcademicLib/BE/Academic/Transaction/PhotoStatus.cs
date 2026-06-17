using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Academic.Transaction
{
    public class PhotoStatus : ResponeValues
    {
        public PhotoStatus()
        {
            studentPhotoStatusColl = new StudentPhotoStatusColl();
            employeePhotoStatusColl = new EmployeePhotoStatusColl();
        }
        public StudentPhotoStatusColl studentPhotoStatusColl { get; set; }
        public EmployeePhotoStatusColl employeePhotoStatusColl { get; set; }
    }
    public class PhotoStatusCollections : List<PhotoStatus>
    {
        public PhotoStatusCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StudentPhotoStatus : ResponeValues
    {
        public int? ClassId { get; set; }
        public string ClassDetails { get; set; } = "";
        public int? TotalStudent { get; set; }
        public int? StdPhotoUploaded { get; set; }
        public int? StdRemaining { get; set; }

    }
    public class StudentPhotoStatusColl : List<StudentPhotoStatus> 
    {
        public StudentPhotoStatusColl()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeePhotoStatus : ResponeValues
    {
        public int? DepartmentId { get; set; }
        public string Department { get; set; } = "";
        public int? TotalEmployee { get; set; }
        public int? EmpPhotoUploaded { get; set; }
        public int? EmpRemaining { get; set; }
    }
    public class EmployeePhotoStatusColl : List<EmployeePhotoStatus> 
    {
        public EmployeePhotoStatusColl()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}