using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Creation
{
    internal class ParentParentExamTypeGroupDB
    {
        DataAccessLayer1 dal = null;
        public ParentParentExamTypeGroupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Exam.Creation.ParentExamTypeGroup beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@DisplayName", beData.DisplayName);
            cmd.Parameters.AddWithValue("@SNo", beData.OrderNo);
            cmd.Parameters.AddWithValue("@ResultDate", beData.ResultDate);
            cmd.Parameters.AddWithValue("@ResultTime", beData.ResultTime); 
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateParentExamTypeGroup";
            }
            else
            {
                cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddParentExamTypeGroup";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
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
                    SaveParentExamTypeGroupDetails(beData.CUserId, resVal.RId, beData.ParentExamTypeGroupDetailsColl);
                   
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
        private void SaveParentExamTypeGroupDetails(int UserId, int ParentExamTypeGroupId, List<BE.Exam.Creation.ParentExamTypeGroupDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || ParentExamTypeGroupId == 0)
                return;

            foreach (BE.Exam.Creation.ParentExamTypeGroupDetails beData in beDataColl)
            {
                if(beData.ExamTypeGroupId.HasValue && beData.ExamTypeGroupId.Value > 0)
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@ParentExamTypeGroupId", ParentExamTypeGroupId);
                    cmd.Parameters.AddWithValue("@Sno", beData.SNO);
                    cmd.Parameters.AddWithValue("@ExamTypeGroupId", beData.ExamTypeGroupId);
                    cmd.Parameters.AddWithValue("@Percent", beData.Percent);
                    cmd.Parameters.AddWithValue("@DisplayName", beData.DisplayName);
                    cmd.Parameters.AddWithValue("@IsCalculateAttendance", beData.IsCalculateAttendance);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_AddParentExamTypeGroupDetails";
                    cmd.ExecuteNonQuery();
                }
               
            }

        }
       
        public BE.Exam.Creation.ParentExamTypeGroupCollections getAllParentExamTypeGroup(int UserId, int EntityId)
        {
            BE.Exam.Creation.ParentExamTypeGroupCollections dataColl = new BE.Exam.Creation.ParentExamTypeGroupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllParentExamTypeGroup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Creation.ParentExamTypeGroup beData = new BE.Exam.Creation.ParentExamTypeGroup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DisplayName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ResultDate = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ResultTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.ResultDateBS = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.DisplayYearBS = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.DisplayYearAD = reader.GetInt32(8);
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
        public BE.Exam.Creation.ParentExamTypeGroup getParentExamTypeGroupById(int UserId, int EntityId, int ExamTypeId)
        {
            BE.Exam.Creation.ParentExamTypeGroup beData = new BE.Exam.Creation.ParentExamTypeGroup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", ExamTypeId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetParentExamTypeGroupById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Exam.Creation.ParentExamTypeGroup();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DisplayName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ResultDate = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.ResultTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.DisplayYearBS = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.DisplayYearAD = reader.GetInt32(7);
                }
                reader.NextResult();
                beData.ParentExamTypeGroupDetailsColl = new List<BE.Exam.Creation.ParentExamTypeGroupDetails>();
                while (reader.Read())
                {
                    BE.Exam.Creation.ParentExamTypeGroupDetails det = new BE.Exam.Creation.ParentExamTypeGroupDetails();
                    det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.SNO = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.ExamTypeGroupId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.Percent = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) det.DisplayName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) det.IsCalculateAttendance = reader.GetBoolean(5);
                    beData.ParentExamTypeGroupDetailsColl.Add(det);
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
            cmd.CommandText = "usp_DelParentExamTypeGroupById";
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
