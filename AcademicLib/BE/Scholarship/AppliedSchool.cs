using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class AppliedSchool : ResponeValues
	{

		public int? SchoolId { get; set; }
		public string Name { get; set; } = "";
		public string Address { get; set; } = "";
		public string Email { get; set; } = "";
		public string ContactNo { get; set; } = "";
		public string Description { get; set; } = "";
		public int OrderNo { get; set; }
		public AppliedSchool()
		{
			SchoolSubjectListColl = new SchoolSubjectListCollections();
		}
		public SchoolSubjectListCollections SchoolSubjectListColl { get; set; }
		public List<int> ForClassIdColl { get; set; } = new List<int>();
	}
	public class SchoolSubjectList
	{

		public int SchoolId { get; set; }
		public int? SubjectId { get; set; }
		public string Name { get; set; } = "";
		public bool AllowSubject { get; set; }
	}


	public class SchoolSubjectListCollections : System.Collections.Generic.List<SchoolSubjectList>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class AppliedSchoolCollections : System.Collections.Generic.List<AppliedSchool>
	{
		public AppliedSchoolCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

