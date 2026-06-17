using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Setup
{
    internal class UpgradeStudentClassDB
    {
        DataAccessLayer1 dal = null;
        public UpgradeStudentClassDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.Academic.Setup.UpgradeStudentClass beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FromClassId", beData.FromClassId);
                cmd.Parameters.AddWithValue("@ToClassId", beData.ToClassId);
                cmd.Parameters.AddWithValue("@CanUpgarde", beData.CanUpgarde);
                cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                cmd.CommandText = "usp_AddUpgradeStudentClass";
                cmd.ExecuteNonQuery();
                dal.CommitTransaction();
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                resVal.IsSuccess = true;
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

        public ResponeValues SaveUpdate(int UserId,BE.Academic.Setup.UpgradeStudentClassCollections dataCll)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=" + UserId.ToString() + " ; " + "delete from Tbl_UpgradeStudentClass";
                cmd.ExecuteNonQuery();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (var beData in dataCll)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@FromClassId", beData.FromClassId);
                    cmd.Parameters.AddWithValue("@ToClassId", beData.ToClassId);
                    cmd.Parameters.AddWithValue("@CanUpgarde", beData.CanUpgarde);
                    cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                    cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                    cmd.CommandText = "usp_AddUpgradeStudentClass";
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                resVal.IsSuccess = true;
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
        public BE.Academic.Setup.UpgradeStudentClassCollections getAllUpgradeStudentClass(int UserId, int EntityId)
        {
            BE.Academic.Setup.UpgradeStudentClassCollections dataColl = new BE.Academic.Setup.UpgradeStudentClassCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllUpgradeStudentClass";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Setup.UpgradeStudentClass beData = new BE.Academic.Setup.UpgradeStudentClass();

                    if (!(reader[0] is DBNull)) beData.FromClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ToClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.CanUpgarde = reader.GetBoolean(2);


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
        public ResponeValues DeleteById(int UserId, int EntityId, int ClassId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.CommandText = "usp_DelClassWiseAcademicMonthById";
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
