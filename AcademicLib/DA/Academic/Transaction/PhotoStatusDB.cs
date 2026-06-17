using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class PhotoStatusDB
    {
        DataAccessLayer1 dal = null;
        public PhotoStatusDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
		public BE.Academic.Transaction.PhotoStatus getAllPhotoStatus(int UserId, int EntityId, int AcademicYearId,int BranchId)
		{
			BE.Academic.Transaction.PhotoStatus photoStatus = new BE.Academic.Transaction.PhotoStatus();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@BranchId", BranchId);
			cmd.CommandText = "usp_GetStudentEmployeePhotoStatus";
			try
			{
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                photoStatus.studentPhotoStatusColl = new BE.Academic.Transaction.StudentPhotoStatusColl();
				while (reader.Read())
				{
					BE.Academic.Transaction.StudentPhotoStatus stdbeData = new BE.Academic.Transaction.StudentPhotoStatus();
					if (!(reader[0] is DBNull)) stdbeData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) stdbeData.ClassDetails = reader.GetString(1);
					if (!(reader[2] is DBNull)) stdbeData.TotalStudent = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) stdbeData.StdPhotoUploaded = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) stdbeData.StdRemaining = reader.GetInt32(4);
					photoStatus.studentPhotoStatusColl.Add(stdbeData);
				}
                if (reader.NextResult())
                {

					photoStatus.employeePhotoStatusColl = new BE.Academic.Transaction.EmployeePhotoStatusColl();
					while (reader.Read())
					{
						BE.Academic.Transaction.EmployeePhotoStatus empbeData = new BE.Academic.Transaction.EmployeePhotoStatus();
						if (!(reader[0] is DBNull)) empbeData.DepartmentId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) empbeData.Department = reader.GetString(1);
						if (!(reader[2] is DBNull)) empbeData.TotalEmployee = reader.GetInt32(2);
						if (!(reader[3] is DBNull)) empbeData.EmpPhotoUploaded = reader.GetInt32(3);
						if (!(reader[4] is DBNull)) empbeData.EmpRemaining = reader.GetInt32(4);
						photoStatus.employeePhotoStatusColl.Add(empbeData);
					}
				}
				reader.Close();
				photoStatus.IsSuccess = true;
				photoStatus.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ee)
			{
				photoStatus.IsSuccess = false;
				photoStatus.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return photoStatus;
		}
	}
}