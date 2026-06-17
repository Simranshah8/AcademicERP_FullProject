using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
    internal class ExamBulkRoomDB
    {
        DataAccessLayer1 dal = null;
        public ExamBulkRoomDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(int UserId, AcademicLib.BE.Exam.Transaction.ExamBulkRoomCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                var fst = dataColl.First();
                cmd.CommandText = "usp_DelBulkRoomDetails";
                cmd.ExecuteNonQuery();

                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@RoomName", beData.RoomName);
                    cmd.Parameters.AddWithValue("@TotalCapacity", beData.TotalCapacity);
                    cmd.Parameters.AddWithValue("@TotalBench", beData.TotalBench);
                    cmd.Parameters.AddWithValue("@NoOfBanchRow", beData.NoOfBanchRow);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.Add("@ExamRoomId", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

                    cmd.CommandText = "usp_AddExamBulkRoom";
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["@ExamRoomId"].Value != DBNull.Value)
                    {
                        resVal.RId = Convert.ToInt32(cmd.Parameters["@ExamRoomId"].Value);
                    }
                    else
                    {
                        resVal.RId = 0;
                    }

                    if (resVal.RId > 0)
                    {
                        SaveBulkRoomDetailDetails(beData.CUserId, resVal.RId, beData.DetailColl);
                    }
                    else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Failed to retrieve ExamRoomId.";
                        break;
                    }
                }

                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Exam Bulk Room Saved";
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

        // Method to save child table details
        private void SaveBulkRoomDetailDetails(int UserId, int ExamRoomId, BE.Exam.Transaction.ExamBulkRoomDetailCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || ExamRoomId == 0)
                return;

            foreach (BE.Exam.Transaction.ExamBulkRoomDetail beData in beDataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Banch_Row_Name", beData.Banch_Row_Name);
                cmd.Parameters.AddWithValue("@NoOfBanch", beData.NoOfBanch);
                cmd.Parameters.AddWithValue("@NoOfSeatsInRow", beData.NoOfSeatsInRow);
                cmd.Parameters.AddWithValue("@ExamRoomId", ExamRoomId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddBulkRoomDetail";
                cmd.ExecuteNonQuery();
            }
        }



        public BE.Exam.Transaction.ExamBulkRoomCollections getAllExamBulkRoom(int UserId, int EntityId)
        {
            BE.Exam.Transaction.ExamBulkRoomCollections dataColl = new BE.Exam.Transaction.ExamBulkRoomCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllExamBulkRoom";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Exam.Transaction.ExamBulkRoom beData = new BE.Exam.Transaction.ExamBulkRoom();
                    if (!(reader[0] is DBNull)) beData.RoomName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.TotalCapacity = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.TotalBench = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.NoOfBanchRow = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ExamRoomId = reader.GetInt32(4);
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

