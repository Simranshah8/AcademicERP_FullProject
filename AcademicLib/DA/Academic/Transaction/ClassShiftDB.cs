using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class ClassShiftDB
    {
        DataAccessLayer1 dal = null;
        public ClassShiftDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int AcademicYearId, BE.Academic.Transaction.ClassShift beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@WeeklyDayOff", beData.WeeklyDayOff);
            cmd.Parameters.AddWithValue("@StartTime", beData.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", beData.EndTime);
            cmd.Parameters.AddWithValue("@NoofBreak", beData.NoofBreak);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ClassShiftId", beData.ClassShiftId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateClassShift";
            }
            else
            {
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddClassShift";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@ForOnlineClass", beData.ForOnlineClass);
            cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@AbsentNoticeTime", beData.AbsentNoticeTime);
            cmd.Parameters.AddWithValue("@SecondWeeklyDayOff", beData.SecondWeeklyDayOff);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

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
        public AcademicLib.BE.Academic.Transaction.ClassShiftCollections getAllClassShift(int UserId, int EntityId,int AcademicYearId,bool ForTran)
        {
            AcademicLib.BE.Academic.Transaction.ClassShiftCollections dataColl = new AcademicLib.BE.Academic.Transaction.ClassShiftCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ForTran", ForTran);
            cmd.CommandText = "usp_GetAllClassShift";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ClassShift beData = new AcademicLib.BE.Academic.Transaction.ClassShift();
                    beData.ClassShiftId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.WeeklyDayOff = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.StartTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.EndTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.NoofBreak = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ForOnlineClass = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsActive = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.SecondWeeklyDayOff = reader.GetInt32(8);
                    if (beData.StartTime.HasValue && beData.EndTime.HasValue)
                    {
                        int min =(int) (beData.EndTime.Value - beData.StartTime.Value).TotalMinutes;
                        beData.Duration = min;

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
        public AcademicLib.BE.Academic.Transaction.ClassShift getClassShiftById(int UserId, int EntityId, int ClassShiftId)
        {
            AcademicLib.BE.Academic.Transaction.ClassShift beData = new AcademicLib.BE.Academic.Transaction.ClassShift();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetClassShiftById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Academic.Transaction.ClassShift();
                    beData.ClassShiftId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.WeeklyDayOff = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.StartTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.EndTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.NoofBreak = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ForOnlineClass = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsActive = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.AbsentNoticeTime = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.SecondWeeklyDayOff = reader.GetInt32(9);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int ClassShiftId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
            cmd.CommandText = "usp_DelClassShiftById";
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

        //Added by Simran
        public AcademicLib.BE.Academic.Transaction.ClassShiftCollections GetClassWiseShift(int UserId, int EntityId, int? AcademicYearId,int ClassId)
        {
            AcademicLib.BE.Academic.Transaction.ClassShiftCollections dataColl = new AcademicLib.BE.Academic.Transaction.ClassShiftCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_GetClassWiseShift";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ClassShift beData = new AcademicLib.BE.Academic.Transaction.ClassShift();
                    beData.ClassShiftId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.WeeklyDayOff = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.StartTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.EndTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.NoofBreak = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ForOnlineClass = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsActive = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.SecondWeeklyDayOff = reader.GetInt32(8);
                    if (beData.StartTime.HasValue && beData.EndTime.HasValue)
                    {
                        int min = (int)(beData.EndTime.Value - beData.StartTime.Value).TotalMinutes;
                        beData.Duration = min;

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
        //End
    }
}
