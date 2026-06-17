using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class HealthChekUpExam : ResponeValues 
	{ 

		public int? ExamCheckUpId { get; set; } 
		public int? ClassTypeId { get; set; } 
		public int? SectionTypeId { get; set; } 
		public int? ExamTypeId { get; set; } 
		public int? ClassCopyId { get; set; } 
		public int? SectionCopyId { get; set; }
		public string ClassName { get; set; } = "";
		public string SectionName { get; set; } = "";
		public string ExamTypeName { get; set; } = "";
		public string TestName { get; set; } = "";
		public HealthChekUpExam()
		{
		ExamCheckUpDetColl  =new ExamCheckUpDetCollections();
		}
		public ExamCheckUpDetCollections ExamCheckUpDetColl  { get; set; } 
		}
		public class ExamCheckUpDet
	{ 

		public int ExamCheckUpId { get; set; } 
		public int? TestNameTypeId { get; set; } 
		public string DefaultValue { get; set; } ="" ; 
		public string DefaultRemarks { get; set; } ="" ; 
		} 

		public class ExamCheckUpDetCollections  : System.Collections.Generic.List<ExamCheckUpDet>
	{ 

		public string ResponseMSG { get; set; }= "" ; 

		public bool IsSuccess { get; set; }

		}
	public class HealthChekUpExamCollections : System.Collections.Generic.List<HealthChekUpExam>
	{
		public HealthChekUpExamCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

