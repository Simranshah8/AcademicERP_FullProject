using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Academic.Creation
{
    internal class AcademicYearDB
    {
        DataAccessLayer1 dal = null;
        public AcademicYearDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Creation.AcademicYear beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateAcademicYear";
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddAcademicYear";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@StartMonth", beData.StartMonth);
            cmd.Parameters.AddWithValue("@EndMonth", beData.EndMonth);
            cmd.Parameters.AddWithValue("@IsRunning", beData.IsRunning);
            cmd.Parameters.AddWithValue("@CostClassId", beData.CostClassId);
            cmd.Parameters.AddWithValue("@StartDate", beData.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", beData.EndDate);
            cmd.Parameters.AddWithValue("@Api_Id", beData.Api_Id);
            cmd.Parameters.AddWithValue("@Api_ResponseId", beData.Api_ResponseId);
            cmd.Parameters.AddWithValue("@LastApiCallAt", beData.LastApiCallAt);
            cmd.Parameters.AddWithValue("@LastResponse", beData.LastResponse);
            cmd.Parameters.AddWithValue("@YearNepali", beData.yearNepali);
            cmd.Parameters.AddWithValue("@YearEnglish", beData.yearEnglish);
            try
            {
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
        public BE.Academic.Creation.AcademicYearCollections getAllAcademicYear(int UserId, int EntityId)
        {
            BE.Academic.Creation.AcademicYearCollections dataColl = new BE.Academic.Creation.AcademicYearCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllAcademicYear";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Creation.AcademicYear beData = new BE.Academic.Creation.AcademicYear();
                    beData.AcademicYearId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.StartMonth = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.EndMonth = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.IsRunning = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.CostClassId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.StartDate = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.EndDate = reader.GetDateTime(9);
                    try
                    {
                        if (!(reader[10] is DBNull)) beData.yearNepali = reader.GetString(10);
                        if (!(reader[11] is DBNull)) beData.yearEnglish = reader.GetString(11);
                    }
                    catch { }
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
        public BE.Academic.Creation.AcademicYear getPeriod(int UserId, int AcademicYearId)
        {
            BE.Academic.Creation.AcademicYear beData = new BE.Academic.Creation.AcademicYear();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@UserId", UserId); 
            cmd.CommandText = "usp_GetRAYPeriod";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Creation.AcademicYear();
                    if (!(reader[0] is DBNull)) beData.StartDate = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) beData.EndDate = reader.GetDateTime(1);
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
        public BE.Academic.Creation.AcademicYear getAcademicYearById(int UserId, int EntityId, int AcademicYearId)
        {
            BE.Academic.Creation.AcademicYear beData = new BE.Academic.Creation.AcademicYear();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAcademicYearById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Creation.AcademicYear();
                    beData.AcademicYearId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.StartMonth = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.EndMonth = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.IsRunning = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.CostClassId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.StartDate = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.EndDate = reader.GetDateTime(9);
                    try
                    {
                        if (!(reader[10] is DBNull)) beData.yearNepali = reader.GetString(10);
                        if (!(reader[11] is DBNull)) beData.yearEnglish = reader.GetString(11);
                    }
                    catch { }
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
        public ResponeValues DeleteById(int UserId, int EntityId, int AcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_DelAcademicYearById";
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

        public ResponeValues getDefaultAcademicYearId(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "EXEC sp_set_session_context @key=N'UserId', @value=@UserId; select dbo.fn_GetCurAcademicYear();";
        
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if(!(reader[0] is DBNull))
                    {
                        resVal.RId = Convert.ToInt32(reader[0]);
                        resVal.IsSuccess = true;
                        resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                    }else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "No Academic Year Found";
                    }
                    
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "No Academic Year Found";
                }

                reader.Close();

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
