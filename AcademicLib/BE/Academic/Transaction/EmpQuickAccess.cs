using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Academic.Transaction
{
   public class EmpAttachment : ResponeValues
    {
        public int? EmployeeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DocPath { get; set; }
        public string DocumentType { get; set; }
        public string Extension { get; set; }

    }
    public class EmpAttachmentCollections : System.Collections.Generic.List<EmpAttachment>
    {
        public EmpAttachmentCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class EmpComplain : ResponeValues
    {
        public int? EmployeeId { get; set; }
        public DateTime? ComplainDate { get; set; }
        public string Remarks { get; set; }
        public string ActionRemarks { get; set; }
        public string ComplainType { get; set; }
        public int? ActionTakenBy{ get; set; }
        public string ActionTakenByName { get; set; }
        public DateTime? ActionDate { get; set; }

    }
    public class EmpComplainCollections : System.Collections.Generic.List<EmpComplain>
    {
        public EmpComplainCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

   
    public class EmpLeaveTaken : ResponeValues
    {
        public int? EmployeeId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string LeaveType { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public double? TotalDays { get; set; }
        public string Remarks { get; set; }
        public string ApprovedByUser { get; set; }
        public string RequestMiti { get; set; }
        public string FromMiti { get; set; }
        public string ToMiti { get; set; }

        public int? ApprovedTypeId { get; set; }
        public string ApprovedRemarks { get; set; }
        public DateTime? ApprovedLogDateTime { get; set; }
        public string ApprovedMiti { get; set; }
        public int LeaveRequestId { get; set; }
        public List<Dynamic.BusinessEntity.GeneralDocument> DocumentColl = new List<Dynamic.BusinessEntity.GeneralDocument>();

    }
    public class EmpLeaveTakenCollections : System.Collections.Generic.List<EmpLeaveTaken>
    {
        public EmpLeaveTakenCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentComplain : ResponeValues
    {
        public int? StudentId { get; set; }
        public DateTime? ComplainDate { get; set; }
        public string Remarks { get; set; }
        public string ActionRemarks { get; set; }
        public string ComplainType { get; set; }
        public int? ActionTakenBy { get; set; }
        public string ActionTakenByName { get; set; }
        public DateTime? ActionDate { get; set; }

    }
    public class StudentComplainCollections : System.Collections.Generic.List<StudentComplain>
    {
        public StudentComplainCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentLeaveTaken : ResponeValues
    {
        public int? StudentId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string LeaveType { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public double? TotalDays { get; set; }
        public string Remarks { get; set; }
        public string ApprovedByUser { get; set; }
        public string RequestMiti { get; set; }
        public string FromMiti { get; set; }
        public string ToMiti { get; set; }

    }
    public class StudentLeaveTakenCollections : System.Collections.Generic.List<StudentLeaveTaken>
    {
        public StudentLeaveTakenCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentAttachmentForQA : ResponeValues
    {
        public int? EmployeeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DocPath { get; set; }
        public string DocumentType { get; set; }
        public string Extension { get; set; }

    }
    public class StudentAttachmentForQACollections : System.Collections.Generic.List<StudentAttachmentForQA>
    {
        public StudentAttachmentForQACollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    //j
    public class StudentAttendance : ResponeValues
    {
        public int? StudentId { get; set; }
        public int? YearId { get; set; }
        public int? MonthId { get; set; }
        public string MonthName { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string Day6 { get; set; }
        public string Day7 { get; set; }
        public string Day8 { get; set; }
        public string Day9 { get; set; }
        public string Day10 { get; set; }
        public string Day11 { get; set; }
        public string Day12 { get; set; }
        public string Day13 { get; set; }
        public string Day14 { get; set; }
        public string Day15 { get; set; }
        public string Day16 { get; set; }
        public string Day17 { get; set; }
        public string Day18 { get; set; }
        public string Day19 { get; set; }
        public string Day20 { get; set; }
        public string Day21 { get; set; }
        public string Day22 { get; set; }
        public string Day23 { get; set; }
        public string Day24 { get; set; }
        public string Day25 { get; set; }
        public string Day26 { get; set; }
        public string Day27 { get; set; }
        public string Day28 { get; set; }
        public string Day29 { get; set; }
        public string Day30 { get; set; }
        public string Day31 { get; set; }
        public string Day32 { get; set; }
        public int? TotalDays { get; set; }
        public int? TotalWeekEnd { get; set; }
        public int? TotalHoliday { get; set; }
        public int? TotalPresent { get; set; }
        public int? TotalLeave { get; set; }
        public int? TotalAbsent { get; set; }

    }
    public class StudentAttendanceCollections : System.Collections.Generic.List<StudentAttendance>
    {
        public StudentAttendanceCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}