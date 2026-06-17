using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Academic.Creation
{

	public class ClassGroups : ResponeValues
	{

		public int? ClassGroupId { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public int? OrderNo { get; set; }
		public List<int> ClassGroupsDetailsColl { get; set; }

	}
	public class ClassGroupsCollections : List<ClassGroups>
    {
        public ClassGroupsCollections()
        {
            ResponseMSG = "";
        }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
	public class ClassGroupsDetails
	{
		public int ClassGroupId { get; set; }
		public int ClassId { get; set; }
	
	}

	public class ClassGroupsDetailsCollections : System.Collections.Generic.List<int>
	{
		public ClassGroupsDetailsCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }

	}
}

