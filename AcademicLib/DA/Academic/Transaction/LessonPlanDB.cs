using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.Academic.Transaction
{
    internal class LessonPlanDB
    {
        DataAccessLayer1 dal = null;
        public LessonPlanDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Transaction.LessonPlan beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
            cmd.Parameters.AddWithValue("@NoOfLesson", beData.NoOfLesson);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateLessonPlan";
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddLessonPlan";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@CoverFilePath", beData.CoverFilePath);
            cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
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

                if (resVal.IsSuccess && resVal.RId > 0)
                    SaveLessonPlanDet(beData.CUserId, resVal.RId, beData.DetailsColl);

                //cmd.Parameters.Clear();
                //cmd.CommandText = "usp_UpdateLessonPlanStartEndDateAuto";
                //cmd.ExecuteNonQuery();
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
        private void SaveLessonPlanDet(int UserId, int TranId, List<BE.Academic.Transaction.LessonPlanDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            int sno = 1;
            foreach (BE.Academic.Transaction.LessonPlanDetails beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.LessonName))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@SNo", beData.SNo);
                    cmd.Parameters.AddWithValue("@LessonName", beData.LessonName);
                    cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
                    cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
                    //cmd.Parameters.AddWithValue("@PlanStartDate_AD", beData.PlanStartDate_AD);
                    //cmd.Parameters.AddWithValue("@PlanEndDate_AD", beData.PlanEndDate_AD);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddLessonPlanDetails";
                    cmd.ExecuteNonQuery();
                    beData.LessonId = Convert.ToInt32(cmd.Parameters[4].Value);
                    SaveLessonTopic(UserId, beData.LessonId,beData.SNo, beData.TopicColl);
                    sno++;
                }               
            }

        }
        private void SaveLessonTopic(int UserId, int TranId,int LessonSNo, List<BE.Academic.Transaction.LessonTopic> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            int sno = 1;
            foreach (BE.Academic.Transaction.LessonTopic beData in beDataColl)
            {
                if (!string.IsNullOrEmpty(beData.TopicName))
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@LessonId", TranId);
                    cmd.Parameters.AddWithValue("@SNo", beData.SNo);
                    cmd.Parameters.AddWithValue("@LessonSNo", LessonSNo);
                    cmd.Parameters.AddWithValue("@TopicName", beData.TopicName);
                    //cmd.Parameters.AddWithValue("@PlanStartDate_AD", beData.PlanStartDate_AD);
                    //cmd.Parameters.AddWithValue("@PlanEndDate_AD", beData.PlanEndDate_AD);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddLessonTopic";
                    cmd.ExecuteNonQuery();
                    sno++;
                }
                
            }

        }

        public ResponeValues UpdatePlanDate(BE.Academic.Transaction.LessonPlan beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
          

            try
            { 
                if(string.IsNullOrEmpty(beData.SectionId))
                {
                    foreach (BE.Academic.Transaction.LessonPlanDetails det in beData.DetailsColl)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                        cmd.Parameters.AddWithValue("@LessonId", det.LessonId);
                        cmd.Parameters.AddWithValue("@PlanStartDate_AD", det.PlanStartDate_AD);
                        cmd.Parameters.AddWithValue("@PlanEndDate_AD", det.PlanEndDate_AD);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "usp_UpdateLessonPlanDate";
                        cmd.ExecuteNonQuery();

                        foreach (var topic in det.TopicColl)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                            cmd.Parameters.AddWithValue("@LessonId", det.LessonId);
                            cmd.Parameters.AddWithValue("@SNo", topic.SNo);
                            cmd.Parameters.AddWithValue("@PlanStartDate_AD", topic.PlanStartDate_AD);
                            cmd.Parameters.AddWithValue("@PlanEndDate_AD", topic.PlanEndDate_AD);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "usp_UpdateTopicPlanDate";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    string[] sectionIdColl = beData.SectionId.Split(',');

                    foreach(var sec in sectionIdColl)
                    {
                        if(sec!="0" && !string.IsNullOrEmpty(sec))
                        {
                            foreach (BE.Academic.Transaction.LessonPlanDetails det in beData.DetailsColl)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                                cmd.Parameters.AddWithValue("@LessonId", det.LessonId);
                                cmd.Parameters.AddWithValue("@PlanStartDate_AD", det.PlanStartDate_AD);
                                cmd.Parameters.AddWithValue("@PlanEndDate_AD", det.PlanEndDate_AD);
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.CommandText = "usp_UpdateLessonPlanDate";
                                cmd.ExecuteNonQuery();

                                foreach (var topic in det.TopicColl)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                                    cmd.Parameters.AddWithValue("@LessonId", det.LessonId);
                                    cmd.Parameters.AddWithValue("@SNo", topic.SNo);
                                    cmd.Parameters.AddWithValue("@PlanStartDate_AD", topic.PlanStartDate_AD);
                                    cmd.Parameters.AddWithValue("@PlanEndDate_AD", topic.PlanEndDate_AD);
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd.CommandText = "usp_UpdateTopicPlanDate";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        
                    }
              
                }
                
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Lesson Plan Date Updated Success";


            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public AcademicLib.BE.Academic.Transaction.LessonPlan getLessonPlanByClassSubjectWise(int UserId, int ClassId, int SubjectId, string SectionIdColl, int? BatchId, int? ClassYearId, int? SemesterId, int? AcademicYearId)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlan beData = new AcademicLib.BE.Academic.Transaction.LessonPlan();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetLessonPlanByClassSubjectWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Academic.Transaction.LessonPlan();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SubjectId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.NoOfLesson = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.CoverFilePath = reader.GetString(4);
                    //Added
                    if (!(reader[5] is DBNull)) beData.BatchId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ClassYearId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.SemesterId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.AcademicYearId = reader.GetInt32(8);
                }

                if (beData.TranId > 0)
                {
                    reader.NextResult();
                    while (reader.Read())
                    {
                        BE.Academic.Transaction.LessonPlanDetails det = new BE.Academic.Transaction.LessonPlanDetails();
                        int tranId = reader.GetInt32(0);
                        det.LessonId = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) det.SNo = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) det.LessonName = reader.GetString(3);
                        //if (!(reader[4] is DBNull)) det.PlanStartDate_AD = reader.GetDateTime(4);
                        //if (!(reader[5] is DBNull)) det.PlanEndDate_AD = reader.GetDateTime(5);
                        //if (!(reader[6] is DBNull)) det.PlanStartDate_BS = reader.GetString(6);
                        //if (!(reader[7] is DBNull)) det.PlanEndDate_BS = reader.GetString(7);

                        //if (!(reader[8] is DBNull)) det.StartDate_AD = reader.GetDateTime(8);
                        //if (!(reader[9] is DBNull)) det.EndDate_AD = reader.GetDateTime(9);
                        //if (!(reader[10] is DBNull)) det.StartRemarks = reader.GetString(10);
                        //if (!(reader[11] is DBNull)) det.EndRemarks = reader.GetString(11);
                        //if (!(reader[12] is DBNull)) det.StartBy = reader.GetInt32(12);
                        //if (!(reader[13] is DBNull)) det.StartDate_BS = reader.GetString(13);
                        //if (!(reader[14] is DBNull)) det.EndDate_BS = reader.GetString(14);
                        if (!(reader[4] is DBNull)) det.EmpName = reader.GetString(4);
                        if (!(reader[5] is DBNull)) det.EmpCode = reader.GetString(5);
                        //if (!(reader[17] is DBNull)) det.Status = reader.GetString(17);
                        //if (!(reader[18] is DBNull)) det.StatusValue = reader.GetInt32(18);
                        //if (!(reader[19] is DBNull)) det.TotalDays = reader.GetInt32(19);
                        beData.DetailsColl.Add(det);
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        BE.Academic.Transaction.LessonTopic det = new BE.Academic.Transaction.LessonTopic();
                        det.LessonId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) det.SNo = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) det.TopicName = reader.GetString(2);
                        //if (!(reader[3] is DBNull)) det.PlanStartDate_AD = reader.GetDateTime(3);
                        //if (!(reader[4] is DBNull)) det.PlanEndDate_AD = reader.GetDateTime(4);
                        //if (!(reader[5] is DBNull)) det.PlanStartDate_BS = reader.GetString(5);
                        //if (!(reader[6] is DBNull)) det.PlanEndDate_BS = reader.GetString(6);

                        //if (!(reader[7] is DBNull)) det.StartDate_AD = reader.GetDateTime(7);
                        //if (!(reader[8] is DBNull)) det.EndDate_AD = reader.GetDateTime(8);
                        //if (!(reader[9] is DBNull)) det.StartRemarks = reader.GetString(9);
                        //if (!(reader[10] is DBNull)) det.EndRemarks = reader.GetString(10);
                        //if (!(reader[11] is DBNull)) det.StartBy = reader.GetInt32(11);
                        //if (!(reader[12] is DBNull)) det.StartDate_BS = reader.GetString(12);
                        //if (!(reader[13] is DBNull)) det.EndDate_BS = reader.GetString(13);
                        if (!(reader[3] is DBNull)) det.EmpName = reader.GetString(3);
                        if (!(reader[4] is DBNull)) det.EmpCode = reader.GetString(4);
                        //if (!(reader[16] is DBNull)) det.Status = reader.GetString(16);
                        //if (!(reader[17] is DBNull)) det.StatusValue = reader.GetInt32(17);
                        //if (!(reader[18] is DBNull)) det.TotalDays = reader.GetInt32(18);

                        //if (det.StatusValue == 1)
                        //{
                        //    if (det.PlanStartDate_AD.HasValue)
                        //    {
                        //        int daysDiff = (DateTime.Today - det.PlanStartDate_AD.Value).Days;

                        //        if (daysDiff > 0)
                        //        {
                        //            det.StatusDays = " from " + daysDiff.ToString() + " Days";
                        //        }
                        //    }
                        //}
                        //else if (det.StatusValue == 2)
                        //{
                        //    if (det.StartDate_AD.HasValue)
                        //    {
                        //        int daysDiff = (DateTime.Today - det.StartDate_AD.Value).Days;

                        //        if (daysDiff > 0)
                        //        {
                        //            det.StatusDays = " from " + daysDiff.ToString() + " Days";
                        //        }
                        //    }

                        //}
                        //else if (det.StatusValue == 3)
                        //{
                        //    if (det.EndDate_AD.HasValue)
                        //    {
                        //        int daysDiff = (DateTime.Today - det.EndDate_AD.Value).Days;

                        //        if (daysDiff > 0)
                        //        {
                        //            det.StatusDays = " Before " + daysDiff.ToString() + " Days";
                        //        }
                        //    }
                        //}

                        beData.DetailsColl.Find(p1 => p1.LessonId == det.LessonId).TopicColl.Add(det);
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        BE.Academic.Transaction.LessonTopicTeacherContent det1 = new BE.Academic.Transaction.LessonTopicTeacherContent();
                        det1.LessonId = reader.GetInt32(0);
                        det1.LessonSNo = reader.GetInt32(1);
                        det1.TopicSNo = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) det1.SNo = reader.GetInt32(3);
                        if (!(reader[4] is DBNull)) det1.ForDate = reader.GetDateTime(4);
                        if (!(reader[5] is DBNull)) det1.FileName = reader.GetString(5);
                        if (!(reader[6] is DBNull)) det1.FilePath = reader.GetString(6);
                        if (!(reader[7] is DBNull)) det1.Contents = reader.GetString(7);
                        //if (!(reader[8] is DBNull)) det1.ForDateBS = reader.GetString(8);

                        //if (!(reader[9] is DBNull)) det1.StartDate_AD = reader.GetDateTime(9);
                        //if (!(reader[10] is DBNull)) det1.EndDate_AD = reader.GetDateTime(10);
                        //if (!(reader[11] is DBNull)) det1.StartRemarks = reader.GetString(11);
                        //if (!(reader[12] is DBNull)) det1.EndRemarks = reader.GetString(12);
                        //if (!(reader[13] is DBNull)) det1.StartBy = reader.GetInt32(13);
                        //if (!(reader[14] is DBNull)) det1.StartDate_BS = reader.GetString(14);
                        //if (!(reader[15] is DBNull)) det1.EndDate_BS = reader.GetString(15);
                        if (!(reader[8] is DBNull)) det1.EmpName = reader.GetString(8);
                        if (!(reader[9] is DBNull)) det1.EmpCode = reader.GetString(9);
                        //if (!(reader[18] is DBNull)) det1.Status = reader.GetString(18);
                        //if (!(reader[19] is DBNull)) det1.StatusValue = reader.GetInt32(19);
                        //if (!(reader[20] is DBNull)) det1.TotalDays = reader.GetInt32(20);
                        beData.DetailsColl.Find(p1 => p1.LessonId == det1.LessonId).TopicColl.Find(p1 => p1.SNo == det1.TopicSNo).ContentsColl.Add(det1);
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        BE.Academic.Transaction.LessonTopicTeacherContent det1 = new BE.Academic.Transaction.LessonTopicTeacherContent();
                        det1.LessonId = reader.GetInt32(0);
                        det1.LessonSNo = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) det1.SNo = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) det1.ForDate = reader.GetDateTime(3);
                        if (!(reader[4] is DBNull)) det1.FileName = reader.GetString(4);
                        if (!(reader[5] is DBNull)) det1.FilePath = reader.GetString(5);
                        if (!(reader[6] is DBNull)) det1.Contents = reader.GetString(6);
                        //if (!(reader[7] is DBNull)) det1.ForDateBS = reader.GetString(7);

                        beData.DetailsColl.Find(p1 => p1.LessonId == det1.LessonId).ContentsColl.Add(det1);
                    }
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

        public AcademicLib.BE.Academic.Transaction.LessonPlanCollections getLessonPlanByClass(int UserId, int? ClassId,int? SectionId, int? EmployeeId, int? SubjectId, int ReportType, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlanCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonPlanCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@ReportType", ReportType);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetLessonPlanByClass";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.LessonPlan beData = new AcademicLib.BE.Academic.Transaction.LessonPlan();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.SubjectId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.NoOfLesson = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.CoverFilePath = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SubjectName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.EmpName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.EmpPhotoPath = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.EmpDesignation = reader.GetString(10);
                    dataColl.Add(beData);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Transaction.LessonPlanDetails det = new BE.Academic.Transaction.LessonPlanDetails();
                    int tranId = reader.GetInt32(0);
                    det.LessonId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.SNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.LessonName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.PlanStartDate_AD = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) det.PlanEndDate_AD = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) det.PlanStartDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) det.PlanEndDate_BS = reader.GetString(7);

                    if (!(reader[8] is DBNull)) det.StartDate_AD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) det.EndDate_AD = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) det.StartRemarks = reader.GetString(10);
                    if (!(reader[11] is DBNull)) det.EndRemarks = reader.GetString(11);
                    if (!(reader[12] is DBNull)) det.StartBy = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) det.StartDate_BS = reader.GetString(13);
                    if (!(reader[14] is DBNull)) det.EndDate_BS = reader.GetString(14);
                    if (!(reader[15] is DBNull)) det.EmpName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) det.EmpCode = reader.GetString(16);
                    if (!(reader[17] is DBNull)) det.Status = reader.GetString(17);
                    if (!(reader[18] is DBNull)) det.StatusValue = reader.GetInt32(18);
                    if (!(reader[19] is DBNull)) det.TotalDays = reader.GetInt32(19);
                    dataColl.Find(p1=>p1.TranId== tranId).DetailsColl.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Transaction.LessonTopic det = new BE.Academic.Transaction.LessonTopic();
                    int tranId = reader.GetInt32(0);
                    det.LessonId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.SNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.TopicName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.PlanStartDate_AD = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) det.PlanEndDate_AD = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) det.PlanStartDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) det.PlanEndDate_BS = reader.GetString(7);

                    if (!(reader[8] is DBNull)) det.StartDate_AD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) det.EndDate_AD = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) det.StartRemarks = reader.GetString(10);
                    if (!(reader[11] is DBNull)) det.EndRemarks = reader.GetString(11);
                    if (!(reader[12] is DBNull)) det.StartBy = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) det.StartDate_BS = reader.GetString(13);
                    if (!(reader[14] is DBNull)) det.EndDate_BS = reader.GetString(14);
                    if (!(reader[15] is DBNull)) det.EmpName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) det.EmpCode = reader.GetString(16);
                    if (!(reader[17] is DBNull)) det.Status = reader.GetString(17);
                    if (!(reader[18] is DBNull)) det.StatusValue = reader.GetInt32(18);
                    if (!(reader[19] is DBNull)) det.TotalDays = reader.GetInt32(19);

                    if (det.StatusValue == 1)
                    {
                        if (det.PlanStartDate_AD.HasValue)
                        {
                            int daysDiff = (DateTime.Today-det.PlanStartDate_AD.Value ).Days;

                            if (daysDiff > 0)
                            {
                                det.StatusDays = " from " + daysDiff.ToString() + " Days";
                            }
                        }
                    }
                    else if (det.StatusValue == 2)
                    {
                        if (det.StartDate_AD.HasValue)
                        {
                            int daysDiff = (DateTime.Today-det.StartDate_AD.Value).Days;

                            if (daysDiff > 0)
                            {
                                det.StatusDays = " from " + daysDiff.ToString() + " Days";
                            }
                        }

                    }
                    else if (det.StatusValue == 3)
                    {
                        if (det.EndDate_AD.HasValue)
                        {
                            int daysDiff = (DateTime.Today - det.EndDate_AD.Value ).Days;

                            if (daysDiff > 0)
                            {
                                det.StatusDays = " Before " + daysDiff.ToString() + " Days";
                            }
                        }
                    }
                    dataColl.Find(p1 => p1.TranId == tranId).DetailsColl.Find(p1 => p1.LessonId == det.LessonId).TopicColl.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Transaction.LessonTopicTeacherContent det1 = new BE.Academic.Transaction.LessonTopicTeacherContent();
                    int tranId = reader.GetInt32(0);
                    det1.LessonId = reader.GetInt32(1);
                    det1.LessonSNo = reader.GetInt32(2);
                    det1.TopicSNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.SNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det1.ForDate = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) det1.FileName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) det1.FilePath = reader.GetString(7);
                    if (!(reader[8] is DBNull)) det1.Contents = reader.GetString(8);
                    if (!(reader[9] is DBNull)) det1.ForDateBS = reader.GetString(9);

                    if (!(reader[10] is DBNull)) det1.StartDate_AD = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) det1.EndDate_AD = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) det1.StartRemarks = reader.GetString(12);
                    if (!(reader[13] is DBNull)) det1.EndRemarks = reader.GetString(13);
                    if (!(reader[14] is DBNull)) det1.StartBy = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) det1.StartDate_BS = reader.GetString(15);
                    if (!(reader[16] is DBNull)) det1.EndDate_BS = reader.GetString(16);
                    if (!(reader[17] is DBNull)) det1.EmpName = reader.GetString(17);
                    if (!(reader[18] is DBNull)) det1.EmpCode = reader.GetString(18);
                    if (!(reader[19] is DBNull)) det1.Status = reader.GetString(19);
                    if (!(reader[20] is DBNull)) det1.StatusValue = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) det1.TotalDays = reader.GetInt32(21);
                    dataColl.Find(p1 => p1.TranId == tranId).DetailsColl.Find(p1 => p1.LessonId == det1.LessonId).TopicColl.Find(p1 => p1.SNo == det1.TopicSNo).ContentsColl.Add(det1);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Transaction.LessonTopicTeacherContent det1 = new BE.Academic.Transaction.LessonTopicTeacherContent();
                    int tranId = reader.GetInt32(0);
                    det1.LessonId = reader.GetInt32(1);
                    det1.LessonSNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det1.SNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.ForDate = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) det1.FileName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det1.FilePath = reader.GetString(6);
                    if (!(reader[7] is DBNull)) det1.Contents = reader.GetString(7);
                    if (!(reader[8] is DBNull)) det1.ForDateBS = reader.GetString(8);

                    dataColl.Find(p1 => p1.TranId == tranId).DetailsColl.Find(p1 => p1.LessonId == det1.LessonId).ContentsColl.Add(det1);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Transaction.LessonTopicContent det1 = new BE.Academic.Transaction.LessonTopicContent();
                    int tranId = reader.GetInt32(0);
                    det1.LessonId = reader.GetInt32(1);
                    det1.LessonSNo = reader.GetInt32(2);
                    det1.TopicSNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.SNo = reader.GetInt32(4);                   
                    if (!(reader[5] is DBNull)) det1.FileName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det1.FilePath = reader.GetString(6);                   
                    dataColl.Find(p1 => p1.TranId == tranId).DetailsColl.Find(p1 => p1.LessonId == det1.LessonId).TopicColl.Find(p1 => p1.SNo == det1.TopicSNo).TopicContentsColl.Add(det1);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Transaction.LessonTopicVideo det1 = new BE.Academic.Transaction.LessonTopicVideo();
                    int tranId = reader.GetInt32(0);
                    det1.LessonId = reader.GetInt32(1);
                    det1.LessonSNo = reader.GetInt32(2);
                    det1.TopicSNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det1.SNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det1.Title = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det1.Link = reader.GetString(6);
                    if (!(reader[7] is DBNull)) det1.Remarks = reader.GetString(7);
                    dataColl.Find(p1 => p1.TranId == tranId).DetailsColl.Find(p1 => p1.LessonId == det1.LessonId).TopicColl.Find(p1 => p1.SNo == det1.TopicSNo).VideosColl.Add(det1);
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

        public AcademicLib.BE.Academic.Transaction.ClassShiftCollections getAllClassShift(int UserId, int EntityId)
        {
            AcademicLib.BE.Academic.Transaction.ClassShiftCollections dataColl = new AcademicLib.BE.Academic.Transaction.ClassShiftCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllClassShift";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.ClassShift beData = new AcademicLib.BE.Academic.Transaction.ClassShift();
                    beData.ClassShiftId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.WeeklyDayOff = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.StartTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.EndTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.NoofBreak = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ForOnlineClass = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsActive = reader.GetBoolean(7);

                    if (beData.StartTime.HasValue && beData.EndTime.HasValue)
                    {
                        int min = (int)(beData.EndTime.Value - beData.StartTime.Value).TotalMinutes;
                        beData.Duration = min;

                    }

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
        public AcademicLib.BE.Academic.Transaction.ClassShift getClassShiftById(int UserId, int EntityId, int ClassShiftId)
        {
            AcademicLib.BE.Academic.Transaction.ClassShift beData = new AcademicLib.BE.Academic.Transaction.ClassShift();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetClassShiftById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Academic.Transaction.ClassShift();
                    beData.ClassShiftId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.WeeklyDayOff = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.StartTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.EndTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.NoofBreak = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ForOnlineClass = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.IsActive = reader.GetBoolean(7);

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
        public ResponeValues DeleteById(int UserId, int EntityId, int ClassShiftId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
            cmd.CommandText = "usp_DelClassShiftById";
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

        public ResponeValues SaveLessonTeacherContent(int UserId, List<BE.Academic.Transaction.LessonTopicTeacherContent> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                var fst = dataColl[0];
                cmd.Parameters.AddWithValue("@LessonId", fst.LessonId);
                cmd.Parameters.AddWithValue("@LessonSNo", fst.LessonSNo);                
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_DelLessonTeacherContents";
                cmd.ExecuteNonQuery();

                int sno = 1;
                foreach (BE.Academic.Transaction.LessonTopicTeacherContent det in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@LessonId", det.LessonId);
                    cmd.Parameters.AddWithValue("@LessonSNo", det.LessonSNo);                    
                    cmd.Parameters.AddWithValue("@FileName", det.FileName);
                    cmd.Parameters.AddWithValue("@FilePath", det.FilePath);
                    cmd.Parameters.AddWithValue("@ForDate", det.ForDate);
                    cmd.Parameters.AddWithValue("@Contents", det.Contents);
                    cmd.Parameters.AddWithValue("@SNo", sno);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddLessonTeacherContents";
                    cmd.ExecuteNonQuery();
                    sno++;
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Lesson Contents Updated Success";


            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections getLessonTeacherContent(int UserId, int LessonId, int LessonSNo)
        {
            AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@LessonId", LessonId);
            cmd.Parameters.AddWithValue("@LessonSNo", LessonSNo);            
            cmd.CommandText = "usp_GetLessonTeacherContents";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent beData = new AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent();
                    beData.LessonSNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FileName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.FilePath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ForDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.Contents = reader.GetString(4);

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


        public ResponeValues SaveLessonTopicTeacherContent(int UserId, List<BE.Academic.Transaction.LessonTopicTeacherContent> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                var fst = dataColl[0];
                cmd.Parameters.AddWithValue("@LessonId", fst.LessonId);
                cmd.Parameters.AddWithValue("@LessonSNo", fst.LessonSNo);
                cmd.Parameters.AddWithValue("@TopicSNo", fst.TopicSNo);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_DelLessonTopicTeacherContents";
                cmd.ExecuteNonQuery();

                int sno = 1;
                foreach (BE.Academic.Transaction.LessonTopicTeacherContent det in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@LessonId", det.LessonId);
                    cmd.Parameters.AddWithValue("@LessonSNo", det.LessonSNo);
                    cmd.Parameters.AddWithValue("@TopicSNo", det.TopicSNo);
                    cmd.Parameters.AddWithValue("@FileName", det.FileName);
                    cmd.Parameters.AddWithValue("@FilePath", det.FilePath);
                    cmd.Parameters.AddWithValue("@ForDate", det.ForDate);
                    cmd.Parameters.AddWithValue("@Contents", det.Contents);
                    cmd.Parameters.AddWithValue("@SNo", sno);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddLessonTopicTeacherContents";
                    cmd.ExecuteNonQuery();
                    sno++;
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Lesson Topic Contents Updated Success";


            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections getLessonTopicTeacherContent(int UserId, int LessonId, int LessonSNo, int TopicSNo)
        {
            AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@LessonId", LessonId);
            cmd.Parameters.AddWithValue("@LessonSNo", LessonSNo);
            cmd.Parameters.AddWithValue("@TopicSNo", TopicSNo);
            cmd.CommandText = "usp_GetLessonTopicTeacherContents";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent beData = new AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent();
                    beData.LessonSNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FileName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.FilePath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ForDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.Contents = reader.GetString(4);

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

        public ResponeValues SaveLessonTopicContent(int UserId,List<BE.Academic.Transaction.LessonTopicContent> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            try
            {
                var fst = dataColl[0];
                //cmd.Parameters.AddWithValue("@LessonId", fst.LessonId);
                cmd.Parameters.AddWithValue("@ClassId", fst.ClassId);
                cmd.Parameters.AddWithValue("@BatchId", fst.BatchId);
                cmd.Parameters.AddWithValue("@SemesterId", fst.SemesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", fst.ClassYearId);
                cmd.Parameters.AddWithValue("@SubjectId", fst.SubjectId);
                cmd.Parameters.AddWithValue("@LessonSNo", fst.LessonSNo);
                cmd.Parameters.AddWithValue("@TopicSNo", fst.TopicSNo);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_DelLessonTopicContents";
                cmd.ExecuteNonQuery();

                int sno = 1;
                foreach (BE.Academic.Transaction.LessonTopicContent det in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@LessonId", det.LessonId);
                    cmd.Parameters.AddWithValue("@LessonSNo", det.LessonSNo);
                    cmd.Parameters.AddWithValue("@TopicSNo", det.TopicSNo);
                    cmd.Parameters.AddWithValue("@FileName", det.FileName);
                    cmd.Parameters.AddWithValue("@FilePath", det.FilePath);
                    cmd.Parameters.AddWithValue("@SNo", sno);
                    //New Added
                    cmd.Parameters.AddWithValue("@ClassId", det.ClassId);
                    cmd.Parameters.AddWithValue("@BatchId", det.BatchId);
                    cmd.Parameters.AddWithValue("@SemesterId", det.SemesterId);
                    cmd.Parameters.AddWithValue("@ClassYearId", det.ClassYearId);
                    cmd.Parameters.AddWithValue("@SubjectId", det.SubjectId);
                    //Added Ends
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddLessonTopicContents";
                    cmd.ExecuteNonQuery();
                    sno++;
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Lesson Topic Contents Updated Success";


            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public AcademicLib.BE.Academic.Transaction.LessonTopicContentCollections getLessonTopicContent(int UserId, /*int LessonId,*/int LessonSNo,int TopicSNo,int ClassId, int SubjectId, int? BatchId,int? SemesterId, int? ClassYearId)
        {
            AcademicLib.BE.Academic.Transaction.LessonTopicContentCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonTopicContentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            //cmd.Parameters.AddWithValue("@LessonId", LessonId);
            cmd.Parameters.AddWithValue("@LessonSNo", LessonSNo);
            cmd.Parameters.AddWithValue("@TopicSNo", TopicSNo);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetLessonTopicContents";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.LessonTopicContent beData = new AcademicLib.BE.Academic.Transaction.LessonTopicContent();
                    beData.LessonSNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FileName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.FilePath = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.BatchId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.SemesterId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ClassYearId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.SubjectId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.LessonSNo = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.TopicSNo = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.SNo = reader.GetInt32(10);

                    string fname = beData.FileName.Trim().ToLower();
                    if (fname.Contains(".pdf"))
                    {
                        beData.FileType = "pdf";
                    }else if (fname.Contains(".jpg") || fname.Contains(".jpeg") || fname.Contains(".png"))
                    {
                        beData.FileType = "img";
                    }

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

        public ResponeValues SaveLessonTopicVideo(int UserId, List<BE.Academic.Transaction.LessonTopicVideo> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                var fst = dataColl[0];
                //cmd.Parameters.AddWithValue("@LessonId", fst.LessonId);
                cmd.Parameters.AddWithValue("@ClassId", fst.ClassId);
                cmd.Parameters.AddWithValue("@BatchId", fst.BatchId);
                cmd.Parameters.AddWithValue("@SemesterId", fst.SemesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", fst.ClassYearId);
                cmd.Parameters.AddWithValue("@SubjectId", fst.SubjectId);
                cmd.Parameters.AddWithValue("@LessonSNo", fst.LessonSNo);
                cmd.Parameters.AddWithValue("@TopicSNo", fst.TopicSNo);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_DelLessonTopicVideo";
                cmd.ExecuteNonQuery();

                int sno = 1;
                foreach (BE.Academic.Transaction.LessonTopicVideo det in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@LessonId", det.LessonId);
                    cmd.Parameters.AddWithValue("@LessonSNo", det.LessonSNo);
                    cmd.Parameters.AddWithValue("@TopicSNo", det.TopicSNo);
                    cmd.Parameters.AddWithValue("@Title", det.Title);
                    cmd.Parameters.AddWithValue("@Link", det.Link);
                    cmd.Parameters.AddWithValue("@Remarks", det.Remarks);
                    cmd.Parameters.AddWithValue("@SNo", sno);
                    //New Added
                    cmd.Parameters.AddWithValue("@ClassId", det.ClassId);
                    cmd.Parameters.AddWithValue("@BatchId", det.BatchId);
                    cmd.Parameters.AddWithValue("@SemesterId", det.SemesterId);
                    cmd.Parameters.AddWithValue("@ClassYearId", det.ClassYearId);
                    cmd.Parameters.AddWithValue("@SubjectId", det.SubjectId);
                    //Added Ends
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddLessonTopicVideo";
                    cmd.ExecuteNonQuery();
                    sno++;
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Lesson Topic Videos Updated Success";


            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public AcademicLib.BE.Academic.Transaction.LessonTopicVideoCollections getLessonTopicVideo(int UserId, /*int LessonId,*/int LessonSNo, int TopicSNo, int ClassId, int SubjectId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.BE.Academic.Transaction.LessonTopicVideoCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonTopicVideoCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            //cmd.Parameters.AddWithValue("@LessonId", LessonId);
            cmd.Parameters.AddWithValue("@LessonSNo", LessonSNo);
            cmd.Parameters.AddWithValue("@TopicSNo", TopicSNo);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetLessonTopicVideo";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Academic.Transaction.LessonTopicVideo beData = new AcademicLib.BE.Academic.Transaction.LessonTopicVideo();
                    beData.SNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Title = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Link = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Remarks = reader.GetString(3);

                    if (!(reader[4] is DBNull)) beData.ClassId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.BatchId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.SemesterId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ClassYearId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.SubjectId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.LessonSNo = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.TopicSNo = reader.GetInt32(10);
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


        public ResponeValues SaveLessonTopicQuiz(int UserId,BE.Academic.Transaction.LessonTopicQuiz beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
           // dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            { 
                //cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
                //cmd.Parameters.AddWithValue("@LessonSNo", beData.LessonSNo);
                //cmd.Parameters.AddWithValue("@TopicSNo", beData.TopicSNo);
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.CommandText = "usp_DelLessonTopicQuiz";
                //cmd.ExecuteNonQuery();


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
                cmd.Parameters.AddWithValue("@LessonSNo", beData.LessonSNo);
                cmd.Parameters.AddWithValue("@TopicSNo", beData.TopicSNo);
                cmd.Parameters.AddWithValue("@Topic", beData.Topic);
                cmd.Parameters.AddWithValue("@Description", beData.Description);
                cmd.Parameters.AddWithValue("@NoOfQuestion", beData.NoOfQuestion);
                cmd.Parameters.AddWithValue("@FullMark", beData.FullMark);
                cmd.Parameters.AddWithValue("@PassMark", beData.PassMark);
                cmd.Parameters.AddWithValue("@Duration", beData.Duration);
                cmd.Parameters.AddWithValue("@QuizId", beData.QuizId);
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                //cmd.Parameters.Add("@QuizId", System.Data.SqlDbType.Int);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (isModify)
                {
                    cmd.CommandText = "usp_UpdateLessonTopicQuiz";
                }
                else
                {
                    cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                    cmd.CommandText = "usp_AddLessonTopicQuiz";
                }

                //cmd.CommandText = "usp_AddLessonTopicQuiz";
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[10].Value is DBNull))
                {

                    int quizId = Convert.ToInt32(cmd.Parameters[10].Value);
                    foreach(var que in beData.QuestionColl)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@QuizId", quizId);
                        cmd.Parameters.AddWithValue("@QuestionType", que.QuestionType);
                        cmd.Parameters.AddWithValue("@AnswerType", que.AnswerType);
                        cmd.Parameters.AddWithValue("@QuestionContent", que.QuestionContent);
                        cmd.Parameters.AddWithValue("@ContentPath", que.ContentPath);
                        cmd.Parameters.AddWithValue("@Mark", que.Mark);
                        cmd.Parameters.AddWithValue("@Duration", que.Duration);
                        cmd.Parameters.AddWithValue("@AnswerSNo", que.AnswerSNo);
                        cmd.Parameters.AddWithValue("@SNo", que.SNo);
                        cmd.Parameters.Add("@QuestionId", System.Data.SqlDbType.Int);
                        cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                        cmd.CommandText = "usp_AddLessonTopicQuizQuestion";                        
                        cmd.ExecuteNonQuery();

                        if (!(cmd.Parameters[10].Value is DBNull))
                        {
                            int questionId = Convert.ToInt32(cmd.Parameters[10].Value);
                            foreach (var ans in que.AnswerColl)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@UserId", UserId);
                                cmd.Parameters.AddWithValue("@QuestionId", questionId);
                                cmd.Parameters.AddWithValue("@SNo", ans.SNo);
                                cmd.Parameters.AddWithValue("@AnswerType", ans.AnswerType);
                                cmd.Parameters.AddWithValue("@AnswerContent", ans.AnswerContent);
                                cmd.Parameters.AddWithValue("@ContentPath", ans.ContentPath);
                                cmd.Parameters.AddWithValue("@IsRightAnswer", ans.IsRightAnswer);
                                cmd.CommandText = "usp_AddLessonTopicQuizAnswer";
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                  //  dal.CommitTransaction();
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = "Lesson Topic Quiz Updated Success";
                }
                else
                {
                   // dal.RollbackTransaction();
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Unable To Update Lesson Topic Quiz";
                }

                


            }
            catch (System.Data.SqlClient.SqlException ee)
            {
               // dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
               // dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public AcademicLib.BE.Academic.Transaction.LessonTopicQuiz getLessonTopicQuiz(int UserId, int QuizId)
        {
            AcademicLib.BE.Academic.Transaction.LessonTopicQuiz beData = new BE.Academic.Transaction.LessonTopicQuiz();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@QuizId", QuizId);
            cmd.CommandText = "usp_GetLessonTopicQuiz";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Transaction.LessonTopicQuiz();
                    beData.Topic = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Description = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.NoOfQuestion = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FullMark = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.PassMark = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.Duration = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.QuizId = reader.GetInt32(6);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Transaction.LessonTopicQuizQuestion que = new BE.Academic.Transaction.LessonTopicQuizQuestion();
                    que.QuestionId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) que.SNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) que.QuestionType = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) que.AnswerType = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) que.QuestionContent = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) que.ContentPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) que.Mark = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) que.Duration = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) que.AnswerSNo = Convert.ToInt32(reader[8]);
                    beData.QuestionColl.Add(que);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Academic.Transaction.LessonTopicQuizAnswer ans = new BE.Academic.Transaction.LessonTopicQuizAnswer();
                    ans.QuestionId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) ans.AnswerType = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) ans.AnswerContent = reader.GetString(2);
                    if (!(reader[3] is DBNull)) ans.ContentPath = Convert.ToString(reader[3]);
                    if (!(reader[4] is DBNull)) ans.IsRightAnswer = Convert.ToBoolean(reader[4]);
                    if (!(reader[5] is DBNull)) ans.SNo = reader.GetInt32(5);

                    beData.QuestionColl.Find(p1 => p1.QuestionId == ans.QuestionId).AnswerColl.Add(ans);
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


        public ResponeValues StartLesson(int UserId, BE.Academic.Transaction.LessonPlanDetails beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection(); 
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StartBy", beData.StartBy);
                cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
                cmd.Parameters.AddWithValue("@StartDate", beData.StartDate_AD);
                cmd.Parameters.AddWithValue("@Remarks", beData.StartRemarks);
                cmd.Parameters.Add("@IsSuccess",System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ResponseMSG",System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_UpdateLessonStart";
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);
                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);
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
        public ResponeValues EndLesson(int UserId, BE.Academic.Transaction.LessonPlanDetails beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StartBy", beData.StartBy);
                cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
                cmd.Parameters.AddWithValue("@EndDate", beData.EndDate_AD);
                cmd.Parameters.AddWithValue("@Remarks", beData.EndRemarks);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_UpdateLessonEnd";
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);
                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);
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


        //public ResponeValues StartTopic(int UserId, BE.Academic.Transaction.LessonTopic beData)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    dal.OpenConnection();
        //    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@StartBy", beData.StartBy);
        //        cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);                
        //        cmd.Parameters.AddWithValue("@StartDate", beData.StartDate_AD);
        //        cmd.Parameters.AddWithValue("@Remarks", beData.StartRemarks);
        //        cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
        //        cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
        //        cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters.AddWithValue("@TopicSNo", beData.SNo);
        //        cmd.CommandText = "usp_UpdateLessonTopicStart";
        //        cmd.ExecuteNonQuery();

        //        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);
        //        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);
        //    }
        //    catch (System.Data.SqlClient.SqlException ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    finally
        //    {
        //        dal.CloseConnection();
        //    }
        //    return resVal;
        //}
        public ResponeValues StartTopic(int UserId, BE.Academic.Transaction.LessonTopic beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StartBy", UserId);
                cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
                cmd.Parameters.AddWithValue("@StartDate", beData.StartDate_AD);
                cmd.Parameters.AddWithValue("@Remarks", beData.StartRemarks);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@TopicSNo", beData.SNo);
                //Added by suresh for new table
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                cmd.Parameters.AddWithValue("@LessonSNo", beData.LessonSNo);
                cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                cmd.CommandText = "usp_StartTopic";
                //cmd.CommandText = "usp_UpdateLessonTopicStart";
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);
                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);
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


        //public ResponeValues EndTopic(int UserId, BE.Academic.Transaction.LessonTopic beData)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    dal.OpenConnection();
        //    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@StartBy", beData.StartBy);
        //        cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
        //        cmd.Parameters.AddWithValue("@EndDate", beData.EndDate_AD);
        //        cmd.Parameters.AddWithValue("@Remarks", beData.EndRemarks);
        //        cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
        //        cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
        //        cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters.AddWithValue("@TopicSNo", beData.SNo);
        //        cmd.CommandText = "usp_UpdateLessonTopicEnd";
        //        cmd.ExecuteNonQuery();

        //        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);
        //        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);
        //    }
        //    catch (System.Data.SqlClient.SqlException ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    finally
        //    {
        //        dal.CloseConnection();
        //    }
        //    return resVal;
        //}

        public ResponeValues EndTopic(int UserId, BE.Academic.Transaction.LessonTopic beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@StartBy", UserId);
                cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
                cmd.Parameters.AddWithValue("@EndDate", beData.EndDate_AD);
                cmd.Parameters.AddWithValue("@Remarks", beData.EndRemarks);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@TopicSNo", beData.SNo);
                //cmd.CommandText = "usp_UpdateLessonTopicEnd";
                //Added by suresh for new table
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                cmd.Parameters.AddWithValue("@LessonSNo", beData.LessonSNo);
                cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                cmd.CommandText = "usp_EndTopicPlan";
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);
                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);
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


        //public ResponeValues StartTopicContent(int UserId, BE.Academic.Transaction.LessonTopicTeacherContent beData)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    dal.OpenConnection();
        //    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@StartBy", beData.StartBy);
        //        cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
        //        cmd.Parameters.AddWithValue("@StartDate", beData.StartDate_AD);
        //        cmd.Parameters.AddWithValue("@Remarks", beData.StartRemarks);
        //        cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
        //        cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
        //        cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters.AddWithValue("@TopicSNo", beData.TopicSNo);
        //        cmd.Parameters.AddWithValue("@SNo", beData.SNo);
        //        cmd.CommandText = "usp_UpdateLessonTopicContentStart";
        //        cmd.ExecuteNonQuery();

        //        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);
        //        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);
        //    }
        //    catch (System.Data.SqlClient.SqlException ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    finally
        //    {
        //        dal.CloseConnection();
        //    }
        //    return resVal;
        //}

        public ResponeValues StartTopicContent(int UserId, BE.Academic.Transaction.LessonTopicTeacherContent beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@EmployeeId", UserId);
                cmd.Parameters.AddWithValue("@StartBy", UserId);
                //cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
                cmd.Parameters.AddWithValue("@StartDate", beData.StartDate_AD);
                cmd.Parameters.AddWithValue("@Remarks", beData.StartRemarks);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@LessonSNo", beData.LessonSNo);
                cmd.Parameters.AddWithValue("@TopicSNo", beData.TopicSNo);
                cmd.Parameters.AddWithValue("@ContentSNo", beData.ContentSNo);

                //Added
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                //cmd.Parameters.AddWithValue("@TranId", beData.TranId);
                cmd.CommandText = "usp_AddLessonContentPlanUpdate";
                //cmd.CommandText = "usp_UpdateLessonTopicContentStart";
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);
                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);

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

        //public ResponeValues EndTopicContent(int UserId, BE.Academic.Transaction.LessonTopicTeacherContent beData)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    dal.OpenConnection();
        //    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@StartBy", beData.StartBy);
        //        cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
        //        cmd.Parameters.AddWithValue("@EndDate", beData.EndDate_AD);
        //        cmd.Parameters.AddWithValue("@Remarks", beData.EndRemarks);
        //        cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
        //        cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
        //        cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters.AddWithValue("@TopicSNo", beData.TopicSNo);
        //        cmd.Parameters.AddWithValue("@SNo", beData.SNo);
        //        cmd.CommandText = "usp_UpdateLessonTopicContentEnd";
        //        cmd.ExecuteNonQuery();

        //        resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);
        //        resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);
        //    }
        //    catch (System.Data.SqlClient.SqlException ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    finally
        //    {
        //        dal.CloseConnection();
        //    }
        //    return resVal;
        //}
        public ResponeValues EndTopicContent(int UserId, BE.Academic.Transaction.LessonTopicTeacherContent beData)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@StartBy", beData.StartBy);
                //cmd.Parameters.AddWithValue("@LessonId", beData.LessonId);
                cmd.Parameters.AddWithValue("@EndDate", beData.EndDate_AD);
                cmd.Parameters.AddWithValue("@Remarks", beData.EndRemarks);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@LessonSNo", beData.LessonSNo);
                cmd.Parameters.AddWithValue("@TopicSNo", beData.TopicSNo);
                cmd.Parameters.AddWithValue("@ContentSNo", beData.ContentSNo);
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
                cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
                cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
                cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
                //cmd.Parameters.AddWithValue("@SNo", beData.SNo);
                cmd.CommandText = "usp_EndTopicContent";
                //cmd.CommandText = "usp_UpdateLessonTopicContentEnd";
                cmd.ExecuteNonQuery();

                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);
                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);
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


        public AcademicLib.RE.Academic.TodayLessonPlanCollections getTodayLessonPlan(int UserId,int AcademicYearId,DateTime? ForDate,int? ClassId,int? SectionId,int? SubjectId,int? EmployeeId,int ReportType,int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.RE.Academic.TodayLessonPlanCollections dataColl = new RE.Academic.TodayLessonPlanCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ForDate", ForDate);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.Parameters.AddWithValue("@ReportType", ReportType);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetTodayLessonPlan";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Academic.TodayLessonPlan beData = new RE.Academic.TodayLessonPlan();
                    if (!(reader[0] is DBNull)) beData.LessonId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LessonSNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.TopicSNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.ContentSNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.SubjectName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.CoverFilePath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.LessonName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.TopicName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ForDate = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.ForMiti = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.FileName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.FilePath = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Contents = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.StartDate = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.EndDate = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.StartRemarks = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.EndRemarks = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.StartBy = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.StartMiti = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.EndMiti = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.EmpName = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.EmpCode = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.Status = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.StatusValue = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.TotalDays = reader.GetInt32(24);
                    if (!(reader[25] is DBNull)) beData.ClassName = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.Batch = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.Semester = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.ClassYear = reader.GetString(28);
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


        public BE.Academic.Transaction.LessonTopicQuizCollections getLessonTopicQuizList(int UserId, /*int LessonId,*/int LessonSNo, int TopicSNo, int ClassId, int SubjectId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            BE.Academic.Transaction.LessonTopicQuizCollections dataColl = new BE.Academic.Transaction.LessonTopicQuizCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            //cmd.Parameters.AddWithValue("@LessonId", LessonId);
            cmd.Parameters.AddWithValue("@LessonSNo", LessonSNo);
            cmd.Parameters.AddWithValue("@TopicSNo", TopicSNo);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetLessonTopicQuizList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Transaction.LessonTopicQuiz beData = new BE.Academic.Transaction.LessonTopicQuiz();
                    beData.QuizId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Topic = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.NoOfQuestion = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.FullMark = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.PassMark = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Duration = reader.GetInt32(6);
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

        public ResponeValues DelLessonTopicQuizById(int UserId, int EntityId, int QuizId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@QuizId", QuizId);
            cmd.CommandText = "usp_DelLessonTopicQuizById";
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
