using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class HealthOperation : ResponeValues
	{

		public int? HealthCampaignId { get; set; }
		public string Name { get; set; } = "";
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		public string Organizer { get; set; } = "";
		public bool Vaccination { get; set; }
		public string Description { get; set; } = "";
		public HealthOperation()
		{
			HealthCampaignRepColl = new HealthCampaignRepCollections();
		
		}
		public HealthCampaignRepCollections HealthCampaignRepColl { get; set; }
		public List<int> SelectClassColl { get; set; }
		public List<int> SelectDiseaseColl { get; set; }
		public List<int> SelectVaccineColl { get; set; }
		public List<int> SelectTestColl { get; set; }
	}
	public class HealthCampaignRep
	{
		public int ExaminerId { get; set; }
		public int HealthCampaignId { get; set; }
		public string Name { get; set; } = "";
		public string ExaminerRegdNo { get; set; } = "";
		public string Designation { get; set; } = "";
		public string MobileNo { get; set; } = "";
	}

	public class HealthCampaignRepCollections : System.Collections.Generic.List<HealthCampaignRep>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class SelectClass
	{

		public int HealthCampaignId { get; set; }
		public int ClassId { get; set; }
	}

	public class SelectClassCollections : System.Collections.Generic.List<SelectClass>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class SelectDisease
	{

		public int HealthCampaignId { get; set; }
		public int HealthIssueId { get; set; }
	}

	public class SelectDiseaseCollections : System.Collections.Generic.List<SelectDisease>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class SelectVaccine
	{

		public int HealthCampaignId { get; set; }
		public int VaccineId { get; set; }

	}

	public class SelectVaccineCollections : System.Collections.Generic.List<SelectVaccine>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class SelectTest
	{

		public int HealthCampaignId { get; set; }
		public int TestNameId { get; set; }
	}

	public class SelectTestCollections : System.Collections.Generic.List<SelectTest>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class HealthOperationCollections : System.Collections.Generic.List<HealthOperation>
	{
		public HealthOperationCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}