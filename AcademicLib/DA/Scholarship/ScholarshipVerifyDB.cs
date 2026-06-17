using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Scholarship
{

	internal class ScholarshipVerifyDB
	{
		DataAccessLayer1 dal = null;
		public ScholarshipVerifyDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(AcademicLib.BE.Scholarship.ScholarshipVerify beData)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@V_FirstName", beData.V_FirstName);
			cmd.Parameters.AddWithValue("@V_MiddleName", beData.V_MiddleName);
			cmd.Parameters.AddWithValue("@V_LastName", beData.V_LastName);
			cmd.Parameters.AddWithValue("@V_Gender", beData.V_Gender);
			cmd.Parameters.AddWithValue("@V_DOB", beData.V_DOB);
			cmd.Parameters.AddWithValue("@V_SEESymbolNo", beData.V_SEESymbolNo);
			cmd.Parameters.AddWithValue("@V_SymbolNoAlphabet", beData.V_SymbolNoAlphabet);
			cmd.Parameters.AddWithValue("@V_GPA", beData.V_GPA);
			cmd.Parameters.AddWithValue("@V_Email", beData.V_Email);
			cmd.Parameters.AddWithValue("@V_MobileNo", beData.V_MobileNo);
			cmd.Parameters.AddWithValue("@V_F_FirstName", beData.V_F_FirstName);
			cmd.Parameters.AddWithValue("@V_F_MiddleName", beData.V_F_MiddleName);
			cmd.Parameters.AddWithValue("@V_F_LastName", beData.V_F_LastName);
			cmd.Parameters.AddWithValue("@V_M_FirstName", beData.V_M_FirstName);
			cmd.Parameters.AddWithValue("@V_M_MiddleName", beData.V_M_MiddleName);
			cmd.Parameters.AddWithValue("@V_M_LastName", beData.V_M_LastName);
			cmd.Parameters.AddWithValue("@V_GF_FirstName", beData.V_GF_FirstName);
			cmd.Parameters.AddWithValue("@V_GF_MiddleName", beData.V_GF_MiddleName);
			cmd.Parameters.AddWithValue("@V_GF_LastName", beData.V_GF_LastName);
			cmd.Parameters.AddWithValue("@V_P_Province", beData.V_P_Province);
			cmd.Parameters.AddWithValue("@V_P_District", beData.V_P_District);
			cmd.Parameters.AddWithValue("@V_P_LocalLevel", beData.V_P_LocalLevel);
			cmd.Parameters.AddWithValue("@V_P_WardNo", beData.V_P_WardNo);
			cmd.Parameters.AddWithValue("@V_P_ToleStreet", beData.V_P_ToleStreet);
			cmd.Parameters.AddWithValue("@V_Temp_Province", beData.V_Temp_Province);
			cmd.Parameters.AddWithValue("@V_Temp_District", beData.V_Temp_District);
			cmd.Parameters.AddWithValue("@V_Temp_LocalLevel", beData.V_Temp_LocalLevel);
			cmd.Parameters.AddWithValue("@V_Temp_WardNo", beData.V_Temp_WardNo);
			cmd.Parameters.AddWithValue("@V_Temp_ToleStreet", beData.V_Temp_ToleStreet);
			cmd.Parameters.AddWithValue("@V_BC_CertificateType", beData.V_BC_CertificateType);
			cmd.Parameters.AddWithValue("@V_BC_CertificateNo", beData.V_BC_CertificateNo);
			cmd.Parameters.AddWithValue("@V_BC_IssuedDate", beData.V_BC_IssuedDate);
			cmd.Parameters.AddWithValue("@V_BC_IssuedDistrict", beData.V_BC_IssuedDistrict);
			cmd.Parameters.AddWithValue("@V_BC_IssuedLocalLevel", beData.V_BC_IssuedLocalLevel);
			cmd.Parameters.AddWithValue("@V_BC_DocumentName", beData.V_BC_DocumentName);
			cmd.Parameters.AddWithValue("@V_BC_FilePath", beData.V_BC_FilePath);
			cmd.Parameters.AddWithValue("@V_CtznshipFront_FilePath", beData.V_CtznshipFront_FilePath);
			cmd.Parameters.AddWithValue("@V_CtznshipBack_FilePath", beData.V_CtznshipBack_FilePath);
			cmd.Parameters.AddWithValue("@V_EquivalentBoard", beData.V_EquivalentBoard);
			cmd.Parameters.AddWithValue("@V_Character_Transfer_Certi", beData.V_Character_Transfer_Certi);
			cmd.Parameters.AddWithValue("@V_SchoolName", beData.V_SchoolName);
			cmd.Parameters.AddWithValue("@V_Certi_IssuedDate", beData.V_Certi_IssuedDate);
			cmd.Parameters.AddWithValue("@V_SchoolType", beData.V_SchoolType);
			cmd.Parameters.AddWithValue("@V_SchoolDistrict", beData.V_SchoolDistrict);
			cmd.Parameters.AddWithValue("@V_SchoolLocalLevel", beData.V_SchoolLocalLevel);
			cmd.Parameters.AddWithValue("@V_SchoolWardNo", beData.V_SchoolWardNo);
			cmd.Parameters.AddWithValue("@V_AppliedSubject", beData.V_AppliedSubject);
			cmd.Parameters.AddWithValue("@V_CollegePriority", beData.V_CollegePriority);
			cmd.Parameters.AddWithValue("@V_ScholarshipType", beData.V_ScholarshipType);
			cmd.Parameters.AddWithValue("@V_PovCerti_IssuedDate", beData.V_PovCerti_IssuedDate);
			cmd.Parameters.AddWithValue("@V_PovCerti_RefNo", beData.V_PovCerti_RefNo);
			cmd.Parameters.AddWithValue("@V_PovCerti_IssuedDistrict", beData.V_PovCerti_IssuedDistrict);
			cmd.Parameters.AddWithValue("@V_PovCerti_IssuedLocalLevel", beData.V_PovCerti_IssuedLocalLevel);
			cmd.Parameters.AddWithValue("@V_PovCerti_WardNo", beData.V_PovCerti_WardNo);
			cmd.Parameters.AddWithValue("@V_PovCerti_ToleStreet", beData.V_PovCerti_ToleStreet);
			cmd.Parameters.AddWithValue("@V_IssuerName", beData.V_IssuerName);
			cmd.Parameters.AddWithValue("@V_IssuerDesignation", beData.V_IssuerDesignation);
			cmd.Parameters.AddWithValue("@V_Poverty_CertiFilePath", beData.V_Poverty_CertiFilePath);
			cmd.Parameters.AddWithValue("@V_GovSchoolCerti_IssuedDate", beData.V_GovSchoolCerti_IssuedDate);
			cmd.Parameters.AddWithValue("@V_GovSchoolCerti_RefNo", beData.V_GovSchoolCerti_RefNo);
			cmd.Parameters.AddWithValue("@V_GovSchoolCertiPath", beData.V_GovSchoolCertiPath);
			cmd.Parameters.AddWithValue("@V_ReservationGroup", beData.V_ReservationGroup);
			cmd.Parameters.AddWithValue("@V_ConcernedAuthority", beData.V_ConcernedAuthority);
			cmd.Parameters.AddWithValue("@V_GrpCerti_IssuedDistrict", beData.V_GrpCerti_IssuedDistrict);
			cmd.Parameters.AddWithValue("@V_GrpCerti_IssuedLocalLevel", beData.V_GrpCerti_IssuedLocalLevel);
			cmd.Parameters.AddWithValue("@V_GrpCertiIssue_WardNo", beData.V_GrpCertiIssue_WardNo);
			cmd.Parameters.AddWithValue("@V_GrpCertiIssue_ToleStreet", beData.V_GrpCertiIssue_ToleStreet);
			cmd.Parameters.AddWithValue("@V_GroupWiseCerti_IssuedDate", beData.V_GroupWiseCerti_IssuedDate);
			cmd.Parameters.AddWithValue("@V_GroupWiseCerti_RefNo", beData.V_GroupWiseCerti_RefNo);
			cmd.Parameters.AddWithValue("@V_GroupWiseCerti_Path", beData.V_GroupWiseCerti_Path);
			cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
			//70
			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@TranId", beData.TranId);
			//cmd.Parameters[73].Direction = System.Data.ParameterDirection.Output;
			cmd.CommandText = "usp_AddScholarshipVerify";

			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[74].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[75].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[76].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[73].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[73].Value);

				if (!(cmd.Parameters[74].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[74].Value);

				if (!(cmd.Parameters[75].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[75].Value);

				if (!(cmd.Parameters[76].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[76].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

			}
			catch (System.Data.SqlClient.SqlException ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return resVal;

		}

		public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.CommandText = "usp_DelScholarshipVerifyById";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();

				if (!(cmd.Parameters[3].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

				if (!(cmd.Parameters[4].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

				if (!(cmd.Parameters[5].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

			}
			catch (System.Data.SqlClient.SqlException ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return resVal;
		}
		public AcademicLib.BE.Scholarship.ScholarshipVerifyCollections getAllScholarshipVerify(int UserId, int EntityId)
		{
			AcademicLib.BE.Scholarship.ScholarshipVerifyCollections dataColl = new AcademicLib.BE.Scholarship.ScholarshipVerifyCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllScholarshipVerify";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Scholarship.ScholarshipVerify beData = new AcademicLib.BE.Scholarship.ScholarshipVerify();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.V_FirstName = Convert.ToBoolean(reader[1]);
					if (!(reader[2] is DBNull)) beData.V_MiddleName = Convert.ToBoolean(reader[2]);
					if (!(reader[3] is DBNull)) beData.V_LastName = Convert.ToBoolean(reader[3]);
					if (!(reader[4] is DBNull)) beData.V_Gender = Convert.ToBoolean(reader[4]);
					if (!(reader[5] is DBNull)) beData.V_DOB = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) beData.V_SEESymbolNo = Convert.ToBoolean(reader[6]);
					if (!(reader[7] is DBNull)) beData.V_SymbolNoAlphabet = Convert.ToBoolean(reader[7]);
					if (!(reader[8] is DBNull)) beData.V_GPA = Convert.ToBoolean(reader[8]);
					if (!(reader[9] is DBNull)) beData.V_Email = Convert.ToBoolean(reader[9]);
					if (!(reader[10] is DBNull)) beData.V_MobileNo = Convert.ToBoolean(reader[10]);
					if (!(reader[11] is DBNull)) beData.V_F_FirstName = Convert.ToBoolean(reader[11]);
					if (!(reader[12] is DBNull)) beData.V_F_MiddleName = Convert.ToBoolean(reader[12]);
					if (!(reader[13] is DBNull)) beData.V_F_LastName = Convert.ToBoolean(reader[13]);
					if (!(reader[14] is DBNull)) beData.V_M_FirstName = Convert.ToBoolean(reader[14]);
					if (!(reader[15] is DBNull)) beData.V_M_MiddleName = Convert.ToBoolean(reader[15]);
					if (!(reader[16] is DBNull)) beData.V_M_LastName = Convert.ToBoolean(reader[16]);
					if (!(reader[17] is DBNull)) beData.V_GF_FirstName = Convert.ToBoolean(reader[17]);
					if (!(reader[18] is DBNull)) beData.V_GF_MiddleName = Convert.ToBoolean(reader[18]);
					if (!(reader[19] is DBNull)) beData.V_GF_LastName = Convert.ToBoolean(reader[19]);
					if (!(reader[20] is DBNull)) beData.V_P_Province = Convert.ToBoolean(reader[20]);
					if (!(reader[21] is DBNull)) beData.V_P_District = Convert.ToBoolean(reader[21]);
					if (!(reader[22] is DBNull)) beData.V_P_LocalLevel = Convert.ToBoolean(reader[22]);
					if (!(reader[23] is DBNull)) beData.V_P_WardNo = Convert.ToBoolean(reader[23]);
					if (!(reader[24] is DBNull)) beData.V_P_ToleStreet = Convert.ToBoolean(reader[24]);
					if (!(reader[25] is DBNull)) beData.V_Temp_Province = Convert.ToBoolean(reader[25]);
					if (!(reader[26] is DBNull)) beData.V_Temp_District = Convert.ToBoolean(reader[26]);
					if (!(reader[27] is DBNull)) beData.V_Temp_LocalLevel = Convert.ToBoolean(reader[27]);
					if (!(reader[28] is DBNull)) beData.V_Temp_WardNo = Convert.ToBoolean(reader[28]);
					if (!(reader[29] is DBNull)) beData.V_Temp_ToleStreet = Convert.ToBoolean(reader[29]);
					if (!(reader[30] is DBNull)) beData.V_BC_CertificateType = Convert.ToBoolean(reader[30]);
					if (!(reader[31] is DBNull)) beData.V_BC_CertificateNo = Convert.ToBoolean(reader[31]);
					if (!(reader[32] is DBNull)) beData.V_BC_IssuedDate = Convert.ToBoolean(reader[32]);
					if (!(reader[33] is DBNull)) beData.V_BC_IssuedDistrict = Convert.ToBoolean(reader[33]);
					if (!(reader[34] is DBNull)) beData.V_BC_IssuedLocalLevel = Convert.ToBoolean(reader[34]);
					if (!(reader[35] is DBNull)) beData.V_BC_DocumentName = Convert.ToBoolean(reader[35]);
					if (!(reader[36] is DBNull)) beData.V_BC_FilePath = Convert.ToBoolean(reader[36]);
					if (!(reader[37] is DBNull)) beData.V_CtznshipFront_FilePath = Convert.ToBoolean(reader[37]);
					if (!(reader[38] is DBNull)) beData.V_CtznshipBack_FilePath = Convert.ToBoolean(reader[38]);
					if (!(reader[39] is DBNull)) beData.V_EquivalentBoard = Convert.ToBoolean(reader[39]);
					if (!(reader[40] is DBNull)) beData.V_Character_Transfer_Certi = Convert.ToBoolean(reader[40]);
					if (!(reader[41] is DBNull)) beData.V_SchoolName = Convert.ToBoolean(reader[41]);
					if (!(reader[42] is DBNull)) beData.V_Certi_IssuedDate = Convert.ToBoolean(reader[42]);
					if (!(reader[43] is DBNull)) beData.V_SchoolType = Convert.ToBoolean(reader[43]);
					if (!(reader[44] is DBNull)) beData.V_SchoolDistrict = Convert.ToBoolean(reader[44]);
					if (!(reader[45] is DBNull)) beData.V_SchoolLocalLevel = Convert.ToBoolean(reader[45]);
					if (!(reader[46] is DBNull)) beData.V_SchoolWardNo = Convert.ToBoolean(reader[46]);
					if (!(reader[47] is DBNull)) beData.V_AppliedSubject = Convert.ToBoolean(reader[47]);
					if (!(reader[48] is DBNull)) beData.V_CollegePriority = Convert.ToBoolean(reader[48]);
					if (!(reader[49] is DBNull)) beData.V_ScholarshipType = Convert.ToBoolean(reader[49]);
					if (!(reader[50] is DBNull)) beData.V_PovCerti_IssuedDate = Convert.ToBoolean(reader[50]);
					if (!(reader[51] is DBNull)) beData.V_PovCerti_RefNo = Convert.ToBoolean(reader[51]);
					if (!(reader[52] is DBNull)) beData.V_PovCerti_IssuedDistrict = Convert.ToBoolean(reader[52]);
					if (!(reader[53] is DBNull)) beData.V_PovCerti_IssuedLocalLevel = Convert.ToBoolean(reader[53]);
					if (!(reader[54] is DBNull)) beData.V_PovCerti_WardNo = Convert.ToBoolean(reader[54]);
					if (!(reader[55] is DBNull)) beData.V_PovCerti_ToleStreet = Convert.ToBoolean(reader[55]);
					if (!(reader[56] is DBNull)) beData.V_IssuerName = Convert.ToBoolean(reader[56]);
					if (!(reader[57] is DBNull)) beData.V_IssuerDesignation = Convert.ToBoolean(reader[57]);
					if (!(reader[58] is DBNull)) beData.V_Poverty_CertiFilePath = Convert.ToBoolean(reader[58]);
					if (!(reader[59] is DBNull)) beData.V_GovSchoolCerti_IssuedDate = Convert.ToBoolean(reader[59]);
					if (!(reader[60] is DBNull)) beData.V_GovSchoolCerti_RefNo = Convert.ToBoolean(reader[60]);
					if (!(reader[61] is DBNull)) beData.V_GovSchoolCertiPath = Convert.ToBoolean(reader[61]);
					if (!(reader[62] is DBNull)) beData.V_ReservationGroup = Convert.ToBoolean(reader[62]);
					if (!(reader[63] is DBNull)) beData.V_ConcernedAuthority = Convert.ToBoolean(reader[63]);
					if (!(reader[64] is DBNull)) beData.V_GrpCerti_IssuedDistrict = Convert.ToBoolean(reader[64]);
					if (!(reader[65] is DBNull)) beData.V_GrpCerti_IssuedLocalLevel = Convert.ToBoolean(reader[65]);
					if (!(reader[66] is DBNull)) beData.V_GrpCertiIssue_WardNo = Convert.ToBoolean(reader[66]);
					if (!(reader[67] is DBNull)) beData.V_GrpCertiIssue_ToleStreet = Convert.ToBoolean(reader[67]);
					if (!(reader[68] is DBNull)) beData.V_GroupWiseCerti_IssuedDate = Convert.ToBoolean(reader[68]);
					if (!(reader[69] is DBNull)) beData.V_GroupWiseCerti_RefNo = Convert.ToBoolean(reader[69]);
					if (!(reader[70] is DBNull)) beData.V_GroupWiseCerti_Path = Convert.ToBoolean(reader[70]);
					if (!(reader[71] is DBNull)) beData.Remarks = reader.GetString(71);
					dataColl.Add(beData);
				}
				reader.Close();
				dataColl.IsSuccess = true;
				dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ee)
			{
				dataColl.IsSuccess = false;
				dataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return dataColl;

		}
		public AcademicLib.BE.Scholarship.ScholarshipVerify getScholarshipVerifyById(int UserId, int EntityId, int TranId)
		{
			AcademicLib.BE.Scholarship.ScholarshipVerify beData = new AcademicLib.BE.Scholarship.ScholarshipVerify();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetScholarshipVerifyById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new AcademicLib.BE.Scholarship.ScholarshipVerify();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.V_FirstName = Convert.ToBoolean(reader[1]);
					if (!(reader[2] is DBNull)) beData.V_MiddleName = Convert.ToBoolean(reader[2]);
					if (!(reader[3] is DBNull)) beData.V_LastName = Convert.ToBoolean(reader[3]);
					if (!(reader[4] is DBNull)) beData.V_Gender = Convert.ToBoolean(reader[4]);
					if (!(reader[5] is DBNull)) beData.V_DOB = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) beData.V_SEESymbolNo = Convert.ToBoolean(reader[6]);
					if (!(reader[7] is DBNull)) beData.V_SymbolNoAlphabet = Convert.ToBoolean(reader[7]);
					if (!(reader[8] is DBNull)) beData.V_GPA = Convert.ToBoolean(reader[8]);
					if (!(reader[9] is DBNull)) beData.V_Email = Convert.ToBoolean(reader[9]);
					if (!(reader[10] is DBNull)) beData.V_MobileNo = Convert.ToBoolean(reader[10]);
					if (!(reader[11] is DBNull)) beData.V_F_FirstName = Convert.ToBoolean(reader[11]);
					if (!(reader[12] is DBNull)) beData.V_F_MiddleName = Convert.ToBoolean(reader[12]);
					if (!(reader[13] is DBNull)) beData.V_F_LastName = Convert.ToBoolean(reader[13]);
					if (!(reader[14] is DBNull)) beData.V_M_FirstName = Convert.ToBoolean(reader[14]);
					if (!(reader[15] is DBNull)) beData.V_M_MiddleName = Convert.ToBoolean(reader[15]);
					if (!(reader[16] is DBNull)) beData.V_M_LastName = Convert.ToBoolean(reader[16]);
					if (!(reader[17] is DBNull)) beData.V_GF_FirstName = Convert.ToBoolean(reader[17]);
					if (!(reader[18] is DBNull)) beData.V_GF_MiddleName = Convert.ToBoolean(reader[18]);
					if (!(reader[19] is DBNull)) beData.V_GF_LastName = Convert.ToBoolean(reader[19]);
					if (!(reader[20] is DBNull)) beData.V_P_Province = Convert.ToBoolean(reader[20]);
					if (!(reader[21] is DBNull)) beData.V_P_District = Convert.ToBoolean(reader[21]);
					if (!(reader[22] is DBNull)) beData.V_P_LocalLevel = Convert.ToBoolean(reader[22]);
					if (!(reader[23] is DBNull)) beData.V_P_WardNo = Convert.ToBoolean(reader[23]);
					if (!(reader[24] is DBNull)) beData.V_P_ToleStreet = Convert.ToBoolean(reader[24]);
					if (!(reader[25] is DBNull)) beData.V_Temp_Province = Convert.ToBoolean(reader[25]);
					if (!(reader[26] is DBNull)) beData.V_Temp_District = Convert.ToBoolean(reader[26]);
					if (!(reader[27] is DBNull)) beData.V_Temp_LocalLevel = Convert.ToBoolean(reader[27]);
					if (!(reader[28] is DBNull)) beData.V_Temp_WardNo = Convert.ToBoolean(reader[28]);
					if (!(reader[29] is DBNull)) beData.V_Temp_ToleStreet = Convert.ToBoolean(reader[29]);
					if (!(reader[30] is DBNull)) beData.V_BC_CertificateType = Convert.ToBoolean(reader[30]);
					if (!(reader[31] is DBNull)) beData.V_BC_CertificateNo = Convert.ToBoolean(reader[31]);
					if (!(reader[32] is DBNull)) beData.V_BC_IssuedDate = Convert.ToBoolean(reader[32]);
					if (!(reader[33] is DBNull)) beData.V_BC_IssuedDistrict = Convert.ToBoolean(reader[33]);
					if (!(reader[34] is DBNull)) beData.V_BC_IssuedLocalLevel = Convert.ToBoolean(reader[34]);
					if (!(reader[35] is DBNull)) beData.V_BC_DocumentName = Convert.ToBoolean(reader[35]);
					if (!(reader[36] is DBNull)) beData.V_BC_FilePath = Convert.ToBoolean(reader[36]);
					if (!(reader[37] is DBNull)) beData.V_CtznshipFront_FilePath = Convert.ToBoolean(reader[37]);
					if (!(reader[38] is DBNull)) beData.V_CtznshipBack_FilePath = Convert.ToBoolean(reader[38]);
					if (!(reader[39] is DBNull)) beData.V_EquivalentBoard = Convert.ToBoolean(reader[39]);
					if (!(reader[40] is DBNull)) beData.V_Character_Transfer_Certi = Convert.ToBoolean(reader[40]);
					if (!(reader[41] is DBNull)) beData.V_SchoolName = Convert.ToBoolean(reader[41]);
					if (!(reader[42] is DBNull)) beData.V_Certi_IssuedDate = Convert.ToBoolean(reader[42]);
					if (!(reader[43] is DBNull)) beData.V_SchoolType = Convert.ToBoolean(reader[43]);
					if (!(reader[44] is DBNull)) beData.V_SchoolDistrict = Convert.ToBoolean(reader[44]);
					if (!(reader[45] is DBNull)) beData.V_SchoolLocalLevel = Convert.ToBoolean(reader[45]);
					if (!(reader[46] is DBNull)) beData.V_SchoolWardNo = Convert.ToBoolean(reader[46]);
					if (!(reader[47] is DBNull)) beData.V_AppliedSubject = Convert.ToBoolean(reader[47]);
					if (!(reader[48] is DBNull)) beData.V_CollegePriority = Convert.ToBoolean(reader[48]);
					if (!(reader[49] is DBNull)) beData.V_ScholarshipType = Convert.ToBoolean(reader[49]);
					if (!(reader[50] is DBNull)) beData.V_PovCerti_IssuedDate = Convert.ToBoolean(reader[50]);
					if (!(reader[51] is DBNull)) beData.V_PovCerti_RefNo = Convert.ToBoolean(reader[51]);
					if (!(reader[52] is DBNull)) beData.V_PovCerti_IssuedDistrict = Convert.ToBoolean(reader[52]);
					if (!(reader[53] is DBNull)) beData.V_PovCerti_IssuedLocalLevel = Convert.ToBoolean(reader[53]);
					if (!(reader[54] is DBNull)) beData.V_PovCerti_WardNo = Convert.ToBoolean(reader[54]);
					if (!(reader[55] is DBNull)) beData.V_PovCerti_ToleStreet = Convert.ToBoolean(reader[55]);
					if (!(reader[56] is DBNull)) beData.V_IssuerName = Convert.ToBoolean(reader[56]);
					if (!(reader[57] is DBNull)) beData.V_IssuerDesignation = Convert.ToBoolean(reader[57]);
					if (!(reader[58] is DBNull)) beData.V_Poverty_CertiFilePath = Convert.ToBoolean(reader[58]);
					if (!(reader[59] is DBNull)) beData.V_GovSchoolCerti_IssuedDate = Convert.ToBoolean(reader[59]);
					if (!(reader[60] is DBNull)) beData.V_GovSchoolCerti_RefNo = Convert.ToBoolean(reader[60]);
					if (!(reader[61] is DBNull)) beData.V_GovSchoolCertiPath = Convert.ToBoolean(reader[61]);
					if (!(reader[62] is DBNull)) beData.V_ReservationGroup = Convert.ToBoolean(reader[62]);
					if (!(reader[63] is DBNull)) beData.V_ConcernedAuthority = Convert.ToBoolean(reader[63]);
					if (!(reader[64] is DBNull)) beData.V_GrpCerti_IssuedDistrict = Convert.ToBoolean(reader[64]);
					if (!(reader[65] is DBNull)) beData.V_GrpCerti_IssuedLocalLevel = Convert.ToBoolean(reader[65]);
					if (!(reader[66] is DBNull)) beData.V_GrpCertiIssue_WardNo = Convert.ToBoolean(reader[66]);
					if (!(reader[67] is DBNull)) beData.V_GrpCertiIssue_ToleStreet = Convert.ToBoolean(reader[67]);
					if (!(reader[68] is DBNull)) beData.V_GroupWiseCerti_IssuedDate = Convert.ToBoolean(reader[68]);
					if (!(reader[69] is DBNull)) beData.V_GroupWiseCerti_RefNo = Convert.ToBoolean(reader[69]);
					if (!(reader[70] is DBNull)) beData.V_GroupWiseCerti_Path = Convert.ToBoolean(reader[70]);
					if (!(reader[71] is DBNull)) beData.Remarks = reader.GetString(71);
				}
				reader.Close();
				beData.IsSuccess = true;
				beData.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ee)
			{
				beData.IsSuccess = false;
				beData.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return beData;

		}

	}

}

