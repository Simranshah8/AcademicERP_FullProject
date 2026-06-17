using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ExamTypeWiseDB
    {
        DataAccessLayer1 dal = null;
        public ExamTypeWiseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Exam.Transaction.ExamTypeWise beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
            cmd.Parameters.AddWithValue("@TotalFailSubject", beData.TotalFailSubject);
            cmd.Parameters.AddWithValue("@IsSubjectWise", beData.IsSubjectWise);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@IsTH", beData.IsTH);
            cmd.Parameters.AddWithValue("@IsPR", beData.IsPR);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeWiseId", beData.ExamTypeWiseId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateExamTypeWise";
            }
            else
            {
                cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddExamTypeWise";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;

            //cmd.Parameters.AddWithValue("@StartMonth", beData.StartMonth);
            //cmd.Parameters.AddWithValue("@EndMonth", beData.EndMonth);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[12].Value);

                if (!(cmd.Parameters[13].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[13].Value);

                if (!(cmd.Parameters[14].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[14].Value);

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

        public BE.Exam.Transaction.ExamTypeWiseCollections getAllExamTypeWise(int UserId, int EntityId)
        {
            BE.Exam.Transaction.ExamTypeWiseCollections dataColl = new BE.Exam.Transaction.ExamTypeWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllExamTypeWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Transaction.ExamTypeWise beData = new BE.Exam.Transaction.ExamTypeWise();
                    beData.ExamTypeWiseId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.TotalFailSubject = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.IsSubjectWise = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.SubjectId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.IsTH = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsPR = reader.GetBoolean(7);

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
        public BE.Exam.Transaction.ExamTypeWise getExamTypeWiseById(int UserId, int EntityId, int ExamTypeWiseId)
        {
            BE.Exam.Transaction.ExamTypeWise beData = new BE.Exam.Transaction.ExamTypeWise();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamTypeWiseId", ExamTypeWiseId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExamTypeById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Transaction.ExamTypeWise();
                    beData.ExamTypeWiseId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.TotalFailSubject = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.IsSubjectWise = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) beData.SubjectId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.IsTH = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsPR = reader.GetBoolean(7);

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
        public ResponeValues DeleteById(int UserId, int EntityId, int ExamTypeWiseId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeWiseId", ExamTypeWiseId);
            cmd.CommandText = "usp_DelExamTypeWiseById";
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
