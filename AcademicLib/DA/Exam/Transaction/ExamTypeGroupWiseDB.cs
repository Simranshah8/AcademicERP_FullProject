using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ExamTypeGroupWiseDB
    {
        DataAccessLayer1 dal = null;
        public ExamTypeGroupWiseDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Exam.Transaction.ExamTypeGroupWise beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
            cmd.Parameters.AddWithValue("@ExamTypeGroupId", beData.ExamTypeGroupId);
            cmd.Parameters.AddWithValue("@TotalFailSubject", beData.TotalFailSubject);
            cmd.Parameters.AddWithValue("@IsSubjectWise", beData.IsSubjectWise);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@IsTH", beData.IsTH);
            cmd.Parameters.AddWithValue("@IsPR", beData.IsPR);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeGroupWiseId", beData.ExamTypeGroupWiseId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateExamTypeGroupWise";
            }
            else
            {
                cmd.Parameters[43].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddExamTypeGroupWise";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[44].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[45].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[46].Direction = System.Data.ParameterDirection.Output;

            //cmd.Parameters.AddWithValue("@StartMonth", beData.StartMonth);
            //cmd.Parameters.AddWithValue("@EndMonth", beData.EndMonth);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[43].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[43].Value);

                if (!(cmd.Parameters[44].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[44].Value);

                if (!(cmd.Parameters[45].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[45].Value);

                if (!(cmd.Parameters[46].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[46].Value);

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

        public BE.Exam.Transaction.ExamTypeGroupWiseCollections getAllExamTypeGroupWise(int UserId, int EntityId)
        {
            BE.Exam.Transaction.ExamTypeGroupWiseCollections dataColl = new BE.Exam.Transaction.ExamTypeGroupWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllAdmissionEnquiry";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Transaction.ExamTypeGroupWise beData = new BE.Exam.Transaction.ExamTypeGroupWise();
                    beData.ExamTypeGroupWiseId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ExamTypeGroupId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.TotalFailSubject = reader.GetInt32(4);
                    if (!(reader[4] is DBNull)) beData.IsSubjectWise = reader.GetBoolean(4);
                    if (!(reader[4] is DBNull)) beData.SubjectId = reader.GetInt32(4);
                    if (!(reader[4] is DBNull)) beData.IsTH = reader.GetBoolean(4);
                    if (!(reader[4] is DBNull)) beData.IsPR = reader.GetBoolean(4);

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
        public BE.Exam.Transaction.ExamTypeGroupWise getExamTypeGroupWiseById(int UserId, int EntityId, int ExamTypeGroupWiseId)
        {
            BE.Exam.Transaction.ExamTypeGroupWise beData = new BE.Exam.Transaction.ExamTypeGroupWise();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamTypeGroupWiseId", ExamTypeGroupWiseId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExamTypeById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Transaction.ExamTypeGroupWise();
                    beData.ExamTypeGroupWiseId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ExamTypeGroupId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.TotalFailSubject = reader.GetInt32(4);
                    if (!(reader[4] is DBNull)) beData.IsSubjectWise = reader.GetBoolean(4);
                    if (!(reader[4] is DBNull)) beData.SubjectId = reader.GetInt32(4);
                    if (!(reader[4] is DBNull)) beData.IsTH = reader.GetBoolean(4);
                    if (!(reader[4] is DBNull)) beData.IsPR = reader.GetBoolean(4);

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
        public ResponeValues DeleteById(int UserId, int EntityId, int ExamTypeGroupWiseId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeGroupWiseId", ExamTypeGroupWiseId);
            cmd.CommandText = "usp_DelExamTypeGroupWiseById";
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
