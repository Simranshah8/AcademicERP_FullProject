using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class MarksEntrySubjectDB
    {
        DataAccessLayer1 dal = null;
        public MarksEntrySubjectDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWise beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
            cmd.Parameters.AddWithValue("@IsColumnwiseFocus", beData.IsColumnwiseFocus);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@ObtainMarkTH", beData.ObtainMarkTH);
            cmd.Parameters.AddWithValue("@ObtainMarksPR", beData.ObtainMarksPR);
            cmd.Parameters.AddWithValue("@TotalObtainMark", beData.GraceMarkTH);
            cmd.Parameters.AddWithValue("@TotalObtainMark", beData.GraceMarkPR);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateMarksEntrySubject";
            }
            else
            {
                cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddMarksEntrySubject";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;

            //cmd.Parameters.AddWithValue("@StartMonth", beData.StartMonth);
            //cmd.Parameters.AddWithValue("@EndMonth", beData.EndMonth);

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

        public AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWiseCollections getAllMarkEntrySubjectWise(int UserId, int EntityId)
        {
            AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllMarksEntrySubject";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWise beData = new AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWise();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ExamTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.IsColumnwiseFocus = reader.GetBoolean(3);
                    if (!(reader[4] is DBNull)) beData.StudentId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.SubjectId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ObtainMarkTH = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ObtainMarksPR = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.GraceMarkTH = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.GraceMarkPR = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.Remarks = reader.GetString(10);

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
        public AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWise getMarkEntrySubjectWiseById(int UserId, int EntityId, int TranId)
        {
            AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWise beData = new AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWise();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetMarksEntrySubjectById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Exam.Transaction.MarkEntrySubjectWise();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ExamTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.IsColumnwiseFocus = reader.GetBoolean(3);
                    if (!(reader[4] is DBNull)) beData.StudentId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.SubjectId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ObtainMarkTH = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ObtainMarksPR = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.GraceMarkTH = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.GraceMarkPR = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.Remarks = reader.GetString(10);


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
            cmd.CommandText = "usp_DelMarksEntrySubjectById";
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
