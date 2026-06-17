using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class ThemeConfig : ResponeValues
	{

		public int? TranId { get; set; }
		public int ThemeId { get; set; }
		public string PrimaryColor { get; set; } = "";
		public string SecondaryColor { get; set; } = "";
		public string ThirdColor { get; set; } = "";
		public string FourthColor { get; set; } = "";
		public string FifthColor { get; set; } = "";
	}
	public class ThemeConfigCollections : System.Collections.Generic.List<ThemeConfig>
	{
		public ThemeConfigCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

