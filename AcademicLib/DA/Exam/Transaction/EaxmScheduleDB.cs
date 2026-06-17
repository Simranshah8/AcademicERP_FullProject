using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ExamScheduleDB
    {
        DataAccessLayer1 dal = null;
        public ExamScheduleDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Exam.Transaction.ExamSchedule beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);            
            cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
            cmd.Parameters.AddWithValue("@StartDate", beData.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", beData.EndDate);
            cmd.Parameters.AddWithValue("@StartTime", beData.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", beData.EndTime);
            cmd.Parameters.AddWithValue("@Notes", beData.Notes);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ExamScheduleId", beData.ExamScheduleId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateExamSchedule";
            }
            else
            {
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddExamSchedule";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            string sectionIdCOll = "";
            foreach(var s in beData.SectionIdColl)
            {
                if(!string.IsNullOrEmpty(sectionIdCOll))
                {
                    sectionIdCOll = sectionIdCOll + ",";
                }

                sectionIdCOll = sectionIdCOll + s.ToString();
            }

            cmd.Parameters.AddWithValue("@SectionIdColl", sectionIdCOll);

            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
              
                if(resVal.IsSuccess && resVal.RId > 0)
                {
                    SaveExamScheduleDetails(beData.CUserId, resVal.RId, beData.ExamScheduleDetailsColl);
                    SaveExamScheduleSection(beData.CUserId, resVal.RId, beData.SectionIdColl);
                }


                if (!string.IsNullOrEmpty(beData.ToClassIdColl))
                {
                    if (string.IsNullOrEmpty(beData.ToSectionIdColl))
                        beData.ToSectionIdColl = "";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                    cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                    cmd.Parameters.AddWithValue("@FromClassId", beData.ClassId);
                    cmd.Parameters.AddWithValue("@ToClassIdColl", beData.ToClassIdColl);
                    cmd.Parameters.AddWithValue("@FromSectionIdColl", beData.FromSectionIdColl);
                    cmd.Parameters.AddWithValue("@ToSectionIdColl", beData.ToSectionIdColl);
                    cmd.CommandText = "usp_CopyExamScheduleClassWise";
                    cmd.ExecuteNonQuery();
                }

                cmd.Parameters.Clear();
                cmd.CommandText = "usp_DelDuplicateExamSchedule";
                cmd.ExecuteNonQuery();

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
        private void SaveExamScheduleDetails(int UserId, int ExamScheduleId, List<BE.Exam.Transaction.ExamScheduleDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || ExamScheduleId == 0)
                return;

            foreach (BE.Exam.Transaction.ExamScheduleDetails beData in beDataColl)
            {

                if (beData.ExamShiftId.HasValue && beData.ExamShiftId.Value == 0)
                    beData.ExamShiftId = null;

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamScheduleId", ExamScheduleId);
                cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                cmd.Parameters.AddWithValue("@PaperTypeId", beData.PaperTypeId);
                cmd.Parameters.AddWithValue("@ExamDate", beData.ExamDate);
                cmd.Parameters.AddWithValue("@StartTime", beData.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", beData.EndTime);
                cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                cmd.Parameters.AddWithValue("@ExamShiftId", beData.ExamShiftId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddExamScheduleDetails";
                cmd.ExecuteNonQuery();
            }

        }
        private void SaveExamScheduleSection(int UserId,int ExamScheduleId, int[] SectionIdColl)
        {
            if (SectionIdColl == null || SectionIdColl.Length == 0 || ExamScheduleId == 0)
                return;

            foreach (int sid in SectionIdColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamScheduleId", ExamScheduleId);
                cmd.Parameters.AddWithValue("@SectionId", sid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddExamScheduleSection";
                cmd.ExecuteNonQuery();
            }
        }

        public AcademicLib.BE.Exam.Transaction.ExamSchedule getExamScheduleById(int UserId, int EntityId,int ClassId,string SectionIdColl,int ExamTypeId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            AcademicLib.BE.Exam.Transaction.ExamSchedule beData = new AcademicLib.BE.Exam.Transaction.ExamSchedule();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);

            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);

            cmd.CommandText = "usp_GetExamScheduleByClassId";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    beData = new AcademicLib.BE.Exam.Transaction.ExamSchedule();                    
                    if (!(reader[0] is DBNull)) beData.StartDate = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) beData.StartTime = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.EndDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.EndTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.Notes = reader.GetString(4);
                }
                reader.NextResult();
                beData.ExamScheduleDetailsColl = new List<BE.Exam.Transaction.ExamScheduleDetails>();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamScheduleDetails det = new BE.Exam.Transaction.ExamScheduleDetails();
                    if (!(reader[0] is DBNull)) det.SubjectId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.PaperTypeId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.ExamDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) det.StartTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) det.EndTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) det.Remarks = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det.ExamShiftId = reader.GetInt32(6);
                    beData.ExamScheduleDetailsColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int ClassId, string SectionIdColl, int ExamTypeId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.CommandText = "usp_DelExamScheduleById";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[7].Value);

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
        public AcademicLib.RE.Exam.ExamScheduleCollections GetExamSchedule(int UserId,int AcademicYearId, int? ClassId, string SectionIdColl, int? ExamTypeId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            AcademicLib.RE.Exam.ExamScheduleCollections dataColl = new RE.Exam.ExamScheduleCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionIdColl", SectionIdColl);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetExamSchedule";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.ExamSchedule beData = new RE.Exam.ExamSchedule();
                    if (!(reader[0] is DBNull)) beData.ExamTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ExamName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.StartDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.StartDate_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.EndDate_AD = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.EndDate_BS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.StartTime= reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.EndTime = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Notes = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.SubjectName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Code = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.CodeTH    = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.CodePR = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ExamDate_AD = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.ExamDate_BS = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Remarks = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.PaperType = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.SectionName = reader.GetString(18);

                    if (!(reader[19] is DBNull)) beData.RoomName = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.BenchNo = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.SeatNo = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.ColumnName = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.ExamShift = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.ExamStatus = reader.GetString(24);
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

        public AcademicLib.RE.Exam.ExamScheduleStatusCollections GetExamScheduleStatus(int UserId,  int ExamTypeId)
        {
            AcademicLib.RE.Exam.ExamScheduleStatusCollections dataColl = new RE.Exam.ExamScheduleStatusCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.CommandText = "usp_GetExamScheduleStatusList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.ExamScheduleStatus beData = new RE.Exam.ExamScheduleStatus();
                    if (!(reader[0] is DBNull)) beData.ClassName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.SectionName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.IsPending = reader.GetBoolean(2);
                    if (!(reader[3] is DBNull)) beData.CreateDateTime_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.CreateDateTime_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.UserName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.StartDate_AD = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) beData.EndDate_AD = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.StartDate_BS = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.EndDate_BS = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Batch = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.Semester = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ClassYear = reader.GetString(12);
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

        public AcademicLib.RE.Exam.AllClassExamScheduleCollections GetAllClassExamSchedule(int UserId, int ExamTypeId,bool SectionWise,bool InDetails=false,int? BatchId = null, int? SemesterId = null, int? ClassYearId=null)
        {
            AcademicLib.RE.Exam.AllClassExamScheduleCollections dataColl = new RE.Exam.AllClassExamScheduleCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@SectionWise", SectionWise);
            cmd.Parameters.AddWithValue("@InDetails", InDetails);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);

            cmd.CommandText = "usp_GetPrintExamSchedule";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Exam.AllClassExamSchedule beData = new RE.Exam.AllClassExamSchedule();
                    if (!(reader[0] is DBNull)) beData.ClassOrderNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ExamDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.ExamDate_BS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.NY = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.NM = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ND = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.SubjectName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ExamTime = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.ExamShiftWithTime = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ExamShift = reader.GetString(11);

                    try
                    {
                        if (!(reader[12] is DBNull)) beData.Batch = reader.GetString(12);
                        if (!(reader[13] is DBNull)) beData.Semester = reader.GetString(13);
                        if (!(reader[14] is DBNull)) beData.ClassYear = reader.GetString(14);
                    }
                    catch { }
                    

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
    }
}
