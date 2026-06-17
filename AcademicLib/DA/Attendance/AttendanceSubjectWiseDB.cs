using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.Attendance
{
    internal class AttendanceSubjectWiseDB
    {
        DataAccessLayer1 dal = null;
        public AttendanceSubjectWiseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int AcademicYearId,BE.Attendance.AttendanceSubjectWiseCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                if (dataColl.Count > 0)
                {
                    var fData = dataColl.First();
                    cmd.Parameters.AddWithValue("@UserId", fData.CUserId);
                    cmd.Parameters.AddWithValue("@ClassId", fData.ClassId);
                    cmd.Parameters.AddWithValue("@ForDate", fData.ForDate);
                    cmd.Parameters.AddWithValue("@InOutMode", fData.InOutMode);
                    cmd.Parameters.AddWithValue("@SectionId", fData.SectionId);
                    cmd.Parameters.AddWithValue("@SubjectId", fData.SubjectId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@PeriodId", fData.PeriodId);
                    cmd.CommandText = "usp_DelStudentSubjectWiseAttendance";
                    cmd.ExecuteNonQuery();
                }
                
                foreach (var beData in dataColl)
                {
                    if (beData.Attendance.HasValue)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ForDate", beData.ForDate);
                        cmd.Parameters.AddWithValue("@InOutMode", beData.InOutMode);
                        cmd.Parameters.AddWithValue("@Attendance", beData.Attendance);
                        cmd.Parameters.AddWithValue("@LateMin", beData.LateMin);
                        cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                        cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                        cmd.Parameters.AddWithValue("@PeriodId", beData.PeriodId);
                        cmd.CommandText = "usp_AddStudentSubjectWiseAttendance";
                        cmd.ExecuteNonQuery();

                    }
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student SubjectWise Attendance Done";
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

        public BE.Attendance.AttendanceSubjectWiseCollections getClassWiseAttendance(int UserId,int AcademicYearId, int ClassId, int? SectionId,int SubjectId, DateTime forDate, int InOutMode = 2, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null,int? PeriodId=null)
        {
            BE.Attendance.AttendanceSubjectWiseCollections dataColl = new BE.Attendance.AttendanceSubjectWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ForDate", forDate);
            cmd.Parameters.AddWithValue("@InOutMode", InOutMode);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);

            cmd.Parameters.AddWithValue("@PeriodId", PeriodId);
            cmd.CommandText = "usp_GetStudentSubjectWiseAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Attendance.AttendanceSubjectWise beData = new BE.Attendance.AttendanceSubjectWise();
                    if (!(reader[0] is DBNull)) beData.Attendance = (BE.Attendance.ATTENDANCES)reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LateMin = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Remarks = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.StudentId = reader.GetInt32(3);
                    try
                    {
                        if (!(reader[4] is DBNull)) beData.RegdNo = reader.GetString(4);
                        if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                        if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                        if (!(reader[7] is DBNull)) beData.PhotoPath = reader.GetString(7);
                        if (!(reader[8] is DBNull)) beData.ClassName = reader.GetString(8);
                        if (!(reader[9] is DBNull)) beData.SectionName = reader.GetString(9);
                        if (!(reader[10] is DBNull)) beData.SubjectId = reader.GetInt32(10);
                        if (!(reader[11] is DBNull)) beData.SubjectCode = reader.GetString(11);
                        if (!(reader[12] is DBNull)) beData.SubjectName = reader.GetString(12);
                        if (!(reader[13] is DBNull)) beData.Batch = reader.GetString(13);
                        if (!(reader[14] is DBNull)) beData.Factulty = reader.GetString(14);
                        if (!(reader[15] is DBNull)) beData.Level = reader.GetString(15);
                        if (!(reader[16] is DBNull)) beData.Semester = reader.GetString(16);
                        if (!(reader[17] is DBNull)) beData.ClassYear = reader.GetString(17);
                        if (!(reader[18] is DBNull)) beData.PeriodId = reader.GetInt32(18);
                    }
                    catch { }

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
        public AcademicLib.API.Teacher.AttendanceSummaryCollections getClassWiseSummary(int UserId, int AcademicYearId, int? ClassId, int? SectionId,int? SubjectId, DateTime? fromDate, DateTime? toDate)
        {
            AcademicLib.API.Teacher.AttendanceSummaryCollections dataColl = new API.Teacher.AttendanceSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@DateFrom", fromDate);
            cmd.Parameters.AddWithValue("@DateTo", toDate);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetClassWiseSummarySubjectDA";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Teacher.AttendanceSummary beData = new API.Teacher.AttendanceSummary();
                    if (!(reader[0] is DBNull)) beData.DT_AD = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) beData.DT_BS = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SectionId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.SubjectId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SubjectName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.NoOfStudent = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Present = Convert.ToInt32(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Absent = Convert.ToInt32(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Late = Convert.ToInt32(reader[11]);
                    if (!(reader[12] is DBNull)) beData.Leave = Convert.ToInt32(reader[12]);
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
        public ResponeValues DeleteSubjectWiseAttendance(int UserId, DateTime? ForDate, int? ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId, int? AttendaneTypeId, int? PeriodId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForDate", ForDate);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@AttendaneTypeId", AttendaneTypeId);
            cmd.Parameters.AddWithValue("@PeriodId", PeriodId);
            cmd.CommandText = "usp_DeleteSubjectWiseAttendance";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

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
    }
}
