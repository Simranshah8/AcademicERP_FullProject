using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AppCMS
{
    internal class ProudAlumniDB
    {
        DataAccessLayer1 dal = null;
        public ProudAlumniDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.AppCMS.ProudAlumni beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@DegreeDetails", beData.DegreeDetails);
            cmd.Parameters.AddWithValue("@CurrentCompany", beData.CurrentCompany);
            cmd.Parameters.AddWithValue("@Position", beData.Position);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@ImagePath", beData.ImagePath);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateProudAlumni";
            }
            else
            {
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddProudAlumni";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

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

        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelProudAlumniById";
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
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

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
        public BE.AppCMS.ProudAlumniCollections getAllProudAlumni(int UserId, int EntityId)
        {
            BE.AppCMS.ProudAlumniCollections dataColl = new BE.AppCMS.ProudAlumniCollections();
            dal.OpenConnection(true);
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllProudAlumni";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.AppCMS.ProudAlumni beData = new BE.AppCMS.ProudAlumni();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DegreeDetails = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.CurrentCompany = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Position = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.OrderNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Description = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ImagePath = reader.GetString(7);
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
        public BE.AppCMS.ProudAlumni getProudAlumniById(int UserId, int EntityId, int TranId)
        {
            BE.AppCMS.ProudAlumni beData = new BE.AppCMS.ProudAlumni();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetProudAlumniById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.AppCMS.ProudAlumni();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DegreeDetails = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.CurrentCompany = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Position = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.OrderNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Description = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ImagePath = reader.GetString(7);
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

