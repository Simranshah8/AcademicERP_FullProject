using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Wallet
{
    internal class OnlinePaymentDB
    {
        DataAccessLayer1 dal = null;
        public OnlinePaymentDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveWalletLog(BE.Wallet.OnlinePayment beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@PaymentGateWayId", beData.PaymentGateWayId);
            cmd.Parameters.AddWithValue("@Amount", beData.Amount);
            cmd.Parameters.AddWithValue("@ReferenceId", beData.ReferenceId);
            cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
            cmd.Parameters.AddWithValue("@Notes", beData.Notes);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;            
            cmd.CommandText = "sp_AddOnlinePayment";
            cmd.Parameters.AddWithValue("@FromReq", beData.From);
            cmd.Parameters.AddWithValue("@ReceiptTranId", beData.ReceiptTranId);
            cmd.Parameters.AddWithValue("@ReceiptPath", beData.ReceiptPath);            
            try
            {
                cmd.ExecuteNonQuery();
                
                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public BE.Wallet.PaymentGateWayCollections GetPaymentGateWayList(int UserId)
        {
            BE.Wallet.PaymentGateWayCollections dataColl = new BE.Wallet.PaymentGateWayCollections();

            dal.OpenConnection();

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetPaymentGateWay";

            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Wallet.PaymentGateWay beData = new BE.Wallet.PaymentGateWay();
                    beData.GateWay =(BE.Wallet.PAYMENTGATEWAYS)reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.PrivateKey = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) beData.PublicKey = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) beData.SchoolId = reader.GetString(3);
                    try
                    {
                        if (!(reader[4] is DBNull)) beData.UserName = reader.GetString(4);
                        if (!(reader[5] is DBNull)) beData.Pwd = reader.GetString(5);
                        if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                        if (!(reader[7] is DBNull)) beData.IconPath = reader.GetString(7);
                        if (!(reader[8] is DBNull)) beData.MerchantId = reader.GetString(8);
                        if (!(reader[9] is DBNull)) beData.MerchantName = reader.GetString(9);
                    }
                    catch { }
                    
                    dataColl.Add(beData);
                }

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
