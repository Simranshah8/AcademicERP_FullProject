using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Attendance
{
    internal class AttendanceStudentWiseDB
    {
        DataAccessLayer1 dal = null;
        public AttendanceStudentWiseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int AcademicYearId, BE.Attendance.AttendanceStudentWiseCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                var fData = dataColl.First();                
                cmd.Parameters.AddWithValue("@UserId", fData.CUserId);
                cmd.Parameters.AddWithValue("@ClassId", fData.ClassId);
                cmd.Parameters.AddWithValue("@ForDate", fData.ForDate);
                cmd.Parameters.AddWithValue("@InOutMode", fData.InOutMode);
                cmd.Parameters.AddWithValue("@SectionId", fData.SectionId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.CommandText = "usp_DelStudentDailyAttendance";
                cmd.ExecuteNonQuery();
                
                foreach (var beData in dataColl)
                {
                    if (beData.Attendance.HasValue)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ForDate", beData.ForDate);
                        cmd.Parameters.AddWithValue("@InOutMode", beData.InOutMode);
                        cmd.Parameters.AddWithValue("@Attendance", beData.Attendance.Value);
                        cmd.Parameters.AddWithValue("@LateMin", beData.LateMin);
                        cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                        cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                        cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                        cmd.CommandText = "usp_AddStudentDailyAttendance";
                        cmd.ExecuteNonQuery();

                    }
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student Daily Attendance Done";
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

        public BE.Attendance.AttendanceStudentWiseCollections getClassWiseAttendance(int UserId,int AcademicYearId, int ClassId,int? SectionId,DateTime forDate,int InOutMode=2, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            BE.Attendance.AttendanceStudentWiseCollections dataColl = new BE.Attendance.AttendanceStudentWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ForDate", forDate);
            cmd.Parameters.AddWithValue("@InOutMode", InOutMode);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetStudentDailyAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Attendance.AttendanceStudentWise beData = new BE.Attendance.AttendanceStudentWise();                                    
                    if (!(reader[0] is DBNull)) beData.Attendance =(BE.Attendance.ATTENDANCES) reader.GetInt32(0);
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

                        if (!(reader[10] is DBNull)) beData.BatchId = reader.GetInt32(10);
                        if (!(reader[11] is DBNull)) beData.SemesterId = reader.GetInt32(11);
                        if (!(reader[12] is DBNull)) beData.ClassYearId = reader.GetInt32(12);
                        if (!(reader[13] is DBNull)) beData.Batch = reader.GetString(13);
                        if (!(reader[14] is DBNull)) beData.Semester = reader.GetString(14);
                        if (!(reader[15] is DBNull)) beData.ClassYear = reader.GetString(15);

                    }
                    catch { }

                    beData.IsSuccess = true;
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
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

        public RE.Attendance.StudentManualDailyAttendanceCollections getManualDailyAttendance(int UserId,int AcademicYearId, int? ClassId, int? SectionId, DateTime forDate, int InOutMode = 2,int? BatchId=null,int? SemesterId=null,int? ClassYearId=null, int? ClassShiftId = null)
        {
            RE.Attendance.StudentManualDailyAttendanceCollections dataColl = new RE.Attendance.StudentManualDailyAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ForDate", forDate);
            cmd.Parameters.AddWithValue("@InOutMode", InOutMode);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
            //Added By Suresh on 17 Magh starts
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            //Ends
            cmd.CommandText = "usp_GetStudentManualDailyAttendance";
            try
            {
                int sno = 1;
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Attendance.StudentManualDailyAttendance beData = new RE.Attendance.StudentManualDailyAttendance();
                    beData.SNo = sno;
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegdNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Attendance = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.LateMin = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.Remarks = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.UserId = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.Batch = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Semester = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.ClassYear = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.BatchId = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.SemesterId = reader.GetInt32(17);
                    if (!(reader[18] is DBNull)) beData.ClassYearId = reader.GetInt32(18);
                  
                    if (!(reader[19] is DBNull)) beData.MotherName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.M_Contact = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.GuardianName = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.G_ContactNo = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.PersonalContactNo = reader.GetString(23);
                    dataColl.Add(beData);
                    sno++;
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

        public AcademicLib.API.Teacher.AttendanceSummaryCollections getClassWiseSummary(int UserId,int AcademicYearId, int? ClassId, int? SectionId, DateTime? fromDate,DateTime? toDate)
        {
            AcademicLib.API.Teacher.AttendanceSummaryCollections dataColl = new API.Teacher.AttendanceSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@DateFrom", fromDate);
            cmd.Parameters.AddWithValue("@DateTo", toDate);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetClassWiseSummaryDA";
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
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.NoOfStudent = Convert.ToInt32(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Present = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) beData.Absent = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Late = Convert.ToInt32(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Leave = Convert.ToInt32(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Holiday = Convert.ToInt32(reader[11]);
                    if (!(reader[12] is DBNull)) beData.IsWeekEnd = Convert.ToBoolean(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Batch = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Semester = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.ClassYear = reader.GetString(15);

                    if (beData.NoOfStudent > 0 && beData.Holiday==0)
                    {
                        beData.PresentPer =Math.Round((beData.Present / beData.NoOfStudent) * 100,2);
                        beData.AbsentPer = Math.Round((beData.Absent / beData.NoOfStudent) * 100, 2);
                        beData.LatePer = Math.Round((beData.Late / beData.NoOfStudent) * 100, 2);
                        beData.LeavePer = Math.Round((beData.Leave / beData.NoOfStudent) * 100, 2);
                    }

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
        public RE.OnlineClass.DateWiseAttendanceCollections getPeriodWiseAttendance(int UserId,int AcademicYearId, DateTime forDate, int? classId, int? sectionId, int? BatchId = null, int? ClassYearId = null, int? SemesterId = null)
        {
            RE.OnlineClass.DateWiseAttendanceCollections dataColl = new RE.OnlineClass.DateWiseAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForDate", forDate);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetPeriodWiseManualAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.OnlineClass.DateWiseAttendance beData = new RE.OnlineClass.DateWiseAttendance();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.FatherName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.F_ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Name = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SubjectName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Period = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.Attendance = reader.GetString(10);
                   
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

        public RE.OnlineClass.DateWiseAttendanceCollections getSubjectWiseAttendance(int UserId,int AcademicYearId, DateTime fromDate, DateTime toDate, int? classId, int? sectionId, int subjectId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            RE.OnlineClass.DateWiseAttendanceCollections dataColl = new RE.OnlineClass.DateWiseAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@SubjectId", subjectId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetSubjectWiseManualAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.OnlineClass.DateWiseAttendance beData = new RE.OnlineClass.DateWiseAttendance();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.FatherName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.F_ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Name = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ForDate_AD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.ForDate_BS = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Attendance = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.NY = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.NM = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.ND = reader.GetInt32(13);
                    try
                    {
                        if (!(reader[14] is DBNull)) beData.SubjectName = reader.GetString(14);
                        //if (!(reader[15] is DBNull)) beData.Batch = reader.GetString(15);
                        //if (!(reader[16] is DBNull)) beData.Faculty = reader.GetString(16);
                        //if (!(reader[17] is DBNull)) beData.Level = reader.GetString(17);
                        //if (!(reader[18] is DBNull)) beData.Semester = reader.GetString(18);
                        //if (!(reader[19] is DBNull)) beData.ClassYear = reader.GetString(19);
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
        public List<AcademicLib.RE.Global.StudentMonthlyAttendanceSumm> getMonthlyAttendanceSummaryForDB(int UserId, int AcademicYearId, DateTime? fromDate, DateTime? toDate, int? MonthId)
        {
            List<AcademicLib.RE.Global.StudentMonthlyAttendanceSumm> dataColl = new List<RE.Global.StudentMonthlyAttendanceSumm>();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", fromDate);
            cmd.Parameters.AddWithValue("@DateTo", toDate);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@MonthId", MonthId);
            cmd.CommandText = "usp_GetDailyAttendanceSummaryOfStudentDB";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Global.StudentMonthlyAttendanceSumm att = new RE.Global.StudentMonthlyAttendanceSumm();
                    if (!(reader[0] is DBNull)) att.NY = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) att.TotalPresent = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) att.TotalStudent = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) att.TotalHoliday = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) att.TotalWeekEnd = Convert.ToInt32(reader[4]);
                    dataColl.Add(att);
                }
                reader.Close();
                return dataColl;
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }

        }
        public ResponeValues DeleteStudentManualDailyAttendance(int UserId, DateTime? ForDate, int? ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId, int? AttendaneTypeId)
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
            cmd.CommandText = "usp_DeleteStudentManualDailyAttendance";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

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
        public RE.Attendance.PeriodForAttendanceCollections getPeriodForAttendance(int UserId, int EntityId, int? BatchId, int? ClassId, int? SectionId, int? SemesterId, int? ClassYearId, int SubjectId)
        {
            RE.Attendance.PeriodForAttendanceCollections dataColl = new RE.Attendance.PeriodForAttendanceCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.CommandText = "usp_GetPeriodForAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Attendance.PeriodForAttendance beData = new RE.Attendance.PeriodForAttendance();
                    if (!(reader[0] is DBNull)) beData.Period = reader.GetInt32(0);

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

        public List<AcademicLib.RE.Global.EmployeeMonthlyAttendanceSum> getMonthlyAttendanceSummaryForEmpDB(int UserId, int? YearId, int? MonthId)
        {
            List<AcademicLib.RE.Global.EmployeeMonthlyAttendanceSum> dataColl = new List<RE.Global.EmployeeMonthlyAttendanceSum>();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@YearId", YearId);
            cmd.Parameters.AddWithValue("@MonthId", MonthId);
            cmd.CommandText = "usp_GetDailyAttendanceSummaryOfEmployeeDB";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Global.EmployeeMonthlyAttendanceSum att = new RE.Global.EmployeeMonthlyAttendanceSum();
                    if (!(reader[0] is DBNull)) att.NY = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) att.TotalPresent = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) att.TotalLate = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) att.TotalLeave = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) att.TotalEmployee = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is DBNull)) att.IsHoliday = Convert.ToBoolean(reader[5]);
                    if (!(reader[6] is DBNull)) att.IsWeekend = Convert.ToBoolean(reader[6]);
                    //if (!(reader[7] is DBNull)) att.IsWeekend = Convert.ToBoolean(reader[7]);
                    dataColl.Add(att);
                }
                reader.Close();
                return dataColl;
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }

        }
    }
}
