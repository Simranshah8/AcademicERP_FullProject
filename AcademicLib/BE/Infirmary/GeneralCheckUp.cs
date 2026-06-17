using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class GeneralCheckUp : ResponeValues
	{

		public int? GeneralCheckUpId { get; set; }
		public string Name { get; set; } = "";
		public int? Month { get; set; }
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		public string Organizer { get; set; } = "";
		public bool Vaccination { get; set; }
		public string Description { get; set; } = "";
		public string MonthName { get; set; } = "";
		public GeneralCheckUp()
		{
			GeneralCheckUpRepColl = new GeneralCheckUpRepCollections();

		}
		public GeneralCheckUpRepCollections GeneralCheckUpRepColl { get; set; }
		public List<int> SelectGClassColl { get; set; }
		public List<int> SelectGDiseaseColl { get; set; }
		public List<int> SelectGTestColl { get; set; }
		public List<int> SelectGVaccineColl { get; set; }
	}
	public class GeneralCheckUpRep
	{

		public int GeneralCheckUpId { get; set; }
		public int? ExaminerId { get; set; }
		public string Name { get; set; } = "";
		public string ExaminerRegdNo { get; set; } = "";
		public string Designation { get; set; } = "";
		public string MobileNo { get; set; } = "";
	}

	public class GeneralCheckUpRepCollections : System.Collections.Generic.List<GeneralCheckUpRep>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class SelectGClass
	{

		public int GeneralCheckUpId { get; set; }
		public int ClassId { get; set; }
	}

	public class SelectGClassCollections : System.Collections.Generic.List<SelectGClass>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class SelectGDisease
	{

		public int GeneralCheckUpId { get; set; }
		public int HealthIssueId { get; set; }
	}

	public class SelectGDiseaseCollections : System.Collections.Generic.List<SelectGDisease>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class SelectGTest
	{

		public int GeneralCheckUpId { get; set; }
		public int TestNameId { get; set; }
	}

	public class SelectGTestCollections : System.Collections.Generic.List<SelectGTest>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class SelectGVaccine
	{

		public int GeneralCheckUpId { get; set; }
		public int VaccineId { get; set; }
	}

	public class SelectGVaccineCollections : System.Collections.Generic.List<SelectGVaccine>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class GeneralCheckUpCollections : System.Collections.Generic.List<GeneralCheckUp>
	{
		public GeneralCheckUpCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}
