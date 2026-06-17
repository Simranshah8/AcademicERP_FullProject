using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.DA.Setup
{
    internal class DefaultSettingDB
    {
        DataAccessLayer1 dal = null;
        public DefaultSettingDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, List<AcademicLib.BE.Setup.DefaultSetting> DataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Bit);
                cmd.Parameters[0].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add(new System.Data.DataColumn("ModuleId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("EntityId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("SubEntityId", typeof(int)));
                dt.Columns.Add(new System.Data.DataColumn("SubEntityName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("FieldName", typeof(string)));
                dt.Columns.Add(new System.Data.DataColumn("Value", typeof(string)));

                foreach (var beData in DataColl)
                {
                    System.Data.DataRow dr = dt.NewRow();
                    dr["ModuleId"] = beData.ModuleId;
                    dr["EntityId"] = beData.EntityNoId;

                    if (beData.SubEntityId.HasValue)
                        dr["SubEntityId"] = beData.SubEntityId.Value;
                    else
                        dr["SubEntityId"] = DBNull.Value;

                    if (!string.IsNullOrWhiteSpace(beData.SubEntityName))
                        dr["SubEntityName"] = beData.SubEntityName;
                    else
                        dr["SubEntityName"] = DBNull.Value;
                    dr["FieldName"] = beData.FieldName;
                    dr["Value"] = beData.Value;

                    dt.Rows.Add(dr);
                }
                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@tmpDefaultSettingColl", dt);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                cmd.CommandText = "usp_AddDefaultSetting";
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[0].Value is DBNull)) resVal.ResponseMSG = Convert.ToString(cmd.Parameters[0].Value);
                if (!(cmd.Parameters[1].Value is DBNull)) resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[1].Value);
                if (!(cmd.Parameters[2].Value is DBNull)) resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[2].Value);

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

        public BE.Setup.DefaultSettingCollections GetDefaultSettingById(int UserId, int ModuleId, int EntityId, int? SubEntityId)
        {
            BE.Setup.DefaultSettingCollections dataColl = new BE.Setup.DefaultSettingCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ModuleId", ModuleId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@SubEntityId", SubEntityId);
            cmd.CommandText = "usp_GetDefaultSettingById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Setup.DefaultSetting beData = new BE.Setup.DefaultSetting();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ModuleId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EntityId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SubEntityId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.SubEntityName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FieldName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Value = reader.GetString(6);
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
