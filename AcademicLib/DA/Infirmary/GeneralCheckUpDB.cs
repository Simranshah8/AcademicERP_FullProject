using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{

	internal class GeneralCheckUpDB
	{
		DataAccessLayer1 dal = null;
		public GeneralCheckUpDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.GeneralCheckUp beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", beData.Name);
			cmd.Parameters.AddWithValue("@Month", beData.Month);
			cmd.Parameters.AddWithValue("@DateFrom", beData.DateFrom);
			cmd.Parameters.AddWithValue("@DateTo", beData.DateTo);
			cmd.Parameters.AddWithValue("@Organizer", beData.Organizer);
			cmd.Parameters.AddWithValue("@Vaccination", beData.Vaccination);
			cmd.Parameters.AddWithValue("@Description", beData.Description);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@GeneralCheckUpId", beData.GeneralCheckUpId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateGeneralCheckUp";
			}
			else
			{
				cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddGeneralCheckUp";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[9].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

				if (!(cmd.Parameters[10].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

				if (!(cmd.Parameters[11].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

				if (!(cmd.Parameters[12].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveGeneralCheckUpRepDetails(beData.CUserId, resVal.RId, beData.GeneralCheckUpRepColl);
					SaveSelectGClassDetails(beData.CUserId, resVal.RId, beData.SelectGClassColl);
					SaveSelectGDiseaseDetails(beData.CUserId, resVal.RId, beData.SelectGDiseaseColl);
					SaveSelectGTestDetails(beData.CUserId, resVal.RId, beData.SelectGTestColl);
					SaveSelectGVaccineDetails(beData.CUserId, resVal.RId, beData.SelectGVaccineColl);
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

		public ResponeValues DeleteById(int UserId, int EntityId, int GeneralCheckUpId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@GeneralCheckUpId", GeneralCheckUpId);
			cmd.CommandText = "usp_DelGeneralCheckUpById";
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
		public BE.GeneralCheckUpCollections getAllGeneralCheckUp(int UserId, int EntityId)
		{
			BE.GeneralCheckUpCollections dataColl = new BE.GeneralCheckUpCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllGeneralCheckUp";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.GeneralCheckUp beData = new BE.GeneralCheckUp();
					if (!(reader[0] is DBNull)) beData.GeneralCheckUpId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Month = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.DateFrom = Convert.ToDateTime(reader[3]);
					if (!(reader[4] is DBNull)) beData.DateTo = Convert.ToDateTime(reader[4]);
					if (!(reader[5] is DBNull)) beData.Organizer = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Vaccination = Convert.ToBoolean(reader[6]);
					if (!(reader[7] is DBNull)) beData.Description = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.MonthName = reader.GetString(8);
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
		private void SaveGeneralCheckUpRepDetails(int UserId, int GeneralCheckUpId, BE.GeneralCheckUpRepCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || GeneralCheckUpId == 0)
				return;

			foreach (BE.GeneralCheckUpRep beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@ExaminerId", beData.ExaminerId);
				cmd.Parameters.AddWithValue("@Name", beData.Name);
				cmd.Parameters.AddWithValue("@ExaminerRegdNo", beData.ExaminerRegdNo);
				cmd.Parameters.AddWithValue("@Designation", beData.Designation);
				cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
				cmd.Parameters.AddWithValue("@GeneralCheckUpId", GeneralCheckUpId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddGeneralCheckUpRepDetails";
				cmd.ExecuteNonQuery();
			}

		}

		private void SaveSelectGClassDetails(int UserId, int GeneralCheckUpId, List<int> beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || GeneralCheckUpId == 0)
				return;

			foreach (int beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@ClassId", beData);
				cmd.Parameters.AddWithValue("@GeneralCheckUpId", GeneralCheckUpId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddSelectGClassDetails";
				cmd.ExecuteNonQuery();
			}

		}

		private void SaveSelectGDiseaseDetails(int UserId, int GeneralCheckUpId, List<int> beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || GeneralCheckUpId == 0)
				return;

			foreach (int beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@HealthIssueId", beData);
				cmd.Parameters.AddWithValue("@GeneralCheckUpId", GeneralCheckUpId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddSelectGDiseaseDetails";
				cmd.ExecuteNonQuery();
			}

		}

		private void SaveSelectGTestDetails(int UserId, int GeneralCheckUpId, List<int> beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || GeneralCheckUpId == 0)
				return;

			foreach (int beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@TestNameId", beData);
				cmd.Parameters.AddWithValue("@GeneralCheckUpId", GeneralCheckUpId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddSelectGTestDetails";
				cmd.ExecuteNonQuery();
			}

		}

		private void SaveSelectGVaccineDetails(int UserId, int GeneralCheckUpId, List<int> beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || GeneralCheckUpId == 0)
				return;

			foreach (int beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@VaccineId", beData);
				cmd.Parameters.AddWithValue("@GeneralCheckUpId", GeneralCheckUpId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddSelectGVaccineDetails";
				cmd.ExecuteNonQuery();
			}

		}

		public BE.GeneralCheckUp getGeneralCheckUpById(int UserId, int EntityId, int GeneralCheckUpId)
		{
			BE.GeneralCheckUp beData = new BE.GeneralCheckUp();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@GeneralCheckUpId", GeneralCheckUpId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetGeneralCheckUpById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.GeneralCheckUp();
					if (!(reader[0] is DBNull)) beData.GeneralCheckUpId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Month = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.DateFrom = Convert.ToDateTime(reader[3]);
					if (!(reader[4] is DBNull)) beData.DateTo = Convert.ToDateTime(reader[4]);
					if (!(reader[5] is DBNull)) beData.Organizer = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Vaccination = Convert.ToBoolean(reader[6]);
					if (!(reader[7] is DBNull)) beData.Description = reader.GetString(7);
				}
				reader.NextResult();
				beData.GeneralCheckUpRepColl = new BE.GeneralCheckUpRepCollections();
				while (reader.Read())
				{
					BE.GeneralCheckUpRep det1 = new BE.GeneralCheckUpRep();
					if (!(reader[0] is DBNull)) det1.GeneralCheckUpId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.ExaminerId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) det1.Name = reader.GetString(2);
					if (!(reader[3] is DBNull)) det1.ExaminerRegdNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) det1.Designation = reader.GetString(4);
					if (!(reader[5] is DBNull)) det1.MobileNo = reader.GetString(5);
					beData.GeneralCheckUpRepColl.Add(det1);
				}
				reader.NextResult();
				beData.SelectGClassColl = new List<int>();
				while (reader.Read())
				{

					beData.SelectGClassColl.Add(reader.GetInt32(0));
				}
				reader.NextResult();

				beData.SelectGDiseaseColl = new List<int>();

				while (reader.Read())
				{

					beData.SelectGDiseaseColl.Add(reader.GetInt32(0));
				}
				reader.NextResult();

				beData.SelectGTestColl = new List<int>();
				while (reader.Read())
				{

					beData.SelectGTestColl.Add(reader.GetInt32(0));
				}
				reader.NextResult();
				beData.SelectGVaccineColl = new List<int>();
				while (reader.Read())
				{

					beData.SelectGVaccineColl.Add(reader.GetInt32(0));
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

