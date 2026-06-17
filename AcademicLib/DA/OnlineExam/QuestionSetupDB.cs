using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.OnlineExam
{
    internal class QuestionSetupDB
    {
        DataAccessLayer1 dal = null;
        public QuestionSetupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.OnlineExam.QuestionSetup beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamSetupId", beData.ExamSetupId);
            cmd.Parameters.AddWithValue("@CategoryId", beData.CategoryId);
            cmd.Parameters.AddWithValue("@QNo", beData.QNo);
            cmd.Parameters.AddWithValue("@Marks", beData.Marks);
            cmd.Parameters.AddWithValue("@QuestionTitle", beData.QuestionTitle);
            cmd.Parameters.AddWithValue("@Question", beData.Question);
            cmd.Parameters.AddWithValue("@QuestionPath", beData.QuestionPath);
            cmd.Parameters.AddWithValue("@AnswerSNo", beData.AnswerSNo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateQuestionSetup";
            }
            else
            {
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddQuestionSetup";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@AnswerTitle", beData.AnswerTitle);

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

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveExamSetupQuestionModel(beData.CUserId, resVal.RId, beData.DetailsColl);
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
        private void SaveExamSetupQuestionModel(int UserId, int QuestionSetupId, List<BE.OnlineExam.QuestionSetupDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || QuestionSetupId == 0)
                return;

            foreach (BE.OnlineExam.QuestionSetupDetails beData in beDataColl)
            {
                if(!string.IsNullOrEmpty(beData.Answer) || !string.IsNullOrEmpty(beData.FilePath))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@QuestionSetupId", QuestionSetupId);
                    cmd.Parameters.AddWithValue("@SNO", beData.SNo);
                    cmd.Parameters.AddWithValue("@Answer", beData.Answer);
                    cmd.Parameters.AddWithValue("@FilePath", beData.FilePath);
                    cmd.Parameters.AddWithValue("@IsRightAnswer", beData.IsRightAnswer);
                    cmd.Parameters.AddWithValue("@AnswerTitle", beData.AnswerTitle);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddQuestionSetupAnswerList";
                    cmd.ExecuteNonQuery();
                }
                
            }

        }

        public BE.OnlineExam.QuestionSetupCollections getAllQuestionSetup(int UserId, int EntityId)
        {
            BE.OnlineExam.QuestionSetupCollections dataColl = new BE.OnlineExam.QuestionSetupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllQuestionSetup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.OnlineExam.QuestionSetup beData = new BE.OnlineExam.QuestionSetup();
                    beData.TranId = reader.GetInt32(0);
                   // if (!(reader[1] is DBNull)) beData.ExamTypeId = reader.GetInt32(1);
                   // if (!(reader[2] is DBNull)) beData.Modal = reader.GetInt32(2);
                   // if (!(reader[3] is DBNull)) beData.Category = reader.GetInt32(3);
                   // if (!(reader[4] is DBNull)) beData.Marks = reader.GetInt32(4);
                   //// if (!(reader[5] is DBNull)) beData.QuestionTitle = reader.GetInt32(5);
                   // if (!(reader[6] is DBNull)) beData.Text = reader.GetString(6);
                   // if (!(reader[7] is DBNull)) beData.Image = reader.GetString(7);
                   // if (!(reader[8] is DBNull)) beData.ImagePath = reader.GetString(8);
                   // if (!(reader[9] is DBNull)) beData.AudioPath = reader.GetString(9);
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

        public BE.OnlineExam.QuestionSetupCollections getQuestionSetupList(int UserId, int ExamTypeId,int ClassId,string SectionIdColl,int SubjectId,int examSetupId)
        {
            BE.OnlineExam.QuestionSetupCollections dataColl = new BE.OnlineExam.QuestionSetupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@ExamSetupId", examSetupId);
            cmd.CommandText = "usp_GetQuestionList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.OnlineExam.QuestionSetup beData = new BE.OnlineExam.QuestionSetup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.QNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Marks = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.QuestionTitle = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Question = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.QuestionPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.AnswerSNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.CategoryName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ExamModal = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.AnswerTitle = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.TeacherName = reader.GetString(10);

                    if (!string.IsNullOrEmpty(beData.QuestionPath))
                    {
                        beData.FileCount = 1;
                        string fName = beData.QuestionPath.Trim().ToLower();
                        if (fName.Contains(".pdf"))
                            beData.FileType = "pdf";
                        else if (fName.Contains(".mp3") || fName.Contains(".mp4") || fName.Contains(".wav") || fName.Contains(".3gp"))
                            beData.FileType = "mp3";
                        else
                            beData.FileType = "jpg";
                    }

                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.OnlineExam.QuestionSetupDetails det = new BE.OnlineExam.QuestionSetupDetails();
                    int tranId = reader.GetInt32(0);
                    if (dataColl.Exists(p1 => p1.TranId == tranId))
                    {
                        if (!(reader[1] is DBNull)) det.SNo = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) det.Answer = reader.GetString(2);
                        if (!(reader[3] is DBNull)) det.FilePath = reader.GetString(3);
                        if (!(reader[4] is DBNull)) det.IsRightAnswer = reader.GetBoolean(4);
                        if (!(reader[5] is DBNull)) det.AnswerTitle = reader.GetInt32(5);
                        if (!(reader[6] is DBNull)) det.SNo_Str = reader.GetString(6);

                        if (!string.IsNullOrEmpty(det.FilePath))
                        {
                            det.FileCount = 1;
                            string fName = det.FilePath.Trim().ToLower();
                            if (fName.Contains(".pdf"))
                                det.FileType = "pdf";
                            else if (fName.Contains(".mp3") || fName.Contains(".mp4") || fName.Contains(".wav") || fName.Contains(".3gp"))
                                det.FileType = "mp3";
                            else
                                det.FileType = "jpg";
                        }
                      

                        dataColl.Find(p1 => p1.TranId == tranId).DetailsColl.Add(det);
                    }                    
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

        public BE.OnlineExam.QuestionSetupCollections getQuestionListForAPI(int UserId, int ExamSetupId,bool isTeacher=false,int? studentId=null)
        {
            BE.OnlineExam.QuestionSetupCollections dataColl = new BE.OnlineExam.QuestionSetupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamSetupId", ExamSetupId);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.CommandText = "usp_GetQuestionListForAPI";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.OnlineExam.QuestionSetup beData = new BE.OnlineExam.QuestionSetup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.QNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Marks = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.QuestionTitle = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Question = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.QuestionPath = reader.GetString(5);
                    if (isTeacher)
                    {
                        if (!(reader[6] is DBNull)) beData.AnswerSNo = reader.GetInt32(6);
                    }
                    if (!(reader[7] is DBNull)) beData.CategoryName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ExamModal = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.SubmitType = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.QuestionRemarks = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.StudentAnswerNo = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.AnswerText = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.StudentDocsPath = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.IsCorrect = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.OETranId = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.ObtainMark = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.Remarks = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.IsChecked = reader.GetBoolean(18);

                    if (!string.IsNullOrEmpty(beData.QuestionPath))
                    {
                        beData.FileCount = 1;
                        string fName = beData.QuestionPath.Trim().ToLower();
                        if (fName.Contains(".pdf"))
                            beData.FileType = "pdf";
                        else
                            beData.FileType = "jpg";
                    }

                    if (!string.IsNullOrEmpty(beData.StudentDocsPath))
                    {
                        string[] splits = new string[] { "##" };
                        beData.StudentDocColl = new List<string>();
                        string[] attColl = beData.StudentDocsPath.Split(splits, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var v in attColl)
                            beData.StudentDocColl.Add(v);
                    }

                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.OnlineExam.QuestionSetupDetails det = new BE.OnlineExam.QuestionSetupDetails();
                    int tranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.SNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.Answer = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.FilePath = reader.GetString(3);
                    if (isTeacher)
                    {
                        if (!(reader[4] is DBNull)) det.IsRightAnswer = reader.GetBoolean(4);
                    }

                    if (!(reader[5] is DBNull)) det.SNo_Str = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det.AnswerTitle = reader.GetInt32(6);

                    try
                    {
                        if (!(reader[7] is DBNull)) det.IsCorrect = Convert.ToBoolean(reader[7]);
                        if (!(reader[8] is DBNull)) det.OETranId = reader.GetInt32(8);
                    }
                    catch { }
                    

                    dataColl.Find(p1 => p1.TranId == tranId).DetailsColl.Add(det);
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
        public BE.OnlineExam.QuestionSetupCollections getQuestionSetupByExamSetupId(int UserId,int ExamSetupId,int CategoryId)
        {
            BE.OnlineExam.QuestionSetupCollections dataColl = new BE.OnlineExam.QuestionSetupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamSetupId", ExamSetupId);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.CommandText = "usp_GetQuestionListByExamSetupId";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.OnlineExam.QuestionSetup beData = new BE.OnlineExam.QuestionSetup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.QNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Marks = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.QuestionTitle = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.Question = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.QuestionPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.AnswerSNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.AnswerTitle = reader.GetInt32(7);
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.OnlineExam.QuestionSetupDetails det = new BE.OnlineExam.QuestionSetupDetails();
                    int tranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.SNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.Answer = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.FilePath = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.IsRightAnswer = reader.GetBoolean(4);
                    if (!(reader[5] is DBNull)) det.AnswerTitle = reader.GetInt32(5);                    
                    dataColl.Find(p1 => p1.TranId == tranId).DetailsColl.Add(det);
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
        public BE.OnlineExam.QuestionSetup getQuestionSetupById(int UserId, int EntityId, int TranId)
        {
            BE.OnlineExam.QuestionSetup beData = new BE.OnlineExam.QuestionSetup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetQuestionSetupById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.OnlineExam.QuestionSetup();
                    beData.TranId = reader.GetInt32(0);
                    //if (!(reader[1] is DBNull)) beData.ExamTypeId = reader.GetInt32(1);
                    //if (!(reader[2] is DBNull)) beData.Modal = reader.GetInt32(2);
                    //if (!(reader[3] is DBNull)) beData.Category = reader.GetInt32(3);
                    //if (!(reader[4] is DBNull)) beData.Marks = reader.GetInt32(4);
                    ////if (!(reader[5] is DBNull)) beData.QuestionTitle = reader.GetInt32(5);
                    //if (!(reader[6] is DBNull)) beData.Text = reader.GetString(6);
                    //if (!(reader[7] is DBNull)) beData.Image = reader.GetString(7);
                    //if (!(reader[8] is DBNull)) beData.ImagePath = reader.GetString(8);
                    //if (!(reader[9] is DBNull)) beData.AudioPath = reader.GetString(9);
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
            cmd.CommandText = "usp_DelQuestionSetupById";
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

        public ResponeValues ExamCopyCheck(AcademicLib.API.Teacher.ExamCopyCheck beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OETranId", beData.OETranId);
            cmd.Parameters.AddWithValue("@ObtainMark", beData.ObtainMark);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);            
            cmd.Parameters.AddWithValue("@UserId", beData.UserId);
            cmd.CommandText = "usp_ExamCopyCheck";            
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;            
            try
            {
                cmd.ExecuteNonQuery();
                
                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[6].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0)
                    resVal.ResponseId = resVal.RId.ToString();
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
