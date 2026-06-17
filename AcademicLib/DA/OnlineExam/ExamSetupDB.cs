using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.OnlineExam
{
    internal class ExamSetupDB
    {
        DataAccessLayer1 dal = null;
        public ExamSetupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.OnlineExam.ExamSetup beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SectionIdColl", beData.SectionIdColl);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@Lesson", beData.Lesson);
            cmd.Parameters.AddWithValue("@ExamDate", beData.ExamDate);
            cmd.Parameters.AddWithValue("@StartTime", beData.StartTime);
            cmd.Parameters.AddWithValue("@Duration", beData.Duration);            
            cmd.Parameters.AddWithValue("@FullMarks", beData.FullMarks);
            cmd.Parameters.AddWithValue("@PassMarks", beData.PassMarks);
            cmd.Parameters.AddWithValue("@Instruction", beData.Instruction);
            cmd.Parameters.AddWithValue("@IsAlerttoStudents", beData.IsAlerttoStudents);
            cmd.Parameters.AddWithValue("@IsIncludeNegativeMark", beData.IsIncludeNegativeMark);
            cmd.Parameters.AddWithValue("@DeductMark", beData.DeductMark);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ExamSetupId", beData.ExamSetupId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateExamSetup";
            }
            else
            {
                cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddExamSetup";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[18].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[19].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@ResultDate", beData.ResultDate);
            cmd.Parameters.AddWithValue("@ResultTime", beData.ResultTime);
            cmd.Parameters.AddWithValue("@ShuffleQuestions", beData.ShuffleQuestions);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[16].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[16].Value);

                if (!(cmd.Parameters[17].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[17].Value);

                if (!(cmd.Parameters[18].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[18].Value);

                if (!(cmd.Parameters[19].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[19].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveExamSetupQuestionModel(beData.CUserId, resVal.RId, beData.QuestionModelColl);
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
        private void SaveExamSetupQuestionModel(int UserId, int ExamSetupId, List<BE.OnlineExam.ExamSetupQuestionModel> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || ExamSetupId == 0)
                return;

            int sno = 1;
            foreach (BE.OnlineExam.ExamSetupQuestionModel beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@SNo", sno);
                cmd.Parameters.AddWithValue("@ExamSetupId", ExamSetupId);
                cmd.Parameters.AddWithValue("@CategoryId", beData.CategoryId);                
                cmd.Parameters.AddWithValue("@NoOfQuestion", beData.NoOfQuestion);
                cmd.Parameters.AddWithValue("@Marks", beData.Marks);
                cmd.Parameters.AddWithValue("@Total", beData.Total); 
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddExamSetupQuestionModel";
                cmd.ExecuteNonQuery();
                sno++;
            }

        }

        public RE.OnlineExam.ExamSetupCollections getAllExamSetup(int UserId, int EntityId)
        {
            RE.OnlineExam.ExamSetupCollections dataColl = new RE.OnlineExam.ExamSetupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllExamSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.OnlineExam.ExamSetup beData = new RE.OnlineExam.ExamSetup();
                    beData.ExamSetupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ExamTypeName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SubjectName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Lesson = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ExamDate_AD = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.ExamDate_BS = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.StartTime = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.Duration = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.ResultDate_AD = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.ResultDate_BS = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ResultTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.FullMark = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.PassMark = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.Instruction = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.IsAlterToStudents = reader.GetBoolean(16);
                    if (!(reader[17] is DBNull)) beData.IsIncludeNegativeMark = reader.GetBoolean(17);
                    if (!(reader[18] is DBNull)) beData.DeductMark = Convert.ToDouble(reader[18]);
                    if (!(reader[19] is DBNull)) beData.TeacherName = Convert.ToString(reader[19]);
                    if (!(reader[20] is DBNull)) beData.ShuffleQuestions = Convert.ToBoolean(reader[20]);

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

        public API.OnlineExam.StudentForEvaluateCollections getStudentForEvaluate(int UserId, int examSetupId,int classId,int? sectionId)
        {
            API.OnlineExam.StudentForEvaluateCollections dataColl = new API.OnlineExam.StudentForEvaluateCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamSetupId", examSetupId);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.CommandText = "ups_GetOnlineExamPA";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    API.OnlineExam.StudentForEvaluate beData = new API.OnlineExam.StudentForEvaluate();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.BoardRegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.UserName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.QuestionAttampt = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.Objective_OM = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.Subjective_OM = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Total_OM = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Location = Convert.ToString(reader[10]);
                    if (!(reader[11] is DBNull)) beData.IPAddress = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.StartDateTime = Convert.ToDateTime(reader[12]);
                    if (!(reader[13] is DBNull)) beData.EndDateTime = Convert.ToDateTime(reader[13]);
                    if (!(reader[14] is DBNull)) beData.LastSumitDateTime = Convert.ToDateTime(reader[14]);
                    if (!(reader[15] is DBNull)) beData.FatherName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.ContactNo = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.NoOfFiles = Convert.ToInt32(reader[17]);
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
        public RE.OnlineExam.ExamListCollections getExamListForEvaluate(int UserId, DateTime dateFrom,DateTime dateTo,int examTypeId,int classId,int? sectionId,int subjectId)
        {
            RE.OnlineExam.ExamListCollections dataColl = new RE.OnlineExam.ExamListCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@ExamTypeId", examTypeId);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@SubjectId", subjectId);            
            cmd.CommandText = "usp_GetOnlineExamListForEvaluate";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.OnlineExam.ExamList beData = new RE.OnlineExam.ExamList();
                    beData.ExamSetupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ExamTypeName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ExamDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.ExamDate_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SubjectName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Lession = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.StartTime = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Duration = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.FullMark = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.PassMarks = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Instruction = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.DeductMark = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.ForType = Convert.ToString(reader[13]);
                    if (!(reader[14] is DBNull)) beData.SectionId = Convert.ToInt32(reader[14]);
                    if (!(reader[15] is DBNull)) beData.SectionName = Convert.ToString(reader[15]);
                    if (!(reader[16] is DBNull)) beData.NoOfStudent = Convert.ToInt32(reader[16]);
                    if (!(reader[17] is DBNull)) beData.NoOfPresent = Convert.ToInt32(reader[17]);
                    beData.NoOfAbsent = beData.NoOfStudent - beData.NoOfPresent;
                    beData.ClassId = classId;
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

        public RE.OnlineExam.ExamListCollections getExamListForPreStudent(int UserId)
        {
            RE.OnlineExam.ExamListCollections dataColl = new RE.OnlineExam.ExamListCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId); 
            cmd.CommandText = "usp_GetOnlineExamListForEvaluatePreStudent";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.OnlineExam.ExamList beData = new RE.OnlineExam.ExamList();
                    beData.ExamSetupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ExamTypeName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ExamDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.ExamDate_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SubjectName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Lession = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.StartTime = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Duration = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.FullMark = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.PassMarks = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Instruction = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.DeductMark = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.ForType = Convert.ToString(reader[13]);
                    if (!(reader[14] is DBNull)) beData.SectionId = Convert.ToInt32(reader[14]);
                    if (!(reader[15] is DBNull)) beData.SectionName = Convert.ToString(reader[15]);
                    if (!(reader[16] is DBNull)) beData.NoOfStudent = Convert.ToInt32(reader[16]);
                    if (!(reader[17] is DBNull)) beData.NoOfPresent = Convert.ToInt32(reader[17]);
                    if (!(reader[18] is DBNull)) beData.ClassId = Convert.ToInt32(reader[18]);
                    beData.NoOfAbsent = beData.NoOfStudent - beData.NoOfPresent;                    
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

        public RE.OnlineExam.ExamListCollections getExamList(int UserId,int? ForType)
        {
            RE.OnlineExam.ExamListCollections dataColl = new RE.OnlineExam.ExamListCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForType", ForType);
            cmd.CommandText = "usp_GetOnlineExamList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.OnlineExam.ExamList beData = new RE.OnlineExam.ExamList();
                    beData.ExamSetupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ExamTypeName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ExamDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.ExamDate_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SubjectName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Lession = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.StartTime = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Duration = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.FullMark = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.PassMarks = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Instruction = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.DeductMark = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.ForType = Convert.ToString(reader[13]);
                    if (!(reader[14] is DBNull)) beData.SectionName = Convert.ToString(reader[14]);
                    if (!(reader[15] is DBNull)) beData.ClassId = Convert.ToInt32(reader[15]);
                    if (!(reader[16] is DBNull)) beData.SubjectId = Convert.ToInt32(reader[16]);
                    if (!(reader[17] is DBNull)) beData.SectionId = Convert.ToInt32(reader[17]);

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

        public RE.OnlineExam.ExamList getExamSetupByIdForAPI(int UserId, int ExamSetupId)
        {
            RE.OnlineExam.ExamList beData = new RE.OnlineExam.ExamList();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamSetupId", ExamSetupId);
            cmd.CommandText = "usp_GetExamSetupByIdForAPI";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new RE.OnlineExam.ExamList();
                    beData.ExamSetupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ExamTypeName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ExamDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.ExamDate_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SubjectName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Lession = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.StartTime = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Duration = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.FullMark = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.PassMarks = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Instruction = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.DeductMark = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.ForType = Convert.ToString(reader[13]);
                }
                reader.NextResult();
                beData.QuestionDetailsColl = new List<RE.OnlineExam.QuestionSummary>();
                while (reader.Read())
                {
                    RE.OnlineExam.QuestionSummary ques = new RE.OnlineExam.QuestionSummary();
                    if (!(reader[0] is DBNull)) ques.CategoryName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) ques.ExamModal = reader.GetString(1);
                    if (!(reader[2] is DBNull)) ques.NoOfQuestion = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) ques.Mark = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) ques.Total = Convert.ToDouble(reader[4]);
                    beData.QuestionDetailsColl.Add(ques);
                }
                reader.Close();
                return beData;
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
            
        }
        public BE.OnlineExam.ExamSetup getExamSetupById(int UserId, int EntityId, int ExamSetupId)
        {
            BE.OnlineExam.ExamSetup beData = new BE.OnlineExam.ExamSetup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamSetupId", ExamSetupId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExamSetupById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.OnlineExam.ExamSetup();
                    beData.ExamSetupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ExamTypeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.SectionIdColl = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SubjectId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Lesson = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ExamDate = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.StartTime = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.Duration = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.ResultDate = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.ResultTime = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.FullMarks = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.PassMarks = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Instruction = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.IsAlerttoStudents = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.IsIncludeNegativeMark = reader.GetBoolean(15);
                    if (!(reader[16] is DBNull)) beData.DeductMark = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.ShuffleQuestions = Convert.ToBoolean(reader[17]);

                }
                reader.NextResult();
                beData.QuestionModelColl = new List<BE.OnlineExam.ExamSetupQuestionModel>();
                while(reader.Read())
                {
                    BE.OnlineExam.ExamSetupQuestionModel det = new BE.OnlineExam.ExamSetupQuestionModel();
                    if (!(reader[0] is DBNull)) det.CategoryId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.NoOfQuestion = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.Marks = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) det.Total = Convert.ToDouble(reader[3]);
                    beData.QuestionModelColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int ExamSetupId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ExamSetupId",ExamSetupId);
            cmd.CommandText = "usp_DelExamSetupById";
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

        public List<BE.OnlineExam.ExamSetupQuestionModel> getExamSetupDetails(int UserId, int ExamTypeId,int ClassId,string SectionIdColl,int SubjectId)
        {
            List<BE.OnlineExam.ExamSetupQuestionModel> dataColl = new List<BE.OnlineExam.ExamSetupQuestionModel>();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);            
            cmd.CommandText = "usp_GetQuestionCategoryFromExamSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();                
                while (reader.Read())
                {
                    BE.OnlineExam.ExamSetupQuestionModel det = new BE.OnlineExam.ExamSetupQuestionModel();
                    if (!(reader[0] is DBNull)) det.ExamSetupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.CategoryId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.NoOfQuestion = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.Marks = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) det.Total = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) det.SNo = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) det.CategoryName = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) det.ExamModal = Convert.ToInt32(reader[7]);
                    dataColl.Add(det);
                }
                reader.Close();
             
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }

    }
}
