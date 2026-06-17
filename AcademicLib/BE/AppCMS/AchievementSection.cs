using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{

	public class AchievementSection : ResponeValues
	{

		public int? AchievementSectionId { get; set; }
		public int? CategoryId { get; set; }
		public string Category { get; set; } = "";
		public string AchievementDateBS { get; set; } = "";
		public string Headline { get; set; } = "";
		public string Description { get; set; } = "";
		public string Tags { get; set; } = "";
		public string Photo { get; set; } = "";
		public DateTime AchievementDate { get; set; }
	}
	public class AchievementSectionCollections : System.Collections.Generic.List<AchievementSection>
	{
		public AchievementSectionCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

