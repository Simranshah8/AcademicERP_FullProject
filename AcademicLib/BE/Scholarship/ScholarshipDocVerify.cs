using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class ScholarshipDocVerify : ResponeValues
	{

		public int? VerifyId { get; set; }
		public int? TranId { get; set; }
		public bool V_Photo { get; set; }
		public bool V_Signature { get; set; }
		public bool V_Document { get; set; }
		public bool V_CandidateName { get; set; }
		public bool V_Gender { get; set; }
		public bool V_DOB { get; set; }
		public bool V_FatherName { get; set; }
		public bool V_MotherName { get; set; }
		public bool V_GrandfatherName { get; set; }
		public bool V_Email { get; set; }
		public bool V_MobileNo { get; set; }
		public bool V_PAddress { get; set; }
		public bool V_TempAddress { get; set; }
		public bool V_BCCNo { get; set; }
		public bool V_BCCIssuedDate { get; set; }
		public bool V_BCCIssuedDistrict { get; set; }
		public bool V_BCCIssuedLocalLevel { get; set; }
		public bool V_SchoolName { get; set; }
		public bool V_SchoolType { get; set; }
		public bool V_SchoolDistrict { get; set; }
		public bool V_SchoolLocalLevel { get; set; }
		public bool V_SchoolWardNo { get; set; }
		public bool V_Character_Transfer_Certi { get; set; }
		public bool V_Character_Transfer_CertiDate { get; set; }
		public bool V_ScholarshipType { get; set; }
		public bool V_GovSchoolCertiPath { get; set; }
		public bool V_GovSchoolCertiMiti { get; set; }
		public bool V_GovSchoolCerti_RefNo { get; set; }
		public bool V_Anusuchi3DocPath { get; set; }
		public bool V_Anusuchi3Doc_IssuedMiti { get; set; }
		public bool V_Anusuchi3Doc_RefNo { get; set; }
		public bool V_MigDocPath { get; set; }
		public bool V_Mig_WardId { get; set; }
		public bool V_MigDoc_IssuedMiti { get; set; }
		public bool V_MigDoc_RefNo { get; set; }
		public bool V_LandFillDocPath { get; set; }
		public bool V_LandfilDistrict { get; set; }
		public bool V_LandfillLocalLevel { get; set; }
		public bool V_LandfillWardNo { get; set; }
		public bool V_LandfillDoc_IssuedMiti { get; set; }
		public bool V_LandFill_RefNo { get; set; }
		public int V_Status { get; set; } = 1;
		public string Email { get; set; } = "";
		public string V_Subject { get; set; } = "";
		public string Remarks { get; set; } = "";
		public bool V_Gradesheet_Certi { get; set; }
		public bool V_RelatedSchoolFilePath { get; set; }
		public bool V_RelatedSchoolIssueMiti { get; set; }
		public bool V_RelatedSchoolRefNo { get; set; }
		public ScholarshipDocVerify()
		{
			ReservationGroupList = new ReservationGroupVerifyCollections();
		}
		public ReservationGroupVerifyCollections ReservationGroupList { get; set; }
		public Scholarship ScholarshipDet { get; set; }
		public string SMSText { get; set; }
	}
	public class ReservationGroupVerify
	{

		public int VerifyId { get; set; }
		public int? ReservationGroupId { get; set; }
		public bool V_GroupWiseCerti_Path { get; set; }
		public bool V_ConcernedAuthorityId { get; set; }
		public bool V_GrpCerti_IssuedDistrict { get; set; }
		public bool V_ISSUED_LOCALLEVEL { get; set; }
		public bool V_GroupWiseCertiMiti { get; set; }
		public bool V_GroupWiseCerti_RefNo { get; set; }
	}

	public class ReservationGroupVerifyCollections : System.Collections.Generic.List<ReservationGroupVerify>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class ScholarshipDocVerifyCollections : System.Collections.Generic.List<ScholarshipDocVerify>
	{
		public ScholarshipDocVerifyCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}



