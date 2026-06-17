using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{

	internal class HealthOperationDB
	{
		DataAccessLayer1 dal = null;
		public HealthOperationDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.HealthOperation beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", beData.Name);
			cmd.Parameters.AddWithValue("@DateFrom", beData.DateFrom);
			cmd.Parameters.AddWithValue("@DateTo", beData.DateTo);
			cmd.Parameters.AddWithValue("@Organizer", beData.Organizer);
			cmd.Parameters.AddWithValue("@Vaccination", beData.Vaccination);
			cmd.Parameters.AddWithValue("@Description", beData.Description);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@HealthCampaignId", beData.HealthCampaignId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateHealthOperation";
			}
			else
			{
				cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddHealthOperation";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[8].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[8].Value);

				if (!(cmd.Parameters[9].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

				if (!(cmd.Parameters[10].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

				if (!(cmd.Parameters[11].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveHealthCampaignRepDetails(beData.CUserId, resVal.RId, beData.HealthCampaignRepColl);
					SaveSelectClassDetails(beData.CUserId, resVal.RId, beData.SelectClassColl);
					SaveSelectDiseaseDetails(beData.CUserId, resVal.RId, beData.SelectDiseaseColl);
					SaveSelectVaccineDetails(beData.CUserId, resVal.RId, beData.SelectVaccineColl);
					SaveSelectTestDetails(beData.CUserId, resVal.RId, beData.SelectTestColl);
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

		public ResponeValues DeleteById(int UserId, int EntityId, int HealthCampaignId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@HealthCampaignId", HealthCampaignId);
			cmd.CommandText = "usp_DelHealthOperationById";
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
		public BE.HealthOperationCollections getAllHealthOperation(int UserId, int EntityId)
		{
			BE.HealthOperationCollections dataColl = new BE.HealthOperationCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllHealthOperation";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.HealthOperation beData = new BE.HealthOperation();
					if (!(reader[0] is DBNull)) beData.HealthCampaignId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.DateFrom = Convert.ToDateTime(reader[2]);
					if (!(reader[3] is DBNull)) beData.DateTo = Convert.ToDateTime(reader[3]);
					if (!(reader[4] is DBNull)) beData.Organizer = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Vaccination = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) beData.Description = reader.GetString(6);
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
		//Please replace below code
		private void SaveHealthCampaignRepDetails(int UserId, int HealthCampaignId, BE.HealthCampaignRepCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || HealthCampaignId == 0)
				return;

			foreach (BE.HealthCampaignRep beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@ExaminerId", beData.ExaminerId);
				cmd.Parameters.AddWithValue("@Name", beData.Name);
				cmd.Parameters.AddWithValue("@ExaminerRegdNo", beData.ExaminerRegdNo);
				cmd.Parameters.AddWithValue("@Designation", beData.Designation);
				cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
				cmd.Parameters.AddWithValue("@HealthCampaignId", HealthCampaignId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddHealthCampaignRepDetails";
				cmd.ExecuteNonQuery();
			}

		}
		//Ends
		private void SaveSelectClassDetails(int UserId, int HealthCampaignId, List<int> beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || HealthCampaignId == 0)
				return;

			foreach (int beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@ClassId", beData);
				cmd.Parameters.AddWithValue("@HealthCampaignId", HealthCampaignId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddSelectClassDetails";
				cmd.ExecuteNonQuery();
			}

		}

		private void SaveSelectDiseaseDetails(int UserId, int HealthCampaignId, List<int> beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || HealthCampaignId == 0)
				return;

			foreach (int beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@HealthIssueId", beData);
				cmd.Parameters.AddWithValue("@HealthCampaignId", HealthCampaignId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddSelectDiseaseDetails";
				cmd.ExecuteNonQuery();
			}

		}

		private void SaveSelectVaccineDetails(int UserId, int HealthCampaignId, List<int> beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || HealthCampaignId == 0)
				return;

			foreach (int beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@VaccineId", beData);
				cmd.Parameters.AddWithValue("@HealthCampaignId", HealthCampaignId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddSelectVaccineDetails";
				cmd.ExecuteNonQuery();
			}

		}

		private void SaveSelectTestDetails(int UserId, int HealthCampaignId, List<int> beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || HealthCampaignId == 0)
				return;

			foreach (int beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@TestNameId", beData);
				cmd.Parameters.AddWithValue("@HealthCampaignId", HealthCampaignId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddSelectTestDetails";
				cmd.ExecuteNonQuery();
			}

		}

		public BE.HealthOperation getHealthOperationById(int UserId, int EntityId, int HealthCampaignId)
		{
			BE.HealthOperation beData = new BE.HealthOperation();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@HealthCampaignId", HealthCampaignId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetHealthOperationById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.HealthOperation();
					if (!(reader[0] is DBNull)) beData.HealthCampaignId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.DateFrom = Convert.ToDateTime(reader[2]);
					if (!(reader[3] is DBNull)) beData.DateTo = Convert.ToDateTime(reader[3]);
					if (!(reader[4] is DBNull)) beData.Organizer = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Vaccination = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) beData.Description = reader.GetString(6);
				}
				reader.NextResult();
				beData.HealthCampaignRepColl = new BE.HealthCampaignRepCollections();
				while (reader.Read())
				{
					BE.HealthCampaignRep det1 = new BE.HealthCampaignRep();
					if (!(reader[0] is DBNull)) det1.HealthCampaignId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.ExaminerId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) det1.Name = reader.GetString(2);
					if (!(reader[3] is DBNull)) det1.ExaminerRegdNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) det1.Designation = reader.GetString(4);
					if (!(reader[5] is DBNull)) det1.MobileNo = reader.GetString(5);
					beData.HealthCampaignRepColl.Add(det1);
				}
				reader.NextResult();
				beData.SelectClassColl = new List<int>();
				while (reader.Read())
				{
					beData.SelectClassColl.Add(reader.GetInt32(0));
				}
				reader.NextResult();
				beData.SelectDiseaseColl = new List<int>();
				while (reader.Read())
				{
					beData.SelectDiseaseColl.Add(reader.GetInt32(0));
				}
				reader.NextResult();
				beData.SelectVaccineColl = new List<int>();

				while (reader.Read())
				{
					beData.SelectVaccineColl.Add(reader.GetInt32(0));
				}
				reader.NextResult();
				beData.SelectTestColl = new List<int>();
				while (reader.Read())
				{
					beData.SelectTestColl.Add(reader.GetInt32(0));
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

