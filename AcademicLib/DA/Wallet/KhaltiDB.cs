using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.Wallet
{
    internal class KhaltiDB
    {
        DataAccessLayer1 dal = null;
        public KhaltiDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveWalletLog(BE.Wallet.WalletRequest beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", beData.UserId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@PublicKey", beData.PublicKey);
            cmd.Parameters.AddWithValue("@PrivateKey", beData.PrivateKey);
            cmd.Parameters.AddWithValue("@Url", beData.Url);
            cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
            cmd.Parameters.AddWithValue("@Amount", beData.Amount);
            cmd.Parameters.AddWithValue("@ProductId", beData.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", beData.ProductName);
            cmd.Parameters.AddWithValue("@ProductURL", beData.ProductURL);
            cmd.Parameters.AddWithValue("@Token", beData.Token);
            cmd.Parameters.AddWithValue("@RequestTime", beData.RequestTime);
            cmd.Parameters.AddWithValue("@ResponseTime", beData.ResponseTime);
            cmd.Parameters.AddWithValue("@IpAddress", beData.IpAddress);            
            cmd.Parameters.Add("@NewGuiId", System.Data.SqlDbType.VarChar, 254);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);

            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "[sp_AddWalletRequestLog]";

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[15].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[15].Value);

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[16].Value);

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[17].Value);
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
        public ResponeValues SaveWalletConfirmationLog(BE.Wallet.WalletRequest beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", beData.UserId);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@PublicKey", beData.PublicKey);
            cmd.Parameters.AddWithValue("@PrivateKey", beData.PrivateKey);
            cmd.Parameters.AddWithValue("@Url", beData.Url);
            cmd.Parameters.AddWithValue("@Amount", beData.Amount);
            cmd.Parameters.AddWithValue("@ProductId", beData.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", beData.ProductName);
            cmd.Parameters.AddWithValue("@Token", beData.Token);
            cmd.Parameters.AddWithValue("@RequestTime", beData.RequestTime);
            cmd.Parameters.AddWithValue("@ResponseTime", beData.ResponseTime);
            cmd.Parameters.Add("@NewGuiId", System.Data.SqlDbType.VarChar, 254);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);

            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            cmd.CommandText = "[sp_AddWalletConfirmationLog]";

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[13].Value);

                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[14].Value);
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
        public ResponeValues SaveWalletVerificationLog(BE.Wallet.WalletVerificationLog beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            cmd.Parameters.AddWithValue("@Idx", beData.Idx);
            cmd.Parameters.AddWithValue("@TypeIdx", beData.TypeIdx);
            cmd.Parameters.AddWithValue("@TypeName", beData.TypeName);
            cmd.Parameters.AddWithValue("@StateIdx", beData.StateIdx);
            cmd.Parameters.AddWithValue("@StateName", beData.StateName);
            cmd.Parameters.AddWithValue("@StateTemplate", beData.StateTemplate);
            cmd.Parameters.AddWithValue("@Amount", beData.Amount);
            cmd.Parameters.AddWithValue("@FeeAmount", beData.FeeAmount);
            cmd.Parameters.AddWithValue("@Refunded", beData.Refunded);
            cmd.Parameters.AddWithValue("@Created_On", beData.Created_On);
            cmd.Parameters.AddWithValue("@Ebanker", beData.Ebanker);
            cmd.Parameters.AddWithValue("@UserIdx", beData.UserIdx);
            cmd.Parameters.AddWithValue("@UserName", beData.UserName);
            cmd.Parameters.AddWithValue("@UserMobile", beData.UserMobile);
            cmd.Parameters.AddWithValue("@MerchantIdx", beData.MerchantIdx);
            cmd.Parameters.AddWithValue("@MerchantName", beData.MerchantName);
            cmd.Parameters.AddWithValue("@MerchantMobile", beData.MerchantMobile);
            cmd.Parameters.AddWithValue("@RequestTime", beData.RequestTime);
            cmd.Parameters.AddWithValue("@ResponseTime", beData.ResponseTime);
            cmd.Parameters.Add("@NewGuiId", System.Data.SqlDbType.VarChar, 254);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);

            cmd.Parameters[20].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[21].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[22].Direction = System.Data.ParameterDirection.Output;

            cmd.CommandText = "sp_AddWalletVerifyLog";

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[20].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[20].Value);

                if (!(cmd.Parameters[21].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[21].Value);

                if (!(cmd.Parameters[22].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[22].Value);
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
        public AcademicLib.BE.Wallet.WalletRequest getWalletToken(int UserId,int? EmployeeId,int? StudentId, string NewGuiId)
        {
            AcademicLib.BE.Wallet.WalletRequest beData = null;
            
            dal.OpenConnection();

            try
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@NewGuiId", NewGuiId);
                cmd.CommandText = "sp_GetWalletToken";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    beData = new BE.Wallet.WalletRequest();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.Token = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) beData.Amount = Convert.ToDouble(reader[2]);
                }

                reader.Close();
                return beData;
            }
            catch (Exception ee)
            {
                return null;
            }
            finally
            {
                dal.CloseConnection();
            }

        }
    }
}
