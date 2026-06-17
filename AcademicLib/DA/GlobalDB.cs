using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA
{
    internal class GlobalDB
    {
        DataAccessLayer1 dal = null;
        public GlobalDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues UpdateMonthName()
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;          
            cmd.CommandText = "usp_UpdateMonthName";

            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
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
        public ResponeValues ReGenerateQROfStudentEmp(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            

            try
            {
                cmd.CommandText = "usp_UpdateStudentQR";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "usp_UpdateEmployeeQR";
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

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
        public ResponeValues ClearData(int UserId, int ForEntityId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForEntity", ForEntityId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_ClearData";
            
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

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
        public ResponeValues UpdatePwd(int UserId,string OldPwd,string NewPwd,string UserName)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@OldPwd", OldPwd);
            cmd.Parameters.AddWithValue("@NewPwd", NewPwd);                       
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);            
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdatePwd";
            cmd.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                cmd.ExecuteNonQuery();
                
                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

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

        public ResponeValues UpdateUserPwd(int UserId, string NewPwd)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.Parameters.AddWithValue("@NewPwd", NewPwd);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_UpdateUserPwd";

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

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

        public ResponeValues UpdateAllSubjectMapping()
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.CommandText = "usp_ReUpdateAllSubjectMapping";

            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

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

        public BE.Global.NameAndRole GetNamePhotoPath(int UserId)
        {
            BE.Global.NameAndRole resVal = new BE.Global.NameAndRole();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetNamePhotoPath";

            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   
                    if (!(reader[0] is DBNull)) resVal.Name = reader.GetString(0);
                    if (!(reader[1] is DBNull)) resVal.PhotoPath = reader.GetString(1);
                    if (!(reader[2] is DBNull)) resVal.Role = reader.GetString(2);

                    if (!(reader[3] is DBNull)) resVal.SubjectTeacher = reader.GetBoolean(3);
                    if (!(reader[4] is DBNull)) resVal.ClassTeacher = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) resVal.CoOrdinator = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) resVal.HOD = reader.GetBoolean(6);
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                }

                reader.Close();
                
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
        //forId 1=Student,2=Teacher
        public ResponeValues GetUserIdColl(int UserId, string IdColl,int forId=1)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@IdColl", IdColl);
            cmd.Parameters.AddWithValue("@for", forId);            
            cmd.Parameters.Add("@UserIdColl", System.Data.SqlDbType.VarChar, 8000);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_GetUserIdForEmpStudent";

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[3].Value);

                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

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

        public AcademicLib.BE.Global.StudentNotificationCollections GetStudentIdColl(int UserId, string IdColl, int forId = 1)
        {
            AcademicLib.BE.Global.StudentNotificationCollections dataColl = new BE.Global.StudentNotificationCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@IdColl", IdColl);
            cmd.Parameters.AddWithValue("@for", forId);
            cmd.CommandText = "usp_GetStudentForSEN";

            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Global.StudentNotification beData = new BE.Global.StudentNotification();
                    if (!(reader[0] is DBNull)) beData.Id = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.TodayAD = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.TodayBS = reader.GetString(8);
                    dataColl.Add(beData);
                }
                reader.Close();
      
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
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

        public AcademicLib.BE.Global.StudentNotificationCollections GetStudentIdColl(int UserId, string RegIdColl)
        {
            AcademicLib.BE.Global.StudentNotificationCollections dataColl = new BE.Global.StudentNotificationCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@RegIdColl", RegIdColl);            
            cmd.CommandText = "usp_GetStudentByRegId";

            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Global.StudentNotification beData = new BE.Global.StudentNotification();
                    if (!(reader[0] is DBNull)) beData.Id = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.TodayAD = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.TodayBS = reader.GetString(8);
                    dataColl.Add(beData);
                }
                reader.Close();

                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
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

        public AcademicLib.RE.Global.NotificationLogCollections GetNotificationLog(int UserId,bool isGeneral, DateTime? dateFrom,DateTime? dateTo, ref int TotalRows,int PageNumber=1,int RowsOfPage=100,string For="")
        {
            AcademicLib.RE.Global.NotificationLogCollections dataColl = new RE.Global.NotificationLogCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@IsGeneral", isGeneral);
            cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
            cmd.Parameters.AddWithValue("@RowsOfPage", RowsOfPage);
            cmd.Parameters.Add("@TotalRows", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_GetNotificattionLogForUser";
            cmd.Parameters.AddWithValue("@For", For);
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Global.NotificationLog beData = new RE.Global.NotificationLog();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.IsRead = Convert.ToBoolean(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Heading = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Subject = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Content = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ContentPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.EntityId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.EntityName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.LogDate_AD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.LogDate_BS = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.AtTime = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.SendUserBy = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.SendByPhotoPath = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.SendBy = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.NotificationType = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.SendReceived = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.ClassSection = reader.GetString(16);
                    string content = beData.ContentPath;
                    try
                    {
                        if (beData.ContentPath == "0")
                            beData.ContentPath = "";
                        else
                        {
                            int val =0;
                            int.TryParse(content, out val);
                            if (val>0)
                                beData.ContentPath = "";
                        }

                        //if (string.IsNullOrEmpty(beData.ContentPath))
                        //{
                        //    if (!string.IsNullOrEmpty(beData.SendByPhotoPath))
                        //    {
                        //        beData.ContentPath = beData.SendByPhotoPath;
                        //    }
                        //}

                        if (!string.IsNullOrEmpty(beData.ContentPath))
                        {
                            if(!beData.ContentPath.Contains("//") && !beData.ContentPath.Contains("\\") &&  !beData.ContentPath.Contains("/"))
                            {
                                beData.ContentPath = "";
                            }
                        }
                        
                    }
                    catch { }

                    dataColl.Add(beData);
                }
                reader.Close();

                TotalRows = Convert.ToInt32(cmd.Parameters[6].Value);

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

        public AcademicLib.RE.Global.NotificationLogCollections GetNotificationLogForQuickAccess(int UserId, int StudentId)
        {
            AcademicLib.RE.Global.NotificationLogCollections dataColl = new RE.Global.NotificationLogCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.CommandText = "usp_GetNotificattionLogForQuickAccess";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Global.NotificationLog beData = new RE.Global.NotificationLog();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.IsRead = Convert.ToBoolean(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Heading = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Subject = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Content = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ContentPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.EntityId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.EntityName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.LogDate_AD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.LogDate_BS = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.AtTime = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.SendUserBy = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.SendByPhotoPath = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.SendBy = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.NotificationType = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.SendReceived = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.ClassSection = reader.GetString(16);
                    string content = beData.ContentPath;
                    try
                    {
                        if (beData.ContentPath == "0")
                            beData.ContentPath = "";
                        else
                        {
                            int val = 0;
                            int.TryParse(content, out val);
                            if (val > 0)
                                beData.ContentPath = "";
                        }

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

        public AcademicLib.RE.Global.NotificationLogCollections GetTopNotificationLog(int UserId)
        {
            AcademicLib.RE.Global.NotificationLogCollections dataColl = new RE.Global.NotificationLogCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GeTopNotificattionLogForUser";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Global.NotificationLog beData = new RE.Global.NotificationLog();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.IsRead = Convert.ToBoolean(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Heading = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Subject = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Content = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ContentPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.EntityId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.EntityName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.LogDate_AD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.LogDate_BS = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.AtTime = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.SendUserBy = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.SendByPhotoPath = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.SendBy = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.NotificationType = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.SendReceived = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.ClassSection = reader.GetString(16);

                    try
                    {
                        if (beData.ContentPath == "0")
                            beData.ContentPath = "";
                        else
                        {
                            int val = 0;
                            int.TryParse(beData.ContentPath, out val);
                            if (val > 0)
                                beData.ContentPath = "";
                        }

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

        public AcademicLib.BE.Global.CurrentDate GetDateDetail(int UserId, DateTime? forDate)
        {
            AcademicLib.BE.Global.CurrentDate date = new BE.Global.CurrentDate();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForDate", forDate);            
            cmd.CommandText = "usp_GetCurrentDate";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {                    
                    if (!(reader[0] is DBNull)) date.Date_AD = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) date.Date_BS = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) date.NY = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) date.NM = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) date.ND = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) date.MonthName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) date.StartDayId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) date.DaysInMonth = reader.GetInt32(7);                    
                }
                reader.Close();
               

            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
            return date;
        }

        public AcademicLib.BE.Global.CompanyPeriodMonthCollections GetCompanyPeriodMonth(int UserId)
        {
            AcademicLib.BE.Global.CompanyPeriodMonthCollections dataColl = new BE.Global.CompanyPeriodMonthCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetCompanyPeriodMonth";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Global.CompanyPeriodMonth beData = new BE.Global.CompanyPeriodMonth();
                    if (!(reader[0] is DBNull)) beData.NY = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.NM = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.FromDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.ToDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.MonthName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.DaysInMonth = Convert.ToInt32(reader[5]);
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

        public AcademicLib.RE.Global.AdminDashboard GetAdminDashboard(int UserId,int AcademicYearId,int? branchId)
        {
            AcademicLib.RE.Global.AdminDashboard beData = new RE.Global.AdminDashboard();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BranchId", branchId);
            cmd.CommandText = "usp_GetAdminDashboard";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.Quotes = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) beData.QuotesPhotoPath = Convert.ToString(reader[1]);                    
                }
                reader.NextResult();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.Collection = Convert.ToDouble(reader[0]);
                    if (!(reader[1] is DBNull)) beData.Discount = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Assests = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Expenses = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.Income = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.Liability = Convert.ToDouble(reader[5]);
                }
                reader.NextResult();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.Student_Male = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.Student_Female = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Student_Other = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Student_TM_Male = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) beData.Student_TM_Female = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is DBNull)) beData.Student_TM_Other = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Student_HM_Male = Convert.ToInt32(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Student_HM_Female = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) beData.Student_HM_Other = Convert.ToInt32(reader[8]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    RE.Global.HouseWiseStudent houseWise = new RE.Global.HouseWiseStudent();
                    if (!(reader[0] is DBNull)) houseWise.HouseName = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) houseWise.NoOfStudent = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) houseWise.Male = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) houseWise.Female = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) houseWise.Other = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is DBNull)) houseWise.CoOrdinatetor = Convert.ToString(reader[5]);
                    if (!(reader[6] is DBNull)) houseWise.ContactNo = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) houseWise.PhotoPath = Convert.ToString(reader[7]);
                    beData.HouseWiseStudentColl.Add(houseWise);
                }
                reader.NextResult();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.TotalFeeAmt = Convert.ToDouble(reader[0]);
                    if (!(reader[1] is DBNull)) beData.TotalReceivedAmt = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) beData.TotalDiscountAmt = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.TotalDuesAmt = Convert.ToDouble(reader[3]);

                    if (!(reader[4] is DBNull)) beData.TotalFeeAmt_C = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.TotalReceivedAmt_C = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.TotalDiscountAmt_C = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.TotalDuesAmt_C = Convert.ToDouble(reader[7]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    RE.Global.FeeItemWiseDues feeWise = new RE.Global.FeeItemWiseDues();
                    if (!(reader[0] is DBNull)) feeWise.FeeName = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) feeWise.FeeAmt = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) feeWise.ReceivedAmt = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) feeWise.DiscountAmt = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) feeWise.DuesAmt = Convert.ToDouble(reader[4]);                    
                    beData.FeeItemWiseDuesColl.Add(feeWise);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    RE.Global.ClassWiseDues feeWise = new RE.Global.ClassWiseDues();
                    if (!(reader[0] is DBNull)) feeWise.ClassName = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) feeWise.SectionName = Convert.ToString(reader[1]);                    
                    if (!(reader[2] is DBNull)) feeWise.FeeAmt = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) feeWise.ReceivedAmt = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) feeWise.DiscountAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) feeWise.DuesAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) feeWise.NoOfStudent = Convert.ToInt32(reader[6]);
                    if (!(reader[7] is DBNull)) feeWise.ClassTeacher = Convert.ToString(reader[7]);
                    if (!(reader[8] is DBNull)) feeWise.ContactNo = Convert.ToString(reader[8]);
                    if (!(reader[9] is DBNull)) feeWise.PhotoPath = Convert.ToString(reader[9]);
                    beData.ClassWiseDuesColl.Add(feeWise);
                }
                reader.NextResult();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.Emp_T_Male = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.Emp_T_Female = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Emp_NT_Male = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Emp_NT_Female = Convert.ToInt32(reader[3]);
                    beData.EmpTotalMale = beData.Emp_T_Male + beData.Emp_NT_Male;
                    beData.EmpTotalFemale = beData.Emp_T_Female + beData.Emp_NT_Female;
                }
                reader.NextResult();
                while (reader.Read())
                {
                    RE.Global.RouteWiseStudent feeWise = new RE.Global.RouteWiseStudent();
                    if (!(reader[0] is DBNull)) feeWise.VehicleName = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) feeWise.VehicleNo = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) feeWise.Capacity = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) feeWise.RouteName = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) feeWise.NoOfStudent = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is DBNull)) feeWise.DriverName = Convert.ToString(reader[5]);
                    if (!(reader[6] is DBNull)) feeWise.ContactNo = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) feeWise.PhotoPath = Convert.ToString(reader[7]);                    
                    beData.RouteWiseStudentColl.Add(feeWise);
                }
                reader.NextResult();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.CurrentLoginIP = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) beData.CurrentLoginLocaion = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) beData.CurrentLoginAt_AD = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.CurrentLoginAt_BS = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) beData.LastLoginIP = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) beData.LastLoginLocaion = Convert.ToString(reader[5]);
                    if (!(reader[6] is DBNull)) beData.LastLoginAt_AD = Convert.ToDateTime(reader[6]);
                    if (!(reader[7] is DBNull)) beData.LastLoginAt_BS = Convert.ToString(reader[7]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    RE.Global.Vehicle feeWise = new RE.Global.Vehicle();
                    if (!(reader[0] is DBNull)) feeWise.VehicleName = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) feeWise.VehicleNo = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) feeWise.RenewalDate_AD = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) feeWise.RenewalDate_BS = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) feeWise.JachPassNo = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) feeWise.JachPassValidUpto_AD = Convert.ToDateTime(reader[5]);
                    if (!(reader[6] is DBNull)) feeWise.JachPassValidUpto_BS = Convert.ToString(reader[6]);                    
                    beData.VehicleColl.Add(feeWise);
                }
                reader.NextResult();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.Notification_Student = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.Notification_Teacher = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Notification_Admin = Convert.ToInt32(reader[2]);                    
                }
                reader.NextResult();
                while (reader.Read())
                {
                    RE.Global.LibraryBook feeWise = new RE.Global.LibraryBook();
                    if (!(reader[0] is DBNull)) feeWise.PublicationName = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) feeWise.NoOfBook = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) feeWise.Issues = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) feeWise.NotYetReturn = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) feeWise.BookLost = Convert.ToInt32(reader[4]);
                    //if (!(reader[3] is DBNull)) feeWise.Balance = Convert.ToInt32(reader[3]);                    
                    feeWise.Balance = feeWise.NoOfBook - feeWise.Issues;
                    beData.PublicationWiseBookColl.Add(feeWise);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.API.AppCMS.EventHoliday eventHoliday = new API.AppCMS.EventHoliday();
                    if (!(reader[0] is DBNull)) eventHoliday.HolidayEvent = reader.GetString(0);
                    if (!(reader[1] is DBNull)) eventHoliday.EventType = reader.GetString(1);
                    if (!(reader[2] is DBNull)) eventHoliday.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) eventHoliday.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) eventHoliday.FromDate_AD = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) eventHoliday.ToDate_AD = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) eventHoliday.FromDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) eventHoliday.ToDate_BS = reader.GetString(7);
                    if (!(reader[8] is DBNull)) eventHoliday.ForClass = reader.GetString(8);
                    if (!(reader[9] is DBNull)) eventHoliday.ColorCode = reader.GetString(9);
                    if (!(reader[10] is DBNull)) eventHoliday.ImagePath = reader.GetString(10);
                    if (!(reader[11] is DBNull)) eventHoliday.AtTime = reader.GetString(11);

                    int daysDiff = (eventHoliday.FromDate_AD - DateTime.Today).Days;

                    if (daysDiff > 0)
                        eventHoliday.Remaining = daysDiff.ToString() + " Days Remaining";
                    else
                        eventHoliday.Remaining = "Passed";

                    beData.UpcomingEventHolidayColl.Add(eventHoliday);
                }
                reader.NextResult();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.TodayPresentBoys = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.TodayPresentGirls = Convert.ToInt32(reader[1]);
                }
                reader.NextResult();
                beData.StudentMonthlyAttColl = new List<RE.Global.StudentMonthlyAttendanceSumm>();
                while (reader.Read())
                {
                    AcademicLib.RE.Global.StudentMonthlyAttendanceSumm att = new RE.Global.StudentMonthlyAttendanceSumm();
                    if (!(reader[0] is DBNull)) att.NY = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) att.NM = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) att.TotalPresent = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) att.TotalStudent = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) att.AvgStudentPresent = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) att.TotalHoliday = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) att.TotalWeekEnd = Convert.ToInt32(reader[6]);
                    if (!(reader[7] is DBNull)) att.DaysInMonth = Convert.ToInt32(reader[7]);
                    beData.StudentMonthlyAttColl.Add(att);
                }
                reader.NextResult();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.TodayPresentMale = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.TodayPresentFemale = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.TodayLateMale = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) beData.TodayLateFemale = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) beData.TodayLeaveMale = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is DBNull)) beData.TodayLeaveFemale = Convert.ToInt32(reader[5]);
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

        public ResponeValues GetNotificationCountForSend(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "ups_GetNotificationSendCountWithInTime";

            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) resVal.RId = reader.GetInt32(0);
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                }
                reader.Close();
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

        public AcademicLib.BE.Global.CompanyPeriodMonthCollections usp_GetMonthNameList(int UserId,int AcademicYearId)
        {
            AcademicLib.BE.Global.CompanyPeriodMonthCollections dataColl = new BE.Global.CompanyPeriodMonthCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetMonthNameList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Global.CompanyPeriodMonth beData = new BE.Global.CompanyPeriodMonth();
                    if (!(reader[0] is DBNull)) beData.NM = Convert.ToInt32(reader[0]);                    
                    if (!(reader[1] is DBNull)) beData.MonthName = reader.GetString(1);                    
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

        public Dynamic.BusinessEntity.Security.User getUserPwdByQrCode(int UserId,string QrCode)
        {
            Dynamic.BusinessEntity.Security.User date = null;

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@QrCode", QrCode);
            cmd.CommandText = "usp_GetUserAndPwdByQR";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    date = new Dynamic.BusinessEntity.Security.User();
                    if (!(reader[0] is DBNull)) date.UserId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) date.UserName = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) date.Password = reader.GetString(2);                    
                }
                reader.Close();                 
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
            return date;
        }

        public AcademicLib.BE.Global.CompanyPeriodMonthCollections getAcademicYearMonthList(int UserId,int? AcademicYearId,int? StudentId,int? ClassId)
        {
            AcademicLib.BE.Global.CompanyPeriodMonthCollections dataColl = new BE.Global.CompanyPeriodMonthCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_GetAcademicYearMonthList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Global.CompanyPeriodMonth beData = new BE.Global.CompanyPeriodMonth();
                    if (!(reader[0] is DBNull)) beData.NM = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.MonthName = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) beData.YearName = Convert.ToString(reader[2]);
                    if (!(reader[3] is DBNull)) beData.MonthYear = Convert.ToString(reader[3]);

                    try
                    {
                        if (!(reader[4] is DBNull)) beData.MSNo = Convert.ToInt32(reader[4]);
                        if (!(reader[5] is DBNull)) beData.IsRunningMonth = Convert.ToBoolean(reader[5]);
                    }
                    catch{ }
                    int y = 0;
                    int.TryParse(beData.YearName, out y);
                    beData.NY = y;

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

        public Dynamic.BusinessEntity.Security.UserCollections GetWebUser(int UserId)
        {
            Dynamic.BusinessEntity.Security.UserCollections userColl = new Dynamic.BusinessEntity.Security.UserCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            
            cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value="+UserId.ToString()+ " ; select U.UserId,U.UserName from tbl_User(nolock) U left join tbl_SE_User(nolock) S on S.UserId=U.UserId where S.UserId is null ; ";

            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userColl.Add(new Dynamic.BusinessEntity.Security.User()
                    {
                        UserId = reader.GetInt32(0),
                        UserName = reader.GetString(1)
                    });
                }
                reader.Close();
                userColl.IsSuccess = true;
                userColl.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                userColl.IsSuccess = false;
                userColl.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                userColl.IsSuccess = false;
                userColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return userColl;
        }


        public AcademicLib.API.CRM.CustomerLoginLog getNoOfDataForCRM()
        {
            AcademicLib.API.CRM.CustomerLoginLog beData = new API.CRM.CustomerLoginLog();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.CommandText = "usp_forCRM";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {                   
                    if (!(reader[0] is DBNull)) beData.Api_NoOfBranch = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.Api_NoOfUser = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Api_NoOfStudent = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Api_NoOfEmp = Convert.ToInt32(reader[3]);
                }
                reader.Close();

            }
            catch (Exception ee)
            {
                
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;
        }

        public Dynamic.BusinessEntity.Global.MasterNameCodeIdCollections getMasterId(int UserId, Dynamic.BusinessEntity.Global.MasterNameCodeIdCollections paraColl)
        {
            Dynamic.BusinessEntity.Global.MasterNameCodeIdCollections dataColl = new Dynamic.BusinessEntity.Global.MasterNameCodeIdCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetAcademicMasterId";

            try
            {
                System.Data.DataTable tableAllocation = new System.Data.DataTable();
                tableAllocation.Columns.Add("EntityId", typeof(int));
                tableAllocation.Columns.Add("Name", typeof(string));
                tableAllocation.Columns.Add("Code", typeof(string));
                if (paraColl != null)
                {
                    foreach (var v in paraColl)
                    {
                        var row = tableAllocation.NewRow();
                        row["EntityId"] = v.EnityId;
                        row["Name"] = v.Name;
                        row["Code"] = v.Code;
                        tableAllocation.Rows.Add(row);
                    }

                    System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@NameCodeColl", tableAllocation);
                    sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                }

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dynamic.BusinessEntity.Global.MasterNameCodeId ledA = new Dynamic.BusinessEntity.Global.MasterNameCodeId();
                    if (!(reader[0] is DBNull)) ledA.EnityId = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) ledA.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) ledA.Code = reader.GetString(2);
                    if (!(reader[3] is DBNull)) ledA.Id = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) ledA.BDId = reader.GetInt32(4);
                    dataColl.Add(ledA);
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
