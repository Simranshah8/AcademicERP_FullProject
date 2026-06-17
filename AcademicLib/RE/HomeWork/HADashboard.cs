using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Homework.Reporting
{
    public class HADashboard: ResponeValues
    {

        public HADashboard()
        {
            HomeworkColl = new Homework();
            AssignmentColl = new Assignment();
            TeacherWiseHWColl = new TeacherWiseHWCollection();
            TeacherWiseAssignColl = new TeacherWiseAssignmentCollection();
            OverAndUpcommingdueHWColl = new OverAndUpcommingdueHWCollection();
            OverAndUpcommingdueAssignColl = new OverAndUpcommingdueAssignmentCollection();
            SubmissionStatHWColl = new SubmissionStatHWCollection();
            SubmissionStatAssignColl = new SubmissionStatAssignmentCollection();
        }
       public Homework HomeworkColl { get; set; }
        public Assignment AssignmentColl { get; set; }
        public TeacherWiseHWCollection TeacherWiseHWColl { get; set; }
        public TeacherWiseAssignmentCollection TeacherWiseAssignColl { get; set; }
        public OverAndUpcommingdueHWCollection OverAndUpcommingdueHWColl { get; set; }
        public OverAndUpcommingdueAssignmentCollection OverAndUpcommingdueAssignColl { get; set; }
        public SubmissionStatHWCollection SubmissionStatHWColl { get; set; }
        public SubmissionStatAssignmentCollection SubmissionStatAssignColl { get; set; }

    }
    public class Homework : ResponeValues
    {
        public int? TotalHW { get; set; }
        public int? THSubmission { get; set; }
        public int? THPending { get; set; }
        public int? TSHWOnTime { get; set; }
        public int? TSHWLate { get; set; }
        public int? PSHWDueToday { get; set; }
        public int? PSHWPastDue { get; set; }
    }

    public class Assignment
    {
        public int? TotalAssign { get; set; }
        public int? TASubmission { get; set; }
        public int? TAPending { get; set; }
        public int? TSAssignOnTime { get; set; }
        public int? TSAssignLate { get; set; }
        public int? PSAssignDueToday { get; set; }
        public int? PSAssignPastDue { get; set; }
    }

    public class TeacherWiseHW
    {
        public int? EmployeeId { get; set; }
        public string TeacherName { get; set; } = "";
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? AssignedToday { get; set; }
        public int? SubmittedToday { get; set; }
        public int? Pending { get; set; }
        public int? Overdue { get; set; }
    }
    public class TeacherWiseHWCollection: System.Collections.Generic.List<TeacherWiseHW>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class TeacherWiseAssignment
    {
        public int? EmployeeId { get; set; }
        public string TeacherName { get; set; } = "";
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? AssignedToday { get; set; }
        public int? SubmittedToday { get; set; }
        public int? Pending { get; set; }
        public int? Overdue { get; set; }
    }
    public class TeacherWiseAssignmentCollection: System.Collections.Generic.List<TeacherWiseAssignment>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class OverAndUpcommingdueHW
    {
        public int? ClasId { get; set; }
        public string ClassName { get; set; } = "";
        public int? PastDeadline { get; set; }
        public int? UpcommingDue { get; set; }
    }
    public class OverAndUpcommingdueHWCollection: System.Collections.Generic.List<OverAndUpcommingdueHW>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class OverAndUpcommingdueAssignment
    {
        public int? ClasId { get; set; }
        public string ClassName { get; set; } = "";
        public int? PastDeadline { get; set; }
        public int? UpcommingDue { get; set; }
    }
    public class OverAndUpcommingdueAssignmentCollection: System.Collections.Generic.List<OverAndUpcommingdueAssignment>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class SubmissionStatHW
    {
        public int? StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? SectionId { get; set; }
        public string SectionName { get; set; } = "";
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? HomeWorkId { get; set; }
        public string HWStatus { get; set; } = "";
        public int? FullMark { get; set; }
        public int? ObtainedMark { get; set; }
    }
    public class SubmissionStatHWCollection: System.Collections.Generic.List<SubmissionStatHW>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }

    public class SubmissionStatAssignment
    {
        public int? StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? SectionId { get; set; }
        public string SectionName { get; set; } = "";
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? AssignmentId { get; set; }
        public string AssignmentStatus { get; set; } = "";
        public int? FullMark { get; set; }
        public int? ObtainedMark { get; set; }
    }

    public class SubmissionStatAssignmentCollection: System.Collections.Generic.List<SubmissionStatAssignment>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
}
