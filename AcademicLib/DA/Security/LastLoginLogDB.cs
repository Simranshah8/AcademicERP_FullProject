using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Security
{

	internal class LastLoginLogDB
	{
		DataAccessLayer1 dal = null;
		public LastLoginLogDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
	
		public RE.Security.LastLoginLogCollections getAllLastLoginLog(int UserId)
		{
			RE.Security.LastLoginLogCollections dataColl = new RE.Security.LastLoginLogCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.CommandText = "usp_LoginLogUsers";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					RE.Security.LastLoginLog beData = new RE.Security.LastLoginLog();
					if (!(reader[0] is DBNull)) beData.UserId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.UserName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.UserType = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Code = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.Batch = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.Semester = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.ClassYear = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.Faculty = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.Level = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.Department = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.Designation = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.PublicIP = reader.GetString(14);
					if (!(reader[15] is DBNull)) beData.PCName = reader.GetString(15);
					if (!(reader[16] is DBNull)) beData.AppVersion = reader.GetString(16);
					if (!(reader[17] is DBNull)) beData.LogDateTime = reader.GetDateTime(17);
					if (!(reader[18] is DBNull)) beData.LogMitiTime = reader.GetString(18);
					if (!(reader[19] is DBNull)) beData.BeforeDay  = reader.GetInt32(19);
					if (!(reader[20] is DBNull)) beData.RollNo = reader.GetInt32(20);
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

		public RE.Security.LastLoginLogCollections getAllNeverLogin(int UserId)
		{
			RE.Security.LastLoginLogCollections dataColl = new RE.Security.LastLoginLogCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.CommandText = "usp_NeverLogInUsers";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					RE.Security.LastLoginLog beData = new RE.Security.LastLoginLog();
					if (!(reader[0] is DBNull)) beData.UserId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.UserName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.UserType = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Code = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.Batch = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.Semester = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.ClassYear = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.Faculty = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.Level = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.Department = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.Designation = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.RollNo = reader.GetInt32(14);
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


	}

}

