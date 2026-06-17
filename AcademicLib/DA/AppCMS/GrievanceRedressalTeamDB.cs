using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AppCMS.Creation
{

	internal class GrievanceRedressalTeamDB
	{
		DataAccessLayer1 dal = null;
		public GrievanceRedressalTeamDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);

		}
		public ResponeValues SaveUpdate(BE.AppCMS.Creation.GrievanceRedressalTeam beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", beData.Name);
			cmd.Parameters.AddWithValue("@Designation", beData.Designation);
			cmd.Parameters.AddWithValue("@Qualification", beData.Qualification);
			cmd.Parameters.AddWithValue("@Contact", beData.Contact);
			cmd.Parameters.AddWithValue("@Email", beData.Email);
			cmd.Parameters.AddWithValue("@Department", beData.Department);
			cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
			cmd.Parameters.AddWithValue("@Image", beData.Image);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@GrievanceRedressalId", beData.GrievanceRedressalId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateGrievanceRedressalTeam";
			}
			else
			{
				cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddGrievanceRedressalTeam";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[10].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[10].Value);

				if (!(cmd.Parameters[11].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[11].Value);

				if (!(cmd.Parameters[12].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[12].Value);

				if (!(cmd.Parameters[13].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[13].Value);

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

		public ResponeValues DeleteById(int UserId, int EntityId, int GrievanceRedressalId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@GrievanceRedressalId", GrievanceRedressalId);
			cmd.CommandText = "usp_DelGrievanceRedressalTeamById";
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
		public BE.AppCMS.Creation.GrievanceRedressalTeamCollections getAllGrievanceRedressalTeam(int UserId, int EntityId)
		{
			BE.AppCMS.Creation.GrievanceRedressalTeamCollections dataColl = new BE.AppCMS.Creation.GrievanceRedressalTeamCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllGrievanceRedressalTeam";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.AppCMS.Creation.GrievanceRedressalTeam beData = new BE.AppCMS.Creation.GrievanceRedressalTeam();
					if (!(reader[0] is DBNull)) beData.GrievanceRedressalId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Designation = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Qualification = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Contact = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Email = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Department = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.OrderNo = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.Image = reader.GetString(8);
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
		public BE.AppCMS.Creation.GrievanceRedressalTeam getGrievanceRedressalTeamById(int UserId, int EntityId, int GrievanceRedressalId)
		{
			BE.AppCMS.Creation.GrievanceRedressalTeam beData = new BE.AppCMS.Creation.GrievanceRedressalTeam();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@GrievanceRedressalId", GrievanceRedressalId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetGrievanceRedressalTeamById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.AppCMS.Creation.GrievanceRedressalTeam();
					if (!(reader[0] is DBNull)) beData.GrievanceRedressalId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Designation = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Qualification = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Contact = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Email = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Department = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.OrderNo = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.Image = reader.GetString(8);
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

