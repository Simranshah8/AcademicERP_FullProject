using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{

	internal class LessonPlanningDB
	{
		DataAccessLayer1 dal = null;
		public LessonPlanningDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}



        public ResponeValues SaveUpdateLeavePlanningColl(int UserId, BE.Academic.Transaction.LessonPlanningCollections DataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                //cmd.Parameters.AddWithValue("@TranId", TranId);

                cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
                cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
                cmd.Parameters[0].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
                //cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                System.Data.DataTable tableAllocation = new System.Data.DataTable();
                tableAllocation.Columns.Add("TranId", typeof(int));
                tableAllocation.Columns.Add("ClassId", typeof(int));
                tableAllocation.Columns.Add("SectionIdColl", typeof(string));
                tableAllocation.Columns.Add("BatchId", typeof(int));
                tableAllocation.Columns.Add("SemesterId", typeof(int));
                tableAllocation.Columns.Add("ClassYearId", typeof(int));
                tableAllocation.Columns.Add("SubjectId", typeof(int));
                tableAllocation.Columns.Add("EmployeeId", typeof(int));
                tableAllocation.Columns.Add("LessonSNo", typeof(int));
                tableAllocation.Columns.Add("TopicSNo", typeof(int));
                tableAllocation.Columns.Add("PlanStartDate", typeof(DateTime));
                tableAllocation.Columns.Add("PlanEndDate", typeof(DateTime));

                foreach (var v in DataColl)
                {
                    var row = tableAllocation.NewRow();

                    row["TranId"] = v.TranId;
                    row["ClassId"] = v.ClassId;

                    // SectionIdColl nullable
                    if (!string.IsNullOrWhiteSpace(v.SectionIdColl))
                        row["SectionIdColl"] = v.SectionIdColl;
                    else
                        row["SectionIdColl"] = DBNull.Value;

                    // BatchId nullable
                    if (v.BatchId > 0)
                        row["BatchId"] = v.BatchId;
                    else
                        row["BatchId"] = DBNull.Value;

                    // SemesterId nullable
                    if (v.SemesterId > 0)
                        row["SemesterId"] = v.SemesterId;
                    else
                        row["SemesterId"] = DBNull.Value;

                    // ClassYearId nullable
                    if (v.ClassYearId > 0)
                        row["ClassYearId"] = v.ClassYearId;
                    else
                        row["ClassYearId"] = DBNull.Value;

                    row["SubjectId"] = v.SubjectId;

                    // EmployeeId nullable
                    if (v.EmployeeId > 0)
                        row["EmployeeId"] = v.EmployeeId;
                    else
                        row["EmployeeId"] = DBNull.Value;

                    row["LessonSNo"] = v.LessonSNo;
                    row["TopicSNo"] = v.TopicSNo;
                    // PlanStartDate nullable
                    if (v.PlanStartDate != null && v.PlanStartDate != DateTime.MinValue)
                        row["PlanStartDate"] = v.PlanStartDate;
                    else
                        row["PlanStartDate"] = DBNull.Value;

                    // PlanEndDate nullable
                    if (v.PlanEndDate != null && v.PlanEndDate != DateTime.MinValue)
                        row["PlanEndDate"] = v.PlanEndDate;
                    else
                        row["PlanEndDate"] = DBNull.Value;

                    tableAllocation.Rows.Add(row);
                }

                System.Data.SqlClient.SqlParameter sqlParam = cmd.Parameters.AddWithValue("@LessonPlanningColl", tableAllocation);
                sqlParam.SqlDbType = System.Data.SqlDbType.Structured;
                cmd.CommandText = "usp_AddLessonPlanning";
                cmd.ExecuteNonQuery();
                //if (!(cmd.Parameters[0].Value is DBNull))resVal.RId = Convert.ToInt32(cmd.Parameters[0].Value);
                if (!(cmd.Parameters[0].Value is DBNull)) resVal.ResponseMSG = Convert.ToString(cmd.Parameters[0].Value);
                if (!(cmd.Parameters[1].Value is DBNull)) resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[1].Value);
                //if (resVal.IsSuccess && DataColl.DetailsColl != null)
                //{
                //	SaveLessonPlanningContentDetails(resVal.RId, DataColl.DetailsColl);
                //}
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



        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.CommandText = "usp_DelLessonPlanningById";
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



        public AcademicLib.BE.Academic.Transaction.LessonPlan getLPClassSubjectSecWise(int UserId, int ClassId, int SubjectId, string SectionIdColl, int? BatchId, int? ClassYearId, int? SemesterId, int AcademicYearId)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlan beData = new AcademicLib.BE.Academic.Transaction.LessonPlan();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetAllLessonPlanningClassSubjectwise";
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@SectionIdColl", (object)SectionIdColl ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
			{
				using (var reader = cmd.ExecuteReader())
				{

					while (reader.Read())
					{
						BE.Academic.Transaction.LessonPlanDetails detail = new BE.Academic.Transaction.LessonPlanDetails();
						detail.SNo = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) detail.LessonName = reader.GetString(1);
						if (!(reader[2] is DBNull)) detail.PlanStartDate_AD = reader.GetDateTime(2);
						if (!(reader[3] is DBNull)) detail.PlanStartDate_BS = reader.GetString(3);
						if (!(reader[4] is DBNull)) detail.PlanEndDate_AD = reader.GetDateTime(4);

						if (!(reader[5] is DBNull)) detail.PlanEndDate_BS = reader.GetString(5);
						if (!(reader[6] is DBNull)) detail.TotalDays = reader.GetInt32(6);
						if (!(reader[7] is DBNull)) detail.StatusValue = reader.GetInt32(7);
						beData.DetailsColl.Add(detail);
					}


					if (reader.NextResult())
					{
						while (reader.Read())
						{
							BE.Academic.Transaction.LessonTopic topic = new BE.Academic.Transaction.LessonTopic();
							int lessonSNo = reader.GetInt32(0);

							topic.SNo = reader.GetInt32(1);
							if (!(reader[2] is DBNull)) topic.TopicName = reader.GetString(2);
							if (!(reader[3] is DBNull)) topic.TranId = reader.GetInt32(3);
							if (!(reader[4] is DBNull)) topic.PlanStartDate_AD = reader.GetDateTime(4);
							if (!(reader[5] is DBNull)) topic.PlanEndDate_AD = reader.GetDateTime(5);
							if (!(reader[6] is DBNull)) topic.PlanStartDate_BS = reader.GetString(6);
							if (!(reader[7] is DBNull)) topic.PlanEndDate_BS = reader.GetString(7);
							if (!(reader[8] is DBNull)) topic.StatusValue = reader.GetInt32(8);
							if (!(reader[9] is DBNull)) topic.TotalDays = reader.GetInt32(9);
							var lessonDetail = beData.DetailsColl.Find(x => x.SNo == lessonSNo);
							if (lessonDetail != null)
							{
								lessonDetail.TopicColl.Add(topic);
							}
						}
					}
					if (reader.NextResult())
					{
						while (reader.Read())
						{
							BE.Academic.Transaction.LessonTopicTeacherContent content = new BE.Academic.Transaction.LessonTopicTeacherContent();

							int lessonSNo = reader.GetInt32(0);
							int topicSNo = reader.GetInt32(1);
							// No SNo in content, so skip content.SNo assignment

							if (!(reader[2] is DBNull)) content.ForDate = reader.GetDateTime(2);
							if (!(reader[3] is DBNull)) content.Contents = reader.GetString(3);
							if (!(reader[4] is DBNull)) content.ForDateBS = reader.GetString(4);
							if (!(reader[5] is DBNull)) content.StatusValue = reader.GetInt32(5);
							if (!(reader[6] is DBNull)) content.TotalDays = reader.GetInt32(6);

							int localLessonSNo = lessonSNo;
							int localTopicSNo = topicSNo;

							var lesson = beData.DetailsColl.Find(x => x.SNo == localLessonSNo);
							if (lesson != null)
							{
								var topic = lesson.TopicColl.Find(t => t.SNo == localTopicSNo);
								if (topic != null)
								{
									topic.ContentsColl.Add(content);
								}
							}
						}
					}
				}

				beData.IsSuccess = true;
				beData.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ex)
			{
				beData.IsSuccess = false;
				beData.ResponseMSG = ex.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return beData;
		}




		public ResponeValues SaveLessonPlanningTopicContent(int UserId, List<BE.Academic.Transaction.LessonTopicTeacherContent> dataColl)
		{
			ResponeValues resVal = new ResponeValues();

			dal.OpenConnection();
			dal.BeginTransaction();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			try
			{
				var fst = dataColl[0];
				cmd.Parameters.AddWithValue("@TranId", fst.TranId);				
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_DelLessonPlanningOldContent";
				cmd.ExecuteNonQuery();

				//int sno = 1;
				foreach (BE.Academic.Transaction.LessonTopicTeacherContent det in dataColl)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@UserId", UserId);					
					cmd.Parameters.AddWithValue("@TranId", det.TranId);
					cmd.Parameters.AddWithValue("@ForDate", det.ForDate);
					cmd.Parameters.AddWithValue("@Contents", det.Contents);
                    cmd.Parameters.AddWithValue("@ContentSNo", det.ContentSNo);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "usp_AddLessonPlanningContent";
					cmd.ExecuteNonQuery();
					//sno++;
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


		public AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections getLPTopicContent(int UserId, int TranId)
		{
			AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections dataColl = new AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@TranId", TranId);
			
			cmd.CommandText = "usp_GetLPTopicContents";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent beData = new AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContent();
					beData.TranId = reader.GetInt32(0);			
					if (!(reader[1] is DBNull)) beData.ForDate = reader.GetDateTime(1);
					if (!(reader[2] is DBNull)) beData.Contents = reader.GetString(2);

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

        public AcademicLib.BE.Academic.Transaction.LessonPlan getLessonPlanByForUpdate(int UserId, int ClassId, int SubjectId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId, int? AcademicYearId)
        {
            AcademicLib.BE.Academic.Transaction.LessonPlan beData = new AcademicLib.BE.Academic.Transaction.LessonPlan();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetLessonPlanningUpdate";
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
                        if (!(reader[4] is DBNull)) det.PlanStartDate_AD = reader.GetDateTime(4);
                        if (!(reader[5] is DBNull)) det.PlanEndDate_AD = reader.GetDateTime(5);
                        if (!(reader[6] is DBNull)) det.PlanStartDate_BS = reader.GetString(6);
                        if (!(reader[7] is DBNull)) det.PlanEndDate_BS = reader.GetString(7);

                        //if (!(reader[8] is DBNull)) det.StartDate_AD = reader.GetDateTime(8);
                        //if (!(reader[9] is DBNull)) det.EndDate_AD = reader.GetDateTime(9);
                        //if (!(reader[10] is DBNull)) det.StartRemarks = reader.GetString(10);
                        //if (!(reader[11] is DBNull)) det.EndRemarks = reader.GetString(11);
                        //if (!(reader[12] is DBNull)) det.StartBy = reader.GetInt32(12);
                        //if (!(reader[13] is DBNull)) det.StartDate_BS = reader.GetString(13);
                        //if (!(reader[14] is DBNull)) det.EndDate_BS = reader.GetString(14);
                        //if (!(reader[15] is DBNull)) det.EmpName = reader.GetString(15);
                        //if (!(reader[16] is DBNull)) det.EmpCode = reader.GetString(16);
                        if (!(reader[8] is DBNull)) det.Status = reader.GetString(8);
                        if (!(reader[9] is DBNull)) det.StatusValue = reader.GetInt32(9);
                        if (!(reader[10] is DBNull)) det.TotalDays = reader.GetInt32(10);
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
                        if (!(reader[3] is DBNull)) det.StartDate_AD = reader.GetDateTime(3);
                        if (!(reader[4] is DBNull)) det.EndDate_AD = reader.GetDateTime(4);
                        if (!(reader[5] is DBNull)) det.StartDate_BS = reader.GetString(5);
                        if (!(reader[6] is DBNull)) det.EndDate_BS = reader.GetString(6);

                        if (!(reader[7] is DBNull)) det.StartRemarks = reader.GetString(7);
                        if (!(reader[8] is DBNull)) det.EndRemarks = reader.GetString(8);
                        if (!(reader[9] is DBNull)) det.StartBy = reader.GetInt32(9);
                        //if (!(reader[12] is DBNull)) det.StartDate_BS = reader.GetString(12);
                        //if (!(reader[13] is DBNull)) det.EndDate_BS = reader.GetString(13);
                        if (!(reader[10] is DBNull)) det.EmpName = reader.GetString(10);
                        if (!(reader[11] is DBNull)) det.EmpCode = reader.GetString(11);
                        if (!(reader[12] is DBNull)) det.Status = reader.GetString(12);
                        if (!(reader[13] is DBNull)) det.StatusValue = reader.GetInt32(13);
                        if (!(reader[14] is DBNull)) det.TotalDays = reader.GetInt32(14);

                        if (det.StatusValue == 1)
                        {
                            if (det.PlanStartDate_AD.HasValue)
                            {
                                int daysDiff = (DateTime.Today - det.PlanStartDate_AD.Value).Days;

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
                                int daysDiff = (DateTime.Today - det.StartDate_AD.Value).Days;

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
                                int daysDiff = (DateTime.Today - det.EndDate_AD.Value).Days;

                                if (daysDiff > 0)
                                {
                                    det.StatusDays = " Before " + daysDiff.ToString() + " Days";
                                }
                            }
                        }

                        beData.DetailsColl.Find(p1 => p1.LessonId == det.LessonId).TopicColl.Add(det);
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        BE.Academic.Transaction.LessonTopicTeacherContent det1 = new BE.Academic.Transaction.LessonTopicTeacherContent();
                        det1.LessonId = reader.GetInt32(0);
                        //det1.TranId = reader.GetInt32(0);
                        det1.LessonSNo = reader.GetInt32(1);
                        det1.TopicSNo = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) det1.SNo = reader.GetInt32(3);
                        if (!(reader[4] is DBNull)) det1.ForDate = reader.GetDateTime(4);
                        //if (!(reader[5] is DBNull)) det1.FileName = reader.GetString(5);
                        //if (!(reader[6] is DBNull)) det1.FilePath = reader.GetString(6);
                        if (!(reader[5] is DBNull)) det1.Contents = reader.GetString(5);
                        if (!(reader[6] is DBNull)) det1.ForDateBS = reader.GetString(6);

                        if (!(reader[7] is DBNull)) det1.StartDate_AD = reader.GetDateTime(7);
                        if (!(reader[8] is DBNull)) det1.EndDate_AD = reader.GetDateTime(8);
                        if (!(reader[9] is DBNull)) det1.StartRemarks = reader.GetString(9);
                        if (!(reader[10] is DBNull)) det1.EndRemarks = reader.GetString(10);
                        if (!(reader[11] is DBNull)) det1.StartBy = reader.GetInt32(11);
                        if (!(reader[12] is DBNull)) det1.StartDate_BS = reader.GetString(12);
                        if (!(reader[13] is DBNull)) det1.EndDate_BS = reader.GetString(13);
                        if (!(reader[14] is DBNull)) det1.EmpName = reader.GetString(14);
                        if (!(reader[15] is DBNull)) det1.EmpCode = reader.GetString(15);
                        if (!(reader[16] is DBNull)) det1.Status = reader.GetString(16);
                        if (!(reader[17] is DBNull)) det1.StatusValue = reader.GetInt32(17);
                        if (!(reader[18] is DBNull)) det1.TotalDays = reader.GetInt32(18);
                        beData.DetailsColl.Find(p1 => p1.LessonId == det1.LessonId).TopicColl.Find(p1 => p1.SNo == det1.TopicSNo).ContentsColl.Add(det1);
                    }
                    //reader.NextResult();
                    //while (reader.Read())
                    //{
                    //	BE.Academic.Transaction.LessonTopicTeacherContent det1 = new BE.Academic.Transaction.LessonTopicTeacherContent();
                    //	det1.LessonId = reader.GetInt32(0);
                    //	det1.LessonSNo = reader.GetInt32(1);
                    //	if (!(reader[2] is DBNull)) det1.SNo = reader.GetInt32(2);
                    //	if (!(reader[3] is DBNull)) det1.ForDate = reader.GetDateTime(3);
                    //	if (!(reader[4] is DBNull)) det1.FileName = reader.GetString(4);
                    //	if (!(reader[5] is DBNull)) det1.FilePath = reader.GetString(5);
                    //	if (!(reader[6] is DBNull)) det1.Contents = reader.GetString(6);
                    //	if (!(reader[7] is DBNull)) det1.ForDateBS = reader.GetString(7);

                    //	beData.DetailsColl.Find(p1 => p1.LessonId == det1.LessonId).ContentsColl.Add(det1);
                    //}
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

