using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class RegistrationNumberMethodDB
    {
        DataAccessLayer1 dal = null;
        public RegistrationNumberMethodDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.FrontDesk.Transaction.RegistrationNumberMethod beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NumberingMethod", beData.NumberingMethod);
            cmd.Parameters.AddWithValue("@Prefix", beData.Prefix);
            cmd.Parameters.AddWithValue("@Suffix", beData.Suffix);
            cmd.Parameters.AddWithValue("@StartNo", beData.StartNo);
            cmd.Parameters.AddWithValue("@NumericalPartWidth", beData.NumericalPartWidth);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);            
            cmd.CommandText = "usp_AddRegistrationNumberMethod";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);
            cmd.Parameters.AddWithValue("@Declaration", beData.Declaration);
            cmd.Parameters.AddWithValue("@ActiveAdmission", beData.ActiveAdmission);
            cmd.Parameters.AddWithValue("@AllowReferralForUserIdColl", beData.AllowReferralForUserIdColl);
            try
            {
                cmd.ExecuteNonQuery();

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
        public BE.FrontDesk.Transaction.RegistrationNumberMethod getConfiguration(int UserId, int EntityId, int? BranchId)
        {
            BE.FrontDesk.Transaction.RegistrationNumberMethod beData = new BE.FrontDesk.Transaction.RegistrationNumberMethod();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetRegistrationNumberMethod";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.FrontDesk.Transaction.RegistrationNumberMethod();
                    if (!(reader[0] is DBNull)) beData.BranchId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.NumberingMethod = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Prefix = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Suffix = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.StartNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.NumericalPartWidth = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Declaration = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ActiveAdmission = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.AllowReferralForUserIdColl = reader.GetString(8);
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


    }
}
