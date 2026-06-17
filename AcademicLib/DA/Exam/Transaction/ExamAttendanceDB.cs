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
    internal class ExamAttendanceDB : Dynamic.DataAccess.Common.CommonDB
    {
        DataAccessLayer1 dal = null;
        public ExamAttendanceDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                foreach (var beData in dataColl)
                {
                    if(beData.StudentId>0 && beData.ExamTypeId > 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                        cmd.Parameters.AddWithValue("@WorkingDays", beData.WorkingDays);
                        cmd.Parameters.AddWithValue("@PresentDays", beData.PresentDays);
                        cmd.Parameters.AddWithValue("@AbsentDays", beData.AbsentDays);
                        cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                        cmd.Parameters.AddWithValue("@DateFrom", beData.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", beData.DateTo);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                        cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                        cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                        cmd.CommandText = "usp_AddExamWiseAttendance";
                        cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                        cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                        cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                        cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
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
                            resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
                    }
                }
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
        public AcademicLib.API.Teacher.StudentForExamAttendanceCollections getStudentForExamAttendance(int UserId,int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId)
        {
            AcademicLib.API.Teacher.StudentForExamAttendanceCollections dataColl = new API.Teacher.StudentForExamAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentListForExamAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Teacher.StudentForExamAttendance beData = new API.Teacher.StudentForExamAttendance();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RegdNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.BoardRegdNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SymbolNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.PhotoPath = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.WorkingDays = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) beData.PresentDays = Convert.ToInt32(reader[9]);
                    if (!(reader[10] is DBNull)) beData.AbsentDays = Convert.ToInt32(reader[10]);
                    if (!(reader[11] is DBNull)) beData.DateFrom = Convert.ToDateTime(reader[11]);
                    if (!(reader[12] is DBNull)) beData.DateTo = Convert.ToDateTime(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Remarks = reader.GetString(13);
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
        public AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections getExamWiseAttendanceClassWise(int UserId,int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId)
        {
            AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetExamWiseAttendanceOfClass";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamwiseAttendence beData = new BE.Exam.Transaction.ExamwiseAttendence();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.WorkingDays = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.PresentDays = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.AbsentDays = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.DateFrom = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.DateTo = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
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

        public AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections getAttendanceForExam(int UserId, int ClassId, int? SectionId,DateTime DateFrom,DateTime DateTo,int? AcademicYearId)
        {
            AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAttendanceForExamType";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamwiseAttendence beData = new BE.Exam.Transaction.ExamwiseAttendence();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.WorkingDays = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.PresentDays = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.AbsentDays = reader.GetInt32(3);
                    beData.DateFrom = DateFrom;
                    beData.DateTo = DateTo;
                    //if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
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
        public AcademicLib.BE.Exam.Transaction.ExamAttendanceCollections getAllExamAttendance(int UserId, int EntityId)
        {
            AcademicLib.BE.Exam.Transaction.ExamAttendanceCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllExamAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamAttendance beData = new AcademicLib.BE.Exam.Transaction.ExamAttendance();
                    beData.ExamAttendanceId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Date = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SubjectId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.PaperTypeId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ExamTypeId = reader.GetInt32(5);

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
        public AcademicLib.BE.Exam.Transaction.ExamAttendance getExamAttendanceById(int UserId, int EntityId, int ExamAttendanceId)
        {
            AcademicLib.BE.Exam.Transaction.ExamAttendance beData = new AcademicLib.BE.Exam.Transaction.ExamAttendance();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamAttendanceId", ExamAttendanceId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExamAttendanceById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Exam.Transaction.ExamAttendance();
                    beData.ExamAttendanceId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Date = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SubjectId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.PaperTypeId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ExamTypeId = reader.GetInt32(5);

                }
                reader.NextResult();

                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamAttendanceDetails Qualification = new AcademicLib.BE.Exam.Transaction.ExamAttendanceDetails();

                    if (!(reader[0] is System.DBNull)) Qualification.ExamAttendanceId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Qualification.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) Qualification.StudentName = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Qualification.SubjectId = reader.GetInt32(3);
                    if (!(reader[4] is System.DBNull)) Qualification.IsPresect = reader.GetBoolean(4);
                    if (!(reader[5] is System.DBNull)) Qualification.IsAbsent = reader.GetBoolean(5);
                    if (!(reader[6] is System.DBNull)) Qualification.Remarks = reader.GetString(6);

                    beData.ExamAttendanceDetailsColl.Add(Qualification);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int ExamAttendanceId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ExamAttendanceId", ExamAttendanceId);
            cmd.CommandText = "usp_DelExamAttendanceById";
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
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

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

        public ResponeValues ImportExamWiseAttendance(int UserId, List<BE.Exam.Transaction.ImportExamWiseAttendance> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 800);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;

            if (dataColl != null)
            {
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("AcademicYear", typeof(string));
                table.Columns.Add("ExamName", typeof(string));
                table.Columns.Add("RegNo", typeof(string));
                table.Columns.Add("DateFrom_AD", typeof(string));
                table.Columns.Add("DateTo_AD", typeof(string));
                table.Columns.Add("DateFrom_BS", typeof(string));
                table.Columns.Add("DateTo_BS", typeof(string));
                table.Columns.Add("WorkingDays", typeof(string));
                table.Columns.Add("PresentDays", typeof(string));
                table.Columns.Add("AbsentDays", typeof(string));
                table.Columns.Add("Remarks", typeof(string));
                foreach (var v in dataColl)
                {
                    var row = table.NewRow();
                    row["AcademicYear"] = IsDBNull(v.AcademicYear);
                    row["ExamName"] = IsDBNull(v.ExamName);
                    row["RegNo"] = IsDBNull(v.RegNo);
                    row["DateFrom_AD"] = IsDBNull(v.DateFrom_AD);   
                    row["DateTo_AD"] = IsDBNull(v.DateTo_AD);
                    row["DateFrom_BS"] = IsDBNull(v.DateFrom_BS);
                    row["DateTo_BS"] = IsDBNull(v.DateTo_BS);
                    row["WorkingDays"] = IsDBNull(v.WorkingDays);   
                    row["PresentDays"] = IsDBNull(v.PresentDays);
                    row["AbsentDays"] = IsDBNull(v.AbsentDays);
                    row["Remarks"] = IsDBNull(v.Remarks);
                    table.Rows.Add(row);
                }
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@DataColl", table);
                sqlParam.SqlDbType = SqlDbType.Structured;

            }

            cmd.CommandText = "usp_ImportExamWiseAttendance";
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if (resVal.IsSuccess)
                    dal.CommitTransaction();
                else
                    dal.RollbackTransaction();
                //resVal.IsSuccess = true;
                //resVal.ResponseMSG = "Bill Generate Success";
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
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
