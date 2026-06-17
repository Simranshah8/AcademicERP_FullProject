using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Creation
{
    internal class ExamTypeDB
    {
        DataAccessLayer1 dal = null;
        public ExamTypeDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int AcademicYearId, BE.Exam.Creation.ExamType beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@DisplayName", beData.DisplayName);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
            cmd.Parameters.AddWithValue("@ResultDate", beData.ResultDate);
            cmd.Parameters.AddWithValue("@ResultTime", beData.ResultTime);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateExamType";
            }
            else
            {
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddExamType";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@IsOnlineExam", beData.IsOnlineExam);
            cmd.Parameters.AddWithValue("@ExamDate", beData.ExamDate);
            cmd.Parameters.AddWithValue("@StartTime", beData.StartTime);
            cmd.Parameters.AddWithValue("@Duration", beData.Duration);
            cmd.Parameters.AddWithValue("@SectionWiseExam", beData.SectionWiseExam);

            cmd.Parameters.AddWithValue("@MarkSubmitDeadline_Teacher", beData.MarkSubmitDeadline_Teacher);
            cmd.Parameters.AddWithValue("@MarkSubmitDeadline_Admin", beData.MarkSubmitDeadline_Admin);
            cmd.Parameters.AddWithValue("@ForClassWiseResultPublished", beData.ForClassWiseResultPublished);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@ForPreStudent", beData.ForPreStudent);
            cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);
            cmd.Parameters.AddWithValue("@DisplayYearBS", beData.DisplayYearBS);
            cmd.Parameters.AddWithValue("@DisplayYearAD", beData.DisplayYearAD);
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
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveClassWiseResult(beData.CUserId, resVal.RId, beData.ClassWiseColl);
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
        private void SaveClassWiseResult(int UserId, int ExamTypeId, List<BE.Exam.Creation.ClassWiseResultPublichedDate> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || ExamTypeId == 0)
                return;

            foreach (BE.Exam.Creation.ClassWiseResultPublichedDate beData in beDataColl)
            {
                foreach (var v in beData.ClassIdColl)
                {
                    if (v != 0)
                    {
                        System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
                        cmd.Parameters.AddWithValue("@ClassId", v);
                        cmd.Parameters.AddWithValue("@ResultDateTime", beData.ResultDateTime);
                        cmd.Parameters.AddWithValue("@MarkSubmitDeadline_Teacher", beData.MarkSubmitDeadline_Teacher);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "usp_AddClassWiseResultPublichedDate";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public BE.Exam.Creation.ExamTypeCollections getAllExamType(int UserId, int AcademicYearId, int EntityId, int? ForEntity = null)
        {
            BE.Exam.Creation.ExamTypeCollections dataColl = new BE.Exam.Creation.ExamTypeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ForEntity", ForEntity);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAllExamType";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Creation.ExamType beData = new BE.Exam.Creation.ExamType();
                    beData.ExamTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DisplayName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ResultDate = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ResultTime = reader.GetDateTime(5);

                    if (!(reader[6] is DBNull)) beData.IsOnlineExam = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.ExamDate = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.StartTime = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.Duration = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.SectionWiseExam = reader.GetBoolean(10);

                    if (!(reader[11] is DBNull)) beData.MarkSubmitDeadline_Teacher = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.MarkSubmitDeadline_Admin = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.ForClassWiseResultPublished = reader.GetBoolean(13);
                    if (!(reader[14] is DBNull)) beData.ForPreStudent = reader.GetBoolean(14);

                    if (!(reader[15] is DBNull)) beData.NeedPublished = reader.GetBoolean(15);
                    if (!(reader[16] is DBNull)) beData.LastUpdateDate = reader.GetDateTime(16);
                    if (!(reader[17] is DBNull)) beData.LastPublishDate = reader.GetDateTime(17);
                    if (!(reader[18] is DBNull)) beData.LastUpdateMiti = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.LastPublishMiti = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.BranchId = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.Branch = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.ResultMiti = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[23]);
                    if (!(reader[24] is DBNull)) beData.DisplayYearBS = reader.GetInt32(24);
                    if (!(reader[25] is DBNull)) beData.DisplayYearAD = reader.GetInt32(25);
                    beData.AdminTime = beData.MarkSubmitDeadline_Admin;
                    beData.TeacherTime = beData.MarkSubmitDeadline_Teacher;
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
        public BE.Exam.Creation.ExamType getExamTypeById(int UserId, int EntityId, int ExamTypeId)
        {
            BE.Exam.Creation.ExamType beData = new BE.Exam.Creation.ExamType();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.CommandText = "usp_GetExamTypeById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Creation.ExamType();
                    beData.ExamTypeId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DisplayName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ResultDate = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ResultTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.IsOnlineExam = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.ExamDate = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.StartTime = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) beData.Duration = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.SectionWiseExam = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.MarkSubmitDeadline_Teacher = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.MarkSubmitDeadline_Admin = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.ForClassWiseResultPublished = reader.GetBoolean(13);
                    if (!(reader[14] is DBNull)) beData.ForPreStudent = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[15]);
                    if (!(reader[16] is DBNull)) beData.DisplayYearBS = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.DisplayYearAD = reader.GetInt32(17);
                    beData.AdminTime = beData.MarkSubmitDeadline_Admin;
                    beData.TeacherTime = beData.MarkSubmitDeadline_Teacher;
                }
                reader.NextResult();
                beData.ClassWiseColl = new List<BE.Exam.Creation.ClassWiseResultPublichedDate>();
                var tmpClassColl = new List<BE.Exam.Creation.ClassWiseResultPublichedDate>();
                while (reader.Read())
                {
                    BE.Exam.Creation.ClassWiseResultPublichedDate det = new BE.Exam.Creation.ClassWiseResultPublichedDate();
                    if (!(reader[0] is DBNull)) det.SNo = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) det.ClassId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.ResultDateTime = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) det.MarkSubmitDeadline_Teacher = reader.GetDateTime(3);
                    tmpClassColl.Add(det);
                }
                reader.Close();

                if (tmpClassColl != null && tmpClassColl.Count > 0)
                {
                    var query = from dc in tmpClassColl
                                group dc by dc.ResultDateTime into g
                                select new
                                {
                                    ResultDateTime = g.Key,
                                    MarkSubmitDeadline_Teacher = g.Max(p1 => p1.MarkSubmitDeadline_Teacher),
                                    ChieldColl = g
                                };

                    foreach (var v in query)
                    {
                        BE.Exam.Creation.ClassWiseResultPublichedDate det = new BE.Exam.Creation.ClassWiseResultPublichedDate();
                        det.ResultDateTime = v.ResultDateTime;
                        det.MarkSubmitDeadline_Teacher = v.MarkSubmitDeadline_Teacher;
                        det.ClassIdColl = new List<int>();
                        foreach (var vv in v.ChieldColl)
                            det.ClassIdColl.Add(vv.ClassId);

                        beData.ClassWiseColl.Add(det);
                    }
                }

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
        public ResponeValues DeleteById(int UserId, int EntityId, int ExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.CommandText = "usp_DelExamTypeById";
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
        public ResponeValues GetExamTypeAutoOrderNo(int UserId, int EntityId, int? For)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.Add("@OrderNo", System.Data.SqlDbType.Int);
            cmd.CommandText = "usp_ExamTypeAutoOrderNo";
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@For", For);
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[2].Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
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
