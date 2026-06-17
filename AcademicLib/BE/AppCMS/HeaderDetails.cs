using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class HeaderDetails : ResponeValues
	{
		public int? HeaderDetailId { get; set; }
		public string LogoPhoto { get; set; } = "";
		public string CompanyName { get; set; } = "";
		public string Slogan { get; set; } = "";
		public bool? HeaderIsActive { get; set; }
		public bool? NameIsActive { get; set; }
		public bool? SloganIsActive { get; set; }
	}
	public class HeaderDetailsColl : List<HeaderDetails>
	{
		public HeaderDetailsColl()
		{
			ResponseMSG = " ";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}
