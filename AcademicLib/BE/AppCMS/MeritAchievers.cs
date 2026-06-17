using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AppCMS.Creation
{
	public class MeritAchievers : ResponeValues
	{
		public int? TranId { get; set; }
		public string Name { get; set; } = "";
		public string DegreeDetails { get; set; } = "";
		public int? OrderNo { get; set; }
		public string Description { get; set; } = "";
		public string ImagePath { get; set; } = "";
		public MeritAchievers()
		{
			MeritAchievementsColl = new MeritAchievementsCollections();
		}
		public MeritAchievementsCollections MeritAchievementsColl { get; set; }
	}
	public class MeritAchievements
	{
		public int TranId { get; set; }
		public string Achievement { get; set; } = "";
	}

	public class MeritAchievementsCollections : System.Collections.Generic.List<MeritAchievements>
	{
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}

	public class MeritAchieversCollections : System.Collections.Generic.List<MeritAchievers>
	{
		public MeritAchieversCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}


