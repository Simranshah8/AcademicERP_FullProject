using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Setup
{
    internal class UDFClassDB
    {
        DataAccessLayer1 dal = null;
        public UDFClassDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(UDFClass beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //FieldNo,EntityId,Name,DisplayName,Type,Length,AllowDuplicate,IsMandatory,DefaultValue,IsCurrency,Rows
            cmd.Parameters.AddWithValue("@FieldNo", beData.FieldNo);
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@DisplayName", beData.DisplayName);
            cmd.Parameters.AddWithValue("@Type", beData.Type);
            cmd.Parameters.AddWithValue("@Length", beData.Length);
            cmd.Parameters.AddWithValue("@AllowDuplicate", beData.AllowDuplicate);
            cmd.Parameters.AddWithValue("@IsMandatory", beData.IsMandatory);
            cmd.Parameters.AddWithValue("@DefaultValue", beData.DefaultValue);
            cmd.Parameters.AddWithValue("@IsCurrency", beData.IsCurrency);
            cmd.Parameters.AddWithValue("@Rows", beData.Rows);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@Id", beData.Id);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateUDFClass";
            }
            else
            {
                cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddUDFClass";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@AllowNull", beData.AllowNull);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[13].Value);

                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[14].Value);

                if (!(cmd.Parameters[15].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[15].Value);

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
        public  UDFClassCollections getAllUDFClass(int UserId, int EntityId)
        {
            UDFClassCollections dataColl = new UDFClassCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllUDFClass";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                //FieldNo,EntityId,Name,DisplayName,Type,Length,AllowDuplicate,IsMandatory,DefaultValue,IsCurrency,Rows
                while (reader.Read())
                {
                    UDFClass beData = new UDFClass();
                    beData.Id = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FieldNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EntityId =(Dynamic.BusinessEntity.Global.FormsEntity)reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.DisplayName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Type =(Dynamic.BusinessEntity.Setup.DATATYPES)reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Length = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.AllowDuplicate = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.IsMandatory = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.DefaultValue = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.IsCurrency = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.Rows = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.AllowNull = reader.GetBoolean(12);

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
        public UDFClass getUDFClassById(int UserId, int EntityId, int Id)
        {
            UDFClass beData = new UDFClass();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetUDFClassById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new UDFClass();
                    beData.Id = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FieldNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.EntityId = (Dynamic.BusinessEntity.Global.FormsEntity)reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.DisplayName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Type = (Dynamic.BusinessEntity.Setup.DATATYPES)reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Length = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.AllowDuplicate = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.IsMandatory = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.DefaultValue = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.IsCurrency = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.Rows = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.AllowNull = reader.GetBoolean(12);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int Id)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.CommandText = "usp_DelUDFClassById";
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
