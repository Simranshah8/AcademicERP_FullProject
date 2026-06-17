using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Scholarship
{

	internal class StudentListDB
	{
		DataAccessLayer1 dal = null;
		public StudentListDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		
		public BE.Scholarship.StudentListCollections getAllStudentList(int UserId, int EntityId, int? SubjectId)
		{
			BE.Scholarship.StudentListCollections dataColl = new BE.Scholarship.StudentListCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.CommandText = "usp_GetAllStudentList";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Scholarship.StudentList beData = new BE.Scholarship.StudentList();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.CandidateName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.FatherName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.MotherName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.GenderName = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.DOBMiti = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Email = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.MobileNo = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.SubjectName = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.ScholarshipTypeName = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.SchoolTypeName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.RollNo = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.ExamCenter = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.ExamMiti = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.ReservationGroup = reader.GetString(14);
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

