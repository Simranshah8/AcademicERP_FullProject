using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.Areas.Support.Model
{
	public class GenerateTicket : ResponeValues
	{
		public int? TicketId { get; set; }
		public int CustomerId { get; set; }
		public string UrlName { get; set; }
		public string DBCode { get; set; }
		public string CompanyCode { get; set; } = "";
		public string ContactName { get; set; } = "";
		public string ContactDesignation { get; set; } = "";
		public string ContactMobileNo { get; set; } = "";
		public string EmailId { get; set; } = "";
		public int? SourceId { get; set; }
		public string SpecifySource { get; set; } = "";
		public int? RequirementTypeId { get; set; }

		public string RequirementProblem { get; set; } = "";
		public int? PriorityId { get; set; }
		public string Description { get; set; } = "";
		public int? AssignToId { get; set; }
		public string attachFile { get; set; } = "";
		public string CustomerName { get; set; } = "";
		public string ContactSource { get; set; } = "";
		public string AssignTo { get; set; } = "";
		public string PriorityStatus { get; set; } = "";
		public string RequirementType { get; set; } = "";
		//public string PhotoPath { get; set; }
		public byte[] Photo { get; set; }
		public DateTime? LogDateTime { get; set; }
		public string LogMiti { get; set; }
		public int? CreateBy { get; set; }
		public string UserName { get; set; }
		 
		public string StatusRemarks { get; set; }
		public string LastComment { get; set; }
		public string TicketStatus { get; set; }

		public DateTime OpenDateTime { get; set; }
		public DateTime? CloseDateTime { get; set; }
		public string OpenMiti { get; set; }
		public string ClosedMiti { get; set; }
		public string StatusMinDiff { get; set; }
		public string PendingMinDiff { get; set; }

		public string CustomerApprovedRemarks { get; set; }
		public DateTime? CustomerApprovedAt { get; set; }
		public string ApporivedMiti { get; set; }
		public string CustomerApprovedBy { get; set; }
		//Added By Suresh on 12-12-2023
		public int? AgreementProductNameId { get; set; }
		public string ProductName { get; set; }

		public string PaymentVerifiedBy { get; set; }

	}

	public class GenerateTicketCollections : System.Collections.Generic.List<GenerateTicket>
	{
		public GenerateTicketCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class TicketHis
	{
		public string Status { get; set; }
		public string Remarks { get; set; }
		public string User { get; set; }
		public DateTime LogDateTime { get; set; }
		public string LogMiti { get; set; }
	}
	public class TicketHisCollections 
	{
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }

		public System.Collections.Generic.List<TicketHis> DataColl { get; set; }

	}

}