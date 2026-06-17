using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Wallet
{
    internal class PaymentGatewayDB
    {
        DataAccessLayer1 dal = null;
        public PaymentGatewayDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Wallet.PaymentGateway beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PrivateKey", beData.PrivateKey);
            cmd.Parameters.AddWithValue("@PublicKey", beData.PublicKey);
            cmd.Parameters.AddWithValue("@ForGateWay", beData.ForGateWay);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);           
            cmd.CommandText = "usp_AddPaymentGateWay";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@LedgerId", beData.LedgerId);
            cmd.Parameters.AddWithValue("@SchoolId", beData.SchoolId);
            cmd.Parameters.AddWithValue("@UserName", beData.UserName);
            cmd.Parameters.AddWithValue("@Pwd", beData.Pwd);
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@IconPath", beData.IconPath);
            cmd.Parameters.AddWithValue("@MerchantId", beData.MerchantId);
            cmd.Parameters.AddWithValue("@MerchantName", beData.MerchantName); 
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[5].Value);

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
        public ResponeValues SavePaymentGatewayReturnURL(int ForGateWay,string ResponseLog)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForGateWay", ForGateWay);
            cmd.Parameters.AddWithValue("@ResponseLog", ResponseLog); 
            cmd.CommandText = "usp_AddPaymentGatewayReturnURL"; 
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
        public BE.Wallet.PaymentGatewayCollections getAllPaymentGateway(int UserId, int EntityId)
        {
            BE.Wallet.PaymentGatewayCollections dataColl = new BE.Wallet.PaymentGatewayCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllPaymentGateWay";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Wallet.PaymentGateway beData = new BE.Wallet.PaymentGateway();
                    beData.TranId = reader.GetInt32(0);
                    beData.ForGateWay = (AcademicLib.BE.Wallet.PAYMENTGATEWAYS)reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.PrivateKey = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.PublicKey = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.LedgerId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.LedgerName = reader.GetString(5);

                    if (!(reader[6] is DBNull)) beData.CreateBy = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ModifyBy = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.CreateAt = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.ModifyAt = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.SchoolId = reader.GetString(10);

                    if (!(reader[11] is DBNull)) beData.UserName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Pwd = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.Name = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.IconPath = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.MerchantId = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.MerchantName = reader.GetString(16);
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
    
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelPaymentGateWayById";
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
