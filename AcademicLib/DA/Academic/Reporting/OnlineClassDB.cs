using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Reporting
{
    internal class OnlineClassDB
    {
        DataAccessLayer1 dal = null;
        public OnlineClassDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public AcademicLib.RE.Academic.OnlineClassAdmin getClassList(int UserId, DateTime? forDate)
        {
            AcademicLib.RE.Academic.OnlineClassAdmin data = new RE.Academic.OnlineClassAdmin();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForDate", forDate);            
            cmd.CommandText = "usp_GetSummaryOfOnlineClassForAdmin";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.OnlineClass beData = new RE.Academic.OnlineClass();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.TeacherName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SubjectName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ContactNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.StartDateBS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.StartDateTime = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.Duration = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.Link = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.IsRunning = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.EndDateBS = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.EndDateTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.TimeDiff =Convert.ToInt32(reader[13]);
                    if (!(reader[14] is DBNull)) beData.Present = Convert.ToInt32(reader[14]);
                    if (!(reader[15] is DBNull)) beData.UserId = Convert.ToInt32(reader[15]);
                    data.RunningColl.Add(beData);
                   
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.OnlineClass beData = new RE.Academic.OnlineClass();                    
                    if (!(reader[0] is DBNull)) beData.ClassName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.SectionName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.TeacherName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SubjectName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ContactNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.StartTime = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.EndTime = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassAttem = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.UserId = Convert.ToInt32(reader[8]);
                    if (beData.ClassAttem == "Missed")
                        data.MissedColl.Add(beData);
                    else
                        data.ConductColl.Add(beData);
                }
                reader.NextResult();
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
                    data.PassedColl.Add(beData);
                }
                reader.Close();
                data.IsSuccess = true;
                data.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                data.IsSuccess = false;
                data.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return data;
        }

        public AcademicLib.RE.Academic.OnlineClassAdmin getMissedClassList(int UserId, DateTime? forDate)
        {
            AcademicLib.RE.Academic.OnlineClassAdmin data = new RE.Academic.OnlineClassAdmin();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForDate", forDate);
            cmd.CommandText = "usp_GetMissedClassList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();                
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.OnlineClass beData = new RE.Academic.OnlineClass();
                    if (!(reader[0] is DBNull)) beData.ClassName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.SectionName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.TeacherName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SubjectName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ContactNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.StartTime = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.EndTime = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassAttem = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.UserId = Convert.ToInt32(reader[8]);
                    data.MissedColl.Add(beData);
                }              
                reader.Close();
                data.IsSuccess = true;
                data.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                data.IsSuccess = false;
                data.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return data;
        }
    }
}
