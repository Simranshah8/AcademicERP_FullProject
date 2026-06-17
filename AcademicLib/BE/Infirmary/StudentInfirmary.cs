using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{
	public class StudentDetForInfirmary : ResponeValues
	{
		public int? StudentId { get; set; }
		public string Name { get; set; } = "";
		public string ClassName { get; set; } = "";
		public string SectionName { get; set; } = "";
		public int? RollNo { get; set; }
		public string ContactNo { get; set; } = "";
		public int? StudentTypeId { get; set; }
		public string Address { get; set; } = "";	
		public string FatherName { get; set; } = "";
		public string F_ContactNo { get; set; } = "";
		public string GuardianName { get; set; } = "";
		public string G_ContactNo { get; set; } = "";
		public string PhotoPath { get; set; } = "";	
		public string RegNo { get; set; } = "";	
		public string G_Email { get; set; } = "";	
		public string F_Email { get; set; } = "";	
		public string MotherName { get; set; } = "";	
		public string M_Contact { get; set; } = "";	
		public string M_Email { get; set; } = "";	
		//Prashant AddCode 14 mangsir
		public DateTime? DOB_AD { get; set; }
		public string DOB_BS { get; set; } = "";
		//Added by Suresh for college
		public string Batch { get; set; } = "";

		public string ClassYear { get; set; } = "";
		public string Semester { get; set; } = "";
		public string BloodGroup { get; set; } = "";
		public string Height { get; set; } = "";
		public string Weigth { get; set; } = "";
	}

	public class StudentDetForInfirmaryCollections : System.Collections.Generic.List<StudentDetForInfirmary>
	{
		public StudentDetForInfirmaryCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class MedicalProducts : ResponeValues
	{

		public int? ProductId { get; set; }
		public string Code { get; set; } = "";	
		public string Name { get; set; } = "";
	}
	public class MedicalProductsCollections : System.Collections.Generic.List<MedicalProducts>
	{
		public MedicalProductsCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class StudentPastMedicalHistory : ResponeValues
	{

		public int? TranId { get; set; }
		public int StudentId { get; set; }
		public int HealthIssueId { get; set; }
		public DateTime? ObservedDate { get; set; }
		public string Details { get; set; } = "";
		public string Prescription { get; set; } = "";
		public string HealthIssue { get; set; } = "";
		public bool MedicineTaken { get; set; }
		public string StudentName { get; set; } = "";
		public string HealthIssueName { get; set; } = "";
		public string ObservedDateBS { get; set; } = "";
		public StudentPastMedicalHistory()
		{
			StudentPastMedicineDetColl = new StudentPastMedicineDetCollections();
			StudentPastMedicalDocumentsColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public StudentPastMedicineDetCollections StudentPastMedicineDetColl { get; set; }
		public Dynamic.BusinessEntity.GeneralDocumentCollections StudentPastMedicalDocumentsColl { get; set; }
	}

	public class StudentPastMedicalHistoryCollections : System.Collections.Generic.List<StudentPastMedicalHistory>
	{
		public StudentPastMedicalHistoryCollections()
		{
			ResponseMSG = "";
			StudentPastMedicineDetColl = new StudentPastMedicineDetCollections();
			StudentPastMedicalDocumentsColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public StudentPastMedicineDetCollections StudentPastMedicineDetColl { get; set; }
		public Dynamic.BusinessEntity.GeneralDocumentCollections StudentPastMedicalDocumentsColl { get; set; }
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class StudentPastMedicineDet
	{
		public int TranId { get; set; }
		public int? MedicineId { get; set; }
		public int? NoOfDose { get; set; }
		public int? Duration { get; set; }
		public int? NoOfDays { get; set; }
		public string ProductName { get; set; } = "";
	}

	public class StudentPastMedicineDetCollections : System.Collections.Generic.List<StudentPastMedicineDet>
	{
		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}
	//For Student Health ISsues Tab
	public class StudentHealthIssue : ResponeValues
	{

		public int? TranId { get; set; }
		public int StudentId { get; set; }
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
		public StudentHealthIssue()
		{
			StudentMedicineGivenDetColl = new StudentMedicineGivenDetCollections();
			HealthIssueAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public StudentMedicineGivenDetCollections StudentMedicineGivenDetColl { get; set; }
		public Dynamic.BusinessEntity.GeneralDocumentCollections HealthIssueAttachmentColl { get; set; }
	}

	public class StudentHealthIssueCollections : System.Collections.Generic.List<StudentHealthIssue>
	{
		public StudentHealthIssueCollections()
		{
			ResponseMSG = "";
			StudentMedicineGivenDetColl = new StudentMedicineGivenDetCollections();
			HealthIssueAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public StudentMedicineGivenDetCollections StudentMedicineGivenDetColl { get; set; }
		public Dynamic.BusinessEntity.GeneralDocumentCollections HealthIssueAttachmentColl { get; set; }
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class StudentMedicineGivenDet
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

	public class StudentMedicineGivenDetCollections : System.Collections.Generic.List<StudentMedicineGivenDet>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}
	// Student Immunization

	public class StudentHealthImmunization : ResponeValues
	{

		public int? TranId { get; set; }
		public int StudentId { get; set; }
		public int HealthIssueId { get; set; }
		public int VaccineId { get; set; }
		public int VaccinatorId { get; set; }
		public DateTime? VaccinationDate { get; set; }
		public string Remarks { get; set; } = "";
		public string VaccineName { get; set; } = "";
		public string HealthIssueName { get; set; } = "";
		public string VaccineMiti { get; set; } = "";
		public StudentHealthImmunization()
		{
			StudentImmunizationAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections StudentImmunizationAttachmentColl { get; set; }
	}

	public class StudentHealthImmunizationCollections : System.Collections.Generic.List<StudentHealthImmunization>
	{
		public StudentHealthImmunizationCollections()
		{
			ResponseMSG = "";
			StudentImmunizationAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections StudentImmunizationAttachmentColl { get; set; }
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


	public class LabValue : ResponeValues
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
	public class LabValueCollections : System.Collections.Generic.List<LabValue>
	{
		public LabValueCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class StudentClinicalLabEvaluation : ResponeValues
	{
		public int? TranId { get; set; }
		public int StudentId { get; set; }
		public DateTime? EvaluateDate { get; set; }
		public int TestNameId { get; set; }
		public string TestMethod { get; set; } = "";
		public int HealthIssueId { get; set; }
		public string Remarks { get; set; } = "";
		public string EvaluateMiti { get; set; } = "";
		public string TestName { get; set; } = "";
		public StudentClinicalLabEvaluation()
		{
			LabValueList = new StudentCLELabValueCollections();
		}
		public StudentCLELabValueCollections LabValueList { get; set; }
	}
	public class StudentClinicalLabEvaluationCollections : System.Collections.Generic.List<StudentClinicalLabEvaluation>
	{
		public StudentClinicalLabEvaluationCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class StudentCLELabValue
	{

		public int TranId { get; set; }
		public string Name { get; set; } = "";
		public string Result { get; set; } = "";
		public string Remarks { get; set; } = "";
	}

	public class StudentCLELabValueCollections : System.Collections.Generic.List<StudentCLELabValue>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class StudentGCheckup
	{
		public int TranId { get; set; }
		public int StudentId { get; set; }
		public int TestNameId { get; set; }
		public string TestName { get; set; } = "";
		public string CheckupDateBS { get; set; } = "";
		public double? Value { get; set; }
		public DateTime? CheckupDate { get; set; }
		public string Remarks { get; set; }
	}

	public class StudentGCheckupCollections : System.Collections.Generic.List<StudentGCheckup>
	{
		public StudentGCheckupCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}
