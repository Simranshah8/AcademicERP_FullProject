using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Setup
{
    public class CheckOTPDB
    {
        DataAccessLayer1 dal = null;
        public CheckOTPDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValue CheckOTPEnabled(int UserId, string Username)
        {
            ResponeValue resVal = new ResponeValue();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 400);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_CheckOTPEnabled";
            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);
                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);
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