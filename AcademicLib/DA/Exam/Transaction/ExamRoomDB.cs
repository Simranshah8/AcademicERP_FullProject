using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ExamRoomDB
    {
        DataAccessLayer1 dal = null;
        public ExamRoomDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Exam.Transaction.ExamRoom beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@TotalBanch", beData.TotalBanch);
            cmd.Parameters.AddWithValue("@NoOfBanchRow", beData.NoOfBanchRow);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@RoomId", beData.RoomId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateExamRoom";
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddExamRoom";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@RoomNo", beData.RoomNo);
            cmd.Parameters.AddWithValue("@ExamShiftId", beData.ExamShiftId);
            cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);

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

                if(resVal.RId>0 && resVal.IsSuccess)
                {
                    SaveDetails(beData.CUserId, resVal.RId, beData.DetailColl);
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
        private void SaveDetails(int UserId, int RoomId, List<BE.Exam.Transaction.ExamRoomDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || RoomId == 0)
                return;
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            foreach (BE.Exam.Transaction.ExamRoomDetails beData in beDataColl)
            {

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ExamRoomId",RoomId);
                cmd.Parameters.AddWithValue("@Banch_Row_SNo", beData.Banch_Row_SNo);
                cmd.Parameters.AddWithValue("@Banch_Row_Name", beData.Banch_Row_Name);
                cmd.Parameters.AddWithValue("@NoOfBanch", beData.NoOfBanch);
                cmd.Parameters.AddWithValue("@NoOfSeatsInRow", beData.NoOfSeatsInRow);
                cmd.Parameters.AddWithValue("@Col_1", beData.Col_1);
                cmd.Parameters.AddWithValue("@Col_2", beData.Col_2);
                cmd.Parameters.AddWithValue("@Col_3", beData.Col_3);
                cmd.Parameters.AddWithValue("@Col_4", beData.Col_4);
                cmd.Parameters.AddWithValue("@Col_5", beData.Col_5);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddExamRoomDetails";
                cmd.ExecuteNonQuery();                 
            }
        }
        public AcademicLib.BE.Exam.Transaction.ExamRoomCollections getAllExamRoom(int UserId, int EntityId)
        {
            AcademicLib.BE.Exam.Transaction.ExamRoomCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamRoomCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllExamRoom";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamRoom beData = new AcademicLib.BE.Exam.Transaction.ExamRoom();
                    beData.RoomId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.TotalBanch = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.NoOfBanchRow = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.RoomNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ExamTypeName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ExamShiftName = reader.GetString(6);
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
        public AcademicLib.BE.Exam.Transaction.ExamRoom getExamRoomById(int UserId, int EntityId, int RoomId)
        {
            AcademicLib.BE.Exam.Transaction.ExamRoom beData = new AcademicLib.BE.Exam.Transaction.ExamRoom();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetExamRoomById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Exam.Transaction.ExamRoom();
                    beData.RoomId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.TotalBanch = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.NoOfBanchRow = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.RoomNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ExamShiftId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.ExamTypeId = reader.GetInt32(6);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamRoomDetails det = new BE.Exam.Transaction.ExamRoomDetails();                    
                    if (!(reader[0] is DBNull)) det.Banch_Row_SNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.Banch_Row_Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.NoOfBanch = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) det.NoOfSeatsInRow = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det.Col_1 = reader.GetString(4);
                    if (!(reader[5] is DBNull)) det.Col_2 = reader.GetString(5);
                    if (!(reader[6] is DBNull)) det.Col_3 = reader.GetString(6);
                    if (!(reader[7] is DBNull)) det.Col_4 = reader.GetString(7);
                    if (!(reader[8] is DBNull)) det.Col_5 = reader.GetString(8);
                    beData.DetailColl.Add(det);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int RoomId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
            cmd.CommandText = "usp_DelExamRoomById";
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

        public AcademicLib.BE.Exam.Transaction.ExamRoomCollections getExamShiftWiseRoom(int UserId, int EntityId, int ExamTypeId, int ExamShiftId)
        {
            AcademicLib.BE.Exam.Transaction.ExamRoomCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamRoomCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
            cmd.Parameters.AddWithValue("@ExamShiftId", ExamShiftId);
            cmd.CommandText = "usp_GetExamShiftWiseRoom";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Exam.Transaction.ExamRoom beData = new AcademicLib.BE.Exam.Transaction.ExamRoom();
                    beData.RoomId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
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
