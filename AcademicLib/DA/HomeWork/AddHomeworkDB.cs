using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.HomeWork
{

	internal class AddHomeworkDB
	{

		DataAccessLayer1 dal = null;
		public AddHomeworkDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public AcademicLib.BE.HomeWork.TeacherNameCollections GetTeacherName(int UserId, int EntityId)
		{
			AcademicLib.BE.HomeWork.TeacherNameCollections dataColl = new AcademicLib.BE.HomeWork.TeacherNameCollections();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetTeacherName";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.HomeWork.TeacherName beData = new AcademicLib.BE.HomeWork.TeacherName();
					if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.EmployeeCode = reader.GetString(2);
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
		public AcademicLib.BE.HomeWork.TeacherWiseClassCollections GetTeacherWiseClass(int UserId, int EntityId, int EmployeeId, int? AcademicYearId)
		{
			AcademicLib.BE.HomeWork.TeacherWiseClassCollections dataColl = new AcademicLib.BE.HomeWork.TeacherWiseClassCollections();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.CommandText = "usp_GetTeacherWiseClass";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.HomeWork.TeacherWiseClass beData = new AcademicLib.BE.HomeWork.TeacherWiseClass();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
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