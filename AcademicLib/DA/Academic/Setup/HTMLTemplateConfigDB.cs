using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Academic.Setup
{
    internal class HTMLTemplateConfigDB
    {
        DataAccessLayer1 dal = null;
        public HTMLTemplateConfigDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, BE.Academic.Setup.HTMLTemplateConfigCollection dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                foreach (var beData in dataColl)
                {
                    //if (!beData.IsAllowed)
                    //    continue;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@PreviewPath", beData.PreviewPath);
                    cmd.Parameters.AddWithValue("@SNo", beData.SNo);
                    cmd.Parameters.AddWithValue("@IsAllowed", beData.IsAllowed);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                    cmd.Parameters.Add("@TemplateId", System.Data.SqlDbType.Int);
                    cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                    cmd.CommandText = "usp_AddHTMLTemplatesConfig";
                    cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                    cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                    cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
                    cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@TemplateName", beData.TemplateName);
                    cmd.Parameters.AddWithValue("@ForApp", beData.ForApp);
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
        public BE.Academic.Setup.HTMLTemplateConfigCollection GetHTMLTemplatesConfig(int UserId, int? EntityId)
        {
            BE.Academic.Setup.HTMLTemplateConfigCollection dataColl = new BE.Academic.Setup.HTMLTemplateConfigCollection();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetHTMLTemplatesConfig";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Setup.HTMLTemplateConfig beData = new BE.Academic.Setup.HTMLTemplateConfig();
                    if (!(reader[0] is DBNull)) beData.TemplateId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EntityId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.IsAllowed = reader.GetBoolean(3);
                    if (!(reader[4] is DBNull)) beData.TemplateName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.PreviewPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ForApp = reader.GetBoolean(6);
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


    }
}