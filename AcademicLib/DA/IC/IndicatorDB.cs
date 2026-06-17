using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{

    internal class IndicatorDB
    {

        DataAccessLayer1 dal = null;
        public IndicatorDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Exam.Transaction.Indicator beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
            cmd.Parameters.AddWithValue("@TopicName", beData.TopicName);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateIndicator";
            }
            else
            {
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddIndicator";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveIndicatorDetailsDetails(beData.CUserId, resVal.RId, beData.IndicatorDetailsColl);
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


        public BE.Exam.Transaction.IndicatorCollections getAllIndicator(int UserId, int EntityId, int ClassId, int SubjectId, int? LessonId, string TopicName)
        {
            BE.Exam.Transaction.IndicatorCollections dataColl = new BE.Exam.Transaction.IndicatorCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@LessonId", LessonId);
            cmd.Parameters.AddWithValue("@TopicName", TopicName);
            cmd.CommandText = "usp_GetAllIndicator";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Transaction.Indicator beData = new BE.Exam.Transaction.Indicator();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SubjectId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.LessonId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.TopicName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.IndicatorName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.LessonName = reader.GetString(7);
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
        private void SaveIndicatorDetailsDetails(int UserId, int TranId, BE.Exam.Transaction.IndicatorDetailsCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.Exam.Transaction.IndicatorDetails beData in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
              
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@SNo", beData.SNo);
                cmd.Parameters.AddWithValue("@IndicatorName", beData.IndicatorName);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                cmd.CommandText = "usp_AddIndicatorDetails";
                cmd.ExecuteNonQuery();
            }

        }


        public AcademicLib.BE.Exam.Transaction.SubjectLessonWiseCollections getSubjectLessonWise(int UserId, int EntityId, int ClassId, int SubjectId)
        {
            AcademicLib.BE.Exam.Transaction.SubjectLessonWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.SubjectLessonWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.CommandText = "usp_GetSubjectLessonWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.SubjectLessonWise beData = new AcademicLib.BE.Exam.Transaction.SubjectLessonWise();
                    if (!(reader[0] is DBNull)) beData.LessonId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LessonName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.LessonSno = reader.GetInt32(2);
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


        public AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWiseCollections getLessonTopicDetailsWise(int UserId, int EntityId, int? LessonId)
        {
            AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWiseCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@LessonId", LessonId);
            cmd.CommandText = "usp_GetLessonTopicDetailsWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWise beData = new AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWise();
                    if (!(reader[0] is DBNull)) beData.LessonId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TopicName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.TopicId = reader.GetInt32(2);
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


        //New Code added by suresh on 12 jestha saturday
        public AcademicLib.BE.Exam.Transaction.TopicWiseIndicatorCollections getTopicWiseIndicators(int UserId, int? LessonId, string TopicName)
        {
            AcademicLib.BE.Exam.Transaction.TopicWiseIndicatorCollections dataColl = new AcademicLib.BE.Exam.Transaction.TopicWiseIndicatorCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@LessonId", LessonId);
            cmd.Parameters.AddWithValue("@TopicName", TopicName);
            cmd.CommandText = "usp_GetAllTopicWiseIndicator";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.TopicWiseIndicator beData = new AcademicLib.BE.Exam.Transaction.TopicWiseIndicator();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LessonId = reader.GetInt32(0);
                    if (!(reader[2] is DBNull)) beData.IndicatorName = reader.GetString(2);
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

       
        public BE.Exam.Transaction.IndicatorSummary getIndicatorSummary(int UserId, int EntityId, int? ClassId, int? SubjectId)
        {
            BE.Exam.Transaction.IndicatorSummary beData = new BE.Exam.Transaction.IndicatorSummary();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);

            cmd.CommandText = "usp_GetIndicatorSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Transaction.IndicatorSummary();
                    if (!(reader[0] is DBNull)) beData.TotalLessonIds = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TotalTopicNames = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.TotalIndicatorNames = reader.GetInt32(2);
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

