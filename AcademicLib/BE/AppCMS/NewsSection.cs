using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class NewsSection : ResponeValues
	{

		public int? NewsSectionId { get; set; }
		public int? CategoryId { get; set; }
		public string Category { get; set; } = "";
		public string PublishedDateBS { get; set; } = "";
		public string Headline { get; set; } = "";
		public string Description { get; set; } = "";
		public string Tags { get; set; } = "";
		public string Photo { get; set; } = "";
		public DateTime PublishedDate { get; set; }
	}
	public class NewsSectionCollections : System.Collections.Generic.List<NewsSection>
	{
		public NewsSectionCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

