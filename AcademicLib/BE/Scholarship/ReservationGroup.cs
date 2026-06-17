using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class ReservationGroup : ResponeValues
	{

		public int? TranId { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public int OrderNo { get; set; }
	}
    public class ReservationGroupCollections : System.Collections.Generic.List<ReservationGroup>
    {
        public ReservationGroupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}

