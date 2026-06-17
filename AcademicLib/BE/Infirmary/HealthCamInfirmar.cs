using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class HealthCamInfirmary : ResponeValues
	{
		public int? HealthCamInfirmaryId { get; set; }
		public int? HealthCampaignId { get; set; }
		public DateTime? CampaignDate { get; set; }
		public int? ClassId { get; set; }
		public int? ExaminerId { get; set; }
		public bool IsVaccination { get; set; }
		public string Remarks { get; set; } = "";
		public string ExaminerName { get; set; } = "";
		public string HealthCampaignName { get; set; } = "";
		public string ClassName { get; set; } = "";
		public string CampaignMiti { get; set; } = "";
		public StudentForTestValueCollections StudentForTestValueColl { get; set; } = new StudentForTestValueCollections();
		public StudentForHCVaccinationCollections StudentForHCVaccinationColl { get; set; } = new StudentForHCVaccinationCollections();
		
	}

	public class StudentForTestValue
	{
		public int? HealthCamInfirmaryId { get; set; }
		public int? StudentId { get; set; }
		public int? TestNameId { get; set; }
		public string Value { get; set; }
    }

	public class StudentForTestValueCollections : System.Collections.Generic.List<StudentForTestValue>
	{
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}

	public class StudentForVaccination
	{
		public int? HealthCamInfirmaryId { get; set; }
		public int? StudentId { get; set; }
		public int? TestNameId { get; set; }
		public bool IsAllow { get; set; }
		public string Remarks { get; set; }
	}

	public class StudentForHCVaccinationCollections : System.Collections.Generic.List<StudentForVaccination>
	{
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}

	public class HealthCamInfirmaryCollections : System.Collections.Generic.List<HealthCamInfirmary>
	{
		public HealthCamInfirmaryCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class StudentForHCInfirmary : ResponeValues
	{

		public int? StudentId { get; set; }
		public int? TestNameId { get; set; }
		public double? Value { get; set; }
		public string RegNo { get; set; }
		public int? RollNo { get; set; }
		public int? OrderNo { get; set; }
		public int? ClassId { get; set; }
		public int? ExaminerId { get; set; }
		public bool IsAllow { get; set; }
		public string TestName { get; set; } = "";
		public string Remarks { get; set; }
		public string StudentName { get; set; }

	}
	public class StudentForHCInfirmaryCollections : System.Collections.Generic.List<StudentForHCInfirmary>
	{
		public StudentForHCInfirmaryCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	//For getting data as per the health campaign selected
	public class HealtCampaignData : ResponeValues
	{
		public int? GeneralCheckUpId { get; set; }
		public bool Vaccination { get; set; }
		public HealtCampaignData()
		{
			HealthCampaignExaminerColl = new HealthCampaignExaminerCollection();
			HealthCampaignClassColl = new HealthCampaignClassCollection();
		}
		public HealthCampaignExaminerCollection HealthCampaignExaminerColl { get; set; }
		public HealthCampaignClassCollection HealthCampaignClassColl { get; set; }

	}
	public class HealthCampaignExaminer
	{
		public int GeneralCheckUpId { get; set; }
		public int? ExaminerId { get; set; }
		public string Name { get; set; } = "";
		public string ExaminerRegdNo { get; set; } = "";
		public string Designation { get; set; } = "";
		public string MobileNo { get; set; } = "";
	}

	public class HealthCampaignExaminerCollection : System.Collections.Generic.List<HealthCampaignExaminer>
	{
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}

	public class HealthCampaignClass
	{
		public int GeneralCheckUpId { get; set; }
		public int? ClassId { get; set; }
		public string Name { get; set; } = "";

	}
	public class HealthCampaignClassCollection : System.Collections.Generic.List<HealthCampaignClass>
	{
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}


	public class HealtCampaignDataCollections : System.Collections.Generic.List<HealtCampaignData>
	{
		public HealtCampaignDataCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

