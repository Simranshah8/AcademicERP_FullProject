using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Academic.Creation
{

	public class EnquiryFormConfig : ResponeValues
	{

		public int? TranId { get; set; }
		public int? FormStatus { get; set; }
		public string ClosedMsg { get; set; } = "";
		public string FormHeading { get; set; } = "";
		public bool IsLogoDisplayed { get; set; }
		public bool IsHeaderBDisplayed { get; set; }
		public bool IsFooterBDisplayed { get; set; }
		public bool IsAbouUsContentDisplayed { get; set; }
		public bool IsEnqContentDisplayed { get; set; }
		public string EnqContent { get; set; } = "";
		public int? BranchId { get; set; }
	}
	public class EnquiryFormConfigCollections : System.Collections.Generic.List<EnquiryFormConfig>
	{
		public EnquiryFormConfigCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

