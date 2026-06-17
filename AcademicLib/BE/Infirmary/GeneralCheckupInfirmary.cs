using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{
	public class GeneralCheckupInfirmary : ResponeValues
	{
		public int? TranId { get; set; }
		public int? HealthCampaignId { get; set; }
		public DateTime? CheckupDate { get; set; }
		public int? ClassId { get; set; }
		public int? MonthId { get; set; }
		public int? ExaminerId { get; set; }
		public bool IsVaccination { get; set; }
		public string Remarks { get; set; } = "";
		public string ExaminerName { get; set; } = "";
		public string HealthCampaignName { get; set; } = "";
		public string ClassName { get; set; } = "";
		public string CheckupMiti { get; set; } = "";
		public string Month { get; set; } = "";
		public GeneralCheckupTestValueCollections GeneralCheckupTestValueColl { get; set; } = new GeneralCheckupTestValueCollections();
		public GeneralCheckupVaccinationCollections GeneralCheckupVaccinationColl { get; set; } = new GeneralCheckupVaccinationCollections();
	}

	public class GeneralCheckupTestValue
	{
		public int? TranId { get; set; }
		public int? StudentId { get; set; }
		public int? TestNameId { get; set; }
		public string Value { get; set; }
	}

	public class GeneralCheckupTestValueCollections : System.Collections.Generic.List<GeneralCheckupTestValue>
	{
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}

	public class GeneralCheckupVaccination
	{
		public int? TranId { get; set; }
		public int? StudentId { get; set; }
		public int? TestNameId { get; set; }
		public bool IsAllow { get; set; }
		public string Remarks { get; set; }
	}

	public class GeneralCheckupVaccinationCollections : System.Collections.Generic.List<GeneralCheckupVaccination>
	{
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}

	public class GeneralCheckupInfirmaryCollections : System.Collections.Generic.List<GeneralCheckupInfirmary>
	{
		public GeneralCheckupInfirmaryCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class StudentForGCInfirmary : ResponeValues
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
	public class StudentForGCInfirmaryCollections : System.Collections.Generic.List<StudentForGCInfirmary>
	{
		public StudentForGCInfirmaryCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	//For getting data as per the health campaign selected
	public class GeneralCheckUpData : ResponeValues
	{
		public int? GeneralCheckUpId { get; set; }		
		public bool Vaccination { get; set; }
		public int? Month { get; set; }
		public GeneralCheckUpData()
		{
			GeneralHealthExaminerColl = new GeneralExaminerCollection();
			GeneralCheckupClassColl = new GeneralCheckUpClassCollection();
		}
		public GeneralExaminerCollection GeneralHealthExaminerColl { get; set; }
		public GeneralCheckUpClassCollection GeneralCheckupClassColl { get; set; }

	}
	public class GeneralCheckUpExaminer
	{
		public int GeneralCheckUpId { get; set; }
		public int? ExaminerId { get; set; }
		public string Name { get; set; } = "";
		public string ExaminerRegdNo { get; set; } = "";
		public string Designation { get; set; } = "";
		public string MobileNo { get; set; } = "";
	}	

	public class GeneralExaminerCollection : System.Collections.Generic.List<GeneralCheckUpExaminer>
	{
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}

	public class GeneralCheckUpClass
	{
		public int GeneralCheckUpId { get; set; }
		public int? ClassId { get; set; }
		public string Name { get; set; } = "";
		
	}
	public class GeneralCheckUpClassCollection : System.Collections.Generic.List<GeneralCheckUpClass>
	{
		public string ResponseMSG { get; set; } = "";
		public bool IsSuccess { get; set; }
	}


	public class GeneralCheckUpDataCollections : System.Collections.Generic.List<GeneralCheckUpData>
	{
		public GeneralCheckUpDataCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}

