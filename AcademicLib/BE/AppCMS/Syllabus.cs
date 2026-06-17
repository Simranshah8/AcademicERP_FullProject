using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class SyllabusPlan : ResponeValues
	{

		public int? TranId { get; set; }
		public int? ProgramId { get; set; }
		public int? NoOfSyllabus { get; set; }
		public string Name { get; set; } = "";
		public string Program { get; set; } = "";
		public SyllabusPlan()
        {
			DetailsColl = new SyllabusPlanDetailsCollections();
		}
		public SyllabusPlanDetailsCollections DetailsColl { get; set; }
	}

	public class SyllabusPlanCollections : System.Collections.Generic.List<SyllabusPlan>
	{
		public SyllabusPlanCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
	public class SyllabusPlanDetails : ResponeValues
	{

		public int SyllabusId { get; set; }
		public int? TranId { get; set; }
		public int SNo { get; set; }
		public string SyllabusName { get; set; }
		public SyllabusPlanDetails()
        {
			TopicColl = new SyllabusTopicCollections();
		}
		public SyllabusTopicCollections TopicColl { get; set; }
	}
	public class SyllabusPlanDetailsCollections : System.Collections.Generic.List<SyllabusPlanDetails>
	{
		public SyllabusPlanDetailsCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
	public class SyllabusTopic : ResponeValues
	{

		public int? SyllabusId { get; set; }
		public int? SyllabusSNo { get; set; }
		public int? SNo { get; set; }
		public string TopicName { get; set; } = "";
		
	}
	public class SyllabusTopicCollections : System.Collections.Generic.List<SyllabusTopic>
	{
		public SyllabusTopicCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

