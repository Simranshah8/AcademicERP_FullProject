using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class EquivalentBoard : ResponeValues
	{

		public int? BoardId { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public int OrderNo { get; set; }
		public int? ClassId { get; set; }
	}
	public class EquivalentBoardCollections : System.Collections.Generic.List<EquivalentBoard>
	{
		public EquivalentBoardCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

