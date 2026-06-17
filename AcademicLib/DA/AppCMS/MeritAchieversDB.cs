using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AppCMS.Creation
{

	internal class MeritAchieversDB
	{
		DataAccessLayer1 dal = null;
		public MeritAchieversDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.AppCMS.Creation.MeritAchievers beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", beData.Name);
			cmd.Parameters.AddWithValue("@DegreeDetails", beData.DegreeDetails);
			cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
			cmd.Parameters.AddWithValue("@Description", beData.Description);
			cmd.Parameters.AddWithValue("@ImagePath", beData.ImagePath);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@TranId", beData.TranId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateMeritAchievers";
			}
			else
			{
				cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddMeritAchievers";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[7].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

				if (!(cmd.Parameters[8].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

				if (!(cmd.Parameters[9].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

				if (!(cmd.Parameters[10].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveMeritAchievementsDetails(beData.CUserId, resVal.RId, beData.MeritAchievementsColl);
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

		public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.CommandText = "usp_DelMeritAchieversById";
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
		public BE.AppCMS.Creation.MeritAchieversCollections getAllMeritAchievers(int UserId, int EntityId)
		{
			BE.AppCMS.Creation.MeritAchieversCollections dataColl = new BE.AppCMS.Creation.MeritAchieversCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllMeritAchievers";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.AppCMS.Creation.MeritAchievers beData = new BE.AppCMS.Creation.MeritAchievers();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.DegreeDetails = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.Description = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.ImagePath = reader.GetString(5);
					beData.MeritAchievementsColl = new BE.AppCMS.Creation.MeritAchievementsCollections();
					dataColl.Add(beData);
				}
				reader.NextResult();
                while (reader.Read())
                {
					BE.AppCMS.Creation.MeritAchievements beData = new BE.AppCMS.Creation.MeritAchievements();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Achievement = reader.GetString(1);
					dataColl.Find(p1 => p1.TranId == beData.TranId).MeritAchievementsColl.Add(beData);
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
		private void SaveMeritAchievementsDetails(int UserId, int TranId, BE.AppCMS.Creation.MeritAchievementsCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
				return;

			foreach (BE.AppCMS.Creation.MeritAchievements beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.Parameters.AddWithValue("@TranId", TranId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.Parameters.AddWithValue("@Achievement", beData.Achievement);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "usp_AddMeritAchievementsDetails";
				cmd.ExecuteNonQuery();
			}

		}

		public BE.AppCMS.Creation.MeritAchievers getMeritAchieversById(int UserId, int EntityId, int TranId)
		{
			BE.AppCMS.Creation.MeritAchievers beData = new BE.AppCMS.Creation.MeritAchievers();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetMeritAchieversById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.AppCMS.Creation.MeritAchievers();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.DegreeDetails = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.Description = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.ImagePath = reader.GetString(5);
				}
				reader.NextResult();
				beData.MeritAchievementsColl = new BE.AppCMS.Creation.MeritAchievementsCollections();
				while (reader.Read())
				{
					BE.AppCMS.Creation.MeritAchievements det1 = new BE.AppCMS.Creation.MeritAchievements();
					if (!(reader[0] is DBNull)) det1.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.Achievement = reader.GetString(1);
					beData.MeritAchievementsColl.Add(det1);
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

