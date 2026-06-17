using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AppCMS.Creation
{

	internal class AlumniRegFormDB
	{
		DataAccessLayer1 dal = null;
		public AlumniRegFormDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.AppCMS.Creation.AlumniReg beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@FullName", beData.FullName);
			cmd.Parameters.AddWithValue("@DOB", beData.DOB);
			cmd.Parameters.AddWithValue("@OriginalAddress", beData.OriginalAddress);
			cmd.Parameters.AddWithValue("@CurrentAddress", beData.CurrentAddress);
			cmd.Parameters.AddWithValue("@Contact", beData.Contact);
			cmd.Parameters.AddWithValue("@Email", beData.Email);
			cmd.Parameters.AddWithValue("@JoinedYear", beData.JoinedYear);
			cmd.Parameters.AddWithValue("@StudiedUpTo", beData.StudiedUpTo);
			cmd.Parameters.AddWithValue("@SEE", beData.SEE);
			cmd.Parameters.AddWithValue("@PlusTwo", beData.PlusTwo);
			cmd.Parameters.AddWithValue("@MarksheetPath", beData.MarksheetPath);
			cmd.Parameters.AddWithValue("@ProfilePhoto", beData.ProfilePhoto);
			cmd.Parameters.AddWithValue("@MemoryPhoto1", beData.MemoryPhoto1);
			cmd.Parameters.AddWithValue("@MemoryPhoto2", beData.MemoryPhoto2);
			cmd.Parameters.AddWithValue("@DegreeTitle", beData.DegreeTitle);
			cmd.Parameters.AddWithValue("@University", beData.University);
			cmd.Parameters.AddWithValue("@CurPosition", beData.CurPosition);
			cmd.Parameters.AddWithValue("@CurCompany", beData.CurCompany);
			cmd.Parameters.AddWithValue("@CurUniversity", beData.CurUniversity);
			cmd.Parameters.AddWithValue("@Achievements", beData.Achievements);
			cmd.Parameters.AddWithValue("@Achievement_Doc", beData.Achievement_Doc);
			cmd.Parameters.AddWithValue("@Bio", beData.Bio);
			cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@AlumniRegId", beData.AlumniRegId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateAlumniReg";
			}
			else
			{
				cmd.Parameters[25].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddAlumniReg";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[26].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[27].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[28].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[25].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[25].Value);

				if (!(cmd.Parameters[26].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[26].Value);

				if (!(cmd.Parameters[27].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[27].Value);

				if (!(cmd.Parameters[28].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[28].Value);

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

		public BE.AppCMS.Creation.AlumniRegCollections getAllAlumni(int UserId)
		{
			BE.AppCMS.Creation.AlumniRegCollections dataColl = new BE.AppCMS.Creation.AlumniRegCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.CommandText = "usp_GetAllAlumni";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.AppCMS.Creation.AlumniReg beData = new BE.AppCMS.Creation.AlumniReg();
					if (!(reader[0] is DBNull)) beData.AlumniRegId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.FullName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.DOB = Convert.ToDateTime(reader[2]);
					if (!(reader[3] is DBNull)) beData.OriginalAddress = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.CurrentAddress = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Contact = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Email = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.JoinedYear = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.StudiedUpTo = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.SEE = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.PlusTwo = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.DegreeTitle = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.University = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.CurPosition = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.CurCompany = reader.GetString(14);
					if (!(reader[15] is DBNull)) beData.CurUniversity = reader.GetString(15);
					if (!(reader[16] is DBNull)) beData.Achievements = reader.GetString(16);
					if (!(reader[17] is DBNull)) beData.Bio = reader.GetString(17);
					if (!(reader[18] is DBNull)) beData.Remarks = reader.GetString(18);
					if (!(reader[19] is DBNull)) beData.DOB_BS = reader.GetString(19);
					if (!(reader[20] is DBNull)) beData.ProfilePhoto = reader.GetString(20);
					if (!(reader[21] is DBNull)) beData.MarksheetPath = reader.GetString(21);
					if (!(reader[22] is DBNull)) beData.MemoryPhoto1 = reader.GetString(22);
					if (!(reader[23] is DBNull)) beData.MemoryPhoto2 = reader.GetString(23);
					if (!(reader[24] is DBNull)) beData.Achievement_Doc = reader.GetString(24);

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

		public ResponeValues DeleteById(int UserId, int AlumniRegId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@AlumniRegId", AlumniRegId);
			cmd.CommandText = "usp_DelAlumniRegById";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();

				if (!(cmd.Parameters[2].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

				if (!(cmd.Parameters[3].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

				if (!(cmd.Parameters[4].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

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

		public BE.AppCMS.Creation.AlumniReg getAlumniRegById(int UserId, int AlumniRegId)
		{
			BE.AppCMS.Creation.AlumniReg beData = new BE.AppCMS.Creation.AlumniReg();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@AlumniRegId", AlumniRegId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.CommandText = "usp_GetAlumniRegById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.AppCMS.Creation.AlumniReg();
					if (!(reader[0] is DBNull)) beData.AlumniRegId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.FullName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.DOB = Convert.ToDateTime(reader[2]);
					if (!(reader[3] is DBNull)) beData.OriginalAddress = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.CurrentAddress = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Contact = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Email = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.JoinedYear = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.StudiedUpTo = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.SEE = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.PlusTwo = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.MarksheetPath = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.ProfilePhoto = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.MemoryPhoto1 = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.MemoryPhoto2 = reader.GetString(14);
					if (!(reader[15] is DBNull)) beData.DegreeTitle = reader.GetString(15);
					if (!(reader[16] is DBNull)) beData.University = reader.GetString(16);
					if (!(reader[17] is DBNull)) beData.CurPosition = reader.GetString(17);
					if (!(reader[18] is DBNull)) beData.CurCompany = reader.GetString(18);
					if (!(reader[19] is DBNull)) beData.CurUniversity = reader.GetString(19);
					if (!(reader[20] is DBNull)) beData.Achievements = reader.GetString(20);
					if (!(reader[21] is DBNull)) beData.Achievement_Doc = reader.GetString(21);
					if (!(reader[22] is DBNull)) beData.Bio = reader.GetString(22);
					if (!(reader[23] is DBNull)) beData.Remarks = reader.GetString(23);
					if (!(reader[24] is DBNull)) beData.DOB_BS = reader.GetString(24);

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

