using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Setup
{
    internal class ConfigurationStudentDB
    {
        DataAccessLayer1 dal = null;
        public ConfigurationStudentDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.Academic.Setup.ConfigurationStudent beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegdNumberingMethod", beData.RegdNumberingMethod);
            cmd.Parameters.AddWithValue("@RegdPrefix", beData.RegdPrefix);
            cmd.Parameters.AddWithValue("@RegdSuffix", beData.RegdSuffix);
            cmd.Parameters.AddWithValue("@AutoGenerateRollNo", beData.AutoGenerateRollNo);
            cmd.Parameters.AddWithValue("@ShowLeftStudentinTC_CC", beData.ShowLeftStudentinTC_CC);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);            
            cmd.CommandText = "usp_AddConfigurationStudent";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);            
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@StartNo", beData.StartNo);
            cmd.Parameters.AddWithValue("@NumericalPartWidth", beData.NumericalPartWidth);
            cmd.Parameters.AddWithValue("@AllowReGenerateUserPwd", beData.AllowReGenerateUserPwd);
            cmd.Parameters.AddWithValue("@LeftStudentConfig", beData.LeftStudentConfig);
            cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);
            cmd.Parameters.AddWithValue("@StudentRefAs", beData.StudentRefAs);
            cmd.Parameters.AddWithValue("@StudentRefFeeDebit", beData.StudentRefFeeDebit);
            cmd.Parameters.AddWithValue("@ShowBillingInAdmission", beData.ShowBillingInAdmission);
            cmd.Parameters.AddWithValue("@FilterStudentForBorders", beData.FilterStudentForBorders);
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
        public BE.Academic.Setup.ConfigurationStudent getConfiguration(int UserId, int EntityId,int? BranchId)
        {
            BE.Academic.Setup.ConfigurationStudent beData = new BE.Academic.Setup.ConfigurationStudent();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.CommandText = "usp_GetConfigurationStudent";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Setup.ConfigurationStudent();                  
                    if (!(reader[0] is DBNull)) beData.RegdNumberingMethod = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdPrefix = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.RegdSuffix = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.AutoGenerateRollNo = reader.GetBoolean(3);
                    if (!(reader[4] is DBNull)) beData.ShowLeftStudentinTC_CC = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.StartNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.NumericalPartWidth = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.AllowReGenerateUserPwd = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.LeftStudentConfig = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.StudentRefAs = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.StudentRefFeeDebit = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.ShowBillingInAdmission = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.FilterStudentForBorders = reader.GetBoolean(12);
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
