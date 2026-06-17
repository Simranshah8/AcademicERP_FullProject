using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Scholarship
{

	internal class ScholarshipDB
	{
		DataAccessLayer1 dal = null;
		public ScholarshipDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Scholarship.Scholarship beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@FirstName", beData.FirstName);
			cmd.Parameters.AddWithValue("@MiddleName", beData.MiddleName);
			cmd.Parameters.AddWithValue("@LastName", beData.LastName);
			cmd.Parameters.AddWithValue("@Gender", beData.Gender);
			cmd.Parameters.AddWithValue("@DOB", beData.DOB);
			cmd.Parameters.AddWithValue("@SEESymbolNo", beData.SEESymbolNo);
			cmd.Parameters.AddWithValue("@GPA", Math.Round(beData.GPA.Value,2));
			cmd.Parameters.AddWithValue("@Email", beData.Email);
			cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
			cmd.Parameters.AddWithValue("@F_FirstName", beData.F_FirstName);
			cmd.Parameters.AddWithValue("@F_MiddleName", beData.F_MiddleName);
			cmd.Parameters.AddWithValue("@F_LastName", beData.F_LastName);
			cmd.Parameters.AddWithValue("@M_FirstName", beData.M_FirstName);
			cmd.Parameters.AddWithValue("@M_MiddleName", beData.M_MiddleName);
			cmd.Parameters.AddWithValue("@M_LastName", beData.M_LastName);
			cmd.Parameters.AddWithValue("@GF_FirstName", beData.GF_FirstName);
			cmd.Parameters.AddWithValue("@GF_MiddleName", beData.GF_MiddleName);
			cmd.Parameters.AddWithValue("@GF_LastName", beData.GF_LastName);
			cmd.Parameters.AddWithValue("@P_ProvinceId", beData.P_ProvinceId);
			cmd.Parameters.AddWithValue("@P_DistrictId", beData.P_DistrictId);
			cmd.Parameters.AddWithValue("@P_LocalLevelId", beData.P_LocalLevelId);
			cmd.Parameters.AddWithValue("@P_WardNo", beData.P_WardNo);
			cmd.Parameters.AddWithValue("@P_ToleStreet", beData.P_ToleStreet);
			cmd.Parameters.AddWithValue("@Temp_ProvinceId", beData.Temp_ProvinceId);
			cmd.Parameters.AddWithValue("@Temp_DistrictId", beData.Temp_DistrictId);
			cmd.Parameters.AddWithValue("@Temp_LocalLevelId", beData.Temp_LocalLevelId);
			cmd.Parameters.AddWithValue("@Temp_WardNo", beData.Temp_WardNo);
			cmd.Parameters.AddWithValue("@Temp_ToleStreet", beData.Temp_ToleStreet);
			cmd.Parameters.AddWithValue("@BC_CertificateTypeId", beData.BC_CertificateTypeId);
			cmd.Parameters.AddWithValue("@BC_CertificateNo", beData.BC_CertificateNo);
			cmd.Parameters.AddWithValue("@BC_IssuedDate", beData.BC_IssuedDate);
			cmd.Parameters.AddWithValue("@BC_IssuedDistrictId", beData.BC_IssuedDistrictId);
			cmd.Parameters.AddWithValue("@BC_IssuedLocalLevelId", beData.BC_IssuedLocalLevelId);
			cmd.Parameters.AddWithValue("@BC_IssuedWardNo", beData.BC_IssuedWardNo);
			cmd.Parameters.AddWithValue("@BC_IssuedToleStreet", beData.BC_IssuedToleStreet);
			cmd.Parameters.AddWithValue("@BC_DocumentNameId", beData.BC_DocumentNameId);
			cmd.Parameters.AddWithValue("@BC_FilePath", beData.BC_FilePath);
			cmd.Parameters.AddWithValue("@EquivalentBoardId", beData.EquivalentBoardId);
			cmd.Parameters.AddWithValue("@GradeSheetFilePath", beData.GradeSheetFilePath);
			cmd.Parameters.AddWithValue("@Character_Transfer_Certi", beData.Character_Transfer_Certi);
			cmd.Parameters.AddWithValue("@SchoolName", beData.SchoolName);
			cmd.Parameters.AddWithValue("@SchoolEMISCode", beData.SchoolEMISCode);
			cmd.Parameters.AddWithValue("@SchoolTypeId", beData.SchoolTypeId);
			cmd.Parameters.AddWithValue("@SchoolDistrictId", beData.SchoolDistrictId);
			cmd.Parameters.AddWithValue("@SchoolLocalLevelId", beData.SchoolLocalLevelId);
			cmd.Parameters.AddWithValue("@SchoolWardNo", beData.SchoolWardNo);
			cmd.Parameters.AddWithValue("@SchoolToleStreet", beData.SchoolToleStreet);
			cmd.Parameters.AddWithValue("@AppliedSubjectId", beData.AppliedSubjectId);
			cmd.Parameters.AddWithValue("@ScholarshipTypeId", beData.ScholarshipTypeId);
			cmd.Parameters.AddWithValue("@PovCerti_IssuedDate", beData.PovCerti_IssuedDate);
			cmd.Parameters.AddWithValue("@PovCerti_RefNo", beData.PovCerti_RefNo);
			cmd.Parameters.AddWithValue("@PovCerti_IssuedDistrictId", beData.PovCerti_IssuedDistrictId);
			cmd.Parameters.AddWithValue("@PovCerti_IssuedLocalLevelId", beData.PovCerti_IssuedLocalLevelId);
			cmd.Parameters.AddWithValue("@PovCerti_WardNo", beData.PovCerti_WardNo);
			cmd.Parameters.AddWithValue("@PovCerti_ToleStreet", beData.PovCerti_ToleStreet);
			cmd.Parameters.AddWithValue("@IssuerName", beData.IssuerName);
			cmd.Parameters.AddWithValue("@IssuerDesignation", beData.IssuerDesignation);
			cmd.Parameters.AddWithValue("@Poverty_CertiFilePath", beData.Poverty_CertiFilePath);
			cmd.Parameters.AddWithValue("@GovSchoolCerti_IssuedDate", beData.GovSchoolCerti_IssuedDate);
			cmd.Parameters.AddWithValue("@GovSchoolCerti_RefNo", beData.GovSchoolCerti_RefNo);
			cmd.Parameters.AddWithValue("@GovSchoolCertiPath", beData.GovSchoolCertiPath);



			cmd.Parameters.AddWithValue("@P_Province", beData.P_Province);
			cmd.Parameters.AddWithValue("@P_District", beData.P_District);
			cmd.Parameters.AddWithValue("@P_LocalLevel", beData.P_LocalLevel);
			cmd.Parameters.AddWithValue("@Temp_Province", beData.Temp_Province);
			cmd.Parameters.AddWithValue("@Temp_District", beData.Temp_District);
			cmd.Parameters.AddWithValue("@Temp_LocalLevel", beData.Temp_LocalLevel);

			cmd.Parameters.AddWithValue("@SchoolDistrict", beData.SchoolDistrict);
			cmd.Parameters.AddWithValue("@SchoolLocalLevel", beData.SchoolLocalLevel);
			cmd.Parameters.AddWithValue("@PovCerti_IssuedDistrict", beData.PovCerti_IssuedDistrict);
			cmd.Parameters.AddWithValue("@PovCerti_IssuedLocalLevel", beData.PovCerti_IssuedLocalLevel);


			cmd.Parameters.AddWithValue("@BC_IssuedDistrict", beData.BC_IssuedDistrict);
			cmd.Parameters.AddWithValue("@BC_IssuedLocalLevel", beData.BC_IssuedLocalLevel);

			cmd.Parameters.AddWithValue("@CtznshipFront_FilePath", beData.CtznshipFront_FilePath);
			cmd.Parameters.AddWithValue("@CtznshipBack_FilePath", beData.CtznshipBack_FilePath);
			cmd.Parameters.AddWithValue("@Certi_IssuedDate", beData.Certi_IssuedDate);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@TranId", beData.TranId);

			cmd.Parameters[78].Direction = System.Data.ParameterDirection.Output;
			cmd.CommandText = "usp_AddScholarship";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters.Add("@Pwd", System.Data.SqlDbType.NVarChar, 100);
			cmd.Parameters[79].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[80].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[81].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[82].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters.AddWithValue("@Alphabet", beData.Alphabet);

			//Field Added By Bivek Starts
			cmd.Parameters.AddWithValue("@DOB_AD", beData.DOB_AD);
			cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
			cmd.Parameters.AddWithValue("@SignaturePath", beData.SignaturePath);
			cmd.Parameters.AddWithValue("@Mig_WardId", beData.Mig_WardId);
			cmd.Parameters.AddWithValue("@MigDoc_IssuedDate", beData.MigDoc_IssuedDate);
			cmd.Parameters.AddWithValue("@MigDoc_RefNo", beData.MigDoc_RefNo);
			cmd.Parameters.AddWithValue("@MigDocPath", beData.MigDocPath);

			cmd.Parameters.AddWithValue("@LandfilDistrictId", beData.LandfilDistrictId);
			cmd.Parameters.AddWithValue("@LandfilDistrict", beData.LandfilDistrict);
			cmd.Parameters.AddWithValue("@LandfillLocalLevelId", beData.LandfillLocalLevelId);
			cmd.Parameters.AddWithValue("@LandfillLocalLevel", beData.LandfillLocalLevel);
			cmd.Parameters.AddWithValue("@LandfillWardNo", beData.LandfillWardNo);
			cmd.Parameters.AddWithValue("@LandFill_IssuedDate", beData.LandFill_IssuedDate);
			cmd.Parameters.AddWithValue("@LandFill_RefNo", beData.LandFill_RefNo);
			cmd.Parameters.AddWithValue("@LandFillDocPath", beData.LandFillDocPath);


			cmd.Parameters.AddWithValue("@IPAddress", beData.IPAddress);
			cmd.Parameters.AddWithValue("@Agent", beData.Agent);
			cmd.Parameters.AddWithValue("@Browser", beData.Browser);
			cmd.Parameters.AddWithValue("@GeoLocation", beData.GeoLocation);
			cmd.Parameters.AddWithValue("@Lat", beData.Lat);
			cmd.Parameters.AddWithValue("@Lng", beData.Lng);

			cmd.Parameters.AddWithValue("@Anusuchi3Doc_IssuedDate", beData.Anusuchi3Doc_IssuedDate);
			cmd.Parameters.AddWithValue("@Anusuchi3Doc_RefNo", beData.Anusuchi3Doc_RefNo);
			cmd.Parameters.AddWithValue("@Anusuchi3DocPath", beData.Anusuchi3DocPath);

			cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
			cmd.Parameters.AddWithValue("@RelatedSchoolFilePath", beData.RelatedSchoolFilePath);
			cmd.Parameters.AddWithValue("@RelatedSchoolIssueDate", beData.RelatedSchoolIssueDate);
			cmd.Parameters.AddWithValue("@RelatedSchoolRefNo", beData.RelatedSchoolRefNo);
			cmd.Parameters.AddWithValue("@GradeSheet_CertiPath", beData.GradeSheet_CertiPath);

			//Ends
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[78].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[78].Value);

				if (!(cmd.Parameters[79].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[79].Value);

				if (!(cmd.Parameters[80].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[80].Value);

				if (!(cmd.Parameters[81].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[81].Value);

				if (!(cmd.Parameters[82].Value is DBNull))
					resVal.ResponseId = Convert.ToString(cmd.Parameters[82].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveSchoolPriorityListDetails(beData.CUserId, resVal.RId, beData.SchoolPriorityListColl);
					SaveReservationGroupList(beData.CUserId, resVal.RId, beData.ReservationGroupList);
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

		public BE.Scholarship.ScholarshipCollections getAllScholarship(TableFilter filter,int? StatusId,int? SubjectId,int? ClassId)
		{
			BE.Scholarship.ScholarshipCollections dataColl = new BE.Scholarship.ScholarshipCollections();

			dal.OpenConnection();

			try
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UserId", filter.UserId);
				cmd.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
				cmd.Parameters.AddWithValue("@RowsOfPage", filter.RowsOfPage);
				cmd.Parameters.AddWithValue("@SearchType", filter.SearchType);
				cmd.Parameters.AddWithValue("@SearchCol", filter.SearchCol);
				cmd.Parameters.AddWithValue("@SearchVal", filter.SearchVal);
				cmd.Parameters.AddWithValue("@SortingCol", filter.SortingCol);
				cmd.Parameters.AddWithValue("@SortType", filter.SortType);
				cmd.Parameters.Add("@TotalRows", System.Data.SqlDbType.Int);
				cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_GetAllScholarship";
				cmd.Parameters.AddWithValue("@StatusId", StatusId);
				cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
				cmd.Parameters.AddWithValue("@ClassId", ClassId);
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Scholarship.Scholarship beData = new BE.Scholarship.Scholarship();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.CandidateName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.GenderName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.DOBMiti = Convert.ToString(reader[3]);
					if (!(reader[4] is DBNull)) beData.SEESymbolNo = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Alphabet = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.GPA = Convert.ToDouble(reader[6]);
					if (!(reader[7] is DBNull)) beData.Email = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.MobileNo = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.PermanentAddress = reader.GetString(9);

					if (!(reader[10] is DBNull)) beData.SubjectName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.AppliedMiti = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.RequestStatus = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.RequestMiti = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.StatusUpdateMiti = reader.GetString(14);
					if (!(reader[15] is DBNull)) beData.ReviewByStudent = reader.GetString(15);
					if (!(reader[16] is DBNull)) beData.Verifiedby = reader.GetString(16);
					if (!(reader[17] is DBNull)) beData.ClassName = reader.GetString(17);
					dataColl.Add(beData);
				}
				reader.Close();

				if (!(cmd.Parameters[8].Value is DBNull))
					dataColl.TotalRows = Convert.ToInt32(cmd.Parameters[8].Value);

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
		public BE.Scholarship.ScholarshipCollections getAllScholarship(int UserId, int EntityId)
		{
			BE.Scholarship.ScholarshipCollections dataColl = new BE.Scholarship.ScholarshipCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllScholarship";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Scholarship.Scholarship beData = new BE.Scholarship.Scholarship();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.CandidateName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.GenderName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.DOB = Convert.ToDateTime(reader[3]);
					if (!(reader[4] is DBNull)) beData.SEESymbolNo = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Alphabet = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.GPA = Convert.ToDouble(reader[6]);
					if (!(reader[7] is DBNull)) beData.Email = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.MobileNo = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.PermanentAddress = reader.GetString(9);

					if (!(reader[10] is DBNull)) beData.SubjectName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.AppliedMiti = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.RequestStatus = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.RequestMiti = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.StatusUpdateMiti = reader.GetString(14);
					if (!(reader[15] is DBNull)) beData.ReviewByStudent = reader.GetString(15);
					if (!(reader[16] is DBNull)) beData.Verifiedby = reader.GetString(16);
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
		private void SaveSchoolPriorityListDetails(int UserId, int TranId, BE.Scholarship.SchoolPriorityListCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
				return;

			int sno = 1;
			foreach (BE.Scholarship.SchoolPriorityList beData in beDataColl)
			{
                if (beData.SchoolId > 0)
                {
					System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
					cmd.CommandType = System.Data.CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@SchoolId", beData.SchoolId);
					cmd.Parameters.AddWithValue("@PriorityId", sno);
					cmd.Parameters.AddWithValue("@TranId", TranId);
					cmd.Parameters.AddWithValue("@UserId", UserId);
					//cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "usp_AddSchoolPriorityListDetails";
					cmd.ExecuteNonQuery();
					sno++;
				}				
			}

		}

		private void SaveReservationGroupList(int UserId, int TranId, BE.Scholarship.ReservationListCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
				return;

			foreach (BE.Scholarship.ReservationList beData in beDataColl)
			{
                if (beData.ReservationGroupId > 0)
                {
					System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
					cmd.CommandType = System.Data.CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@ReservationGroupId", beData.ReservationGroupId);
					cmd.Parameters.AddWithValue("@ConcernedAuthorityId", beData.ConcernedAuthorityId);
					cmd.Parameters.AddWithValue("@GrpCerti_IssuedDistrictId", beData.GrpCerti_IssuedDistrictId);
					cmd.Parameters.AddWithValue("@GrpCerti_IssuedDistrict", beData.GrpCerti_IssuedDistrict);
					cmd.Parameters.AddWithValue("@GrpCerti_IssuedLocalLevelId", beData.GrpCerti_IssuedLocalLevelId);
					cmd.Parameters.AddWithValue("@GrpCerti_IssuedLocalLevel", beData.GrpCerti_IssuedLocalLevel);
					cmd.Parameters.AddWithValue("@GrpCertiIssue_WardNo", beData.GrpCertiIssue_WardNo);
					cmd.Parameters.AddWithValue("@GrpCertiIssue_ToleStreet", beData.GrpCertiIssue_ToleStreet);
					cmd.Parameters.AddWithValue("@GroupWiseCerti_IssuedDate", beData.GroupWiseCerti_IssuedDate);
					cmd.Parameters.AddWithValue("@GroupWiseCerti_RefNo", beData.GroupWiseCerti_RefNo);
					cmd.Parameters.AddWithValue("@GroupWiseCerti_Path", beData.GroupWiseCerti_Path);
					cmd.Parameters.AddWithValue("@TranId", TranId);
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.CommandText = "usp_AddReservationGroupListDetails";
					cmd.ExecuteNonQuery();
				}				
			}
		}


		public BE.Scholarship.Scholarship getScholarshipById(int UserId, int EntityId, int TranId)
		{
			BE.Scholarship.Scholarship beData = new BE.Scholarship.Scholarship();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetScholarshipById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Scholarship.Scholarship();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.FirstName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.MiddleName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.LastName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Gender = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.DOB = Convert.ToDateTime(reader[5]);
					if (!(reader[6] is DBNull)) beData.SEESymbolNo = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.Alphabet = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.GPA = Math.Round(Convert.ToDouble(reader[8]),2);
					if (!(reader[9] is DBNull)) beData.Email = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.MobileNo = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.F_FirstName = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.F_MiddleName = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.F_LastName = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.M_FirstName = reader.GetString(14);
					if (!(reader[15] is DBNull)) beData.M_MiddleName = reader.GetString(15);
					if (!(reader[16] is DBNull)) beData.M_LastName = reader.GetString(16);
					if (!(reader[17] is DBNull)) beData.GF_FirstName = reader.GetString(17);
					if (!(reader[18] is DBNull)) beData.GF_MiddleName = reader.GetString(18);
					if (!(reader[19] is DBNull)) beData.GF_LastName = reader.GetString(19);
					if (!(reader[20] is DBNull)) beData.P_ProvinceId = reader.GetInt32(20);
					if (!(reader[21] is DBNull)) beData.P_Province = reader.GetString(21);
					if (!(reader[22] is DBNull)) beData.P_DistrictId = reader.GetInt32(22);
					if (!(reader[23] is DBNull)) beData.P_District = reader.GetString(23);
					if (!(reader[24] is DBNull)) beData.P_LocalLevelId = reader.GetInt32(24);
					if (!(reader[25] is DBNull)) beData.P_LocalLevel = reader.GetString(25);
					if (!(reader[26] is DBNull)) beData.P_WardNo = reader.GetInt32(26);
					if (!(reader[27] is DBNull)) beData.P_ToleStreet = reader.GetString(27);
					if (!(reader[28] is DBNull)) beData.Temp_ProvinceId = reader.GetInt32(28);
					if (!(reader[29] is DBNull)) beData.Temp_Province = reader.GetString(29);
					if (!(reader[30] is DBNull)) beData.Temp_DistrictId = reader.GetInt32(30);
					if (!(reader[31] is DBNull)) beData.Temp_District = reader.GetString(31);
					if (!(reader[32] is DBNull)) beData.Temp_LocalLevelId = reader.GetInt32(32);
					if (!(reader[33] is DBNull)) beData.Temp_LocalLevel = reader.GetString(33);
					if (!(reader[34] is DBNull)) beData.Temp_WardNo = reader.GetInt32(34);
					if (!(reader[35] is DBNull)) beData.Temp_ToleStreet = reader.GetString(35);
					if (!(reader[36] is DBNull)) beData.BC_CertificateTypeId = reader.GetInt32(36);
					if (!(reader[37] is DBNull)) beData.BC_CertificateNo = reader.GetString(37);
					if (!(reader[38] is DBNull)) beData.BC_IssuedDate = Convert.ToDateTime(reader[38]);
					if (!(reader[39] is DBNull)) beData.BC_IssuedDistrictId = reader.GetInt32(39);
					if (!(reader[40] is DBNull)) beData.BC_IssuedDistrict = reader.GetString(40);
					if (!(reader[41] is DBNull)) beData.BC_IssuedLocalLevelId = reader.GetInt32(41);
					if (!(reader[42] is DBNull)) beData.BC_IssuedLocalLevel = reader.GetString(42);
					if (!(reader[43] is DBNull)) beData.BC_IssuedWardNo = reader.GetInt32(43);
					if (!(reader[44] is DBNull)) beData.BC_IssuedToleStreet = reader.GetString(44);
					if (!(reader[45] is DBNull)) beData.BC_DocumentNameId = reader.GetInt32(45);
					if (!(reader[46] is DBNull)) beData.BC_FilePath = reader.GetString(46);
					if (!(reader[47] is DBNull)) beData.CtznshipFront_FilePath = reader.GetString(47);
					if (!(reader[48] is DBNull)) beData.CtznshipBack_FilePath = reader.GetString(48);
					if (!(reader[49] is DBNull)) beData.EquivalentBoardId = reader.GetInt32(49);
					if (!(reader[50] is DBNull)) beData.GradeSheetFilePath = reader.GetString(50);
					if (!(reader[51] is DBNull)) beData.Character_Transfer_Certi = reader.GetString(51);
					if (!(reader[52] is DBNull)) beData.SchoolName = reader.GetString(52);
					if (!(reader[53] is DBNull)) beData.Certi_IssuedDate = Convert.ToDateTime(reader[53]);
					if (!(reader[54] is DBNull)) beData.SchoolEMISCode = reader.GetString(54);
					if (!(reader[55] is DBNull)) beData.SchoolTypeId = reader.GetInt32(55);
					if (!(reader[56] is DBNull)) beData.SchoolDistrictId = reader.GetInt32(56);
					if (!(reader[57] is DBNull)) beData.SchoolDistrict = reader.GetString(57);
					if (!(reader[58] is DBNull)) beData.SchoolLocalLevelId = reader.GetInt32(58);
					if (!(reader[59] is DBNull)) beData.SchoolLocalLevel = reader.GetString(59);
					if (!(reader[60] is DBNull)) beData.SchoolWardNo = reader.GetInt32(60);
					if (!(reader[61] is DBNull)) beData.SchoolToleStreet = reader.GetString(61);
					if (!(reader[62] is DBNull)) beData.AppliedSubjectId = reader.GetInt32(62);
					if (!(reader[63] is DBNull)) beData.ScholarshipTypeId = reader.GetInt32(63);
					if (!(reader[64] is DBNull)) beData.PovCerti_IssuedDate = Convert.ToDateTime(reader[64]);
					if (!(reader[65] is DBNull)) beData.PovCerti_RefNo = reader.GetString(65);
					if (!(reader[66] is DBNull)) beData.PovCerti_IssuedDistrictId = reader.GetInt32(66);
					if (!(reader[67] is DBNull)) beData.PovCerti_IssuedDistrict = reader.GetString(67);
					if (!(reader[68] is DBNull)) beData.PovCerti_IssuedLocalLevelId = reader.GetInt32(68);
					if (!(reader[69] is DBNull)) beData.PovCerti_IssuedLocalLevel = reader.GetString(69);
					if (!(reader[70] is DBNull)) beData.PovCerti_WardNo = reader.GetInt32(70);
					if (!(reader[71] is DBNull)) beData.PovCerti_ToleStreet = reader.GetString(71);
					if (!(reader[72] is DBNull)) beData.IssuerName = reader.GetString(72);
					if (!(reader[73] is DBNull)) beData.IssuerDesignation = reader.GetString(73);
					if (!(reader[74] is DBNull)) beData.Poverty_CertiFilePath = reader.GetString(74);
					if (!(reader[75] is DBNull)) beData.GovSchoolCerti_IssuedDate = Convert.ToDateTime(reader[75]);
					if (!(reader[76] is DBNull)) beData.GovSchoolCerti_RefNo = reader.GetString(76);
					if (!(reader[77] is DBNull)) beData.GovSchoolCertiPath = reader.GetString(77);

					//Field Added By Bivek Starts
					if (!(reader[78] is DBNull)) beData.DOB_AD = Convert.ToDateTime(reader[78]);
					if (!(reader[79] is DBNull)) beData.PhotoPath = reader.GetString(79);
					if (!(reader[80] is DBNull)) beData.SignaturePath = reader.GetString(80);
					if (!(reader[81] is DBNull)) beData.Mig_WardId = reader.GetInt32(81);
					if (!(reader[82] is DBNull)) beData.MigDoc_IssuedDate = Convert.ToDateTime(reader[82]);
					if (!(reader[83] is DBNull)) beData.MigDoc_RefNo = reader.GetString(83);
					if (!(reader[84] is DBNull)) beData.MigDocPath = reader.GetString(84);
					if (!(reader[85] is DBNull)) beData.LandfilDistrictId = reader.GetInt32(85);
					if (!(reader[86] is DBNull)) beData.LandfilDistrict = reader.GetString(86);
					if (!(reader[87] is DBNull)) beData.LandfillLocalLevelId = reader.GetInt32(87);
					if (!(reader[88] is DBNull)) beData.LandfillLocalLevel = reader.GetString(88);
					if (!(reader[89] is DBNull)) beData.LandfillWardNo = reader.GetInt32(89);
					if (!(reader[90] is DBNull)) beData.LandFill_IssuedDate = Convert.ToDateTime(reader[90]);
					if (!(reader[91] is DBNull)) beData.LandFill_RefNo = reader.GetString(91);
					if (!(reader[92] is DBNull)) beData.LandFillDocPath = reader.GetString(92);
					//Ends

					if (!(reader[93] is DBNull)) beData.Anusuchi3Doc_IssuedDate = Convert.ToDateTime(reader[93]);
					if (!(reader[94] is DBNull)) beData.Anusuchi3Doc_RefNo = reader.GetString(94);
					if (!(reader[95] is DBNull)) beData.Anusuchi3DocPath = reader.GetString(95);

					if (!(reader[96] is DBNull)) beData.DOBMiti = reader.GetString(96);
					if (!(reader[97] is DBNull)) beData.BC_IssuedMiti = reader.GetString(97);
					if (!(reader[98] is DBNull)) beData.Certi_IssuedMiti = reader.GetString(98);
					if (!(reader[99] is DBNull)) beData.GovSchoolCertiMiti = reader.GetString(99);
					if (!(reader[100] is DBNull)) beData.MigDoc_IssuedMiti = reader.GetString(100);
					if (!(reader[101] is DBNull)) beData.Anusuchi3Doc_IssuedMiti = reader.GetString(101);
					if (!(reader[102] is DBNull)) beData.BoardName = reader.GetString(102);
					if (!(reader[103] is DBNull)) beData.SubjectName = reader.GetString(103);
					if (!(reader[104] is DBNull)) beData.LandfillDoc_IssuedMiti = reader.GetString(104);
					if (!(reader[105] is DBNull)) beData.ScholarshipTypeName = reader.GetString(105);

					if (!(reader[106] is DBNull)) beData.DayOne = reader.GetString(106);
					if (!(reader[107] is DBNull)) beData.DayTwo = reader.GetString(107);
					if (!(reader[108] is DBNull)) beData.DayThree = reader.GetString(108);

					try
					{
						if (!(reader[109] is DBNull)) beData.RelatedSchoolRefNo = reader.GetString(109);
						if (!(reader[110] is DBNull)) beData.RelatedSchoolIssueDate = reader.GetDateTime(110);
						if (!(reader[111] is DBNull)) beData.RelatedSchoolFilePath = reader.GetString(111);
						if (!(reader[112] is DBNull)) beData.ClassId = reader.GetInt32(112);
						if (!(reader[113] is DBNull)) beData.ClassName = reader.GetString(113);
						if (!(reader[114] is DBNull)) beData.RelatedSchoolIssueMiti = reader.GetString(114);
						if (!(reader[115] is DBNull)) beData.GradeSheet_CertiPath = reader.GetString(115);

					}
					catch { }
					
				}
				reader.NextResult();
				beData.SchoolPriorityListColl = new BE.Scholarship.SchoolPriorityListCollections();
				while (reader.Read())
				{
					BE.Scholarship.SchoolPriorityList det1 = new BE.Scholarship.SchoolPriorityList();
					if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.SchoolId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) det1.PriorityId = reader.GetInt32(2);
					beData.SchoolPriorityListColl.Add(det1);
				}

				reader.NextResult();
				beData.ReservationGroupList = new BE.Scholarship.ReservationListCollections();
				while (reader.Read())
				{
					BE.Scholarship.ReservationList det2 = new BE.Scholarship.ReservationList();
					if (!(reader[0] is DBNull)) det2.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det2.ReservationGroupId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) det2.ConcernedAuthorityId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) det2.GrpCerti_IssuedDistrictId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) det2.GrpCerti_IssuedDistrict = reader.GetString(4);
					if (!(reader[5] is DBNull)) det2.GrpCerti_IssuedLocalLevelId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) det2.GrpCerti_IssuedLocalLevel = reader.GetString(6);
					if (!(reader[7] is DBNull)) det2.GrpCertiIssue_WardNo = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) det2.GrpCertiIssue_ToleStreet = reader.GetString(8);
					if (!(reader[9] is DBNull)) det2.GroupWiseCerti_IssuedDate = Convert.ToDateTime(reader[9]);
					if (!(reader[10] is DBNull)) det2.GroupWiseCerti_RefNo = reader.GetString(10);
					if (!(reader[11] is DBNull)) det2.GroupWiseCerti_Path = reader.GetString(11);
					if (!(reader[12] is DBNull)) det2.ReservationGroupName = reader.GetString(12);
					if (!(reader[13] is DBNull)) det2.ConcernedAuthorityName = reader.GetString(13);
					if (!(reader[14] is DBNull)) det2.GroupWiseCertiMiti = reader.GetString(14);
					beData.ReservationGroupList.Add(det2);
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


		public AcademicLib.BE.Scholarship.SchoolSubjectwiseCollections getSchoolSubjectwise(int UserId, int EntityId, int? SubjectId,int? ClassId)
		{
			AcademicLib.BE.Scholarship.SchoolSubjectwiseCollections dataColl = new AcademicLib.BE.Scholarship.SchoolSubjectwiseCollections();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.CommandText = "usp_GetSchoolSubjectWise";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Scholarship.SchoolSubjectwise beData = new AcademicLib.BE.Scholarship.SchoolSubjectwise();
					if (!(reader[0] is DBNull)) beData.SchoolId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SubjectId = reader.GetInt32(2);
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


		public ResponeValue CheckScholarshipApply(int UserId,  string SEESymbolNo, string Alphabet,int? ClassId)
		{
			ResponeValue resVal = new ResponeValue();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);			
			cmd.Parameters.AddWithValue("@SEESymbolNo", SEESymbolNo);
			cmd.Parameters.AddWithValue("@Alphabet", Alphabet);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 400);
			cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
			cmd.CommandText = "usp_CheckScholarshipApply";
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			try
			{
				cmd.ExecuteNonQuery();
				resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);
				resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);
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

		//GradeSheet
		public BE.Scholarship.GradeSheetCollections getAllGradeSheet(int UserId, int EntityId, string SEESymbolNo, string Alphabet, DateTime DOB_AD, string DOB_BS,double? GPA, string Pwd,int? ClassId)
		{
			BE.Scholarship.GradeSheetCollections dataColl = new BE.Scholarship.GradeSheetCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@SEESymbolNo", SEESymbolNo);
			cmd.Parameters.AddWithValue("@Alphabet", Alphabet);
			cmd.Parameters.AddWithValue("@DOB_AD", DOB_AD);
			cmd.Parameters.AddWithValue("@DOB_BS", DOB_BS);
			cmd.Parameters.AddWithValue("@GPA", GPA);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar,400);
			cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
			cmd.CommandText = "usp_GetAllGradeSheet";
			cmd.Parameters.AddWithValue("@Pwd", Pwd);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Scholarship.GradeSheet beData = new BE.Scholarship.GradeSheet();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.DOB_AD = Convert.ToDateTime(reader[2]);
					if (!(reader[3] is DBNull)) beData.DOB_BS = Convert.ToString(reader[3]);
					if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SEESymbolNo = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Alphabet = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.SchoolName = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.SNO = reader.GetInt32(8);
					if (!(reader[9] is DBNull)) beData.Code = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.SubjectName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.CreditHour = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.Grade = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.GradePoint = Convert.ToDouble(reader[13]);
					if (!(reader[14] is DBNull)) beData.FinalGrade = reader.GetString(14);
					if (!(reader[15] is DBNull)) beData.Remarks = reader.GetString(15);
					if (!(reader[16] is DBNull)) beData.GPA = Math.Round(Convert.ToDouble(reader[16]),2);
					if (!(reader[17] is DBNull)) beData.Avg_GPA = Math.Round(Convert.ToDouble(reader[17]),2);
					dataColl.Add(beData);
				}
				try
				{
					reader.NextResult();
					var beData = new BE.Scholarship.Scholarship();
					beData.IsSuccess = false;
					if (reader.Read())
					{
						if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) beData.FirstName = reader.GetString(1);
						if (!(reader[2] is DBNull)) beData.MiddleName = reader.GetString(2);
						if (!(reader[3] is DBNull)) beData.LastName = reader.GetString(3);
						if (!(reader[4] is DBNull)) beData.Gender = reader.GetInt32(4);
						if (!(reader[5] is DBNull)) beData.DOB = Convert.ToDateTime(reader[5]);
						if (!(reader[6] is DBNull)) beData.SEESymbolNo = reader.GetString(6);
						if (!(reader[7] is DBNull)) beData.Alphabet = reader.GetString(7);
						if (!(reader[8] is DBNull)) beData.GPA = Math.Round(Convert.ToDouble(reader[8]), 2);
						if (!(reader[9] is DBNull)) beData.Email = reader.GetString(9);
						if (!(reader[10] is DBNull)) beData.MobileNo = reader.GetString(10);
						if (!(reader[11] is DBNull)) beData.F_FirstName = reader.GetString(11);
						if (!(reader[12] is DBNull)) beData.F_MiddleName = reader.GetString(12);
						if (!(reader[13] is DBNull)) beData.F_LastName = reader.GetString(13);
						if (!(reader[14] is DBNull)) beData.M_FirstName = reader.GetString(14);
						if (!(reader[15] is DBNull)) beData.M_MiddleName = reader.GetString(15);
						if (!(reader[16] is DBNull)) beData.M_LastName = reader.GetString(16);
						if (!(reader[17] is DBNull)) beData.GF_FirstName = reader.GetString(17);
						if (!(reader[18] is DBNull)) beData.GF_MiddleName = reader.GetString(18);
						if (!(reader[19] is DBNull)) beData.GF_LastName = reader.GetString(19);
						if (!(reader[20] is DBNull)) beData.P_ProvinceId = reader.GetInt32(20);
						if (!(reader[21] is DBNull)) beData.P_Province = reader.GetString(21);
						if (!(reader[22] is DBNull)) beData.P_DistrictId = reader.GetInt32(22);
						if (!(reader[23] is DBNull)) beData.P_District = reader.GetString(23);
						if (!(reader[24] is DBNull)) beData.P_LocalLevelId = reader.GetInt32(24);
						if (!(reader[25] is DBNull)) beData.P_LocalLevel = reader.GetString(25);
						if (!(reader[26] is DBNull)) beData.P_WardNo = reader.GetInt32(26);
						if (!(reader[27] is DBNull)) beData.P_ToleStreet = reader.GetString(27);
						if (!(reader[28] is DBNull)) beData.Temp_ProvinceId = reader.GetInt32(28);
						if (!(reader[29] is DBNull)) beData.Temp_Province = reader.GetString(29);
						if (!(reader[30] is DBNull)) beData.Temp_DistrictId = reader.GetInt32(30);
						if (!(reader[31] is DBNull)) beData.Temp_District = reader.GetString(31);
						if (!(reader[32] is DBNull)) beData.Temp_LocalLevelId = reader.GetInt32(32);
						if (!(reader[33] is DBNull)) beData.Temp_LocalLevel = reader.GetString(33);
						if (!(reader[34] is DBNull)) beData.Temp_WardNo = reader.GetInt32(34);
						if (!(reader[35] is DBNull)) beData.Temp_ToleStreet = reader.GetString(35);
						if (!(reader[36] is DBNull)) beData.BC_CertificateTypeId = reader.GetInt32(36);
						if (!(reader[37] is DBNull)) beData.BC_CertificateNo = reader.GetString(37);
						if (!(reader[38] is DBNull)) beData.BC_IssuedDate = Convert.ToDateTime(reader[38]);
						if (!(reader[39] is DBNull)) beData.BC_IssuedDistrictId = reader.GetInt32(39);
						if (!(reader[40] is DBNull)) beData.BC_IssuedDistrict = reader.GetString(40);
						if (!(reader[41] is DBNull)) beData.BC_IssuedLocalLevelId = reader.GetInt32(41);
						if (!(reader[42] is DBNull)) beData.BC_IssuedLocalLevel = reader.GetString(42);
						if (!(reader[43] is DBNull)) beData.BC_IssuedWardNo = reader.GetInt32(43);
						if (!(reader[44] is DBNull)) beData.BC_IssuedToleStreet = reader.GetString(44);
						if (!(reader[45] is DBNull)) beData.BC_DocumentNameId = reader.GetInt32(45);
						if (!(reader[46] is DBNull)) beData.BC_FilePath = reader.GetString(46);
						if (!(reader[47] is DBNull)) beData.CtznshipFront_FilePath = reader.GetString(47);
						if (!(reader[48] is DBNull)) beData.CtznshipBack_FilePath = reader.GetString(48);
						if (!(reader[49] is DBNull)) beData.EquivalentBoardId = reader.GetInt32(49);
						if (!(reader[50] is DBNull)) beData.GradeSheetFilePath = reader.GetString(50);
						if (!(reader[51] is DBNull)) beData.Character_Transfer_Certi = reader.GetString(51);
						if (!(reader[52] is DBNull)) beData.SchoolName = reader.GetString(52);
						if (!(reader[53] is DBNull)) beData.Certi_IssuedDate = Convert.ToDateTime(reader[53]);
						if (!(reader[54] is DBNull)) beData.SchoolEMISCode = reader.GetString(54);
						if (!(reader[55] is DBNull)) beData.SchoolTypeId = reader.GetInt32(55);
						if (!(reader[56] is DBNull)) beData.SchoolDistrictId = reader.GetInt32(56);
						if (!(reader[57] is DBNull)) beData.SchoolDistrict = reader.GetString(57);
						if (!(reader[58] is DBNull)) beData.SchoolLocalLevelId = reader.GetInt32(58);
						if (!(reader[59] is DBNull)) beData.SchoolLocalLevel = reader.GetString(59);
						if (!(reader[60] is DBNull)) beData.SchoolWardNo = reader.GetInt32(60);
						if (!(reader[61] is DBNull)) beData.SchoolToleStreet = reader.GetString(61);
						if (!(reader[62] is DBNull)) beData.AppliedSubjectId = reader.GetInt32(62);
						if (!(reader[63] is DBNull)) beData.ScholarshipTypeId = reader.GetInt32(63);
						if (!(reader[64] is DBNull)) beData.PovCerti_IssuedDate = Convert.ToDateTime(reader[64]);
						if (!(reader[65] is DBNull)) beData.PovCerti_RefNo = reader.GetString(65);
						if (!(reader[66] is DBNull)) beData.PovCerti_IssuedDistrictId = reader.GetInt32(66);
						if (!(reader[67] is DBNull)) beData.PovCerti_IssuedDistrict = reader.GetString(67);
						if (!(reader[68] is DBNull)) beData.PovCerti_IssuedLocalLevelId = reader.GetInt32(68);
						if (!(reader[69] is DBNull)) beData.PovCerti_IssuedLocalLevel = reader.GetString(69);
						if (!(reader[70] is DBNull)) beData.PovCerti_WardNo = reader.GetInt32(70);
						if (!(reader[71] is DBNull)) beData.PovCerti_ToleStreet = reader.GetString(71);
						if (!(reader[72] is DBNull)) beData.IssuerName = reader.GetString(72);
						if (!(reader[73] is DBNull)) beData.IssuerDesignation = reader.GetString(73);
						if (!(reader[74] is DBNull)) beData.Poverty_CertiFilePath = reader.GetString(74);
						if (!(reader[75] is DBNull)) beData.GovSchoolCerti_IssuedDate = Convert.ToDateTime(reader[75]);
						if (!(reader[76] is DBNull)) beData.GovSchoolCerti_RefNo = reader.GetString(76);
						if (!(reader[77] is DBNull)) beData.GovSchoolCertiPath = reader.GetString(77);

						//Field Added By Bivek Starts
						if (!(reader[78] is DBNull)) beData.DOB_AD = Convert.ToDateTime(reader[78]);
						if (!(reader[79] is DBNull)) beData.PhotoPath = reader.GetString(79);
						if (!(reader[80] is DBNull)) beData.SignaturePath = reader.GetString(80);
						if (!(reader[81] is DBNull)) beData.Mig_WardId = reader.GetInt32(81);
						if (!(reader[82] is DBNull)) beData.MigDoc_IssuedDate = Convert.ToDateTime(reader[82]);
						if (!(reader[83] is DBNull)) beData.MigDoc_RefNo = reader.GetString(83);
						if (!(reader[84] is DBNull)) beData.MigDocPath = reader.GetString(84);
						if (!(reader[85] is DBNull)) beData.LandfilDistrictId = reader.GetInt32(85);
						if (!(reader[86] is DBNull)) beData.LandfilDistrict = reader.GetString(86);
						if (!(reader[87] is DBNull)) beData.LandfillLocalLevelId = reader.GetInt32(87);
						if (!(reader[88] is DBNull)) beData.LandfillLocalLevel = reader.GetString(88);
						if (!(reader[89] is DBNull)) beData.LandfillWardNo = reader.GetInt32(89);
						if (!(reader[90] is DBNull)) beData.LandFill_IssuedDate = Convert.ToDateTime(reader[90]);
						if (!(reader[91] is DBNull)) beData.LandFill_RefNo = reader.GetString(91);
						if (!(reader[92] is DBNull)) beData.LandFillDocPath = reader.GetString(92);
						//Ends

						if (!(reader[93] is DBNull)) beData.Anusuchi3Doc_IssuedDate = Convert.ToDateTime(reader[93]);
						if (!(reader[94] is DBNull)) beData.Anusuchi3Doc_RefNo = reader.GetString(94);
						if (!(reader[95] is DBNull)) beData.Anusuchi3DocPath = reader.GetString(95);

						if (!(reader[96] is DBNull)) beData.DOBMiti = reader.GetString(96);
						if (!(reader[97] is DBNull)) beData.BC_IssuedMiti = reader.GetString(97);
						if (!(reader[98] is DBNull)) beData.Certi_IssuedMiti = reader.GetString(98);
						if (!(reader[99] is DBNull)) beData.GovSchoolCertiMiti = reader.GetString(99);
						if (!(reader[100] is DBNull)) beData.MigDoc_IssuedMiti = reader.GetString(100);
						if (!(reader[101] is DBNull)) beData.Anusuchi3Doc_IssuedMiti = reader.GetString(101);
						if (!(reader[102] is DBNull)) beData.BoardName = reader.GetString(102);
						if (!(reader[103] is DBNull)) beData.SubjectName = reader.GetString(103);
						if (!(reader[104] is DBNull)) beData.LandfillDoc_IssuedMiti = reader.GetString(104);
						if (!(reader[105] is DBNull)) beData.ScholarshipTypeName = reader.GetString(105);

						if (!(reader[106] is DBNull)) beData.DayOne = reader.GetString(106);
						if (!(reader[107] is DBNull)) beData.DayTwo = reader.GetString(107);
						if (!(reader[108] is DBNull)) beData.DayThree = reader.GetString(108);

						try
						{
							if (!(reader[109] is DBNull)) beData.RelatedSchoolRefNo = reader.GetString(109);
							if (!(reader[110] is DBNull)) beData.RelatedSchoolIssueDate = reader.GetDateTime(110);
							if (!(reader[111] is DBNull)) beData.RelatedSchoolFilePath = reader.GetString(111);
							if (!(reader[112] is DBNull)) beData.ClassId = reader.GetInt32(112);
							if (!(reader[113] is DBNull)) beData.ClassName = reader.GetString(113);
							if (!(reader[114] is DBNull)) beData.RelatedSchoolIssueMiti = reader.GetString(114);
							if (!(reader[115] is DBNull)) beData.GradeSheet_CertiPath = reader.GetString(115);

						}
						catch { }

						beData.DOB_AD = beData.DOB;

					}
					reader.NextResult();
					beData.SchoolPriorityListColl = new BE.Scholarship.SchoolPriorityListCollections();
					while (reader.Read())
					{
						BE.Scholarship.SchoolPriorityList det1 = new BE.Scholarship.SchoolPriorityList();
						if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) det1.SchoolId = reader.GetInt32(1);
						if (!(reader[2] is DBNull)) det1.PriorityId = reader.GetInt32(2);
						beData.SchoolPriorityListColl.Add(det1);
					}

					reader.NextResult();
					beData.ReservationGroupList = new BE.Scholarship.ReservationListCollections();
					while (reader.Read())
					{
						BE.Scholarship.ReservationList det2 = new BE.Scholarship.ReservationList();
						if (!(reader[0] is DBNull)) det2.TranId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) det2.ReservationGroupId = reader.GetInt32(1);
						if (!(reader[2] is DBNull)) det2.ConcernedAuthorityId = reader.GetInt32(2);
						if (!(reader[3] is DBNull)) det2.GrpCerti_IssuedDistrictId = reader.GetInt32(3);
						if (!(reader[4] is DBNull)) det2.GrpCerti_IssuedDistrict = reader.GetString(4);
						if (!(reader[5] is DBNull)) det2.GrpCerti_IssuedLocalLevelId = reader.GetInt32(5);
						if (!(reader[6] is DBNull)) det2.GrpCerti_IssuedLocalLevel = reader.GetString(6);
						if (!(reader[7] is DBNull)) det2.GrpCertiIssue_WardNo = reader.GetInt32(7);
						if (!(reader[8] is DBNull)) det2.GrpCertiIssue_ToleStreet = reader.GetString(8);
						if (!(reader[9] is DBNull)) det2.GroupWiseCerti_IssuedDate = Convert.ToDateTime(reader[9]);
						if (!(reader[10] is DBNull)) det2.GroupWiseCerti_RefNo = reader.GetString(10);
						if (!(reader[11] is DBNull)) det2.GroupWiseCerti_Path = reader.GetString(11);
						if (!(reader[12] is DBNull)) det2.ReservationGroupName = reader.GetString(12);
						if (!(reader[13] is DBNull)) det2.ConcernedAuthorityName = reader.GetString(13);
						beData.ReservationGroupList.Add(det2);
					}

					if (beData.TranId > 0)
					{
						beData.IsSuccess = true;

						if (dataColl.Count == 0)
							dataColl.Add(new BE.Scholarship.GradeSheet());

						dataColl[0].Scholarship = beData;
					}

					reader.NextResult();
					var vData = new BE.Scholarship.ScholarshipDocVerify();
					if (reader.Read())
					{
						if (!(reader[0] is DBNull)) vData.VerifyId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) vData.TranId = reader.GetInt32(1);
						if (!(reader[2] is DBNull)) vData.V_Photo = Convert.ToBoolean(reader[2]);
						if (!(reader[3] is DBNull)) vData.V_Signature = Convert.ToBoolean(reader[3]);
						if (!(reader[4] is DBNull)) vData.V_Document = Convert.ToBoolean(reader[4]);
						if (!(reader[5] is DBNull)) vData.V_CandidateName = Convert.ToBoolean(reader[5]);
						if (!(reader[6] is DBNull)) vData.V_Gender = Convert.ToBoolean(reader[6]);
						if (!(reader[7] is DBNull)) vData.V_DOB = Convert.ToBoolean(reader[7]);
						if (!(reader[8] is DBNull)) vData.V_FatherName = Convert.ToBoolean(reader[8]);
						if (!(reader[9] is DBNull)) vData.V_MotherName = Convert.ToBoolean(reader[9]);
						if (!(reader[10] is DBNull)) vData.V_GrandfatherName = Convert.ToBoolean(reader[10]);
						if (!(reader[11] is DBNull)) vData.V_Email = Convert.ToBoolean(reader[11]);
						if (!(reader[12] is DBNull)) vData.V_MobileNo = Convert.ToBoolean(reader[12]);
						if (!(reader[13] is DBNull)) vData.V_PAddress = Convert.ToBoolean(reader[13]);
						if (!(reader[14] is DBNull)) vData.V_TempAddress = Convert.ToBoolean(reader[14]);
						if (!(reader[15] is DBNull)) vData.V_BCCNo = Convert.ToBoolean(reader[15]);
						if (!(reader[16] is DBNull)) vData.V_BCCIssuedDate = Convert.ToBoolean(reader[16]);
						if (!(reader[17] is DBNull)) vData.V_BCCIssuedDistrict = Convert.ToBoolean(reader[17]);
						if (!(reader[18] is DBNull)) vData.V_BCCIssuedLocalLevel = Convert.ToBoolean(reader[18]);
						if (!(reader[19] is DBNull)) vData.V_SchoolName = Convert.ToBoolean(reader[19]);
						if (!(reader[20] is DBNull)) vData.V_SchoolType = Convert.ToBoolean(reader[20]);
						if (!(reader[21] is DBNull)) vData.V_SchoolDistrict = Convert.ToBoolean(reader[21]);
						if (!(reader[22] is DBNull)) vData.V_SchoolLocalLevel = Convert.ToBoolean(reader[22]);
						if (!(reader[23] is DBNull)) vData.V_SchoolWardNo = Convert.ToBoolean(reader[23]);
						if (!(reader[24] is DBNull)) vData.V_Character_Transfer_Certi = Convert.ToBoolean(reader[24]);
						if (!(reader[25] is DBNull)) vData.V_Character_Transfer_CertiDate = Convert.ToBoolean(reader[25]);
						if (!(reader[26] is DBNull)) vData.V_ScholarshipType = Convert.ToBoolean(reader[26]);
						if (!(reader[27] is DBNull)) vData.V_GovSchoolCertiPath = Convert.ToBoolean(reader[27]);
						if (!(reader[28] is DBNull)) vData.V_GovSchoolCertiMiti = Convert.ToBoolean(reader[28]);
						if (!(reader[29] is DBNull)) vData.V_GovSchoolCerti_RefNo = Convert.ToBoolean(reader[29]);
						if (!(reader[30] is DBNull)) vData.V_Anusuchi3DocPath = Convert.ToBoolean(reader[30]);
						if (!(reader[31] is DBNull)) vData.V_Anusuchi3Doc_IssuedMiti = Convert.ToBoolean(reader[31]);
						if (!(reader[32] is DBNull)) vData.V_Anusuchi3Doc_RefNo = Convert.ToBoolean(reader[32]);
						if (!(reader[33] is DBNull)) vData.V_MigDocPath = Convert.ToBoolean(reader[33]);
						if (!(reader[34] is DBNull)) vData.V_Mig_WardId = Convert.ToBoolean(reader[34]);
						if (!(reader[35] is DBNull)) vData.V_MigDoc_IssuedMiti = Convert.ToBoolean(reader[35]);
						if (!(reader[36] is DBNull)) vData.V_MigDoc_RefNo = Convert.ToBoolean(reader[36]);
						if (!(reader[37] is DBNull)) vData.V_LandFillDocPath = Convert.ToBoolean(reader[37]);
						if (!(reader[38] is DBNull)) vData.V_LandfilDistrict = Convert.ToBoolean(reader[38]);
						if (!(reader[39] is DBNull)) vData.V_LandfillLocalLevel = Convert.ToBoolean(reader[39]);
						if (!(reader[40] is DBNull)) vData.V_LandfillWardNo = Convert.ToBoolean(reader[40]);
						if (!(reader[41] is DBNull)) vData.V_LandfillDoc_IssuedMiti = Convert.ToBoolean(reader[41]);
						if (!(reader[42] is DBNull)) vData.V_LandFill_RefNo = Convert.ToBoolean(reader[42]);
						if (!(reader[43] is DBNull)) vData.V_Status = reader.GetInt32(43);
						if (!(reader[44] is DBNull)) vData.Email = reader.GetString(44);
						if (!(reader[45] is DBNull)) vData.V_Subject = reader.GetString(45);
						if (!(reader[46] is DBNull)) vData.Remarks = reader.GetString(46);
					}
					reader.NextResult();
					vData.ReservationGroupList = new BE.Scholarship.ReservationGroupVerifyCollections();
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
						vData.ReservationGroupList.Add(det1);
					}

					if (vData.VerifyId > 0)
					{
						vData.IsSuccess = true;
						dataColl[0].DVerify = vData;
					}
				}
				catch { }
			
				reader.Close();

				dataColl.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);
				dataColl.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

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

		public ResponeValue ResetPwd(int UserId, string TranIdColl)
		{
			ResponeValue resVal = new ResponeValue();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@TranIdColl", TranIdColl);
			cmd.CommandText = "usp_ResetScholarshipPwd";
			try
			{
				cmd.ExecuteNonQuery();
				resVal.IsSuccess = true;
				resVal.ResponseMSG = "Password reset success";
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

	}

}

