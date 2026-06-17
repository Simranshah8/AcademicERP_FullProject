using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Creation
{

    internal class SyllabusDB
    {
        DataAccessLayer1 dal = null;
        public SyllabusDB(string hostName, string dbName)  
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(int UserId, List<BE.Academic.Creation.TermSyllabus> DataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Bit);
                cmd.Parameters[0].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                System.Data.DataTable tableAllocation = new System.Data.DataTable();
                tableAllocation.Columns.Add("ExamTypeId", typeof(int));
                tableAllocation.Columns.Add("ClassId", typeof(int));
                tableAllocation.Columns.Add("SubjectId", typeof(int));
                tableAllocation.Columns.Add("LessonId", typeof(int));
                tableAllocation.Columns.Add("TopicId", typeof(string));
                tableAllocation.Columns.Add("Content", typeof(string));
                tableAllocation.Columns.Add("TeachingPeriod", typeof(string));
                tableAllocation.Columns.Add("TeachingMethods", typeof(string));
                tableAllocation.Columns.Add("TeachingMaterials", typeof(string));
                tableAllocation.Columns.Add("Evaluation", typeof(string));
                tableAllocation.Columns.Add("Remarks", typeof(string));
                tableAllocation.Columns.Add("BatchId", typeof(int));
                tableAllocation.Columns.Add("SemesterId", typeof(int));
                tableAllocation.Columns.Add("ClassYearId", typeof(int));

                foreach (var v in DataColl)
                {
                    var row = tableAllocation.NewRow();
                    row["ExamTypeId"] = v.ExamTypeId;
                    row["ClassId"] = v.ClassId;
                    row["SubjectId"] = v.SubjectId;
                    row["LessonId"] = v.LessonId;
                    row["TopicId"] = v.TopicId;
                    row["Content"] = v.Content;
                    row["TeachingPeriod"] = v.TeachingPeriod;
                    row["TeachingMethods"] = v.TeachingMethods;
                    row["TeachingMaterials"] = v.TeachingMaterials;
                    row["Evaluation"] = v.Evaluation;
                    row["Remarks"] = v.Remarks;
                    row["BatchId"] = v.BatchId.HasValue ? (object)v.BatchId.Value : DBNull.Value;
                    row["SemesterId"] = v.SemesterId.HasValue ? (object)v.SemesterId.Value : DBNull.Value;
                    row["ClassYearId"] = v.ClassYearId.HasValue ? (object)v.ClassYearId.Value : DBNull.Value;

                    tableAllocation.Rows.Add(row);
                }

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@TermSyllabusColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                cmd.CommandText = "usp_AddTermSyllabus";
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[0].Value is DBNull)) resVal.ResponseMSG = Convert.ToString(cmd.Parameters[0].Value);
                if (!(cmd.Parameters[1].Value is DBNull)) resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[1].Value);
                if (!(cmd.Parameters[2].Value is DBNull)) resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[2].Value);

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

        public BE.Academic.Creation.TermSyllabusColl getAllSyllabus(int UserId, int EntityId)
        {
            BE.Academic.Creation.TermSyllabusColl dataColl = new BE.Academic.Creation.TermSyllabusColl();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllTermSyllabus";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Creation.TermSyllabus beData = new BE.Academic.Creation.TermSyllabus();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ExamTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ClassId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.SubjectId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.LessonId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.TopicId = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.TopicNames = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Content = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.TeachingPeriod = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.TeachingMethods = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.TeachingMaterials = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Evaluation = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Remarks = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.BatchId = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.SemesterId = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.ClassYearId = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.ExamName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.ClassName = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.SubjectName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.LessonCount = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.TopicCount = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.Semester = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.ClassYear = reader.GetString(23);
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


        public BE.Academic.Creation.TermSyllabusColl getAllClassSubjectWiseTermSyllabus(int UserId, int EntityId, int? BatchId, int? ClassId, int? SemesterId, int? ClassYearId, int? ExamTypeId, int? SubjectId)
        {
            BE.Academic.Creation.TermSyllabusColl dataColl = new BE.Academic.Creation.TermSyllabusColl();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.CommandText = "usp_GetAllClassSubjectWiseTermSyllabus";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Creation.TermSyllabus beData = new BE.Academic.Creation.TermSyllabus();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LessonId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.TopicId = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Content = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.TeachingPeriod = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.TeachingMethods = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.TeachingMaterials = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Evaluation = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Remarks = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.LessonName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.TopicNames = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ClassId = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.SubjectId = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.ExamTypeId = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.BatchId = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.SemesterId = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.ClassYearId = reader.GetInt32(16);
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

