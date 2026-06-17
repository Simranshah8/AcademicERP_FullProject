using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class SocialMedia : ResponeValues 
	{ 

		public int BranchId { get; set; } 
		public int? SocialMediaId { get; set; } 
		public int OrderNo { get; set; } 
		public string Name { get; set; } ="" ; 
		public string IconPath { get; set; } ="" ; 
		public string ThumbnailPath { get; set; } ="" ; 
		public bool IsActive { get; set; }
		public string URLPath { get; set; } = "";
	}
	public class SocialMediaCollections : System.Collections.Generic.List<SocialMedia>
    {
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}
}

