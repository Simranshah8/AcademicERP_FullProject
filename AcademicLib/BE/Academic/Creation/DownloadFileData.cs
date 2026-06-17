using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Academic.Creation
{
	public class StudentDocFile : ResponeValues
	{
		public int? StudentId { get; set; }	
		public string RegNo { get; set; } = "";
		public string Name { get; set; } = "";
		public DateTime? DOB_AD { get; set; }
		public string Email { get; set; } = "";
		public string PhotoPath { get; set; } = "";
		public string SignaturePath { get; set; } = "";
		public string ClassName { get; set; } = "";
		public string SectionName { get; set; } = "";
		public int RollNo { get; set; }		
		public string PA_FullAddress { get; set; } = "";
		public string Batch { get; set; } = "";
		public string Semester { get; set; } = "";
		public string ClassYear { get; set; } = "";
		public StudentDocFile()
		{
			AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
	}

	public class StudentDocFileCollections : System.Collections.Generic.List<StudentDocFile>
	{
		public StudentDocFileCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class EmployeeDocFile : ResponeValues
	{
		public int? EmployeeId { get; set; }
		public string EmployeeCode { get; set; } = "";
		public string Name { get; set; } = "";
		public DateTime? DOB_AD { get; set; }
		public string OfficeEmailId { get; set; } = "";
		public string PhotoPath { get; set; } = "";
		public string SignaturePath { get; set; } = "";
		public string PA_FullAddress { get; set; } = "";
		public string Department { get; set; } = "";
		public string Designation { get; set; } = "";
		public EmployeeDocFile()
		{
			AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
	}

	public class EmployeeDocFileCollections : System.Collections.Generic.List<EmployeeDocFile>
	{
		public EmployeeDocFileCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}