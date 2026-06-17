using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.AppCMS.Creation
{
    internal class EventHolidayDB
    {
        DataAccessLayer1 dal = null;
        public EventHolidayDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.AppCMS.Creation.EventHoliday beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //EventTypeId,Name,Description,FromDate,ToDate,ApplicableForClass
            cmd.Parameters.AddWithValue("@EventTypeId", beData.EventTypeId);
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@FromDate", beData.FromDate);
            cmd.Parameters.AddWithValue("@ToDate", beData.ToDate);
            cmd.Parameters.AddWithValue("@ApplicableForClass", beData.ApplicableForClass);
            cmd.Parameters.AddWithValue("@EventHolidayId", beData.EventHolidayId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateEventHoliday";
            }
            else
            {
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddEventHoliday";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@AtTime", beData.AtTime);
            cmd.Parameters.AddWithValue("@DepartmentId", beData.DepartmentId);
            cmd.Parameters.AddWithValue("@DesignationId", beData.DesignationId);
            cmd.Parameters.AddWithValue("@ServiceTypeId", beData.ServiceTypeId);
            cmd.Parameters.AddWithValue("@GenderId", beData.GenderId);
            cmd.Parameters.AddWithValue("@Branch", beData.Branch);
            cmd.Parameters.AddWithValue("@Employee", beData.Employee);
            cmd.Parameters.AddWithValue("@Religion", beData.Religion);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
           
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

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

        public ResponeValues SaveWeekend(int UserId,AcademicLib.BE.AppCMS.Creation.WeekendCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;

           
            try
            {
                cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "delete from tbl_Weekend";
                cmd.ExecuteNonQuery();

                foreach (var beData in dataColl)
                {
                    if (beData.IsChecked)
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@DayId", beData.DayId);
                        cmd.Parameters.AddWithValue("@ColorCode", beData.ColorCode);
                        cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                        //cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "insert into tbl_Weekend(DayId,ColorCode,CreateBy) values(@DayId,@ColorCode,@UserId)";
                        cmd.CommandText = "usp_AddWeekEnd";
                        cmd.ExecuteNonQuery();
                    }                    
                }
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Weekend Save Done";

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
        public AcademicLib.BE.AppCMS.Creation.WeekendCollections getWeekend(int UserId,string BranchCode)
        {
            AcademicLib.BE.AppCMS.Creation.WeekendCollections dataColl = new AcademicLib.BE.AppCMS.Creation.WeekendCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);            
            cmd.CommandText = "usp_GetAllWeekend";
            try
            {                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.Weekend beData = new AcademicLib.BE.AppCMS.Creation.Weekend();                    
                    if (!(reader[0] is DBNull)) beData.DayId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ColorCode = reader.GetString(1);                    
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
        public AcademicLib.BE.AppCMS.Creation.EventHolidayCollections getAllEventHoliday(int UserId, int EntityId,string BranchCode)
        {
            AcademicLib.BE.AppCMS.Creation.EventHolidayCollections dataColl = new AcademicLib.BE.AppCMS.Creation.EventHolidayCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.CommandText = "usp_GetAllEventHoliday";
            try
            {
                //EventTypeId,Name,Description,FromDate,ToDate,ApplicableForClass
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.AppCMS.Creation.EventHoliday beData = new AcademicLib.BE.AppCMS.Creation.EventHoliday();
                    beData.EventHolidayId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EventTypeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.FromDate = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ToDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.ApplicableForClass = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.AtTime = Convert.ToDateTime(reader[7].ToString());
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
        public AcademicLib.BE.AppCMS.Creation.EventHoliday getEventHolidayById(int UserId, int EntityId, int EventHolidayId)
        {
            AcademicLib.BE.AppCMS.Creation.EventHoliday beData = new AcademicLib.BE.AppCMS.Creation.EventHoliday();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventHolidayId", EventHolidayId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEventHolidayById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.AppCMS.Creation.EventHoliday();
                    beData.EventHolidayId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EventTypeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.FromDate = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ToDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.ApplicableForClass = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.AtTime = Convert.ToDateTime(reader[7].ToString());
                    if (!(reader[8] is DBNull)) beData.DepartmentId = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.DesignationId = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ServiceTypeId = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.GenderId = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Branch = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Employee = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Religion = reader.GetString(14);
                    
                    if (!(reader[15] is DBNull)) beData.ClassYearId = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.BatchId = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.SemesterId = reader.GetString(17);
                   
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
        public ResponeValues DeleteById(int UserId, int EntityId, int EventHolidayId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@EventHolidayId", EventHolidayId);
            cmd.CommandText = "usp_DelEventHolidayById";
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

    }
}
