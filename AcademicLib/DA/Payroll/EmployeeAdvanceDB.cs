using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Payroll
{
    internal class EmployeeAdvanceDB
    {
        DataAccessLayer1 dal = null;
        public EmployeeAdvanceDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Payroll.EmployeeAdvance beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
            cmd.Parameters.AddWithValue("@AdvanceDate", beData.AdvanceDate);
            cmd.Parameters.AddWithValue("@AdvanceTypeId", beData.AdvanceTypeId);
            cmd.Parameters.AddWithValue("@AdvanceAmount", beData.AdvanceAmount);
            cmd.Parameters.AddWithValue("@Installment", beData.Installment);
            cmd.Parameters.AddWithValue("@DeductionAmount", beData.DeductionAmount);
            cmd.Parameters.AddWithValue("@EffDate", beData.EffDate);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateEmployeeAdvance";
            }
            else
            {
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddEmployeeAdvance";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[13].Value);

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
        public AcademicLib.BE.Payroll.EmployeeAdvanceCollections getAllEmployeeAdvance(int UserId, int EntityId)
        {
            AcademicLib.BE.Payroll.EmployeeAdvanceCollections dataColl = new AcademicLib.BE.Payroll.EmployeeAdvanceCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllEmployeeAdvance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Payroll.EmployeeAdvance beData = new AcademicLib.BE.Payroll.EmployeeAdvance();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BranchName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.EmployeeCode = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.EmployeeName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.AdvanceType = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.AdvanceAmount = reader.GetDouble(5);
                    if (!(reader[6] is DBNull)) beData.DeductionAmount = reader.GetDouble(6);
                    if (!(reader[7] is DBNull)) beData.EffDate = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.Remarks = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.EffMiti = reader.GetString(9);
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
        public AcademicLib.BE.Payroll.EmployeeAdvance getEmployeeAdvanceById(int UserId, int EntityId, int TranId)
        {
            AcademicLib.BE.Payroll.EmployeeAdvance beData = new AcademicLib.BE.Payroll.EmployeeAdvance();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEmployeeAdvanceById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Payroll.EmployeeAdvance();
                    beData.TranId = reader.GetInt32(0);                   
                    if (!(reader[1] is DBNull)) beData.EmployeeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AdvanceDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.AdvanceTypeId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.AdvanceAmount = reader.GetDouble(4);
                    if (!(reader[5] is DBNull)) beData.Installment = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.DeductionAmount = reader.GetDouble(6);
                    if (!(reader[7] is DBNull)) beData.EffDate = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.Remarks = reader.GetString(8);
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
            cmd.CommandText = "usp_DelEmployeeAdvanceById";
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