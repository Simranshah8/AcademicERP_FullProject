using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.Areas.Support.Model
{
	public class TicketComment : ResponeValues
	{
		public int? TicketId { get; set; }
		public string Comment { get; set; } = "";
		public string UserName { get; set; } = "";
		public DateTime LogDateTime { get; set; }
		public string LogMiti { get; set; } = "";
		public string attachFile { get; set; } = "";

		public int? TranId { get; set; }

	}

	public class TicketApproved : ResponeValues
	{
		public int TicketId { get; set; }
		public string ApprovedBy { get; set; }
		public string ApprovedRemarks { get; set; }
		public string CompanyCode { get; set; }
		public string UrlName { get; set; }

		public int? TranId { get; set; }
		public string DBCode { get; set; }
	}


}