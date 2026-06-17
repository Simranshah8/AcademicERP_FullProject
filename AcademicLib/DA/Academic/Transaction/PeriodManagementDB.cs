using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class PeriodManagementDB
    {
        DataAccessLayer1 dal = null;
        public PeriodManagementDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Transaction.PeriodManagement beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassShiftId", beData.ClassShiftId);
            cmd.Parameters.AddWithValue("@NoOfPeriod", beData.NoOfPeriod);
            cmd.Parameters.AddWithValue("@EachPeriodDuration", beData.EachPeriodDuration);
            cmd.Parameters.AddWithValue("@B1BreakAfterPeriod", beData.B1BreakAfterPeriod);
            cmd.Parameters.AddWithValue("@B1TimeDuration", beData.B1TimeDuration);
            cmd.Parameters.AddWithValue("@B2BreakAfterPeriod", beData.B2BreakAfterPeriod);
            cmd.Parameters.AddWithValue("@B2TimeDuration", beData.B2TimeDuration);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdatePeriodManagement";
            }
            else
            {
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddPeriodManagement";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@B3BreakAfterPeriod", beData.B3BreakAfterPeriod);
            cmd.Parameters.AddWithValue("@B3TimeDuration", beData.B3TimeDuration);
            cmd.Parameters.AddWithValue("@B4BreakAfterPeriod", beData.B4BreakAfterPeriod);
            cmd.Parameters.AddWithValue("@B4TimeDuration", beData.B4TimeDuration);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SavePeriodManagementDetails(beData.CUserId, resVal.RId, beData.PeriodManagementDetailsColl);
                    
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
        private void SavePeriodManagementDetails(int UserId, int PeriodManagementId, List<BE.Academic.Transaction.PeriodManagementDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || PeriodManagementId == 0)
                return;

            foreach (BE.Academic.Transaction.PeriodManagementDetails beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@PeriodManagementId", PeriodManagementId);
                cmd.Parameters.AddWithValue("@Period", beData.Period);
                cmd.Parameters.AddWithValue("@StartTime", beData.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", beData.EndTime);
                cmd.Parameters.AddWithValue("@TimeDuration", beData.TimeDuration);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_AddPeriodManagementDetails";
                cmd.ExecuteNonQuery();
            }

        }

        public AcademicLib.BE.Academic.Transaction.PeriodManagementCollections getAllPeriodManagement(int UserId, int EntityId)
        {
            AcademicLib.BE.Academic.Transaction.PeriodManagementCollections dataColl = new AcademicLib.BE.Academic.Transaction.PeriodManagementCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllPeriodManagement";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.PeriodManagement beData = new AcademicLib.BE.Academic.Transaction.PeriodManagement();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassShiftName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.NoOfPeriod = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.EachPeriodDuration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.B1BreakAfterPeriod = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.B1TimeDuration = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.B2BreakAfterPeriod = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.B2TimeDuration = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.B3BreakAfterPeriod = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.B3TimeDuration = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.B4BreakAfterPeriod = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.B4TimeDuration = reader.GetInt32(11);
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
        public AcademicLib.BE.Academic.Transaction.PeriodManagement getPeriodManagementById(int UserId, int EntityId, int TranId)
        {
            AcademicLib.BE.Academic.Transaction.PeriodManagement beData = new AcademicLib.BE.Academic.Transaction.PeriodManagement();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetPeriodManagementById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Academic.Transaction.PeriodManagement();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassShiftId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.NoOfPeriod = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.EachPeriodDuration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.B1BreakAfterPeriod = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.B1TimeDuration = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.B2BreakAfterPeriod = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.B2TimeDuration = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.B3BreakAfterPeriod = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.B3TimeDuration = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.B4BreakAfterPeriod = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.B4TimeDuration = reader.GetInt32(11);

                }
                beData.PeriodManagementDetailsColl = new List<BE.Academic.Transaction.PeriodManagementDetails>();
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.PeriodManagementDetails det = new BE.Academic.Transaction.PeriodManagementDetails();                    
                    if (!(reader[0] is DBNull)) det.Period = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.StartTime = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) det.EndTime = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) det.TimeDuration = reader.GetInt32(3);
                    beData.PeriodManagementDetailsColl.Add(det);
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
        public AcademicLib.BE.Academic.Transaction.PeriodManagement getPeriodManagementByClassShiftId(int UserId, int EntityId, int ClassShiftId)
        {
            AcademicLib.BE.Academic.Transaction.PeriodManagement beData = new AcademicLib.BE.Academic.Transaction.PeriodManagement();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetPeriodManagementByClassShiftId";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Academic.Transaction.PeriodManagement();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassShiftId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.NoOfPeriod = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.EachPeriodDuration = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.B1BreakAfterPeriod = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.B1TimeDuration = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.B2BreakAfterPeriod = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.B2TimeDuration = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.B3BreakAfterPeriod = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.B3TimeDuration = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.B4BreakAfterPeriod = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.B4TimeDuration = reader.GetInt32(11);

                }
                beData.PeriodManagementDetailsColl = new List<BE.Academic.Transaction.PeriodManagementDetails>();
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.PeriodManagementDetails det = new BE.Academic.Transaction.PeriodManagementDetails();
                    if (!(reader[0] is DBNull)) det.Period = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.StartTime = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) det.EndTime = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) det.TimeDuration = reader.GetInt32(3);
                    beData.PeriodManagementDetailsColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelPeriodManagementById";
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
