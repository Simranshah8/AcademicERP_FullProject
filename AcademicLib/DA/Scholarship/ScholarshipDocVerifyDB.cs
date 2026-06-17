using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Scholarship
{

	internal class ScholarshipDocVerifyDB
	{
		DataAccessLayer1 dal = null;
		public ScholarshipDocVerifyDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Scholarship.ScholarshipDocVerify beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@TranId", beData.TranId);
			cmd.Parameters.AddWithValue("@V_Photo", beData.V_Photo);
			cmd.Parameters.AddWithValue("@V_Signature", beData.V_Signature);
			cmd.Parameters.AddWithValue("@V_Document", beData.V_Document);
			cmd.Parameters.AddWithValue("@V_CandidateName", beData.V_CandidateName);
			cmd.Parameters.AddWithValue("@V_Gender", beData.V_Gender);
			cmd.Parameters.AddWithValue("@V_DOB", beData.V_DOB);
			cmd.Parameters.AddWithValue("@V_FatherName", beData.V_FatherName);
			cmd.Parameters.AddWithValue("@V_MotherName", beData.V_MotherName);
			cmd.Parameters.AddWithValue("@V_GrandfatherName", beData.V_GrandfatherName);
			cmd.Parameters.AddWithValue("@V_Email", beData.V_Email);
			cmd.Parameters.AddWithValue("@V_MobileNo", beData.V_MobileNo);
			cmd.Parameters.AddWithValue("@V_PAddress", beData.V_PAddress);
			cmd.Parameters.AddWithValue("@V_TempAddress", beData.V_TempAddress);
			cmd.Parameters.AddWithValue("@V_BCCNo", beData.V_BCCNo);
			cmd.Parameters.AddWithValue("@V_BCCIssuedDate", beData.V_BCCIssuedDate);
			cmd.Parameters.AddWithValue("@V_BCCIssuedDistrict", beData.V_BCCIssuedDistrict);
			cmd.Parameters.AddWithValue("@V_BCCIssuedLocalLevel", beData.V_BCCIssuedLocalLevel);
			cmd.Parameters.AddWithValue("@V_SchoolName", beData.V_SchoolName);
			cmd.Parameters.AddWithValue("@V_SchoolType", beData.V_SchoolType);
			cmd.Parameters.AddWithValue("@V_SchoolDistrict", beData.V_SchoolDistrict);
			cmd.Parameters.AddWithValue("@V_SchoolLocalLevel", beData.V_SchoolLocalLevel);
			cmd.Parameters.AddWithValue("@V_SchoolWardNo", beData.V_SchoolWardNo);
			cmd.Parameters.AddWithValue("@V_Character_Transfer_Certi", beData.V_Character_Transfer_Certi);
			cmd.Parameters.AddWithValue("@V_Character_Transfer_CertiDate", beData.V_Character_Transfer_CertiDate);
			cmd.Parameters.AddWithValue("@V_ScholarshipType", beData.V_ScholarshipType);
			cmd.Parameters.AddWithValue("@V_GovSchoolCertiPath", beData.V_GovSchoolCertiPath);
			cmd.Parameters.AddWithValue("@V_GovSchoolCertiMiti", beData.V_GovSchoolCertiMiti);
			cmd.Parameters.AddWithValue("@V_GovSchoolCerti_RefNo", beData.V_GovSchoolCerti_RefNo);
			cmd.Parameters.AddWithValue("@V_Anusuchi3DocPath", beData.V_Anusuchi3DocPath);
			cmd.Parameters.AddWithValue("@V_Anusuchi3Doc_IssuedMiti", beData.V_Anusuchi3Doc_IssuedMiti);
			cmd.Parameters.AddWithValue("@V_Anusuchi3Doc_RefNo", beData.V_Anusuchi3Doc_RefNo);
			cmd.Parameters.AddWithValue("@V_MigDocPath", beData.V_MigDocPath);
			cmd.Parameters.AddWithValue("@V_Mig_WardId", beData.V_Mig_WardId);
			cmd.Parameters.AddWithValue("@V_MigDoc_IssuedMiti", beData.V_MigDoc_IssuedMiti);
			cmd.Parameters.AddWithValue("@V_MigDoc_RefNo", beData.V_MigDoc_RefNo);
			cmd.Parameters.AddWithValue("@V_LandFillDocPath", beData.V_LandFillDocPath);
			cmd.Parameters.AddWithValue("@V_LandfilDistrict", beData.V_LandfilDistrict);
			cmd.Parameters.AddWithValue("@V_LandfillLocalLevel", beData.V_LandfillLocalLevel);
			cmd.Parameters.AddWithValue("@V_LandfillWardNo", beData.V_LandfillWardNo);
			cmd.Parameters.AddWithValue("@V_LandfillDoc_IssuedMiti", beData.V_LandfillDoc_IssuedMiti);
			cmd.Parameters.AddWithValue("@V_LandFill_RefNo", beData.V_LandFill_RefNo);
			cmd.Parameters.AddWithValue("@V_Status", beData.V_Status);
			cmd.Parameters.AddWithValue("@Email", beData.Email);
			cmd.Parameters.AddWithValue("@V_Subject", beData.V_Subject);
			cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@VerifyId", beData.VerifyId);

			//48
			cmd.CommandText = "usp_AddScholarshipDocVerify";
			//if (isModify)
			//{
			//	cmd.CommandText = "usp_UpdateScholarshipDocVerify";
			//}
			//else
			//{
			//	cmd.Parameters[48].Direction = System.Data.ParameterDirection.Output;
			//	cmd.CommandText = "usp_AddScholarshipDocVerify";
			//}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[48].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[49].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[50].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[51].Direction = System.Data.ParameterDirection.Output;

			//Added By suresh on 2 shrawan for birgunj class 9
			cmd.Parameters.AddWithValue("@V_Gradesheet_Certi", beData.V_Gradesheet_Certi);
			cmd.Parameters.AddWithValue("@V_RelatedSchoolFilePath", beData.V_RelatedSchoolFilePath);
			cmd.Parameters.AddWithValue("@V_RelatedSchoolIssueMiti", beData.V_RelatedSchoolIssueMiti);
			cmd.Parameters.AddWithValue("@V_RelatedSchoolRefNo", beData.V_RelatedSchoolRefNo);
			cmd.Parameters.AddWithValue("@SMSText", beData.SMSText);
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[48].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[48].Value);

				if (!(cmd.Parameters[49].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[49].Value);

				if (!(cmd.Parameters[50].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[50].Value);

				if (!(cmd.Parameters[51].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[51].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveReservationGroupVerifyDetails(beData.CUserId,beData.TranId.Value, resVal.RId, beData.ReservationGroupList);
				}
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

		public ResponeValues DeleteById(int UserId, int EntityId, int VerifyId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@VerifyId", VerifyId);
			cmd.CommandText = "usp_DelScholarshipDocVerifyById";
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
		public BE.Scholarship.ScholarshipDocVerifyCollections getAllScholarshipDocVerify(int UserId, int EntityId)
		{
			BE.Scholarship.ScholarshipDocVerifyCollections dataColl = new BE.Scholarship.ScholarshipDocVerifyCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllScholarshipDocVerify";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Scholarship.ScholarshipDocVerify beData = new BE.Scholarship.ScholarshipDocVerify();
					if (!(reader[0] is DBNull)) beData.VerifyId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TranId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.V_Photo = Convert.ToBoolean(reader[2]);
					if (!(reader[3] is DBNull)) beData.V_Signature = Convert.ToBoolean(reader[3]);
					if (!(reader[4] is DBNull)) beData.V_Document = Convert.ToBoolean(reader[4]);
					if (!(reader[5] is DBNull)) beData.V_CandidateName = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) beData.V_Gender = Convert.ToBoolean(reader[6]);
					if (!(reader[7] is DBNull)) beData.V_DOB = Convert.ToBoolean(reader[7]);
					if (!(reader[8] is DBNull)) beData.V_FatherName = Convert.ToBoolean(reader[8]);
					if (!(reader[9] is DBNull)) beData.V_MotherName = Convert.ToBoolean(reader[9]);
					if (!(reader[10] is DBNull)) beData.V_GrandfatherName = Convert.ToBoolean(reader[10]);
					if (!(reader[11] is DBNull)) beData.V_Email = Convert.ToBoolean(reader[11]);
					if (!(reader[12] is DBNull)) beData.V_MobileNo = Convert.ToBoolean(reader[12]);
					if (!(reader[13] is DBNull)) beData.V_PAddress = Convert.ToBoolean(reader[13]);
					if (!(reader[14] is DBNull)) beData.V_TempAddress = Convert.ToBoolean(reader[14]);
					if (!(reader[15] is DBNull)) beData.V_BCCNo = Convert.ToBoolean(reader[15]);
					if (!(reader[16] is DBNull)) beData.V_BCCIssuedDate = Convert.ToBoolean(reader[16]);
					if (!(reader[17] is DBNull)) beData.V_BCCIssuedDistrict = Convert.ToBoolean(reader[17]);
					if (!(reader[18] is DBNull)) beData.V_BCCIssuedLocalLevel = Convert.ToBoolean(reader[18]);
					if (!(reader[19] is DBNull)) beData.V_SchoolName = Convert.ToBoolean(reader[19]);
					if (!(reader[20] is DBNull)) beData.V_SchoolType = Convert.ToBoolean(reader[20]);
					if (!(reader[21] is DBNull)) beData.V_SchoolDistrict = Convert.ToBoolean(reader[21]);
					if (!(reader[22] is DBNull)) beData.V_SchoolLocalLevel = Convert.ToBoolean(reader[22]);
					if (!(reader[23] is DBNull)) beData.V_SchoolWardNo = Convert.ToBoolean(reader[23]);
					if (!(reader[24] is DBNull)) beData.V_Character_Transfer_Certi = Convert.ToBoolean(reader[24]);
					if (!(reader[25] is DBNull)) beData.V_Character_Transfer_CertiDate = Convert.ToBoolean(reader[25]);
					if (!(reader[26] is DBNull)) beData.V_ScholarshipType = Convert.ToBoolean(reader[26]);
					if (!(reader[27] is DBNull)) beData.V_GovSchoolCertiPath = Convert.ToBoolean(reader[27]);
					if (!(reader[28] is DBNull)) beData.V_GovSchoolCertiMiti = Convert.ToBoolean(reader[28]);
					if (!(reader[29] is DBNull)) beData.V_GovSchoolCerti_RefNo = Convert.ToBoolean(reader[29]);
					if (!(reader[30] is DBNull)) beData.V_Anusuchi3DocPath = Convert.ToBoolean(reader[30]);
					if (!(reader[31] is DBNull)) beData.V_Anusuchi3Doc_IssuedMiti = Convert.ToBoolean(reader[31]);
					if (!(reader[32] is DBNull)) beData.V_Anusuchi3Doc_RefNo = Convert.ToBoolean(reader[32]);
					if (!(reader[33] is DBNull)) beData.V_MigDocPath = Convert.ToBoolean(reader[33]);
					if (!(reader[34] is DBNull)) beData.V_Mig_WardId = Convert.ToBoolean(reader[34]);
					if (!(reader[35] is DBNull)) beData.V_MigDoc_IssuedMiti = Convert.ToBoolean(reader[35]);
					if (!(reader[36] is DBNull)) beData.V_MigDoc_RefNo = Convert.ToBoolean(reader[36]);
					if (!(reader[37] is DBNull)) beData.V_LandFillDocPath = Convert.ToBoolean(reader[37]);
					if (!(reader[38] is DBNull)) beData.V_LandfilDistrict = Convert.ToBoolean(reader[38]);
					if (!(reader[39] is DBNull)) beData.V_LandfillLocalLevel = Convert.ToBoolean(reader[39]);
					if (!(reader[40] is DBNull)) beData.V_LandfillWardNo = Convert.ToBoolean(reader[40]);
					if (!(reader[41] is DBNull)) beData.V_LandfillDoc_IssuedMiti = Convert.ToBoolean(reader[41]);
					if (!(reader[42] is DBNull)) beData.V_LandFill_RefNo = Convert.ToBoolean(reader[42]);
					if (!(reader[43] is DBNull)) beData.V_Status = reader.GetInt32(43);
					if (!(reader[44] is DBNull)) beData.Email = reader.GetString(44);
					if (!(reader[45] is DBNull)) beData.V_Subject = reader.GetString(45);
					if (!(reader[46] is DBNull)) beData.Remarks = reader.GetString(46);
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
		private void SaveReservationGroupVerifyDetails(int UserId,int TranId, int VerifyId, BE.Scholarship.ReservationGroupVerifyCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || VerifyId == 0)
				return;

			foreach (BE.Scholarship.ReservationGroupVerify beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@ReservationGroupId", beData.ReservationGroupId);
				cmd.Parameters.AddWithValue("@V_GroupWiseCerti_Path", beData.V_GroupWiseCerti_Path);
				cmd.Parameters.AddWithValue("@V_ConcernedAuthorityId", beData.V_ConcernedAuthorityId);
				cmd.Parameters.AddWithValue("@V_GrpCerti_IssuedDistrict", beData.V_GrpCerti_IssuedDistrict);
				cmd.Parameters.AddWithValue("@V_ISSUED_LOCALLEVEL", beData.V_ISSUED_LOCALLEVEL);
				cmd.Parameters.AddWithValue("@V_GroupWiseCertiMiti", beData.V_GroupWiseCertiMiti);
				cmd.Parameters.AddWithValue("@V_GroupWiseCerti_RefNo", beData.V_GroupWiseCerti_RefNo);
				cmd.Parameters.AddWithValue("@VerifyId", VerifyId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.Parameters.AddWithValue("@TranId", TranId);

				cmd.CommandText = "usp_AddReservationGroupVerifyDetails";
				cmd.ExecuteNonQuery();
			}

		}

		public BE.Scholarship.ScholarshipDocVerify getScholarshipDocVerifyById(int UserId, int EntityId, int TranId)
		{
			BE.Scholarship.ScholarshipDocVerify beData = new BE.Scholarship.ScholarshipDocVerify();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetScholarshipDocVerifyById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Scholarship.ScholarshipDocVerify();
					if (!(reader[0] is DBNull)) beData.VerifyId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TranId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.V_Photo = Convert.ToBoolean(reader[2]);
					if (!(reader[3] is DBNull)) beData.V_Signature = Convert.ToBoolean(reader[3]);
					if (!(reader[4] is DBNull)) beData.V_Document = Convert.ToBoolean(reader[4]);
					if (!(reader[5] is DBNull)) beData.V_CandidateName = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) beData.V_Gender = Convert.ToBoolean(reader[6]);
					if (!(reader[7] is DBNull)) beData.V_DOB = Convert.ToBoolean(reader[7]);
					if (!(reader[8] is DBNull)) beData.V_FatherName = Convert.ToBoolean(reader[8]);
					if (!(reader[9] is DBNull)) beData.V_MotherName = Convert.ToBoolean(reader[9]);
					if (!(reader[10] is DBNull)) beData.V_GrandfatherName = Convert.ToBoolean(reader[10]);
					if (!(reader[11] is DBNull)) beData.V_Email = Convert.ToBoolean(reader[11]);
					if (!(reader[12] is DBNull)) beData.V_MobileNo = Convert.ToBoolean(reader[12]);
					if (!(reader[13] is DBNull)) beData.V_PAddress = Convert.ToBoolean(reader[13]);
					if (!(reader[14] is DBNull)) beData.V_TempAddress = Convert.ToBoolean(reader[14]);
					if (!(reader[15] is DBNull)) beData.V_BCCNo = Convert.ToBoolean(reader[15]);
					if (!(reader[16] is DBNull)) beData.V_BCCIssuedDate = Convert.ToBoolean(reader[16]);
					if (!(reader[17] is DBNull)) beData.V_BCCIssuedDistrict = Convert.ToBoolean(reader[17]);
					if (!(reader[18] is DBNull)) beData.V_BCCIssuedLocalLevel = Convert.ToBoolean(reader[18]);
					if (!(reader[19] is DBNull)) beData.V_SchoolName = Convert.ToBoolean(reader[19]);
					if (!(reader[20] is DBNull)) beData.V_SchoolType = Convert.ToBoolean(reader[20]);
					if (!(reader[21] is DBNull)) beData.V_SchoolDistrict = Convert.ToBoolean(reader[21]);
					if (!(reader[22] is DBNull)) beData.V_SchoolLocalLevel = Convert.ToBoolean(reader[22]);
					if (!(reader[23] is DBNull)) beData.V_SchoolWardNo = Convert.ToBoolean(reader[23]);
					if (!(reader[24] is DBNull)) beData.V_Character_Transfer_Certi = Convert.ToBoolean(reader[24]);
					if (!(reader[25] is DBNull)) beData.V_Character_Transfer_CertiDate = Convert.ToBoolean(reader[25]);
					if (!(reader[26] is DBNull)) beData.V_ScholarshipType = Convert.ToBoolean(reader[26]);
					if (!(reader[27] is DBNull)) beData.V_GovSchoolCertiPath = Convert.ToBoolean(reader[27]);
					if (!(reader[28] is DBNull)) beData.V_GovSchoolCertiMiti = Convert.ToBoolean(reader[28]);
					if (!(reader[29] is DBNull)) beData.V_GovSchoolCerti_RefNo = Convert.ToBoolean(reader[29]);
					if (!(reader[30] is DBNull)) beData.V_Anusuchi3DocPath = Convert.ToBoolean(reader[30]);
					if (!(reader[31] is DBNull)) beData.V_Anusuchi3Doc_IssuedMiti = Convert.ToBoolean(reader[31]);
					if (!(reader[32] is DBNull)) beData.V_Anusuchi3Doc_RefNo = Convert.ToBoolean(reader[32]);
					if (!(reader[33] is DBNull)) beData.V_MigDocPath = Convert.ToBoolean(reader[33]);
					if (!(reader[34] is DBNull)) beData.V_Mig_WardId = Convert.ToBoolean(reader[34]);
					if (!(reader[35] is DBNull)) beData.V_MigDoc_IssuedMiti = Convert.ToBoolean(reader[35]);
					if (!(reader[36] is DBNull)) beData.V_MigDoc_RefNo = Convert.ToBoolean(reader[36]);
					if (!(reader[37] is DBNull)) beData.V_LandFillDocPath = Convert.ToBoolean(reader[37]);
					if (!(reader[38] is DBNull)) beData.V_LandfilDistrict = Convert.ToBoolean(reader[38]);
					if (!(reader[39] is DBNull)) beData.V_LandfillLocalLevel = Convert.ToBoolean(reader[39]);
					if (!(reader[40] is DBNull)) beData.V_LandfillWardNo = Convert.ToBoolean(reader[40]);
					if (!(reader[41] is DBNull)) beData.V_LandfillDoc_IssuedMiti = Convert.ToBoolean(reader[41]);
					if (!(reader[42] is DBNull)) beData.V_LandFill_RefNo = Convert.ToBoolean(reader[42]);
					if (!(reader[43] is DBNull)) beData.V_Status = reader.GetInt32(43);
					if (!(reader[44] is DBNull)) beData.Email = reader.GetString(44);
					if (!(reader[45] is DBNull)) beData.V_Subject = reader.GetString(45);
					if (!(reader[46] is DBNull)) beData.Remarks = reader.GetString(46);
					if (!(reader[47] is DBNull)) beData.V_Gradesheet_Certi = Convert.ToBoolean(reader[47]);
					if (!(reader[48] is DBNull)) beData.V_RelatedSchoolFilePath = Convert.ToBoolean(reader[48]);
					if (!(reader[49] is DBNull)) beData.V_RelatedSchoolIssueMiti = Convert.ToBoolean(reader[49]);
					if (!(reader[50] is DBNull)) beData.V_RelatedSchoolRefNo = Convert.ToBoolean(reader[50]);
				}
				reader.NextResult();
				beData.ReservationGroupList = new BE.Scholarship.ReservationGroupVerifyCollections();
				while (reader.Read())
				{
					BE.Scholarship.ReservationGroupVerify det1 = new BE.Scholarship.ReservationGroupVerify();
					if (!(reader[0] is DBNull)) det1.VerifyId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.ReservationGroupId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) det1.V_GroupWiseCerti_Path = Convert.ToBoolean(reader[2]);
					if (!(reader[3] is DBNull)) det1.V_ConcernedAuthorityId = Convert.ToBoolean(reader[3]);
					if (!(reader[4] is DBNull)) det1.V_GrpCerti_IssuedDistrict = Convert.ToBoolean(reader[4]);
					if (!(reader[5] is DBNull)) det1.V_ISSUED_LOCALLEVEL = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) det1.V_GroupWiseCertiMiti = Convert.ToBoolean(reader[6]);
					if (!(reader[7] is DBNull)) det1.V_GroupWiseCerti_RefNo = Convert.ToBoolean(reader[7]);
					beData.ReservationGroupList.Add(det1);
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

