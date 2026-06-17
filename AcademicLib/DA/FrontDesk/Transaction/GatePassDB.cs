using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class GatePassDB
    {
        DataAccessLayer1 dal = null;
        public GatePassDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.FrontDesk.Transaction.GatePass beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@Contact", beData.Contact);
            cmd.Parameters.AddWithValue("@Email", beData.Email);
            cmd.Parameters.AddWithValue("@MeeTo", beData.MeeTo);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@OthersName", beData.OthersName);
            cmd.Parameters.AddWithValue("@Purpose", beData.Purpose);
            cmd.Parameters.AddWithValue("@InTime", beData.InTime);
            cmd.Parameters.AddWithValue("@ValidityTime", beData.ValidityTime);
            cmd.Parameters.AddWithValue("@OutTime", beData.OutTime);      
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateGatePass";
            }
            else
            {
                cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddGatePass";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);            
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[18].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@WillReturnBack", beData.WillReturnBack);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[15].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[15].Value);

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[16].Value);

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[17].Value);

                if (!(cmd.Parameters[18].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[18].Value);

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


        public ResponeValues UpdateInTime(int UserId,int TranId, DateTime InTime)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@InTime", InTime);
            cmd.CommandText = "usp_GetUpdateGatePassInTime";
            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.UPDATE_SUCCESS;
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
        public BE.FrontDesk.Transaction.GatePassCollections getAllGatePass(int UserId, int EntityId, DateTime? dateFrom, DateTime? dateTo)
        {
            BE.FrontDesk.Transaction.GatePassCollections dataColl = new BE.FrontDesk.Transaction.GatePassCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetAllGatePass";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.FrontDesk.Transaction.GatePass beData = new BE.FrontDesk.Transaction.GatePass();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Contact = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Email = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.MeeTo = (AcademicLib.BE.FrontDesk.Transaction.MEETTOS)reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.StudentId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.EmployeeId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.OthersName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Purpose = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.InTime = Convert.ToDateTime(reader[10]);
                    if (!(reader[11] is DBNull)) beData.ValidityTime = Convert.ToDateTime(reader[11]);
                    if (!(reader[12] is DBNull)) beData.OutTime = Convert.ToDateTime(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Remarks = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.UserName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.LogDateTime = reader.GetDateTime(15);
                    if (!(reader[16] is DBNull)) beData.LogMiti = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.BranchName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.GatePassNo = reader.GetInt32(18);
                    if (!(reader[19] is DBNull)) beData.WillReturnBack = reader.GetBoolean(19);
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
        public BE.FrontDesk.Transaction.GatePass getGatePassById(int UserId, int EntityId, int TranId)
        {
            BE.FrontDesk.Transaction.GatePass beData = new BE.FrontDesk.Transaction.GatePass();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "[usp_GetGatePassById]";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.FrontDesk.Transaction.GatePass();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Contact = reader.GetString(3);
                    if (!(reader[5] is DBNull)) beData.MeeTo = (AcademicLib.BE.FrontDesk.Transaction.MEETTOS)reader.GetInt32(5);
                    if (!(reader[5] is DBNull)) beData.StudentId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.EmployeeId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.OthersName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Email = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Purpose = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.InTime = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.ValidityTime = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.OutTime = reader.GetDateTime(12);                    
                    if (!(reader[15] is DBNull)) beData.Remarks = reader.GetString(15);
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
            cmd.CommandText = "usp_DelGatePassById";
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
