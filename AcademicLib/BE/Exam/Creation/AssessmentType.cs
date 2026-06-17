using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Exam.Creation
{

	public class AssessmentType : ResponeValues
	{
		public int? AssessmentTypeId { get; set; }
		public int? BranchId { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public int? OrderNo { get; set; }
		public bool? IsActive { get; set; }
	}
	public class AssessmentTypeColl : List<AssessmentType>
    {
		public AssessmentTypeColl()
		{
			ResponseMSG = " ";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }

	}
}

