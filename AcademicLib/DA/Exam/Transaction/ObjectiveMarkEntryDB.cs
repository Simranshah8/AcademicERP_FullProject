using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Exam.Transaction
{
    public class ObjectiveMarkEntryDB
    {
        DataAccessLayer1 dal = null;
        public ObjectiveMarkEntryDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

		public BE.Exam.Transaction.StudentCollection GetStudentList(int UserId, int EntityId, int ClassId,int? SectionId)
		{
			BE.Exam.Transaction.StudentCollection beDataColl = new BE.Exam.Transaction.StudentCollection();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.CommandText = "usp_GetStudentListForObjMarkEntry";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Exam.Transaction.Student det1 = new BE.Exam.Transaction.Student();
					if (!(reader[0] is DBNull)) det1.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.RegNo = reader.GetString(1);
					if (!(reader[2] is DBNull)) det1.StudentName = reader.GetString(2);
					if (!(reader[3] is DBNull)) det1.RollNo = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) det1.SymbolNo = reader.GetString(4);
					beDataColl.Add(det1);
				}
				reader.Close();
				beDataColl.IsSuccess = true;
				beDataColl.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ee)
			{
				beDataColl.IsSuccess = false;
				beDataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return beDataColl;

		}
		public BE.Exam.Transaction.ObjectiveDetailsCollections GetObjectiveList(int UserId, int EntityId, int ClassId, int? SectionId, int SubjectId, int ExamTypeId)
		{
			BE.Exam.Transaction.ObjectiveDetailsCollections beDataColl = new BE.Exam.Transaction.ObjectiveDetailsCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.CommandText = "usp_GetObjectiveList";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Exam.Transaction.ObjectiveDetails det1 = new BE.Exam.Transaction.ObjectiveDetails();
					if (!(reader[0] is DBNull)) det1.ObjectiveId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) det1.Marks = reader.GetDouble(2);
					if (!(reader[3] is DBNull)) det1.SNo = reader.GetInt32(3);
					beDataColl.Add(det1);
				}
				reader.Close();
				beDataColl.IsSuccess = true;
				beDataColl.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ee)
			{
				beDataColl.IsSuccess = false;
				beDataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return beDataColl;

		}
	}
}