using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{
    internal class ExtraEntityDB
    {
        DataAccessLayer1 dal = null;
        public ExtraEntityDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Academic.Transaction.ExtraEntity beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@Notes", beData.Notes);
            cmd.Parameters.AddWithValue("@For", beData.For);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateExtraEntity";
            }
            else
            {
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddExtraEntity";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0)
                {
                    SaveExtraEntityAttribute(beData.ExtraEntityAttributeColl, resVal.RId, beData.CUserId);
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

        private void SaveExtraEntityAttribute(List<AcademicLib.BE.Academic.Transaction.ExtraEntityAttribute> dataColl, int TranId, int UserId)
        {
            int sno = 1;
            foreach (var beData in dataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", beData.Name);
                cmd.Parameters.AddWithValue("@DataType", beData.DataType);
                cmd.Parameters.AddWithValue("@DefaultValue", beData.DefaultValue);
                cmd.Parameters.AddWithValue("@IsMandatory", beData.IsMandatory);
                cmd.Parameters.AddWithValue("@MinLen", beData.MinLen);
                cmd.Parameters.AddWithValue("@MaxLen", beData.MaxLen);
                cmd.Parameters.AddWithValue("@SelectOptions", beData.SelectOptions);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@SNo", sno);
                cmd.CommandText = "usp_AddExtraEntityAttribute";
                cmd.ExecuteNonQuery();
                sno++;
            }
        }
        public AcademicLib.BE.Academic.Transaction.ExtraEntityCollections getAllExtraEntity(int UserId, int EntityId)
        {
            AcademicLib.BE.Academic.Transaction.ExtraEntityCollections dataColl = new AcademicLib.BE.Academic.Transaction.ExtraEntityCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllExtraEntity";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ExtraEntity beData = new AcademicLib.BE.Academic.Transaction.ExtraEntity();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.For = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Notes = reader.GetString(4);
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ExtraEntityAttribute det = new BE.Academic.Transaction.ExtraEntityAttribute();
                    det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.DataType = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.DefaultValue = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.IsMandatory = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) det.MinLen = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) det.MaxLen = Convert.ToInt32(reader[6]);
                    if (!(reader[7] is DBNull)) det.SelectOptions = reader.GetString(7);
                    if (!(reader[8] is DBNull)) det.SNo = Convert.ToInt32(reader[8]);
                    dataColl.Find(p1 => p1.TranId == det.TranId).ExtraEntityAttributeColl.Add(det);
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

        public AcademicLib.BE.Academic.Transaction.ExtraEntity getExtraEntityById(int UserId, int EntityId, int TranId)
        {
            AcademicLib.BE.Academic.Transaction.ExtraEntity beData = new AcademicLib.BE.Academic.Transaction.ExtraEntity();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExtraEntityById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Academic.Transaction.ExtraEntity();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.For = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Description = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Notes = reader.GetString(5);
                }
                beData.ExtraEntityAttributeColl = new List<BE.Academic.Transaction.ExtraEntityAttribute>();
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ExtraEntityAttribute det = new AcademicLib.BE.Academic.Transaction.ExtraEntityAttribute();
                    det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.DataType = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.DefaultValue = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.IsMandatory = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) det.MinLen = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) det.MaxLen = Convert.ToInt32(reader[6]);
                    if (!(reader[7] is DBNull)) det.SelectOptions = reader.GetString(7);
                    beData.ExtraEntityAttributeColl.Add(det);
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
            cmd.CommandText = "usp_DelExtraEntityById";
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