using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.DA.Exam.Transaction
{
    public class ObjectiveMarkEntryDB: Dynamic.DataAccess.Common.CommonDB
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
                    if (!(reader[4] is DBNull)) det1.Mark = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) det1.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det1.ColumnWiseFocus = Convert.ToBoolean(reader[6]);
                    if (!(reader[7] is DBNull)) det1.TotObjMark = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) det1.StudentId = reader.GetInt32(8);
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

			public ResponeValues SaveObjectiveMarksEntry(int UserId, List<AcademicLib.BE.Exam.Transaction.Student> DataColl)
			{
				ResponeValues resVal = new ResponeValues();
				dal.OpenConnection();
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				try
				{
					cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
					cmd.Parameters[0].Direction = System.Data.ParameterDirection.Output;

					cmd.Parameters.AddWithValue("@UserId", UserId);
					System.Data.DataTable tableAllocation = new System.Data.DataTable();
					tableAllocation.Columns.Add("StudentId", typeof(int));
					tableAllocation.Columns.Add("Mark", typeof(float));
					tableAllocation.Columns.Add("TotObjMark", typeof(float));
					tableAllocation.Columns.Add("Remarks", typeof(string));
					tableAllocation.Columns.Add("SNo", typeof(int));
					tableAllocation.Columns.Add("ClassId", typeof(int));
					tableAllocation.Columns.Add("SectionId", typeof(int));
					tableAllocation.Columns.Add("SubjectId", typeof(int));
					tableAllocation.Columns.Add("ExamTypeId", typeof(int));
                    tableAllocation.Columns.Add("BatchId", typeof(int));
                    tableAllocation.Columns.Add("SemesterId", typeof(int));
                    tableAllocation.Columns.Add("ClassYearId", typeof(int));
                    tableAllocation.Columns.Add("ColumnWiseFocus", typeof(bool));
                    tableAllocation.Columns.Add("ObjectiveId", typeof(int));
					foreach (var v in DataColl)
					{
						var row = tableAllocation.NewRow();
					    row["StudentId"] = IsDBNull(v.StudentId);
					    row["Mark"] = IsDBNull(v.Mark);
					    row["TotObjMark"] = IsDBNull(v.TotObjMark);
					    row["Remarks"] = IsDBNull(v.Remarks);
					    row["SNo"] = IsDBNull(v.SNo);
					    row["ClassId"] = IsDBNull(v.ClassId);
					    row["SectionId"] = IsDBNull(v.SectionId);
					    row["SubjectId"] = IsDBNull(v.SubjectId);
				     	row["ExamTypeId"] = IsDBNull(v.ExamTypeId);
                        row["BatchId"] = IsDBNull(v.BatchId);
                        row["SemesterId"] = IsDBNull(v.SemesterId);
                        row["ClassYearId"] = IsDBNull(v.ClassYearId);
                        row["ColumnWiseFocus"] = IsDBNull(v.ColumnWiseFocus);
                        row["ObjectiveId"] = IsDBNull(v.ObjectiveId);
				        //row["StudentId"] = v.StudentId;
						//row["Mark"] = v.Mark;
						//row["TotObjMark"] = v.TotObjMark;
						//row["Remarks"] = v.Remarks;
						//row["SNo"] = v.SNo;
						//row["ClassId"] = v.ClassId;
						//row["SectionId"] = v.SectionId;
						//row["SubjectId"] = v.SubjectId;
						//row["ExamTypeId"] = v.ExamTypeId;
						////row["BatchId"] = v.BatchId;
						////row["SemesterId"] = v.SemesterId;
						////row["ClassYearId"] = v.ClassYearId;
						//row["ColumnWiseFocus"] = v.ColumnWiseFocus;

						tableAllocation.Rows.Add(row);
					}

					System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@ObjectiveMarkColl", tableAllocation);
					sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
         
					cmd.CommandText = "usp_SaveObjectiveMarksEntry";
					cmd.ExecuteNonQuery();

					if (!(cmd.Parameters[0].Value is DBNull)) resVal.ResponseMSG = Convert.ToString(cmd.Parameters[0].Value);

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