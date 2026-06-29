using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Exam.Transaction
{

	public class Objective : ResponeValues
	{

		public int? ObjectiveId { get; set; }
		public int? ClassId { get; set; }
		public int? ExamTypeId { get; set; }
		public int? SubjectId { get; set; }
		public int? AcademicYearId { get; set; }
		public int? SectionId { get; set; }
		public double? FullMark { get; set; }
		public string ClassName { get; set; } = "";
		public string ExamName { get; set; } = "";
		public string SubjectName { get; set; } = "";
		public string SectionName { get; set; } = "";

		public int? FromExamTypeId { get; set; }
		public int? ToExamTypeId { get; set; }
		public int? FromClassId { get; set; }
		public int? ToClassId { get; set; }
		public int? FromSectionId { get; set; }
		public int? ToSectionId { get; set; }
		public string FromClassName { get; set; } = "";
		public string FromExamTypeName { get; set; } = "";
		public string ToExamTypeName { get; set; } = "";
		public string ToClassName { get; set; } = "";
		public string FromSectionName { get; set; } = "";
		public string ToSectionName { get; set; } = "";
		public string TransferCreateBy { get; set; } = "";
		public string TransferDate { get; set; } = "";
		public Objective()
		{
			ObjectiveDetailsColl = new ObjectiveDetailsCollections();
		}
		public ObjectiveDetailsCollections ObjectiveDetailsColl { get; set; }
	}

	public class ObjectiveCollections : System.Collections.Generic.List<Objective>
	{
		public ObjectiveCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
	public class ObjectiveDetails
	{

		public int? ObjectiveId { get; set; }
		public int? StudentId { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public bool IsActive { get; set; }
		public double? Marks { get; set; }
		public double? FullMark { get; set; }
		public int? SNo { get; set; }
		public string Remarks { get; set; } = "";
		public double? TotObjMark { get; set; }
		public double? Mark { get; set; }
		public bool? ColumnWiseFocus { get; set; }
	}

	public class ObjectiveDetailsCollections : System.Collections.Generic.List<ObjectiveDetails>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

}

