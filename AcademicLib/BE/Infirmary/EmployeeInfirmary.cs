using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE
{
	public class EmployeeDetForInfirmary : ResponeValues
	{
		public int? EmployeeId { get; set; }
		public string Name { get; set; } = "";
		public string EmployeeCode { get; set; } = "";
		public string Designation { get; set; } = "";
		public string Department { get; set; } = "";
		public string BranchName { get; set; } = "";
		public string ContactNo { get; set; } = "";
		public string EmailId { get; set; } = "";
		public string Address { get; set; } = "";
		public string FatherName { get; set; } = "";
		public string MotherName { get; set; } = "";
		public string DOB_BS { get; set; } = "";
		public DateTime? DobAD { get; set; }
		public string PhotoPath { get; set; } = "";
		public string Category { get; set; } = "";
		public string SpouseName { get; set; } = "";
		public string OfficeEmailId { get; set; } = "";
		public string PersnalContactNo { get; set; } = "";
		public string FatherContactNo { get; set; } = "";
		public string MotherContactNo { get; set; } = "";
		public string SpouseContactNo { get; set; } = "";
		public string Age { get; set; } = "";

	}

	public class EmployeeDetForInfirmaryCollections : System.Collections.Generic.List<EmployeeDetForInfirmary>
	{
		public EmployeeDetForInfirmaryCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class EMedicalProducts : ResponeValues
	{

		public int? ProductId { get; set; }
		public string Code { get; set; } = "";
		public string Name { get; set; } = "";
	}
	public class EMedicalProductsCollections : System.Collections.Generic.List<EMedicalProducts>
	{
		public EMedicalProductsCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class EmployeePastMedicalHistory : ResponeValues
	{

		public int? TranId { get; set; }
		public int EmployeeId { get; set; }
		public int HealthIssueId { get; set; }
		public DateTime? ObservedDate { get; set; }
		public string Details { get; set; } = "";
		public string Prescription { get; set; } = "";
		public string HealthIssue { get; set; } = "";
		public bool MedicineTaken { get; set; }
		public string EmployeeName { get; set; } = "";
		public string HealthIssueName { get; set; } = "";
		public string ObservedDateBS { get; set; } = "";
		public EmployeePastMedicalHistory()
		{
			EmployeePastMedicineDetColl = new EmployeePastMedicineDetCollections();
			EmployeePastMedicalDocumentsColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public EmployeePastMedicineDetCollections EmployeePastMedicineDetColl { get; set; }
		public Dynamic.BusinessEntity.GeneralDocumentCollections EmployeePastMedicalDocumentsColl { get; set; }
	}

	public class EmployeePastMedicalHistoryCollections : System.Collections.Generic.List<EmployeePastMedicalHistory>
	{
		public EmployeePastMedicalHistoryCollections()
		{
			ResponseMSG = "";
			EmployeePastMedicineDetColl = new EmployeePastMedicineDetCollections();
			EmployeePastMedicalDocumentsColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public EmployeePastMedicineDetCollections EmployeePastMedicineDetColl { get; set; }
		public Dynamic.BusinessEntity.GeneralDocumentCollections EmployeePastMedicalDocumentsColl { get; set; }
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class EmployeePastMedicineDet
	{
		public int TranId { get; set; }
		public int? MedicineId { get; set; }
		public int? NoOfDose { get; set; }
		public int? Duration { get; set; }
		public int? NoOfDays { get; set; }
		public string ProductName { get; set; } = "";
	}

	public class EmployeePastMedicineDetCollections : System.Collections.Generic.List<EmployeePastMedicineDet>
	{
		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}
	//For Employee Health ISsues Tab
	public class EmployeeHealthIssue : ResponeValues
	{

		public int? TranId { get; set; }
		public int EmployeeId { get; set; }
		public DateTime? ObservedDate { get; set; }
		public DateTime? ObservedTime { get; set; }
		public int? ObservedAt { get; set; }
		public int HealthIssueId { get; set; }
		public bool IsAdmitted { get; set; }
		public DateTime? AdmittedDate { get; set; }
		public string AdmittedAt { get; set; }
		public DateTime? DischargedDate { get; set; }
		public bool MedicineGiven { get; set; }
		public string Prescription { get; set; } = "";
		public int PrescribedBy { get; set; }
		public string ObservedMiti { get; set; } = "";
		public string AdmittedMiti { get; set; } = "";
		public string DischargedMiti { get; set; } = "";
		public string HealthIssueName { get; set; } = "";
		public string PrescribedByName { get; set; } = "";
		public string ObservedDateBS { get; set; } = "";
		public EmployeeHealthIssue()
		{
			EmployeeMedicineGivenDetColl = new EmployeeMedicineGivenDetCollections();
			HealthIssueAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public EmployeeMedicineGivenDetCollections EmployeeMedicineGivenDetColl { get; set; }
		public Dynamic.BusinessEntity.GeneralDocumentCollections HealthIssueAttachmentColl { get; set; }
	}

	public class EmployeeHealthIssueCollections : System.Collections.Generic.List<EmployeeHealthIssue>
	{
		public EmployeeHealthIssueCollections()
		{
			ResponseMSG = "";
			EmployeeMedicineGivenDetColl = new EmployeeMedicineGivenDetCollections();
			HealthIssueAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public EmployeeMedicineGivenDetCollections EmployeeMedicineGivenDetColl { get; set; }
		public Dynamic.BusinessEntity.GeneralDocumentCollections HealthIssueAttachmentColl { get; set; }
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class EmployeeMedicineGivenDet
	{

		public int TranId { get; set; }
		public int? MedicineId { get; set; }
		public int? NoOfDose { get; set; }
		public int? Duration { get; set; }
		public int? NoOfDays { get; set; }
		public double Qty { get; set; }
		public double Price { get; set; }
		public double Amount { get; set; }
		public string ProductName { get; set; } = "";
	}

	public class EmployeeMedicineGivenDetCollections : System.Collections.Generic.List<EmployeeMedicineGivenDet>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}
	// Employee Immunization

	public class EmployeeHealthImmunization : ResponeValues
	{

		public int? TranId { get; set; }
		public int EmployeeId { get; set; }
		public int HealthIssueId { get; set; }
		public int VaccineId { get; set; }
		public int VaccinatorId { get; set; }
		public DateTime? VaccinationDate { get; set; }
		public string Remarks { get; set; } = "";
		public string VaccineName { get; set; } = "";
		public string HealthIssueName { get; set; } = "";
		public string VaccineMiti { get; set; } = "";
		public EmployeeHealthImmunization()
		{
			EmployeeImmunizationAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections EmployeeImmunizationAttachmentColl { get; set; }
	}

	public class EmployeeHealthImmunizationCollections : System.Collections.Generic.List<EmployeeHealthImmunization>
	{
		public EmployeeHealthImmunizationCollections()
		{
			ResponseMSG = "";
			EmployeeImmunizationAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections EmployeeImmunizationAttachmentColl { get; set; }
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


	public class ELabValue : ResponeValues
	{

		public int TestNameId { get; set; }
		public string Name { get; set; } = "";
		public int DefaultValue { get; set; }
		public string NormalRange { get; set; } = "";
		public string LowerRange { get; set; } = "";
		public string UpperRange { get; set; } = "";
		public string GroupName { get; set; } = "";
		public string Remarks { get; set; } = "";
	}
	public class ELabValueCollections : System.Collections.Generic.List<ELabValue>
	{
		public ELabValueCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class EmployeeClinicalLabEvaluation : ResponeValues
	{
		public int? TranId { get; set; }
		public int EmployeeId { get; set; }
		public DateTime? EvaluateDate { get; set; }
		public int TestNameId { get; set; }
		public string TestMethod { get; set; } = "";
		public int HealthIssueId { get; set; }
		public string Remarks { get; set; } = "";
		public string EvaluateMiti { get; set; } = "";
		public string TestName { get; set; } = "";
		public EmployeeClinicalLabEvaluation()
		{
			LabValueList = new EmployeeCLELabValueCollections();
		}
		public EmployeeCLELabValueCollections LabValueList { get; set; }
	}
	public class EmployeeClinicalLabEvaluationCollections : System.Collections.Generic.List<EmployeeClinicalLabEvaluation>
	{
		public EmployeeClinicalLabEvaluationCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class EmployeeCLELabValue
	{

		public int TranId { get; set; }
		public string Name { get; set; } = "";
		public string Result { get; set; } = "";
		public string Remarks { get; set; } = "";
	}

	public class EmployeeCLELabValueCollections : System.Collections.Generic.List<EmployeeCLELabValue>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class EmployeeGCheckup
	{
		public int TranId { get; set; }
		public int EmployeeId { get; set; }
		public int TestNameId { get; set; }
		public string TestName { get; set; } = "";
		public string CheckupDateBS { get; set; } = "";
		public double? Value { get; set; }
		public DateTime? CheckupDate { get; set; }
		public string Remarks { get; set; }
	}

	public class EmployeeGCheckupCollections : System.Collections.Generic.List<EmployeeGCheckup>
	{
		public EmployeeGCheckupCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}
