using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class Scholarship : ResponeValues
	{

		public int? TranId { get; set; }
		public string FirstName { get; set; } = "";
		public string MiddleName { get; set; } = "";
		public string LastName { get; set; } = "";

		public string FullName
        {
			get
            {
				return ((FirstName + " " + MiddleName).Trim() + " " + LastName).Trim();
            }
        }
		public int Gender { get; set; } = 1;
		public DateTime? DOB { get; set; }
		public string SEESymbolNo { get; set; } = "";
		public string Alphabet { get; set; } = "";
		public double? GPA { get; set; }
		public string Email { get; set; } = "";
		public string MobileNo { get; set; } = "";
		public string F_FirstName { get; set; } = "";
		public string F_MiddleName { get; set; } = "";
		public string F_LastName { get; set; } = "";
		public string M_FirstName { get; set; } = "";
		public string M_MiddleName { get; set; } = "";
		public string M_LastName { get; set; } = "";
		public string GF_FirstName { get; set; } = "";
		public string GF_MiddleName { get; set; } = "";
		public string GF_LastName { get; set; } = "";
		public int? P_ProvinceId { get; set; }
		public int? P_DistrictId { get; set; }
		public int? P_LocalLevelId { get; set; }
		public int? P_WardNo { get; set; }
		public string P_ToleStreet { get; set; } = "";
		public int? Temp_ProvinceId { get; set; }
		public int? Temp_DistrictId { get; set; }
		public int? Temp_LocalLevelId { get; set; }
		public int? Temp_WardNo { get; set; }
		public string Temp_ToleStreet { get; set; } = "";
		public int? BC_CertificateTypeId { get; set; }
		public string BC_CertificateNo { get; set; } = "";
		public DateTime? BC_IssuedDate { get; set; }
		public int? BC_IssuedDistrictId { get; set; }
		public int? BC_IssuedLocalLevelId { get; set; }
		public int? BC_IssuedWardNo { get; set; }
		public string BC_IssuedToleStreet { get; set; } = "";
		public int? BC_DocumentNameId { get; set; }
		public string BC_FilePath { get; set; } = "";

		public int? EquivalentBoardId { get; set; }
		public string GradeSheetFilePath { get; set; } = "";
		public string Character_Transfer_Certi { get; set; } = "";
		public string SchoolName { get; set; } = "";
		public string SchoolEMISCode { get; set; } = "";
		public int? SchoolTypeId { get; set; }
		public int? SchoolDistrictId { get; set; }
		public int? SchoolLocalLevelId { get; set; }
		public int? SchoolWardNo { get; set; }
		public string SchoolToleStreet { get; set; } = "";
		public int? AppliedSubjectId { get; set; }
		public int? ScholarshipTypeId { get; set; }
		public DateTime? PovCerti_IssuedDate { get; set; }
		public string PovCerti_RefNo { get; set; } = "";
		public int? PovCerti_IssuedDistrictId { get; set; }
		public int? PovCerti_IssuedLocalLevelId { get; set; }
		public int? PovCerti_WardNo { get; set; }
		public string PovCerti_ToleStreet { get; set; } = "";
		public string IssuerName { get; set; } = "";
		public string IssuerDesignation { get; set; } = "";
		public string Poverty_CertiFilePath { get; set; } = "";
		public DateTime? GovSchoolCerti_IssuedDate { get; set; }
		public string GovSchoolCerti_RefNo { get; set; } = "";
		public string GovSchoolCertiPath { get; set; } = "";

		//public int? ConcernedAuthorityId { get; set; }
		//public int? GrpCerti_IssuedDistrictId { get; set; }
		//public int? GrpCerti_IssuedLocalLevelId { get; set; }
		//public int? GrpCertiIssue_WardNo { get; set; }
		//public string GrpCertiIssue_ToleStreet { get; set; } = "";
		//public DateTime? GroupWiseCerti_IssuedDate { get; set; }
		//public string GroupWiseCerti_RefNo { get; set; } = "";
		//public string GroupWiseCerti_Path { get; set; } = "";		
		public byte[] Photo { get; set; }
		public string attachFile { get; set; } = "";

		public string P_Province { get; set; } = "";
		public string P_District { get; set; } = "";
		public string P_LocalLevel { get; set; } = "";
		public string Temp_Province { get; set; } = "";
		public string Temp_District { get; set; } = "";
		public string Temp_LocalLevel { get; set; } = "";

		public string SchoolDistrict { get; set; } = "";
		public string SchoolLocalLevel { get; set; } = "";
		public string PovCerti_IssuedDistrict { get; set; } = "";
		public string PovCerti_IssuedLocalLevel { get; set; } = "";

		//public string GrpCerti_IssuedDistrict { get; set; } = "";
		//public string GrpCerti_IssuedLocalLevel { get; set; } = "";

		public string BC_IssuedDistrict { get; set; } = "";
		public string BC_IssuedLocalLevel { get; set; } = "";

		public string CtznshipFront_FilePath { get; set; } = "";
		public string CtznshipBack_FilePath { get; set; } = "";
		public DateTime? Certi_IssuedDate { get; set; }
		public string GenderName { get; set; } = "";
		public string PermanentAddress { get; set; } = "";
		public string CandidateName { get; set; } = "";

		//Field Added by bivek starts
		public DateTime? DOB_AD { get; set; }
		public string PhotoPath { get; set; } = "";
		public string SignaturePath { get; set; } = "";
		public int? Mig_WardId { get; set; }
		public DateTime? MigDoc_IssuedDate { get; set; }
		public string MigDoc_RefNo { get; set; } = "";
		public string MigDocPath { get; set; } = "";
		public int? LandfilDistrictId { get; set; }
		public string LandfilDistrict { get; set; } = "";
		public int? LandfillLocalLevelId { get; set; }
		public string LandfillLocalLevel { get; set; } = "";
		public int? LandfillWardNo { get; set; }
		public DateTime? LandFill_IssuedDate { get; set; }
		public string LandFill_RefNo { get; set; } = "";
		public string LandFillDocPath { get; set; } = "";

		public string IPAddress { get; set; } = "";
		public string Agent { get; set; } = "";
		public string Browser { get; set; } = "";
		public string GeoLocation { get; set; } = "";
		public double? Lat { get; set; }
		public double? Lng { get; set; }

		//Ends
		public Scholarship()
		{
			SchoolPriorityListColl = new SchoolPriorityListCollections();
			ReservationGroupList = new ReservationListCollections();
		}

		public DateTime? Anusuchi3Doc_IssuedDate { get; set; }
		public string Anusuchi3Doc_RefNo { get; set; } = "";
		public string Anusuchi3DocPath { get; set; } = "";
		public SchoolPriorityListCollections SchoolPriorityListColl { get; set; }
		public ReservationListCollections ReservationGroupList { get; set; }

		//Adde by suresh for preview Modal
		public string DOBMiti { get; set; } = "";
		public string BC_IssuedMiti { get; set; } = "";
		public string Certi_IssuedMiti { get; set; } = "";
		public string GovSchoolCertiMiti { get; set; } = "";
		public string MigDoc_IssuedMiti { get; set; } = "";
		public string Anusuchi3Doc_IssuedMiti { get; set; } = "";
		public string BoardName { get; set; } = "";
		public string SubjectName { get; set; } = "";
		public string LandfillDoc_IssuedMiti { get; set; } = "";
		public string ScholarshipTypeName { get; set; } = "";
		public string AppliedMiti { get; set; } = "";
		public string RequestStatus { get; set; } = "";
		public string RequestMiti { get; set; } = "";
		public string StatusUpdateMiti { get; set; } = "";
		public string ReviewByStudent { get; set; } = "";
		public string Verifiedby { get; set; } = "";
		public string DayOne { get; set; } = "";
		public string DayTwo { get; set; } = "";
		public string DayThree { get; set; } = "";
		public int? ClassId { get; set; }
		public string RelatedSchoolRefNo { get; set; }
		public DateTime? RelatedSchoolIssueDate { get; set; }
		public string RelatedSchoolFilePath { get; set; }
		public string ClassName { get; set; }
		public string RelatedSchoolIssueMiti { get; set; }
		public string GradeSheet_CertiPath { get; set; } = "";

        public int? Status { get; set; }
    }
	public class SchoolPriorityList
	{

		public int TranId { get; set; }
		public int? SchoolId { get; set; }
		public int? PriorityId { get; set; }
	}

	public class ReservationList
	{

		public int TranId { get; set; }
		public int? ReservationGroupId { get; set; }
		public int? ConcernedAuthorityId { get; set; }
		public int? GrpCerti_IssuedDistrictId { get; set; }
		public string GrpCerti_IssuedDistrict { get; set; } = "";
		public int? GrpCerti_IssuedLocalLevelId { get; set; }
		public string GrpCerti_IssuedLocalLevel { get; set; } = "";
		public int? GrpCertiIssue_WardNo { get; set; }
		public string GrpCertiIssue_ToleStreet { get; set; } = "";
		public DateTime? GroupWiseCerti_IssuedDate { get; set; }
		public string GroupWiseCerti_RefNo { get; set; } = "";
		public string GroupWiseCerti_Path { get; set; } = "";
		public string ReservationGroupName { get; set; } = "";
		public string ConcernedAuthorityName { get; set; } = "";
		public string GroupWiseCertiMiti { get; set; } = "";

	}
	public class SchoolPriorityListCollections : System.Collections.Generic.List<SchoolPriorityList>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class ReservationListCollections : System.Collections.Generic.List<ReservationList>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class ScholarshipCollections : System.Collections.Generic.List<Scholarship>
	{
		public ScholarshipCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
		public int TotalRows { get; set; }
	}

	public class SchoolSubjectwise : ResponeValues
	{

		public int? SchoolId { get; set; }
		public string Name { get; set; } = "";
		public int? SubjectId { get; set; }
	}
	public class SchoolSubjectwiseCollections : System.Collections.Generic.List<SchoolSubjectwise>
	{
		public SchoolSubjectwiseCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}


	public class GradeSheet : ResponeValues
	{

		public int? TranId { get; set; }
		public string StudentName { get; set; } = "";
		public DateTime? DOB_AD { get; set; }
		public string DOB_BS { get; set; }
		public int? RollNo { get; set; }
		public string SEESymbolNo { get; set; } = "";
		public string Alphabet { get; set; } = "";
		public string SchoolName { get; set; } = "";
		public int? SNO { get; set; }
		public string Code { get; set; } = "";
		public string SubjectName { get; set; } = "";
		public string CreditHour { get; set; } = "";
		public string Grade { get; set; } = "";
		public double? GradePoint { get; set; }
		public string FinalGrade { get; set; } = "";
		public string Remarks { get; set; } = "";
		public double? GPA { get; set; }
		public double? Avg_GPA { get; set; }

		public AcademicLib.BE.Scholarship.Scholarship Scholarship { get; set; } = new Scholarship();
		public BE.Scholarship.ScholarshipDocVerify DVerify { get; set; } = new ScholarshipDocVerify();
	}
	public class GradeSheetCollections : System.Collections.Generic.List<GradeSheet>
	{
		public GradeSheetCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
        public int TotalRows { get; set; }
    }
}

