using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class Complain :  ResponeValues
    {
		public int? ComplainId { get; set; }
		public int? ComplainTypeId { get; set; }
		public int? SourceId { get; set; }
		public string PhoneNo { get; set; } = "";
		public string AssignTo { get; set; } = "";
		public string Remarks { get; set; } = "";
		public int? BranchId { get; set; }
		public DateTime? ComplainDate { get; set; }
		public int? StudentId { get; set; }
		public int? EmployeeId { get; set; }
		public string OthersName { get; set; } = "";
		public int? AssignToId { get; set; }
		public int? ComplainBy { get; set; }
		public byte[] Photo { get; set; }
		public string PhotoPath { get; set; } = "";
		public string ComplainMiti { get; set; } = "";
		public string AssignToName { get; set; } = "";
		public string ComplainTypeName { get; set; } = "";
		public string ActionRemarks { get; set; } = "";
		public string ActionTakenBy { get; set; } = "";
		public string ActionMiti { get; set; } = "";
		public Complain()
		{
			AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
	}
public class ComplainCollections : System.Collections.Generic.List<Complain>
	{

        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

	public class ComplainReply : ResponeValues
	{
		public int? ComplainId { get; set; }
		public string Remarks { get; set; } = "";
	}

}
