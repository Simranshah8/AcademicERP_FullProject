using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ExamWiseHeightWeightDB
    {
        DataAccessLayer1 dal = null;
        public ExamWiseHeightWeightDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues TransforHeightWeight(int UserId, int FromExamTypeId, int ToExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            SqlCommand command = this.dal.GetCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", UserId);
            command.Parameters.AddWithValue("@FromExamTypeId", FromExamTypeId);
            command.Parameters.AddWithValue("@ToExamTypeId", ToExamTypeId);
            command.CommandText = "usp_TransforHeightWeight";
            command.Parameters.Add("@ResponseMSG", SqlDbType.NVarChar, 254);
            command.Parameters.Add("@IsSuccess", SqlDbType.Bit);
            command.Parameters.Add("@ErrorNumber", SqlDbType.Int);
            command.Parameters[3].Direction = ParameterDirection.Output;
            command.Parameters[4].Direction = ParameterDirection.Output;
            command.Parameters[5].Direction = ParameterDirection.Output;
            try
            {
                command.ExecuteNonQuery();
                if (!(command.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(command.Parameters[3].Value);
                if (!(command.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(command.Parameters[4].Value);
                if (!(command.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(command.Parameters[5].Value);
                if (!resVal.IsSuccess)
                {
                    if (resVal.ErrorNumber > 0)
                        resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
                }
            }
            catch (SqlException ex)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex.Message;
            }
            catch (Exception ex)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public BE.Exam.Transaction.ExamWiseHeightWeightCollections GetExamWiseHeightWeights(int UserId)
        {
            BE.Exam.Transaction.ExamWiseHeightWeightCollections dataColl = new BE.Exam.Transaction.ExamWiseHeightWeightCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetTransforHeightWeight";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Transaction.ExamWiseHeightWeight beData = new BE.Exam.Transaction.ExamWiseHeightWeight();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FromExamTypeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ToExamTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FromExamType = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ToExamType = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.UserId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.UserName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.TransferDate = Convert.ToDateTime(reader[7]);
                    if (!(reader[8] is DBNull)) beData.TransferDateBS = reader.GetString(8);
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


        public ResponeValues DeleteById(int UserId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelTransforHeightWeight";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

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


    }
}