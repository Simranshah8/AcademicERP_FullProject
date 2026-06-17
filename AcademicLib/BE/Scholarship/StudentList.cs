using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{
	public class StudentList: ResponeValues
	{
		public int? TranId { get; set; }
		public string CandidateName { get; set; } = "";
		public string FatherName { get; set; } = "";
		public string MotherName { get; set; } = "";
		public string GenderName { get; set; } = "";		
		public string DOBMiti { get; set; } = "";		
		public string Email { get; set; } = "";
		public string MobileNo { get; set; } = "";		
		public string SubjectName { get; set; } = "";	
		public string ScholarshipTypeName { get; set; } = "";
		public string SchoolTypeName { get; set; } = "";
		public string RollNo { get; set; } = "";
		public string ExamCenter { get; set; } = "";
		public string ExamMiti { get; set; } = "";
		public string ReservationGroup { get; set; } = "";
		
	}

	public class StudentListCollections : System.Collections.Generic.List<StudentList>
	{
		public StudentListCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	
}

