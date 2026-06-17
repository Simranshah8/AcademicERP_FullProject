using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.OnlineClass
{
    internal class OnlinePlatformDB
    {
        DataAccessLayer1 dal = null;
        public OnlinePlatformDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.OnlineClass.OnlinePlatform beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.UserId);
            cmd.Parameters.AddWithValue("@PlatformType", beData.PlatformType);
            cmd.Parameters.AddWithValue("@UserName", beData.UserName);
            cmd.Parameters.AddWithValue("@Pwd", beData.Pwd);
            cmd.Parameters.AddWithValue("@Link", beData.Link);
            cmd.Parameters.AddWithValue("@CreateBy", beData.CUserId);
            cmd.CommandText = "usp_AddUpdateOnlinePlatform";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            try
            {
                cmd.ExecuteNonQuery();
              
                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[8].Value);

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
        public BE.OnlineClass.OnlinePlatformCollections getAllOnlinePlatform(int UserId, int EntityId)
        {
            BE.OnlineClass.OnlinePlatformCollections dataColl = new BE.OnlineClass.OnlinePlatformCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllOnlinePlatform";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.OnlineClass.OnlinePlatform beData = new BE.OnlineClass.OnlinePlatform();                    
                    if (!(reader[0] is DBNull)) beData.PlatformType =(BE.Global.ONLINE_PLATFORMS)reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Pwd = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Link = reader.GetString(3);
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

        public RE.OnlineClass.DateWiseAttendanceCollections getDateWiseAttendance(int UserId, DateTime forDate,int? classId,int? sectionId)
        {
            RE.OnlineClass.DateWiseAttendanceCollections dataColl = new RE.OnlineClass.DateWiseAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForDate", forDate);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.CommandText = "usp_GetDateWiseOnlineAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.OnlineClass.DateWiseAttendance beData = new RE.OnlineClass.DateWiseAttendance();
                    if (!(reader[0] is DBNull)) beData.StudentId =reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.FatherName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.F_ContactNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Name = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SubjectName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Period = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.ClassStartTime = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.ClassEndTime = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.JoinDateTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.LeftDateTime = reader.GetDateTime(13);
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

        public RE.OnlineClass.DateWiseAttendanceCollections getSubjectWiseAttendance(int UserId, DateTime fromDate,DateTime toDate, int? classId, int? sectionId,int subjectId)
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
            cmd.CommandText = "usp_GetSubjectWiseOnlineAttendance";
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
                    if (!(reader[10] is DBNull)) beData.ClassStartTime = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.ClassEndTime = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.JoinDateTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.LeftDateTime = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.Batch = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.Faculty = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Level = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Semester = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.ClassYear = reader.GetString(18);                    

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

        public RE.OnlineClass.EmployeeAttendanceCollections getEmployeeAttendance(int UserId, DateTime fromDate, DateTime toDate)
        {
            RE.OnlineClass.EmployeeAttendanceCollections dataColl = new RE.OnlineClass.EmployeeAttendanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);
            cmd.CommandText = "usp_GetEmployeeOnlineClassAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.OnlineClass.EmployeeAttendance beData = new RE.OnlineClass.EmployeeAttendance();
                    if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Department = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Designation = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ForDate_AD = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.ForDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ScheduleClass = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.NoOfClassHosted = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.TranIdColl = reader.GetString(9);
                    
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

        public AcademicLib.RE.Academic.PassedOnlineClassCollections getEmployeeAttendanceDet(int UserId, string tranIdColl)
        {
            AcademicLib.RE.Academic.PassedOnlineClassCollections dataColl = new RE.Academic.PassedOnlineClassCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranIdColl", tranIdColl);            
            cmd.CommandText = "usp_GetEmpOnlineAttendanceDetails";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.PassedOnlineClass beData = new RE.Academic.PassedOnlineClass();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.PlatformType = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.ShiftName = Convert.ToString(reader[2]);
                    if (!(reader[3] is DBNull)) beData.ClassName = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) beData.SubjectName = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) beData.StartDateTime_AD = Convert.ToDateTime(reader[5]);
                    if (!(reader[6] is DBNull)) beData.StartDate_BS = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) beData.EndDateTime_AD = Convert.ToDateTime(reader[7]);
                    if (!(reader[8] is DBNull)) beData.EndDate_BS = Convert.ToString(reader[8]);
                    if (!(reader[9] is DBNull)) beData.IsRunning = Convert.ToBoolean(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Notes = Convert.ToString(reader[10]);
                    if (!(reader[11] is DBNull)) beData.UserName = Convert.ToString(reader[11]);
                    if (!(reader[12] is DBNull)) beData.Pwd = Convert.ToString(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Link = Convert.ToString(reader[13]);
                    if (!(reader[14] is DBNull)) beData.TeacherName = Convert.ToString(reader[14]);
                    if (!(reader[15] is DBNull)) beData.ContactNo = Convert.ToString(reader[15]);
                    if (!(reader[16] is DBNull)) beData.Duration = Convert.ToInt32(reader[16]);
                    if (!(reader[17] is DBNull)) beData.NoOfStudent = Convert.ToInt32(reader[17]);
                    if (!(reader[18] is DBNull)) beData.NoOfPresent = Convert.ToInt32(reader[18]);
                    if (!(reader[19] is DBNull)) beData.FirstJoinAt = Convert.ToString(reader[19]);
                    if (!(reader[20] is DBNull)) beData.LastJoinAt = Convert.ToString(reader[20]);
                    if (!(reader[21] is DBNull)) beData.PresentMinute = Convert.ToInt32(reader[21]);
                    if (!(reader[22] is DBNull)) beData.TeacherPhotoPath = Convert.ToString(reader[22]);
                    if (!(reader[23] is DBNull)) beData.SectionName = Convert.ToString(reader[23]);
                    beData.ForDate = new DateTime(beData.StartDateTime_AD.Year, beData.StartDateTime_AD.Month, beData.StartDateTime_AD.Day);                    
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
