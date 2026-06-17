using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Creation
{
    internal class ExamTypeGroupDB
    {
        DataAccessLayer1 dal = null;
        public ExamTypeGroupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int AcademicYearId, BE.Exam.Creation.ExamTypeGroup beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@DisplayName", beData.DisplayName);
            cmd.Parameters.AddWithValue("@SNO", beData.OrderNo);
            cmd.Parameters.AddWithValue("@ResultDate", beData.ResultDate);
            cmd.Parameters.AddWithValue("@ResultTime", beData.ResultTime);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeGroupId", beData.ExamTypeGroupId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateExamTypeGroup";
            }
            else
            {
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddExamTypeGroup";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@CurrentExamTypeId", beData.CurrentExamTypeId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
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
                    SaveExamTypeGroupDetails(beData.CUserId, resVal.RId, beData.ExamTypeGroupDetailsColl);
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
        private void SaveExamTypeGroupDetails(int UserId, int ExamTypeGroupId,List<BE.Exam.Creation.ExamTypeGroupDetails>  beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || ExamTypeGroupId == 0)
                return;

            foreach (BE.Exam.Creation.ExamTypeGroupDetails beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamTypeGroupId", ExamTypeGroupId);
                cmd.Parameters.AddWithValue("@SNO", beData.SNO);
                cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
                cmd.Parameters.AddWithValue("@Percent", beData.Percent);
                cmd.Parameters.AddWithValue("@DisplayName", beData.DisplayName);
                cmd.Parameters.AddWithValue("@IsCalculateAttendance", beData.IsCalculateAttenDance);
                cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddExamTypeGroupDetails";
                cmd.Parameters.AddWithValue("@ShowGradingSubject", beData.ShowGradingSubject);
                cmd.ExecuteNonQuery();

                if(!(cmd.Parameters[7].Value is DBNull))
                {
                    int tranId = Convert.ToInt32(cmd.Parameters[7].Value);
                    if (beData.ExamTypeWiseSubjectColl != null && beData.ExamTypeWiseSubjectColl.Count > 0)
                        SaveUpdateExamTypeWiseSubject(UserId,tranId, beData.ExamTypeWiseSubjectColl);
                }


            }
        }

        public void SaveUpdateExamTypeWiseSubject(int UserId,int TranId, List<BE.Exam.Creation.ExamTypeWiseSubject> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.Exam.Creation.ExamTypeWiseSubject beData in beDataColl)
            {
                if(beData.SubjectId.HasValue && beData.SubjectId.Value > 0)
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
                    cmd.Parameters.AddWithValue("@PercentPR", beData.PercentPR);
                    cmd.Parameters.AddWithValue("@PercentTH", beData.PercentTH);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddExamTypeWiseSubject";
                    cmd.ExecuteNonQuery();
                }                
            }
        }
        public BE.Exam.Creation.ExamTypeGroupCollections getAllExamTypeGroup(int UserId, int AcademicYearId, int EntityId)
        {
            BE.Exam.Creation.ExamTypeGroupCollections dataColl = new BE.Exam.Creation.ExamTypeGroupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAllExamTypeGroup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Creation.ExamTypeGroup beData = new BE.Exam.Creation.ExamTypeGroup();
                    beData.ExamTypeGroupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DisplayName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ResultDate = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ResultTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.ResultDateBS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.CurrentExamTypeId = reader.GetInt32(7);

                    if (!(reader[8] is DBNull)) beData.NeedPublished = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.LastUpdateDate = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.LastPublishDate = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.LastUpdateMiti = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.LastPublishMiti = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[13]);
                    if (!(reader[14] is DBNull)) beData.DisplayYearBS = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.DisplayYearAD = reader.GetInt32(15);
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
        public BE.Exam.Creation.ExamTypeGroup getExamTypeGroupById(int UserId, int EntityId, int ExamTypeGroupId)
        {
            BE.Exam.Creation.ExamTypeGroup beData = new BE.Exam.Creation.ExamTypeGroup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamTypeGroupId", ExamTypeGroupId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExamTypeGroupById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Creation.ExamTypeGroup();
                    beData.ExamTypeGroupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DisplayName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ResultDate = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ResultTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.CurrentExamTypeId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[7]);
                    if (!(reader[8] is DBNull)) beData.DisplayYearBS = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.DisplayYearAD = reader.GetInt32(9);
                }
                reader.NextResult();
                beData.ExamTypeGroupDetailsColl = new List<BE.Exam.Creation.ExamTypeGroupDetails>();
                while (reader.Read())
                {
                    BE.Exam.Creation.ExamTypeGroupDetails det = new BE.Exam.Creation.ExamTypeGroupDetails();
                    det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.SNO = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.ExamTypeId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.Percent = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) det.DisplayName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) det.IsCalculateAttenDance = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) det.ShowGradingSubject = reader.GetBoolean(6);

                    beData.ExamTypeGroupDetailsColl.Add(det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    BE.Exam.Creation.ExamTypeWiseSubject det = new BE.Exam.Creation.ExamTypeWiseSubject();
                    det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.SubjectId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.PercentTH = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) det.PercentPR = Convert.ToDouble(reader[3]);

                    beData.ExamTypeGroupDetailsColl.Find(p1 => p1.TranId == det.TranId).ExamTypeWiseSubjectColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int ExamTypeGroupId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeGroupId", ExamTypeGroupId);
            cmd.CommandText = "usp_DelExamTypeGroupById";
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
