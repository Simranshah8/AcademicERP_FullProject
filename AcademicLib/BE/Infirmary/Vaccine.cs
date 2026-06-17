using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class Vaccine : ResponeValues 
	{ 

		public int? VaccineId { get; set; } 
		public string Name { get; set; } ="" ; 
		public string CompanyName { get; set; } ="" ; 
		public int OrderNo { get; set; } 
		public int? VaccineForId { get; set; } 
		public string Description { get; set; } ="" ; 
		public string VaccineForName { get; set; } ="" ; 
		}
	public class VaccineCollections : System.Collections.Generic.List<Vaccine>
	{
		public VaccineCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

