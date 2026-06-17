using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class NewsCategory : ResponeValues
	{

		public int? CategoryId { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public int? OrderNo { get; set; }
	}
	public class NewsCategoryCollections : System.Collections.Generic.List<NewsCategory>
	{
		public NewsCategoryCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

