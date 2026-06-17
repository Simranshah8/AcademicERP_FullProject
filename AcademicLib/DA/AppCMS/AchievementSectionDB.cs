using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AppCMS.Creation
{

	internal class AchievementSectionDB
	{
		DataAccessLayer1 dal = null;
		public AchievementSectionDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.AppCMS.Creation.AchievementSection beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Headline", beData.Headline);
			cmd.Parameters.AddWithValue("@CategoryId", beData.CategoryId);
			cmd.Parameters.AddWithValue("@AchievementDate", beData.AchievementDate);
			cmd.Parameters.AddWithValue("@Description", beData.Description);
			cmd.Parameters.AddWithValue("@Tags", beData.Tags);
			cmd.Parameters.AddWithValue("@Photo", beData.Photo);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@AchievementSectionId", beData.AchievementSectionId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateAchievementSection";
			}
			else
			{
				cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddAchievementSection";
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


		public BE.AppCMS.Creation.AchievementSectionCollections getAllAchievementSection(int UserId, int EntityId)
		{
			BE.AppCMS.Creation.AchievementSectionCollections dataColl = new BE.AppCMS.Creation.AchievementSectionCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllAchievementSection";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.AppCMS.Creation.AchievementSection beData = new BE.AppCMS.Creation.AchievementSection();
					if (!(reader[0] is DBNull)) beData.AchievementSectionId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Headline = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.AchievementDateBS = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Tags = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Category = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Photo = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Description = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.AchievementDate = reader.GetDateTime(7);
					dataColl.Add(beData);
				};
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

		public BE.AppCMS.Creation.AchievementSection getAchievementSectionById(int UserId, int EntityId, int AchievementSectionId)
		{
			BE.AppCMS.Creation.AchievementSection beData = new BE.AppCMS.Creation.AchievementSection();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@AchievementSectionId", AchievementSectionId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAchievementSectionById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.AppCMS.Creation.AchievementSection();
					if (!(reader[0] is DBNull)) beData.AchievementSectionId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Headline = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.CategoryId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.AchievementDate = reader.GetDateTime(3);
					if (!(reader[4] is DBNull)) beData.Description = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Tags = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Photo = reader.GetString(6);
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



		public ResponeValues DeleteById(int UserId, int EntityId, int AchievementSectionId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@AchievementSectionId", AchievementSectionId);
			cmd.CommandText = "usp_DelAchievementSectionById";
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

	}

}

