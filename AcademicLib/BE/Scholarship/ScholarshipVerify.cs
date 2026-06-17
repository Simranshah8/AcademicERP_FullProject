using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Scholarship
{

	public class ScholarshipVerify : ResponeValues
	{

		public int? TranId { get; set; }
		public bool V_FirstName { get; set; }
		public bool V_MiddleName { get; set; }
		public bool V_LastName { get; set; }
		public bool V_Gender { get; set; }
		public bool V_DOB { get; set; }
		public bool V_SEESymbolNo { get; set; }
		public bool V_SymbolNoAlphabet { get; set; }
		public bool V_GPA { get; set; }
		public bool V_Email { get; set; }
		public bool V_MobileNo { get; set; }
		public bool V_F_FirstName { get; set; }
		public bool V_F_MiddleName { get; set; }
		public bool V_F_LastName { get; set; }
		public bool V_M_FirstName { get; set; }
		public bool V_M_MiddleName { get; set; }
		public bool V_M_LastName { get; set; }
		public bool V_GF_FirstName { get; set; }
		public bool V_GF_MiddleName { get; set; }
		public bool V_GF_LastName { get; set; }
		public bool V_P_Province { get; set; }
		public bool V_P_District { get; set; }
		public bool V_P_LocalLevel { get; set; }
		public bool V_P_WardNo { get; set; }
		public bool V_P_ToleStreet { get; set; }
		public bool V_Temp_Province { get; set; }
		public bool V_Temp_District { get; set; }
		public bool V_Temp_LocalLevel { get; set; }
		public bool V_Temp_WardNo { get; set; }
		public bool V_Temp_ToleStreet { get; set; }
		public bool V_BC_CertificateType { get; set; }
		public bool V_BC_CertificateNo { get; set; }
		public bool V_BC_IssuedDate { get; set; }
		public bool V_BC_IssuedDistrict { get; set; }
		public bool V_BC_IssuedLocalLevel { get; set; }
		public bool V_BC_DocumentName { get; set; }
		public bool V_BC_FilePath { get; set; }
		public bool V_CtznshipFront_FilePath { get; set; }
		public bool V_CtznshipBack_FilePath { get; set; }
		public bool V_EquivalentBoard { get; set; }
		public bool V_Character_Transfer_Certi { get; set; }
		public bool V_SchoolName { get; set; }
		public bool V_Certi_IssuedDate { get; set; }
		public bool V_SchoolType { get; set; }
		public bool V_SchoolDistrict { get; set; }
		public bool V_SchoolLocalLevel { get; set; }
		public bool V_SchoolWardNo { get; set; }
		public bool V_AppliedSubject { get; set; }
		public bool V_CollegePriority { get; set; }
		public bool V_ScholarshipType { get; set; }
		public bool V_PovCerti_IssuedDate { get; set; }
		public bool V_PovCerti_RefNo { get; set; }
		public bool V_PovCerti_IssuedDistrict { get; set; }
		public bool V_PovCerti_IssuedLocalLevel { get; set; }
		public bool V_PovCerti_WardNo { get; set; }
		public bool V_PovCerti_ToleStreet { get; set; }
		public bool V_IssuerName { get; set; }
		public bool V_IssuerDesignation { get; set; }
		public bool V_Poverty_CertiFilePath { get; set; }
		public bool V_GovSchoolCerti_IssuedDate { get; set; }
		public bool V_GovSchoolCerti_RefNo { get; set; }
		public bool V_GovSchoolCertiPath { get; set; }
		public bool V_ReservationGroup { get; set; }
		public bool V_ConcernedAuthority { get; set; }
		public bool V_GrpCerti_IssuedDistrict { get; set; }
		public bool V_GrpCerti_IssuedLocalLevel { get; set; }
		public bool V_GrpCertiIssue_WardNo { get; set; }
		public bool V_GrpCertiIssue_ToleStreet { get; set; }
		public bool V_GroupWiseCerti_IssuedDate { get; set; }
		public bool V_GroupWiseCerti_RefNo { get; set; }
		public bool V_GroupWiseCerti_Path { get; set; }
		public string Remarks { get; set; } = "";
	}
	public class ScholarshipVerifyCollections : System.Collections.Generic.List<ScholarshipVerify>
	{
		public ScholarshipVerifyCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

